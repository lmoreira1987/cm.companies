using System;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web;
using GServiceManagerMVC.ViewModels.Global;
using GServiceManagerMVC.Filters;
using GServiceManagerMVC.DAL.Dashboard;
using GServiceManagerMVC.BLL.Dashboard;
using GServiceManagerMVC.ViewModels.Dashboard;
using System.Transactions;
using GServiceManagerMVC.Models;

namespace GServiceManagerMVC.Controllers
{
    public class DashboardController : Controller
    {
        #region Propriedades

        DashboardDAL dal;
        DashboardBLL bll;

        #endregion

        #region Public

        [Autenticar]
        public ActionResult Index()
        {
            LoginViewModel login = (LoginViewModel)Session["Usuario"];

            long idPerfil = login.idPerfil.FirstOrDefault();

            if (idPerfil == 4) // Felipe
            {
                dal = new DashboardDAL();
                bll = new DashboardBLL();

                #region ViewBags

                List<TempoViewModel> tempo = dal.SelectTempoEfetifo(login.id);

                ViewBag.Semana = bll.GetSemana(tempo);
                ViewBag.Mes = bll.GetMes(tempo);
                ViewBag.Concluidas = dal.SelectAtividadesConcluidas(login.id);

                #endregion

                informacoesUser infoUser = new informacoesUser();
                infoUser.atividadesUser = dal.MinhasAtividades(login.id);
                infoUser.horasMes = dal.HorasTrabalhadasMes(login.id);
                infoUser.atividadesdoGrupo = dal.AtividadesGrupo(login.id);

                return View("~/Views/Dashboard/IndexColaborador.cshtml", infoUser);
            }
            else if (idPerfil == 2)// Arlanio
            {
                return View("~/Views/Dashboard/Index.cshtml");
            }
            else
            {
                return View("~/Views/Dashboard/Index.cshtml");
            }
        }

        [HttpGet]
        public ActionResult AtividadeLog(long Id)
        {
            LoginViewModel login = new LoginViewModel();

            if (Session["Usuario"] != null)
            {
                login = (LoginViewModel)Session["Usuario"];
            }
            else
            {
                if (Request.Cookies["Usuario"] != null)
                {
                    login.avatar = Server.HtmlEncode(Request.Cookies["Usuario"]["avatar"]);
                    if (String.IsNullOrEmpty(login.avatar))
                    {
                        login.avatar = "Sombra.jpg";
                    }
                    login.nome = Server.HtmlEncode(Request.Cookies["Usuario"]["nome"]);
                    login.login = Server.HtmlEncode(Request.Cookies["Usuario"]["login"]);
                }
            }

            DashboardDAL dal = new DashboardDAL();

            AtividadeLog atividade = dal.AtividadeLog(Id);

            return View("~/Views/Dashboard/AtividadeLog.cshtml", atividade);
        }

        [HttpPost]
        public ActionResult AlterarStatusAtividade(SelectOptionViewModel objeto)
        {
            DashboardDAL dal = new DashboardDAL();

            long id = objeto.id;
            long novoStatus = Convert.ToInt32(objeto.nome);

            dal.MudarStatusAtividade(id, novoStatus);

            AtividadeLog model = dal.AtividadeLog(id);

            return PartialView("~/Views/Dashboard/_Dashboard/_PainelAtividade.cshtml", model);
        }

        [HttpPost]
        public JsonResult PegarAtividadeGrupo(SelectOptionViewModel objeto)
        {
            LoginViewModel login = (LoginViewModel)Session["Usuario"];

            DashboardDAL dal = new DashboardDAL();

            long id = objeto.id;
            long novoStatus = Convert.ToInt32(objeto.nome);

            if (dal.IniciarAtividadeGrupo(id, novoStatus, login.id))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult LogWorkAtividade(long objeto)
        {
            DashboardDAL dal = new DashboardDAL();
            AtividadeLog model = dal.AtividadeLog(objeto);

            return PartialView("~/Views/Dashboard/_Dashboard/_LogWork.cshtml", model);
        }

        [HttpPost]
        public JsonResult logarAtividade(SalvarAtividadeLog objeto)
        {
            DashboardDAL dal = new DashboardDAL();

            LoginViewModel login = (LoginViewModel)Session["Usuario"];

            if (dal.salvarLog(objeto.atividadeId, objeto.tempoEfetivoConsumido, objeto.apontamento, objeto.status, login.id))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CancelarLogWorkAtividade(long objeto)
        {
            DashboardDAL dal = new DashboardDAL();
            AtividadeLog model = dal.AtividadeLog(objeto);

            return PartialView("~/Views/Dashboard/_Dashboard/_PainelAtividade.cshtml", model);
        }

        [HttpPost]
        public ActionResult RetornarPainelApontamento(long objeto)
        {
            DashboardDAL dal = new DashboardDAL();
            AtividadeLog model = dal.AtividadeLog(objeto);

            return PartialView("~/Views/Dashboard/_Dashboard/_PainelApontamentos.cshtml", model);
        }

        [HttpPost]
        public ActionResult RetornarPainelAnexos(long objeto)
        {
            DashboardDAL dal = new DashboardDAL();
            AtividadeLog model = dal.AtividadeLog(objeto);

            return PartialView("~/Views/Dashboard/_Dashboard/_PainelAnexos.cshtml", model);
        }

        [HttpPost]
        public ActionResult MinhasAtividades()
        {
            LoginViewModel login = (LoginViewModel)Session["Usuario"];

            dal = new DashboardDAL();
            informacoesUser infoUser = new informacoesUser();
            infoUser.atividadesUser = dal.MinhasAtividades(login.id);
            infoUser.atividadesdoGrupo = dal.AtividadesGrupo(login.id);
            List<AtividadeUser> model = infoUser.atividadesUser;

            return View("~/Views/Dashboard/_Dashboard/_ColaboradorPainelMinhasAtividades.cshtml", model);
        }

        public ActionResult HorasTrabalhadasMes(long id)
        {
            DashboardDAL dal = new DashboardDAL();

            HorasMes horas = dal.HorasTrabalhadasMes(id);

            return PartialView("~/Views/Dashboard/_Dashboard/_ColaboradorPainelMinhasHoras.cshtml", horas);
        }

        public JsonResult SetTempData(int objeto)
        {
            TempData["idRelatorio"] = objeto;

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDate()
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            return Json(date, JsonRequestBehavior.AllowGet);
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

        public JsonResult SalvarAtividade(LogarAtividadeViewModel objeto)
        {
            dal = new DashboardDAL();

            long atividadeId = Convert.ToInt64(objeto.atividadeId);

            var login = (LoginViewModel)Session["Usuario"];

            bool check = dal.InsertAtividade(atividadeId, objeto, login.id, login.login);

            if (check)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
