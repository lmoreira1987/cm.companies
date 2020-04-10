using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Transactions;
using GServiceManagerMVC.Models;
using System.Collections.Generic;
using GServiceManagerMVC.Filters;
using GServiceManagerMVC.DAL.Dashboard;
using GServiceManagerMVC.BLL.Dashboard;
using GServiceManagerMVC.ViewModels.Global;
using GServiceManagerMVC.ViewModels.Dashboard;

namespace GServiceManagerMVC.DAL.Dashboard
{
    public class DashboardDAL
    {
        #region Public

        public List<TempoViewModel> SelectTempoEfetifo(long id)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                return (from rec in banco.Recursoes
                        join log in banco.LogAtividades
                        on rec.recursoId equals log.recurso
                        where rec.usuarioId.Value == id
                        && log.dataOcorrencia.Month == DateTime.Now.Month
                        && log.dataOcorrencia.Year == DateTime.Now.Year
                        orderby log.dataOcorrencia descending
                        select new TempoViewModel
                         {
                             tempo = log.tempoEfetivoConsumido.HasValue ? log.tempoEfetivoConsumido.Value : 0,
                             dataOcorrencia = log.dataOcorrencia
                         }).ToList();
            }
        }

        public int SelectAtividadesConcluidas(long id)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                var atividades = (from rec in banco.Recursoes
                                  join log in banco.LogAtividades
                                  on rec.recursoId equals log.recurso
                                  where rec.usuarioId.Value == id
                                  && log.Atividade.statusAtividadeId == 4
                                  select log.Atividade.atividadeId).ToList();

                return atividades.Distinct().Count();
            }
        }

        #region Colaborador

        public List<AtividadeUser> MinhasAtividades(long id)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                #region select

                var atsUser = (from r in banco.Recursoes
                               join l in banco.LogAtividades.Include("Recurso")
                                    on r.recursoId equals l.recurso
                               join a in banco.Atividades
                                    on l.atividadeId equals a.atividadeId
                               join s in banco.Servicoes
                                    on a.servicoId equals s.servicoId
                               join o in banco.Ordems
                                    on s.ordemId equals o.ordemId
                               join p in banco.Projetoes
                                    on o.projetoId equals p.projetoId
                               join sa in banco.StatusAtividades
                                    on a.statusAtividadeId equals sa.statusAtividadeId
                               join ap in banco.AtividadePrazoes
                                    on a.atividadeId equals ap.atividadeId
                               join ugu in banco.UsuarioGrupoUsuarios
                                    on l.Recurso1.usuarioId equals ugu.usuarioId
                               join g in banco.GrupoUsuarios
                                    on ugu.grupoUsuarioId equals g.grupoUsuarioId
                               where r.usuarioId == id
                               && (a.statusAtividadeId == 1
                               || a.statusAtividadeId == 2
                               || a.statusAtividadeId == 3
                               || a.statusAtividadeId == 6)
                               select new AtividadeUser
                               {
                                   grupoId = g.grupoUsuarioId,
                                   grupoNome = g.nome,
                                   atividadeId = a.atividadeId,
                                   atividadeNome = a.nome,
                                   statusAtividadeId = a.statusAtividadeId,
                                   statusNome = sa.nome,
                                   projetoId = s.Ordem.Projeto.projetoId,
                                   projetoNome = p.nome,
                                   ordemId = s.ordemId,
                                   servicoId = s.servicoId,
                                   dataEstimadaIni = ap.dataEstimadaInicio,
                                   dataEstimadaFim = ap.dataEstimadaFim,
                                   quantidadeHoras = l.tempoEfetivoConsumido.HasValue ? l.tempoEfetivoConsumido.Value : 0
                               }).AsEnumerable();

                var listaGrouped = (from atUs in atsUser
                                    group atUs by new
                                    {
                                        atUs.atividadeId,
                                        atUs.atividadeNome,
                                        atUs.statusAtividadeId,
                                        atUs.statusNome,
                                        atUs.projetoId,
                                        atUs.projetoNome,
                                        atUs.ordemId,
                                        atUs.servicoId,
                                        atUs.dataEstimadaIni,
                                        atUs.dataEstimadaFim
                                    }
                                        into nova
                                        select new
                                        {
                                            nova.Key.atividadeId,
                                            nova.Key.atividadeNome,
                                            nova.Key.statusAtividadeId,
                                            nova.Key.statusNome,
                                            nova.Key.projetoId,
                                            nova.Key.projetoNome,
                                            nova.Key.ordemId,
                                            nova.Key.servicoId,
                                            nova.Key.dataEstimadaIni,
                                            nova.Key.dataEstimadaFim,
                                            quantidadeHoras = nova.Sum(x => x.quantidadeHoras)
                                        });
                #endregion

                List<AtividadeUser> atividadeUser = new List<AtividadeUser>();

                foreach (var a in listaGrouped)
                {
                    AtividadeUser act = new AtividadeUser();

                    act.atividadeId = a.atividadeId;
                    act.atividadeNome = a.atividadeNome;
                    act.statusAtividadeId = a.statusAtividadeId;
                    act.statusNome = a.statusNome;
                    act.projetoId = a.projetoId;
                    act.projetoNome = a.projetoNome;
                    act.ordemId = a.ordemId;
                    act.servicoId = a.servicoId;
                    act.dataEstimadaIni = a.dataEstimadaIni;
                    act.dataEstimadaFim = a.dataEstimadaFim;
                    act.quantidadeHoras = a.quantidadeHoras;

                    atividadeUser.Add(act);
                }

                return atividadeUser;
            }
        }

        public List<AtividadeGrupo> AtividadesGrupo(long id)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                List<AtividadeGrupo> atvdGrupList = new List<AtividadeGrupo>();

                var result = banco.Spr_Atividades_Grupo(id);

                foreach (var item in result)
                {
                    AtividadeGrupo atividadeG = new AtividadeGrupo
                    {
                        atividadeId = item.atividadeId,
                        nomeProjeto = item.Projeto,
                        nomeAtividade = item.Atividade,
                        dataEstimadaInicio = item.dataEstimadaInicio,
                        dataEstimadaFim = item.dataEstimadaFim,
                        tipoAtividadeNome = item.nomeTipoAtividade,
                        atividadePrazoEstimado = item.prazoEstimadoConclusao,
                        atividadeDescricao = item.descricao
                    };
                    atvdGrupList.Add(atividadeG);
                }
                return atvdGrupList;
            }
        }

        public AtividadeLog AtividadeLog(long id)
        {
            AtividadeLog atividade = new AtividadeLog();

            using (GSMEntities banco = new GSMEntities())
            {
                atividade = (from a in banco.Atividades
                             join s in banco.StatusAtividades
                             on a.statusAtividadeId equals s.statusAtividadeId
                             join t in banco.TipoAtividades
                             on a.tipoAtividadeId equals t.tipoAtividadeId
                             join l in banco.LogAtividades
                             on a.atividadeId equals l.atividadeId
                             join pe in banco.AtividadePrazoes
                             on a.atividadeId equals pe.atividadeId
                             where a.atividadeId == id
                             select new AtividadeLog
                             {
                                 atividadeId = a.atividadeId,
                                 atividadeNome = a.nome,
                                 atividadeDescricao = a.descricao,
                                 atividadeDataEstimadaInicio = a.dataEfetivaInicio.HasValue ? a.dataEstimadaInicio : null,
                                 atividadeDataEstimadaFim = a.dataEstimadaFim.HasValue ? a.dataEfetivaFim : null,
                                 tipoAtividadeNome = t.nome,
                                 atividadePrazoEstimado = pe.prazoEstimadoConclusao.HasValue ? pe.prazoEstimadoConclusao : 0,
                                 statusAtividadeNome = s.nome
                             }).FirstOrDefault();

                List<Apontamento> apontamentos = (from l in banco.LogAtividades
                                                  join r in banco.Recursoes
                                                  on l.recurso equals r.recursoId
                                                  join u in banco.Usuarios
                                                  on r.usuarioId equals u.usuarioId
                                                  where l.atividadeId == id
                                                  select new Apontamento
                                                  {
                                                      usuarioLogin = u.login,
                                                      dataInclusão = l.dataInclusao,
                                                      apontamento = l.apontamento
                                                  }).ToList();

                atividade.Apontamentos = apontamentos;
                atividade.Anexos = AnexosDeAtividade(id);
            }
            return atividade;
        }

        public List<Anexos> AnexosDeAtividade(long id)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                List<Anexos> anexos = (from an in banco.Anexoes
                                       join at in banco.Atividades
                                           on an.atividadeId equals at.atividadeId
                                       where at.atividadeId == id
                                       select new Anexos
                                       {
                                           sequenciaAnexo = an.sequenciaAnexo,
                                           nome = an.nome,
                                           nomeInterno = an.nomeInterno,
                                           dataUpload = an.dataUpload,
                                           caminho = an.caminho,
                                           servicoId = an.servicoId,
                                           ordemId = an.ordemId,
                                           parecerId = an.parecerId,
                                           usuarioIdInclusao = an.usuarioIdInclusao,
                                           usuarioIdAlteracao = an.usuarioIdAlteracao,
                                           dataInclusao = an.dataInclusao,
                                           dataAlteracao = an.dataAlteracao,
                                           status = an.status,
                                           atividadeId = an.atividadeId,
                                           logAtividadeId = an.logAtividadeId
                                       }).ToList();


                return anexos;
            }
        }

        public void MudarStatusAtividade(long id, long novoStatus)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                var atividade = banco.Atividades.Where(a => a.atividadeId.Equals(id)).FirstOrDefault();

                atividade.statusAtividadeId = novoStatus;
                banco.SaveChanges();
            }
        }

        public bool IniciarAtividadeGrupo(long id, long novoStatus, long userId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (GSMEntities banco = new GSMEntities())
                {
                    try
                    {

                        var atividade = banco.Atividades.Where(a => a.atividadeId.Equals(id)).FirstOrDefault();
                        atividade.statusAtividadeId = novoStatus;

                        var atividadePrazo = (from ap in banco.AtividadePrazoes
                                              where ap.atividadeId == id
                                              orderby ap.atividadePrazoId descending
                                              select ap).FirstOrDefault();

                        atividadePrazo.usuarioIdEstimado = userId;

                        Usuario user = banco.Usuarios.Where(u => u.usuarioId.Equals(userId)).First();

                        LogAtividade logInicial = new LogAtividade();

                        logInicial.sequenciaLogAtividade = 1;
                        logInicial.dataOcorrencia = DateTime.Today;
                        logInicial.tempoEfetivoConsumido = 0;
                        logInicial.apontamento = "**************** TAREFA INICIADA POR " + user.login + " ****************";
                        logInicial.recurso = banco.Recursoes.Where(r => r.usuarioId == userId).Select(u => u.recursoId).FirstOrDefault();
                        logInicial.atividadeId = id;
                        logInicial.usuarioIdInclusao = userId;
                        logInicial.usuarioIdAlteracao = userId;
                        logInicial.dataInclusao = DateTime.Today;
                        logInicial.dataAlteracao = DateTime.Today;
                        logInicial.status = "A";
                        logInicial.DataInicio = DateTime.Today;

                        banco.LogAtividades.Add(logInicial);

                        banco.SaveChanges();

                        scope.Complete();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
        }

        public bool salvarLog(long atividadeId, int tempoEfetivoConsumido, string apontamento, string status, long userId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (GSMEntities banco = new GSMEntities())
                {
                    try
                    {

                        DashboardDAL dal = new DashboardDAL();

                        LogAtividade logInicial = new LogAtividade
                        {
                            sequenciaLogAtividade = banco.LogAtividades.Where(l => l.atividadeId == atividadeId).OrderByDescending(l => l.logAtividadeId).Select(l => l.sequenciaLogAtividade).FirstOrDefault() + 1,
                            dataOcorrencia = DateTime.Today,
                            tempoEfetivoConsumido = tempoEfetivoConsumido,
                            apontamento = apontamento,
                            recurso = banco.Recursoes.Where(r => r.usuarioId == userId).Select(u => u.recursoId).FirstOrDefault(),
                            atividadeId = atividadeId,
                            usuarioIdInclusao = userId,
                            usuarioIdAlteracao = userId,
                            dataInclusao = DateTime.Today,
                            dataAlteracao = DateTime.Today,
                            status = "A",
                            DataInicio = banco.LogAtividades.Where(l => l.atividadeId == atividadeId).OrderBy(l => l.logAtividadeId).Select(l => l.DataInicio).FirstOrDefault(),
                            DataFim = banco.LogAtividades.Where(l => l.atividadeId == atividadeId).OrderBy(l => l.logAtividadeId).Select(l => l.DataFim).FirstOrDefault(),
                        };

                        banco.LogAtividades.Add(logInicial);
                        banco.SaveChanges();
                        scope.Complete();

                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
        }

        public HorasMes HorasTrabalhadasMes(long id)
        {

            using (GSMEntities banco = new GSMEntities())
            {

                HorasMes horasMes = new HorasMes();
                horasMes.diasMes = new List<int>();
                horasMes.nomesProjetosMes = new List<string>();
                horasMes.projetosMes = new List<ProjetoMes>();

                #region Projetos no mês

                horasMes.nomesProjetosMes = (from a in banco.Atividades
                                             join s in banco.Servicoes
                                                     on a.servicoId equals s.servicoId
                                             join o in banco.Ordems
                                                     on s.ordemId equals o.ordemId
                                             join p in banco.Projetoes
                                                     on o.projetoId equals p.projetoId
                                             join l in banco.LogAtividades
                                                     on a.atividadeId equals l.atividadeId
                                             join r in banco.Recursoes
                                                     on l.recurso equals r.recursoId
                                             join u in banco.Usuarios
                                                     on r.usuarioId equals u.usuarioId
                                             where u.usuarioId == id && (l.dataOcorrencia.Month == DateTime.Today.Month)
                                             select (
                                                 p.nome
                                             )).ToList().Distinct();

                #endregion

                #region Dias do mês
                DateTime primeiro = Convert.ToDateTime("01/" + DateTime.Today.Month + "/" + DateTime.Today.Year);
                DateTime ultimo;
                if (DateTime.Today.Month < 12)
                {
                    ultimo = Convert.ToDateTime("01/" + (DateTime.Today.Month + 1) + "/" + DateTime.Today.Year);
                }
                else
                {
                    ultimo = Convert.ToDateTime("01/" + "01/" + (DateTime.Today.Year + 1));
                }

                while (primeiro.CompareTo(ultimo) < 0)
                {
                    if (primeiro.DayOfWeek != DayOfWeek.Saturday && primeiro.DayOfWeek != DayOfWeek.Sunday)
                    {
                        int numeroDia = primeiro.Day;
                        horasMes.diasMes.Add(numeroDia);
                    }
                    primeiro = primeiro.AddDays(1);
                }

                #endregion


                foreach (var projName in horasMes.nomesProjetosMes)
                {
                    List<int> horasDiaProj = new List<int>();

                    foreach (var dia in horasMes.diasMes)
                    {
                        var projHorasDia = (from a in banco.Atividades
                                            join s in banco.Servicoes
                                                    on a.servicoId equals s.servicoId
                                            join o in banco.Ordems
                                                    on s.ordemId equals o.ordemId
                                            join p in banco.Projetoes
                                                    on o.projetoId equals p.projetoId
                                            join l in banco.LogAtividades
                                                    on a.atividadeId equals l.atividadeId
                                            join r in banco.Recursoes
                                                    on l.recurso equals r.recursoId
                                            join u in banco.Usuarios
                                                    on r.usuarioId equals u.usuarioId
                                            where u.usuarioId == id
                                            && (l.dataOcorrencia.Month == DateTime.Today.Month)
                                            && (p.nome == projName)
                                            && (l.dataOcorrencia.Day == dia)
                                            select
                                            (
                                                l.tempoEfetivoConsumido
                                            )).ToList();
                        int tempo = 0;

                        foreach (var time in projHorasDia)
                        {
                            tempo += time.HasValue ? Convert.ToInt32(time) : 0;
                        }

                        horasDiaProj.Add(tempo);
                    }

                    ProjetoMes projM = new ProjetoMes();
                    projM.projeto = projName;
                    projM.tempoEfetivoConsumido = horasDiaProj;

                    horasMes.projetosMes.Add(projM);
                }

                return horasMes;
            }
        }

        public int QtdHoras(long id)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                return (from l in banco.LogAtividades
                        where l.atividadeId == id && l.tempoEfetivoConsumido.HasValue
                        select l.tempoEfetivoConsumido.Value).Sum();
            }
        }

        public bool InsertAtividade(long atividadeId, LogarAtividadeViewModel objeto, long idUsuario, string login)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (GSMEntities banco = new GSMEntities())
                {
                    try
                    {
                        long idOS = (from a in banco.Atividades
                                     join s in banco.Servicoes on a.servicoId equals s.servicoId
                                     where a.atividadeId == atividadeId
                                     select s.ordemId).FirstOrDefault();

                        long idServico = (from a in banco.Atividades
                                          join s in banco.Servicoes on a.servicoId equals s.servicoId
                                          where a.atividadeId == atividadeId
                                          select s.servicoId).FirstOrDefault();

                        #region Arquivos

                        List<Models.Anexo> caminhos = new List<Models.Anexo>();
                        List<Upload> up = objeto.uploads;

                        bool movimentou = MovimentarArquivos(atividadeId, idOS, idServico, login, up, ref caminhos);

                        if (caminhos.Count > 0)
                        {
                            int countAnexo = banco.Anexoes.Where(x => x.ordemId == idOS).Count();

                            foreach (var item in caminhos)
                            {
                                countAnexo += 1;
                                Models.Anexo nx = new Models.Anexo
                                {
                                    sequenciaAnexo = countAnexo,
                                    nome = item.nome,
                                    nomeInterno = item.nomeInterno,
                                    caminho = item.caminho,
                                    servicoId = item.servicoId,
                                    ordemId = item.ordemId,
                                    dataUpload = item.dataUpload,
                                    usuarioIdInclusao = idUsuario,
                                    dataInclusao = DateTime.Now,
                                    status = "A",
                                    atividadeId = item.atividadeId
                                };
                                banco.Anexoes.Add(nx);
                                banco.SaveChanges();
                            }
                        }

                        #endregion Arquivos

                        if (movimentou)
                        {
                            scope.Complete();
                            return true;
                        }
                        else
                            return false;

                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
        }


        #endregion Colaborador

        #endregion Public

        #region Private

        private bool MovimentarArquivos(long atividadeID, long idOS, long idServico, string login, List<Upload> up, ref List<Models.Anexo> caminhos)
        {
            try
            {
                string inicial = "~/Upload/OS_" + idOS;

                if (up != null)
                {
                    DirectoryInfo srcIncial = new DirectoryInfo(HttpContext.Current.Server.MapPath(inicial));

                    if (!srcIncial.Exists)
                        srcIncial.Create();

                    DirectoryInfo source = new DirectoryInfo(HttpContext.Current.Server.MapPath(inicial + "/Servico_" + idServico));

                    if (!source.Exists)
                        source.Create();

                    foreach (var item in up)
                    {
                        string temporario = string.Empty;
                        string original = string.Empty;

                            DirectoryInfo source2 = new DirectoryInfo(HttpContext.Current.Server.MapPath(inicial + "/Servico_" + idServico + "/Atividade_" + atividadeID));

                            if (!source2.Exists)
                                source2.Create();

                            temporario = Path.Combine(HttpContext.Current.Server.MapPath("~/Upload/" + login), Path.GetFileName(item.anexo));
                            original = Path.Combine(HttpContext.Current.Server.MapPath(inicial + "/Servico_" + idServico + "/Atividade_" + atividadeID), Path.GetFileName(item.anexo));

                            File.Copy(temporario, original, true);

                            Models.Anexo nx = new Models.Anexo
                            {
                                nome = item.descricao,
                                nomeInterno = item.anexo,
                                caminho = inicial + "/Servico_" + idServico + "/Atividade_" + atividadeID,
                                servicoId = idServico,
                                ordemId = idOS,
                                dataUpload = item.dtUpload,
                                atividadeId = atividadeID
                            };

                            caminhos.Add(nx);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

    }
}