using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
using System.IO;

using GServiceManagerMVC.ViewModels.Global;
using GServiceManagerMVC.ViewModels.Fabrica;
using GServiceManagerMVC.Models;


namespace GServiceManagerMVC.DAL.Fabrica
{
    public class AtividadeDAL
    {
        #region Public

        public List<SelectOptionViewModel> SelectProjetos()
        {
            using (GSMEntities banco = new GSMEntities())
            {
                return (from pro in banco.Projetoes
                        where pro.tipoSituacaoId == 1
                        orderby pro.nome
                        select new SelectOptionViewModel
                        {
                            id = pro.projetoId,
                            nome = pro.nome
                        }).ToList();
            }
        }

        public int SelectAtividadesCount(PesquisaAtividadeViewModel objeto)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                List<long> status = banco.StatusAtividades.Where(x => x.nome.ToLower().Equals("encerrada") || x.nome.ToLower().Equals("concluída") || x.nome.ToLower().Equals("cancelada")).Select(x => x.statusAtividadeId).ToList();

                return (from ati in banco.Atividades
                        join ser in banco.Servicoes
                        on ati.servicoId equals ser.servicoId
                        join os in banco.Ordems
                        on ser.ordemId equals os.ordemId
                        where (os.Projeto.projetoId == objeto.projeto || objeto.projeto == 0)
                        && (ati.dataEstimadaInicio >= objeto.de || objeto.de == null)
                        && (ati.dataEstimadaFim <= objeto.ate || objeto.ate == null)
                        && (os.os.Contains(objeto.nome) || string.IsNullOrEmpty(objeto.nome))
                        && (os.ordemId == objeto.os || objeto.os == 0)
                        && (!status.Contains(ati.statusAtividadeId))
                        select new AtividadeViewModel
                        {
                            id = ati.atividadeId,
                            nome = ati.nome,
                            status = ati.StatusAtividade.nome
                        }).Count();
            }
        }

        public List<AtividadeViewModel> SelectAtividades(PesquisaAtividadeViewModel objeto)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                int linhaInicial = (objeto.pagina - 1) * objeto.intervalo;

                List<long> status = banco.StatusAtividades.Where(x => x.nome.ToLower().Equals("encerrada") || x.nome.ToLower().Equals("concluída") || x.nome.ToLower().Equals("cancelada")).Select(x => x.statusAtividadeId).ToList();

                var sub = (from ati in banco.Atividades
                           join ser in banco.Servicoes
                           on ati.servicoId equals ser.servicoId
                           join os in banco.Ordems
                           on ser.ordemId equals os.ordemId
                           where (os.Projeto.projetoId == objeto.projeto || objeto.projeto == 0)
                           && (ati.dataEstimadaInicio >= objeto.de || objeto.de == null)
                           && (ati.dataEstimadaFim <= objeto.ate || objeto.ate == null)
                           && (os.os.Contains(objeto.nome) || string.IsNullOrEmpty(objeto.nome))
                           && (os.ordemId == objeto.os || objeto.os == 0)
                           && (!status.Contains(ati.statusAtividadeId))
                           orderby ati.atividadeId descending
                           select ati.atividadeId).Take(linhaInicial).ToList();

                return (from ati in banco.Atividades
                        join ser in banco.Servicoes
                        on ati.servicoId equals ser.servicoId
                        join os in banco.Ordems
                        on ser.ordemId equals os.ordemId
                        where (os.Projeto.projetoId == objeto.projeto || objeto.projeto == 0)
                        && (ati.dataEstimadaInicio >= objeto.de || objeto.de == null)
                        && (ati.dataEstimadaFim <= objeto.ate || objeto.ate == null)
                        && (os.os.Contains(objeto.nome) || string.IsNullOrEmpty(objeto.nome))
                        && (os.ordemId == objeto.os || objeto.os == 0)
                        && !sub.Contains(ati.atividadeId)
                        && (!status.Contains(ati.statusAtividadeId))
                        orderby ati.atividadeId descending
                        select new AtividadeViewModel
                        {
                            id = ati.atividadeId,
                            nome = ati.nome,
                            status = ati.StatusAtividade.nome
                        }).Take(objeto.intervalo).ToList();
            }
        }

        public List<SelectOptionViewModel> SelectOSOption(string objeto)
        {
            long numero;
            bool check = long.TryParse(objeto, out numero);

            using (GSMEntities banco = new GSMEntities())
            {
                if (check)
                {
                    return (from os in banco.Ordems
                            where os.ordemId.Equals(numero)
                            select new SelectOptionViewModel
                            {
                                id = os.ordemId,
                                nome = os.assunto
                            }).ToList();
                }
                else
                {
                    return (from os in banco.Ordems
                            where os.os.Contains(objeto)
                            select new SelectOptionViewModel
                            {
                                id = os.ordemId,
                                nome = os.assunto
                            }).ToList();
                }
            }
        }

        public bool SelectOSBool(long objeto)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                return banco.Ordems.Where(x => x.ordemId == objeto).Any();
            }
        }

        public List<SelectOptionViewModel> SelectTipoAtividade()
        {
            using (GSMEntities banco = new GSMEntities())
            {
                return banco.TipoAtividades.Select(x => new SelectOptionViewModel { id = x.tipoAtividadeId, nome = x.nome }).ToList();
            }
        }

        public OSDescricaoViewModel SelectOS(long objeto)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                var date = banco.PropostaPrazoes.Where(x => x.ordemId == objeto).Select(x => new { x.dataEstimadaInicio, x.dataEstimadaFim }).FirstOrDefault();

                DateTime? ini = null;
                DateTime? fim = null;

                if (date != null)
                {
                    ini = date.dataEstimadaInicio;
                    fim = date.dataEstimadaInicio;
                }

                return (from os in banco.Ordems
                        where os.ordemId == objeto
                        select new OSDescricaoViewModel
                        {
                            id = os.ordemId,
                            os = os.os,
                            assunto = os.assunto,
                            dataEstimativaIni = ini,
                            dataEstimativaFim = fim,
                            tipo = os.TipoOrdemServico.nome,
                            projeto = os.Projeto.nome
                        }).FirstOrDefault();
            }
        }

        public OSDescricaoViewModel SelectOSAtividade(long objeto, ref long idOS)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                long idOSTemp = banco.Atividades.Where(x => x.atividadeId == objeto).Select(x => x.Servico.ordemId).FirstOrDefault();
                idOS = idOSTemp;

                var date = banco.PropostaPrazoes.Where(x => x.ordemId == idOSTemp).Select(x => new { x.dataEstimadaInicio, x.dataEstimadaFim }).FirstOrDefault();

                DateTime? ini = null;
                DateTime? fim = null;

                if (date != null)
                {
                    ini = date.dataEstimadaInicio;
                    fim = date.dataEstimadaInicio;
                }

                return (from os in banco.Ordems
                        where os.ordemId == idOSTemp
                        select new OSDescricaoViewModel
                        {
                            id = os.ordemId,
                            os = os.os,
                            assunto = os.assunto,
                            dataEstimativaIni = ini,
                            dataEstimativaFim = fim,
                            tipo = os.TipoOrdemServico.nome,
                            projeto = os.Projeto.nome
                        }).FirstOrDefault();
            }
        }

        public List<SelectOptionRecursoViewModel> SelectRecurso(long id)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                List<SelectOptionRecursoViewModel> resultado = new List<SelectOptionRecursoViewModel>();

                var recursos = banco.Spr_Recursos_Selecionar_MVC(id);

                foreach (var item in recursos)
                {
                    SelectOptionRecursoViewModel option = new SelectOptionRecursoViewModel
                    {
                        id = item.usuarioId,
                        idRecurso = item.tecnologiaId,
                        nome = item.Usuario
                    };

                    resultado.Add(option);
                }

                return resultado;
            }
        }

        public List<AnexoViewModel> SelectAnexo(long id)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                var anexo = (from an in banco.Anexoes
                             where an.ordemId == id
                             select new AnexoViewModel
                             {
                                 id = an.anexoId,
                                 descricao = an.nome,
                                 arquivo = an.nomeInterno,
                                 data = an.dataUpload.Value,
                                 editar = false
                             }).ToList();

                var resultado = new List<AnexoViewModel>();

                foreach (var item in anexo)
                {
                    if (!resultado.Select(x => x.descricao).Contains(item.descricao))
                        resultado.Add(item);
                }

                return resultado;
            }
        }

        public bool InsertAtividade(long idOS, List<DateTime> datas, SalvarAtividadeViewModel objeto, long idUsuario, string login)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (GSMEntities banco = new GSMEntities())
                {
                    try
                    {
                        Ordem os = banco.Ordems.Where(x => x.ordemId == idOS).FirstOrDefault();
                        long idServico = 0;

                        #region Previsão

                        int previsao = 0;

                        if (datas.Count > 0)
                        {
                            previsao = objeto.prazo * datas.Count + 1;
                        }
                        else
                            previsao = objeto.prazo;

                        #endregion

                        #region Serviço

                        if (os.Servicoes == null)
                        {
                            Servico servico = new Servico
                            {
                                sequenciaServico = 1,
                                nome = os.assunto,
                                prioridade = 0,
                                tecnologiaId = objeto.recursoTec,
                                dataServico = DateTime.Now,
                                previsaoHorasExecucao = previsao,
                                atenderSLA = false,
                                ordemId = idOS,
                                usuarioIdInclusao = idUsuario,
                                dataInclusao = DateTime.Now,
                                status = "A",
                                Descricao = os.os
                            };

                            banco.Servicoes.Add(servico);
                            banco.SaveChanges();

                            idServico = servico.servicoId;
                        }
                        else
                        {
                            Servico serTec = os.Servicoes.Where(x => x.tecnologiaId == objeto.recursoTec && x.ordemId == idOS).FirstOrDefault();

                            if (serTec == null)
                            {
                                Servico servico = new Servico
                                {
                                    sequenciaServico = os.Servicoes.Count + 1,
                                    nome = os.assunto,
                                    prioridade = 0,
                                    tecnologiaId = objeto.recursoTec,
                                    dataServico = DateTime.Now,
                                    previsaoHorasExecucao = previsao,
                                    atenderSLA = false,
                                    ordemId = idOS,
                                    usuarioIdInclusao = idUsuario,
                                    dataInclusao = DateTime.Now,
                                    status = "A",
                                    Descricao = os.os
                                };

                                banco.Servicoes.Add(servico);
                                banco.SaveChanges();

                                idServico = servico.servicoId;
                            }
                            else
                            {
                                idServico = serTec.servicoId;

                                int somaPrevisao = banco.Servicoes.Where(x => x.tecnologiaId == objeto.recursoTec && x.ordemId == idOS).Select(x => x.previsaoHorasExecucao.Value).Sum();

                                serTec.previsaoHorasExecucao = previsao + somaPrevisao;

                                banco.SaveChanges();
                            }
                        }

                        #endregion

                        #region Atividade

                        int atiCount = banco.Atividades.Where(x => x.servicoId == idServico).Count();
                        List<long> listaAtividades = new List<long>();

                        if (datas.Count > 0)
                        {
                            atiCount += 1;

                            Atividade ati = new Atividade
                            {
                                sequenciaAtividade = atiCount,
                                nome = objeto.atividades,
                                descricao = objeto.descricao,
                                dataEstimadaInicio = objeto.dataIni,
                                dataEstimadaFim = objeto.dataFim,
                                tipoAtividadeId = objeto.tipoAti,
                                servicoId = idServico,
                                statusAtividadeId = 1,
                                usuarioIdInclusao = idUsuario,
                                dataInclusao = DateTime.Now,
                                status = "A",
                                prazoEfetivo = objeto.prazo,
                                tecnologiaId = objeto.recursoTec
                            };

                            banco.Atividades.Add(ati);
                            banco.SaveChanges();

                            listaAtividades.Add(ati.atividadeId);

                            foreach (var item in datas)
                            {
                                atiCount += 1;

                                Atividade atividade = new Atividade
                                {
                                    sequenciaAtividade = atiCount,
                                    nome = objeto.atividades,
                                    descricao = objeto.descricao,
                                    dataEstimadaInicio = item,
                                    dataEstimadaFim = objeto.dataFim,
                                    tipoAtividadeId = objeto.tipoAti,
                                    servicoId = idServico,
                                    statusAtividadeId = 1,
                                    usuarioIdInclusao = idUsuario,
                                    dataInclusao = DateTime.Now,
                                    status = "A",
                                    prazoEfetivo = objeto.prazo,
                                    tecnologiaId = objeto.recursoTec
                                };

                                banco.Atividades.Add(atividade);
                                banco.SaveChanges();

                                listaAtividades.Add(atividade.atividadeId);
                            }
                        }
                        else
                        {
                            Atividade atividade = new Atividade
                            {
                                sequenciaAtividade = atiCount + 1,
                                nome = objeto.atividades,
                                descricao = objeto.descricao,
                                //dataEfetivaFim
                                //dataEfetivaInicio
                                dataEstimadaInicio = objeto.dataIni,
                                dataEstimadaFim = objeto.dataFim,
                                tipoAtividadeId = objeto.tipoAti,
                                servicoId = idServico,
                                statusAtividadeId = 1,
                                usuarioIdInclusao = idUsuario,
                                dataInclusao = DateTime.Now,
                                status = "A",
                                prazoEfetivo = objeto.prazo,
                                tecnologiaId = objeto.recursoTec

                            };

                            banco.Atividades.Add(atividade);
                            banco.SaveChanges();

                            listaAtividades.Add(atividade.atividadeId);
                        }

                        #endregion

                        #region Arquivos

                        List<GServiceManagerMVC.Models.Anexo> anexoOrdem;

                        if (objeto.downloads != null)
                        {
                            var down = objeto.downloads.Select(y => y.id).ToList();
                            anexoOrdem = banco.Anexoes.Where(x => down.Contains(x.anexoId)).ToList();
                        }
                        else
                            anexoOrdem = new List<Models.Anexo>();

                        List<Models.Anexo> caminhos = new List<Models.Anexo>();
                        List<Upload> up = objeto.uploads;
                        List<Download> dwn = objeto.downloads;

                        bool movimentou = MovimentarArquivos(idOS, idServico, listaAtividades, login, up, dwn, anexoOrdem, ref caminhos);

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

                        #endregion

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

        public bool UpdateCancelarAtividade(long id)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                try
                {
                    Atividade ati = banco.Atividades.Where(x => x.atividadeId == id).FirstOrDefault();
                    ati.statusAtividadeId = banco.StatusAtividades.Where(x => x.nome.ToLower().Equals("Cancelada")).Select(x => x.statusAtividadeId).FirstOrDefault();

                    banco.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public EditarAtividadeViewModel SelectAtividadeEditar(long id)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                return (from ati in banco.Atividades
                        where ati.atividadeId == id
                        select new EditarAtividadeViewModel
                        {
                            idAti = id,
                            tipoAti = ati.tipoAtividadeId,
                            atividade = ati.nome,
                            descricao = ati.descricao,
                            dataIni = ati.dataEstimadaInicio,
                            dataFim = ati.dataEstimadaFim,
                            prazo = ati.prazoEfetivo.HasValue ? ati.prazoEfetivo.Value : 0,
                            recursoTec = ati.Servico.tecnologiaId.Value

                        }).FirstOrDefault();
            }
        }

        public SelectOptionViewModel SelectOsIdNome(long id)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                return banco.Ordems.Where(x => x.ordemId == id).Select(x => new SelectOptionViewModel { id = x.ordemId, nome = x.assunto }).FirstOrDefault();
            }
        }

        public bool UpdateAtividade(long idOS, List<DateTime> datas, EditarAtividadeViewModel objeto, long idUsuario, string login)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                using (GSMEntities banco = new GSMEntities())
                {
                    try
                    {
                        Ordem os = banco.Ordems.Where(x => x.ordemId == idOS).FirstOrDefault();
                        long idServico = 0;

                        #region Previsão

                        int previsao = 0;

                        if (datas.Count > 0)
                        {
                            previsao = objeto.prazo * datas.Count;
                        }
                        else
                            previsao = objeto.prazo;

                        #endregion

                        #region Serviço

                        Servico serTec = os.Servicoes.Where(x => x.tecnologiaId == objeto.recursoTec && x.ordemId == idOS).FirstOrDefault();
                        idServico = serTec.servicoId;

                        int somaPrevisao = banco.Servicoes.Where(x => x.tecnologiaId == objeto.recursoTec && x.ordemId == idOS).Select(x => x.previsaoHorasExecucao.Value).Sum();

                        serTec.previsaoHorasExecucao = previsao + somaPrevisao;

                        banco.SaveChanges();

                        #endregion

                        #region Atividade

                        int atiCount = banco.Atividades.Where(x => x.servicoId == idServico).Count();
                        List<long> listaAtividades = new List<long>();

                        if (datas.Count > 0)
                        {
                            Atividade ati = banco.Atividades.Where(x => x.atividadeId == objeto.idAti).FirstOrDefault();
                            ati.sequenciaAtividade = atiCount + 1;
                            ati.nome = objeto.atividade;
                            ati.descricao = objeto.descricao;
                            ati.dataEstimadaInicio = objeto.dataIni;
                            ati.dataEstimadaFim = objeto.dataFim;
                            ati.tipoAtividadeId = objeto.tipoAti;
                            ati.usuarioIdAlteracao = idUsuario;
                            ati.dataAlteracao = DateTime.Now;
                            ati.prazoEfetivo = objeto.prazo;

                            banco.SaveChanges();

                            listaAtividades.Add(objeto.idAti);

                            foreach (var item in datas)
                            {
                                atiCount += 1;

                                Atividade atividade = new Atividade
                                {
                                    sequenciaAtividade = atiCount,
                                    nome = objeto.atividade,
                                    descricao = objeto.descricao,
                                    dataEstimadaInicio = item,
                                    dataEstimadaFim = objeto.dataFim,
                                    tipoAtividadeId = objeto.tipoAti,
                                    servicoId = idServico,
                                    statusAtividadeId = 1,
                                    usuarioIdInclusao = idUsuario,
                                    dataInclusao = DateTime.Now,
                                    status = "A",
                                    prazoEfetivo = objeto.prazo,
                                    tecnologiaId = objeto.recursoTec
                                };

                                banco.Atividades.Add(atividade);
                                banco.SaveChanges();

                                listaAtividades.Add(atividade.atividadeId);
                            }
                        }
                        else
                        {
                            Atividade ati = banco.Atividades.Where(x => x.atividadeId == objeto.idAti).FirstOrDefault();
                            ati.sequenciaAtividade = atiCount + 1;
                            ati.nome = objeto.atividade;
                            ati.descricao = objeto.descricao;
                            ati.dataEstimadaInicio = objeto.dataIni;
                            ati.dataEstimadaFim = objeto.dataFim;
                            ati.tipoAtividadeId = objeto.tipoAti;
                            ati.usuarioIdAlteracao = idUsuario;
                            ati.dataAlteracao = DateTime.Now;
                            ati.prazoEfetivo = objeto.prazo;

                            banco.SaveChanges();

                            listaAtividades.Add(objeto.idAti);
                        }

                        #endregion

                        #region Deletar Anexo

                        if (objeto.deletar != null)
                        {
                            List<Models.Anexo> anexo = banco.Anexoes.Where(x => objeto.deletar.Contains(x.anexoId)).ToList();

                            string caminhoDeletar = "~/Upload/OS_" + idOS + "/Servico_" + idServico + "/Atividade_" + objeto.idAti;

                            foreach (var item in anexo)
                            {
                                DeletarArquivo(caminhoDeletar, item.nomeInterno);

                                banco.Anexoes.Remove(item);
                                banco.SaveChanges();
                            }
                        }

                        #endregion

                        #region Arquivos

                        List<GServiceManagerMVC.Models.Anexo> anexoOrdem;

                        if (objeto.downloads != null)
                        {
                            var down = objeto.downloads.Select(y => y.id).ToList();
                            anexoOrdem = banco.Anexoes.Where(x => down.Contains(x.anexoId)).ToList();
                        }
                        else
                            anexoOrdem = new List<Models.Anexo>();

                        List<Models.Anexo> caminhos = new List<Models.Anexo>();
                        List<Upload> up = objeto.uploads;
                        List<Download> dwn = objeto.downloads;

                        bool movimentou = MovimentarArquivos(idOS, idServico, listaAtividades, login, up, dwn, anexoOrdem, ref caminhos);

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

                        #endregion

                        #region Copiar Arquivos Salvos

                        if (banco.Anexoes.Where(x => x.anexoId == objeto.idAti).Any() && datas.Count > 0)
                        {
                            caminhos.Clear();

                            MovimentarArquivosSalvos(idOS, idServico, objeto.idAti, listaAtividades, ref caminhos);

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
                        }

                        #endregion

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

        public List<AnexoViewModel> SelectAnexosEditar(long id)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                return (from an in banco.Anexoes
                        where an.atividadeId == id
                        select new AnexoViewModel
                        {
                            id = an.anexoId,
                            descricao = an.nome,
                            arquivo = an.nomeInterno,
                            data = an.dataUpload.Value,
                            editar = true
                        }).ToList();
            }
        }

        #endregion

        #region Private

        private bool MovimentarArquivos(long idOS, long idServico, List<long> idsAtividade, string login, List<Upload> up, List<Download> dwn, List<GServiceManagerMVC.Models.Anexo> anexos, ref List<Models.Anexo> caminhos)
        {
            try
            {
                string inicial = "~/Upload/OS_" + idOS;

                #region upload

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

                        temporario = Path.Combine(HttpContext.Current.Server.MapPath("~/Upload/" + login), Path.GetFileName(item.anexo));
                        original = Path.Combine(HttpContext.Current.Server.MapPath(inicial + "/Servico_" + idServico), Path.GetFileName(item.anexo));

                        File.Copy(temporario, original, true);

                        Models.Anexo nx1 = new Models.Anexo
                        {
                            nome = item.descricao,
                            nomeInterno = item.anexo,
                            caminho = inicial + "/Servico_" + idServico,
                            servicoId = idServico,
                            ordemId = idOS,
                            dataUpload = item.dtUpload
                        };

                        caminhos.Add(nx1);

                        foreach (var ati in idsAtividade)
                        {
                            DirectoryInfo source2 = new DirectoryInfo(HttpContext.Current.Server.MapPath(inicial + "/Servico_" + idServico + "/Atividade_" + ati));

                            if (!source2.Exists)
                                source2.Create();

                            temporario = Path.Combine(HttpContext.Current.Server.MapPath("~/Upload/" + login), Path.GetFileName(item.anexo));
                            original = Path.Combine(HttpContext.Current.Server.MapPath(inicial + "/Servico_" + idServico + "/Atividade_" + ati), Path.GetFileName(item.anexo));

                            File.Copy(temporario, original, true);

                            Models.Anexo nx2 = new Models.Anexo
                            {
                                nome = item.descricao,
                                nomeInterno = item.anexo,
                                caminho = inicial + "/Servico_" + idServico + "/Atividade_" + ati,
                                servicoId = idServico,
                                ordemId = idOS,
                                dataUpload = item.dtUpload,
                                atividadeId = ati
                            };

                            caminhos.Add(nx2);
                        }
                    }

                    DirectoryInfo source3 = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Upload/" + login));

                    foreach (FileInfo file in source3.GetFiles())
                    {
                        file.Delete();
                    }

                    source3.Delete();
                }

                #endregion

                #region Download

                if (dwn != null)
                {
                    DirectoryInfo srcIncial = new DirectoryInfo(HttpContext.Current.Server.MapPath(inicial));

                    if (!srcIncial.Exists)
                        srcIncial.Create();

                    DirectoryInfo source = new DirectoryInfo(HttpContext.Current.Server.MapPath(inicial + "/Servico_" + idServico));

                    if (!source.Exists)
                        source.Create();

                    using (GSMEntities banco = new GSMEntities())
                    {
                        foreach (var item in dwn)
                        {
                            foreach (var ati in idsAtividade)
                            {
                                DirectoryInfo source2 = new DirectoryInfo(HttpContext.Current.Server.MapPath(inicial + "/Servico_" + idServico + "/Atividade_" + ati));

                                if (!source2.Exists)
                                    source2.Create();

                                string caminho = anexos.Where(x => x.anexoId == item.id).Select(x => x.caminho).FirstOrDefault();
                                string temporario = Path.Combine(HttpContext.Current.Server.MapPath(caminho), Path.GetFileName(item.anexo));
                                string original = Path.Combine(HttpContext.Current.Server.MapPath(inicial + "/Servico_" + idServico + "/Atividade_" + ati), Path.GetFileName(item.anexo));

                                File.Copy(temporario, original, true);

                                Models.Anexo nx1 = new Models.Anexo
                                {
                                    nome = item.descricao,
                                    nomeInterno = item.anexo,
                                    caminho = inicial + "/Servico_" + idServico + "/Atividade_" + ati,
                                    servicoId = idServico,
                                    ordemId = idOS,
                                    dataUpload = item.dtUpload,
                                    atividadeId = ati
                                };

                                caminhos.Add(nx1);
                            }
                        }
                    }
                }

                #endregion

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void MovimentarArquivosSalvos(long idOS, long idServico, long idAtividade, List<long> idsAtividade, ref List<Models.Anexo> caminhos)
        {
            idsAtividade.Remove(idAtividade);
            string atividadeCaminho = "~/Upload/OS_" + idOS + "/Servico_" + idServico + "/Atividade_" + idAtividade;
            DirectoryInfo source = new DirectoryInfo(HttpContext.Current.Server.MapPath(atividadeCaminho));

            using (GSMEntities banco = new GSMEntities())
            {
                var anexos = banco.Anexoes.Where(x => x.atividadeId == idAtividade).ToList();

                foreach (var ati in idsAtividade)
                {
                    string novaAti = "~/Upload/OS_" + idOS + "/Servico_" + idServico + '/' + "Atividade_" + ati;

                    DirectoryInfo source1 = new DirectoryInfo(HttpContext.Current.Server.MapPath(novaAti));

                    if (!source1.Exists)
                        source1.Create();

                    foreach (FileInfo file in source.GetFiles())
                    {
                        string salvar = HttpContext.Current.Server.MapPath(novaAti + "/" + file.Name);
                        string original = HttpContext.Current.Server.MapPath(atividadeCaminho + "/" + file.Name);

                        File.Copy(original, salvar, true);

                        var anx = anexos.Where(x => x.nomeInterno.Equals(file.Name)).FirstOrDefault();

                        Models.Anexo nx = new Models.Anexo
                        {
                            nome = anx.nome,
                            nomeInterno = anx.nomeInterno,
                            caminho = novaAti,
                            servicoId = idServico,
                            ordemId = idOS,
                            dataUpload = DateTime.Now,
                            atividadeId = ati
                        };

                        caminhos.Add(nx);
                    }
                }
            }
        }

        private void DeletarArquivo(string url, string arquivo)
        {
            url = HttpContext.Current.Server.MapPath(url);
            url = Path.Combine(url, Path.GetFileName(arquivo));

            File.Delete(url);
        }

        #endregion
    }
}