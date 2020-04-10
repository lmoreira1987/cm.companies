using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System;
using System.IO;
using System.Web;

using GServiceManagerMVC.Filters;
using GServiceManagerMVC.BLL.Fabrica;
using GServiceManagerMVC.DAL.Fabrica;
using GServiceManagerMVC.ViewModels.Fabrica;
using GServiceManagerMVC.ViewModels.Global;


namespace GServiceManagerMVC.Controllers
{
    [Autenticar]
    public class FabricaController : Controller
    {
        #region Propriedades

        AtividadeBLL bllAtividade;
        AtividadeDAL dllAtividade;

        #endregion

        #region Public

        #region Atividade
        public ActionResult Atividade()
        {
            dllAtividade = new AtividadeDAL();

            Session["PesquisaAtividades"] = null;

            ViewBag.MensagemVazia = "Utilizar o filtro para pesquisar as atividades.";
            ViewBag.Projetos = dllAtividade.SelectProjetos();
            ViewBag.Count = 0;

            return View(new List<AtividadeViewModel>());
        }

        public ActionResult PesquisarAtividade(PesquisaAtividadeViewModel objeto)
        {
            dllAtividade = new AtividadeDAL();

            decimal count = dllAtividade.SelectAtividadesCount(objeto);
            List<AtividadeViewModel> model = dllAtividade.SelectAtividades(objeto);

            Session["PesquisaAtividades"] = objeto;

            #region ViewBags

            ViewBag.Projetos = dllAtividade.SelectProjetos();

            if (count > 0)
                ViewBag.MensagemVazia = "";
            else
                ViewBag.MensagemVazia = "Nenhuma atividade encontrada.";

            int countResultado = 0;

            if (count % objeto.intervalo == 0)
            {
                count = Math.Floor(count / objeto.intervalo);
                countResultado = Convert.ToInt32(count);
            }
            else
            {
                count = Math.Floor(count / objeto.intervalo);
                countResultado = Convert.ToInt32(count % objeto.intervalo == 0 ? count : count + 1);
            }

            ViewBag.Count = countResultado;

            #endregion

            return PartialView("~/Views/Fabrica/_Atividade/_TabelaAtividades.cshtml", model);
        }

        public ActionResult PaginarAtividades(int objeto)
        {
            dllAtividade = new AtividadeDAL();

            PesquisaAtividadeViewModel pesquisa = (PesquisaAtividadeViewModel)Session["PesquisaAtividades"];
            pesquisa.pagina = objeto;

            List<AtividadeViewModel> model = dllAtividade.SelectAtividades(pesquisa);

            return PartialView("~/Views/Fabrica/_Atividade/_TabelaAtividadesConteudo.cshtml", model);
        }

        public ActionResult PesquisarAtividadeAtualizar()
        {
            dllAtividade = new AtividadeDAL();

            PesquisaAtividadeViewModel objeto = (PesquisaAtividadeViewModel)Session["PesquisaAtividades"];
            objeto.pagina = 1;

            decimal count = dllAtividade.SelectAtividadesCount(objeto);
            List<AtividadeViewModel> model = dllAtividade.SelectAtividades(objeto);

            Session["PesquisaAtividades"] = objeto;

            #region ViewBags

            ViewBag.Projetos = dllAtividade.SelectProjetos();

            if (count > 0)
                ViewBag.MensagemVazia = "";
            else
                ViewBag.MensagemVazia = "Nenhuma atividade encontrada.";

            int countResultado = 0;

            if (count % objeto.intervalo == 0)
            {
                count = Math.Floor(count / objeto.intervalo);
                countResultado = Convert.ToInt32(count);
            }
            else
            {
                count = Math.Floor(count / objeto.intervalo);
                countResultado = Convert.ToInt32(count % objeto.intervalo == 0 ? count : count + 1);
            }

            ViewBag.Count = countResultado;

            #endregion

            return PartialView("~/Views/Fabrica/_Atividade/_TabelaAtividades.cshtml", model);
        }

        public ActionResult CriarAtividade()
        {
            return PartialView("~/Views/Fabrica/_Atividade/_AtividadesCriarAtividades.cshtml");
        }

        public ActionResult CriarAtividadeConteudo()
        {
            dllAtividade = new AtividadeDAL();
            ViewBag.TipoAtividade = dllAtividade.SelectTipoAtividade();
            ViewBag.Recurso = dllAtividade.SelectRecurso((long)Session["ID_OS"]);

            return PartialView("~/Views/Fabrica/_Atividade/_AtividadesCriarAtividadesConteudo.cshtml");
        }

        public ActionResult CriarAtividadeConteudoAnexo()
        {
            dllAtividade = new AtividadeDAL();

            long numero = (long)Session["ID_OS"];

            List<AnexoViewModel> model = dllAtividade.SelectAnexo(numero);

            return PartialView("~/Views/Fabrica/_Atividade/_AtividadesCriarAtividadesConteudoAnexo.cshtml", model);
        }

        public ActionResult CriarAtividadeOS()
        {
            dllAtividade = new AtividadeDAL();

            long numero = (long)Session["ID_OS"];

            OSDescricaoViewModel model = dllAtividade.SelectOS(numero);

            return PartialView("~/Views/Fabrica/_Atividade/_AtividadesCriarAtividadesOS.cshtml", model);
        }

        public JsonResult VerificaOS(string objeto)
        {
            dllAtividade = new AtividadeDAL();
            objeto = objeto.Split(' ').First();

            long numero;
            bool check = long.TryParse(objeto, out numero);

            if (check)
            {
                Session["ID_OS"] = numero;

                if (dllAtividade.SelectOSBool(numero))
                    return Json(true, JsonRequestBehavior.AllowGet);
                else
                    return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TypeAHeadOS(string query)
        {
            dllAtividade = new AtividadeDAL();
            List<SelectOptionViewModel> lista = dllAtividade.SelectOSOption(query);
            List<string> resultado = new List<string>();

            foreach (var item in lista)
            {
                resultado.Add(item.id + " - " + item.nome);
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FileUpload()
        {
            string fileName = "";
            var login = (LoginViewModel)Session["Usuario"];

            DirectoryInfo source = new DirectoryInfo(Server.MapPath("~/Upload/" + login.login));

            if (!source.Exists)
                source.Create();

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;

                if (hpf.ContentLength == 0)
                    continue;

                string savedFileName = Path.Combine(Server.MapPath("~/Upload/" + login.login), Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName);

                fileName = hpf.FileName;
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteFiles()
        {
            var login = (LoginViewModel)Session["Usuario"];

            DirectoryInfo source = new DirectoryInfo(Server.MapPath("~/Upload/" + login.login));

            if (!source.Exists)
                source.Create();

            foreach (FileInfo file in source.GetFiles())
            {
                file.Delete();
            }

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDate()
        {
            return Json(DateTime.Now.ToString(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SalvarAtividade(SalvarAtividadeViewModel objeto)
        {
            dllAtividade = new AtividadeDAL();

            long idOS = (long)Session["ID_OS"];
            var login = (LoginViewModel)Session["Usuario"];

            #region Propagar

            List<DateTime> listaDatas = new List<DateTime>();

            if (objeto.datas != null)
            {
                var datas = objeto.datas.Split(',');

                foreach (var item in datas)
                {
                    listaDatas.Add(Convert.ToDateTime(item));
                }
            }

            #endregion

            bool check = dllAtividade.InsertAtividade(idOS, listaDatas, objeto, login.id, login.login);

            if (check)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CancelarAtividade(long objeto)
        {
            dllAtividade = new AtividadeDAL();

            return Json(dllAtividade.UpdateCancelarAtividade(objeto), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditAtividade(long objeto)
        {
            dllAtividade = new AtividadeDAL();
            long idOS = 0;

            OSDescricaoViewModel os = dllAtividade.SelectOSAtividade(objeto, ref idOS);
            EditarAtividadeViewModel model = dllAtividade.SelectAtividadeEditar(objeto);
            model.osDescricao = os;

            SelectOptionViewModel option = dllAtividade.SelectOsIdNome(idOS);
            model.id = option.id + " - " + option.nome;

            Session["ID_OS"] = idOS;

            ViewBag.TipoAtividade = dllAtividade.SelectTipoAtividade();
            ViewBag.Recurso = dllAtividade.SelectRecurso(idOS);

            return PartialView("~/Views/Fabrica/_Atividade/_AtividadesEditAtividades.cshtml", model);
        }

        public ActionResult EditAtividadeConteudoAnexo(long id)
        {
            dllAtividade = new AtividadeDAL();

            long numero = (long)Session["ID_OS"];

            List<AnexoViewModel> model = dllAtividade.SelectAnexo(numero);

            model.AddRange(dllAtividade.SelectAnexosEditar(id));

            return PartialView("~/Views/Fabrica/_Atividade/_AtividadesEditAtividadesConteudoAnexo.cshtml", model);
        }

        public JsonResult EditarAtividade(EditarAtividadeViewModel objeto)
        {
            dllAtividade = new AtividadeDAL();

            long idOS = (long)Session["ID_OS"];
            var login = (LoginViewModel)Session["Usuario"];

            #region Propagar

            List<DateTime> listaDatas = new List<DateTime>();

            if (objeto.datas != null)
            {
                var datas = objeto.datas.Split(',');

                foreach (var item in datas)
                {
                    listaDatas.Add(Convert.ToDateTime(item));
                }
            }

            #endregion

            bool check = dllAtividade.UpdateAtividade(idOS, listaDatas, objeto, login.id, login.login);

            if (check)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion
    }
}