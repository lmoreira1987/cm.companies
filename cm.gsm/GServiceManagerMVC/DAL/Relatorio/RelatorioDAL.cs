using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

using GServiceManagerMVC.ViewModels.Relatorio;
using GServiceManagerMVC.Models;

namespace GServiceManagerMVC.DAL.Relatorio
{
    public class RelatorioDAL
    {
        #region Public


        public List<ColaboradorViewModel> SelectRelatorioSemanaColaborador(long id)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                int controler = 0;

                switch (DateTime.Now.DayOfWeek.ToString().ToLower())
                {
                    case "tuesday":
                        controler = 1;
                        break;
                    case "wednesday":
                        controler = 2;
                        break;
                    case "thursday":
                        controler = 3;
                        break;
                    case "friday":
                        controler = 4;
                        break;
                    case "saturday":
                        controler = 5;
                        break;
                    case "sunday":
                        controler = 6;
                        break;
                }

                DateTime data = DateTime.Now.Date.AddDays(-controler);

                return (from rec in banco.Recursoes
                        join log in banco.LogAtividades.Include("Atividade")
                        on rec.recursoId equals log.recurso
                        where rec.usuarioId.Value == id
                        && log.dataOcorrencia.Month == DateTime.Now.Month
                        && log.dataOcorrencia.Year == DateTime.Now.Year
                        && log.dataOcorrencia.Day >= data.Day
                        orderby log.dataOcorrencia descending
                        select new ColaboradorViewModel
                        {
                            ordem = log.Atividade.Servico.ordemId,
                            servico = log.Atividade.Servico.servicoId,
                            atividade = log.Atividade.nome,
                            tipoAtividade = log.Atividade.TipoAtividade.nome,
                            statusAtividade = log.Atividade.StatusAtividade.nome,
                            dataOcorrencia = log.dataOcorrencia,
                            tempoEfetivo = log.tempoEfetivoConsumido.HasValue ? log.tempoEfetivoConsumido.Value : 0,
                            prazoEstimado = log.Atividade.Servico.previsaoHorasExecucao.HasValue ? log.Atividade.Servico.previsaoHorasExecucao.Value : 0,
                            apontamento = log.apontamento,
                            dataEstimativaIni = log.Atividade.dataEstimadaInicio,
                            dataEstimativaFim = log.Atividade.dataEstimadaFim,
                            dataEfetivaIni = log.Atividade.dataEfetivaInicio,
                            dataEfetivaFim = log.Atividade.dataEfetivaFim
                        }).ToList();
            }
        }

        public List<ColaboradorViewModel> SelectRelatorioMesColaborador(long id)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                return (from rec in banco.Recursoes
                        join log in banco.LogAtividades.Include("Atividade")
                        on rec.recursoId equals log.recurso
                        where rec.usuarioId.Value == id
                        && log.dataOcorrencia.Month == DateTime.Now.Month
                        && log.dataOcorrencia.Year == DateTime.Now.Year
                        orderby log.dataOcorrencia descending
                        select new ColaboradorViewModel
                        {
                            ordem = log.Atividade.Servico.ordemId,
                            servico = log.Atividade.Servico.servicoId,
                            atividade = log.Atividade.nome,
                            tipoAtividade = log.Atividade.TipoAtividade.nome,
                            statusAtividade = log.Atividade.StatusAtividade.nome,
                            dataOcorrencia = log.dataOcorrencia,
                            tempoEfetivo = log.tempoEfetivoConsumido.HasValue ? log.tempoEfetivoConsumido.Value : 0,
                            prazoEstimado = log.Atividade.Servico.previsaoHorasExecucao.HasValue ? log.Atividade.Servico.previsaoHorasExecucao.Value : 0,
                            apontamento = log.apontamento,
                            dataEstimativaIni = log.Atividade.dataEstimadaInicio,
                            dataEstimativaFim = log.Atividade.dataEstimadaFim,
                            dataEfetivaIni = log.Atividade.dataEfetivaInicio,
                            dataEfetivaFim = log.Atividade.dataEfetivaFim
                        }).ToList();
            }
        }

        public List<ColaboradorViewModel> SelectRelatorioAtividadeColaborador(long id)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                var lista = (from rec in banco.Recursoes
                             join log in banco.LogAtividades
                             on rec.recursoId equals log.recurso
                             join ati in banco.Atividades.Include("StatusAtividade")
                             on log.atividadeId equals ati.atividadeId
                             join ser in banco.Servicoes.Include("Ordem").Include("Projeto")
                             on ati.servicoId equals ser.servicoId
                             where rec.usuarioId.Value == id && (ati.statusAtividadeId == 4 || ati.statusAtividadeId == 5)
                             orderby ati.dataAlteracao descending
                             select new
                             {
                                 ordem = ser.ordemId,
                                 servico = ser.servicoId,
                                 projeto = ser.Ordem.Projeto.nome,
                                 atividade = ati.nome,
                                 statusAtividade = ati.StatusAtividade.nome,
                                 statusOS = ser.Ordem.Status1.nome,
                                 dataEfetivaFim = ati.dataAlteracao,
                                 tempoEfetivo = log.tempoEfetivoConsumido.HasValue ? log.tempoEfetivoConsumido.Value : 0
                             }).AsEnumerable();

                var listaGrouped = (from lis in lista
                                    group lis by new { lis.ordem, lis.servico, lis.projeto, lis.atividade, lis.statusAtividade, lis.statusOS, lis.dataEfetivaFim }
                                        into nova
                                        select new
                                        {
                                            nova.Key.ordem,
                                            nova.Key.servico,
                                            nova.Key.projeto,
                                            nova.Key.atividade,
                                            nova.Key.statusAtividade,
                                            nova.Key.statusOS,
                                            nova.Key.dataEfetivaFim,
                                            tempoEfetivo = nova.Sum(x => x.tempoEfetivo)
                                        });

                List<ColaboradorViewModel> resultado = new List<ColaboradorViewModel>();

                foreach(var item in listaGrouped)
                {
                    ColaboradorViewModel col = new ColaboradorViewModel();
                    col.ordem = item.ordem;
                    col.servico = item.servico;
                    col.projeto = item.projeto;
                    col.atividade = item.atividade;
                    col.statusAtividade = item.statusAtividade;
                    col.statusOS = item.statusOS;
                    col.dataEfetivaFim = item.dataEfetivaFim;
                    col.tempoEfetivo = item.tempoEfetivo;

                    resultado.Add(col);
                }

                return resultado;
            }
        }

        public List<ColaboradorViewModel> SelectRelatorioAtividadeGrupoColaborador(long id)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                List<long> listaIdsGrupo = banco.UsuarioGrupoUsuarios.Where(x => x.usuarioId == id).Select(x => x.grupoUsuarioId).ToList();
                List<long> ListaIdsUsuario = banco.UsuarioGrupoUsuarios.Where(x => listaIdsGrupo.Contains(x.grupoUsuarioId)).Select(x => x.usuarioId).ToList();
                DateTime data = DateTime.Now.AddMonths(-3);

                var lista = (from usu in banco.Usuarios
                             join rec in banco.Recursoes
                             on usu.usuarioId equals rec.usuarioId
                             join log in banco.LogAtividades
                             on rec.recursoId equals log.recurso
                             join ati in banco.Atividades.Include("StatusAtividade")
                             on log.atividadeId equals ati.atividadeId
                             join ser in banco.Servicoes.Include("Ordem").Include("Projeto")
                             on ati.servicoId equals ser.servicoId
                             where ListaIdsUsuario.Contains(usu.usuarioId) && (ati.statusAtividadeId == 4 || ati.statusAtividadeId == 5)
                             && ati.dataAlteracao.Value >= data
                             orderby ati.dataAlteracao descending
                             select new
                             {
                                 usuario = usu.nome,
                                 ordem = ser.ordemId,
                                 servico = ser.servicoId,
                                 projeto = ser.Ordem.Projeto.nome,
                                 atividade = ati.nome,
                                 statusAtividade = ati.StatusAtividade.nome,
                                 statusOS = ser.Ordem.Status1.nome,
                                 dataEfetivaFim = ati.dataAlteracao,
                                 tempoEfetivo = log.tempoEfetivoConsumido.HasValue ? log.tempoEfetivoConsumido.Value : 0
                             }).AsEnumerable();

                var listaGrouped = (from lis in lista
                                    group lis by new { lis.usuario, lis.ordem, lis.servico, lis.projeto, lis.atividade, lis.statusAtividade, lis.statusOS, lis.dataEfetivaFim }
                                        into nova
                                        select new
                                        {
                                            nova.Key.usuario,
                                            nova.Key.ordem,
                                            nova.Key.servico,
                                            nova.Key.projeto,
                                            nova.Key.atividade,
                                            nova.Key.statusAtividade,
                                            nova.Key.statusOS,
                                            nova.Key.dataEfetivaFim,
                                            tempoEfetivo = nova.Sum(x => x.tempoEfetivo)
                                        });

                List<ColaboradorViewModel> resultado = new List<ColaboradorViewModel>();

                foreach (var item in listaGrouped)
                {
                    ColaboradorViewModel col = new ColaboradorViewModel();
                    col.usuario = item.usuario;
                    col.ordem = item.ordem;
                    col.servico = item.servico;
                    col.projeto = item.projeto;
                    col.atividade = item.atividade;
                    col.statusAtividade = item.statusAtividade;
                    col.statusOS = item.statusOS;
                    col.dataEfetivaFim = item.dataEfetivaFim;
                    col.tempoEfetivo = item.tempoEfetivo;

                    resultado.Add(col);
                }

                return resultado;
            }
        }

        #endregion
    }
}