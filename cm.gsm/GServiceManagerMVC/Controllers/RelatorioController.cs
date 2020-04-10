using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using System;

using GServiceManagerMVC.DAL.Relatorio;
using GServiceManagerMVC.ViewModels.Global;

namespace GServiceManagerMVC.Controllers
{
    public class RelatorioController : Controller
    {
        #region Propriedades

        RelatorioDAL dal;

        #endregion

        #region Public

        public ActionResult Index()
        {
            LoginViewModel login = (LoginViewModel)Session["Usuario"];

            if (login == null)
                return RedirectToAction("Desbloquear", "Login");

            if (TempData["idRelatorio"] == null)
                return RedirectToAction("Index", "Dashboard");

            int id = (int)TempData["idRelatorio"];
            long IdPerfil = login.idPerfil.FirstOrDefault();

            dal = new RelatorioDAL();

            #region Colaborador

            if (IdPerfil == 4)
            {
                if (id == 1)
                {
                    var objeto = dal.SelectRelatorioSemanaColaborador(login.id);

                    Tuple<long, int, dynamic> model = new Tuple<long, int, dynamic>(login.idPerfil.FirstOrDefault(), id, objeto);

                    return View(model);
                }
                else if (id == 2)
                {
                    var objeto = dal.SelectRelatorioMesColaborador(login.id);

                    Tuple<long, int, dynamic> model = new Tuple<long, int, dynamic>(login.idPerfil.FirstOrDefault(), id, objeto);

                    return View(model);
                }
                else if (id == 3)
                {
                    var objeto = dal.SelectRelatorioAtividadeColaborador(login.id);

                    Tuple<long, int, dynamic> model = new Tuple<long, int, dynamic>(login.idPerfil.FirstOrDefault(), id, objeto);

                    return View(model);
                }
                else
                {
                    var objeto = dal.SelectRelatorioAtividadeGrupoColaborador(login.id);

                    Tuple<long, int, dynamic> model = new Tuple<long, int, dynamic>(login.idPerfil.FirstOrDefault(), id, objeto);

                    return View(model);
                }
            }

            #endregion

            Tuple<long, int, dynamic> erro = new Tuple<long, int, dynamic>(login.idPerfil.FirstOrDefault(), id, "");

            return View(erro);
        }

        #region Colaborador

        public ActionResult RelatorioSemanaColaborador()
        {
            LoginViewModel login = (LoginViewModel)Session["Usuario"];
            dal = new RelatorioDAL();

            return PartialView("~/Views/Relatorio/_RelatorioSemanaColaborador.cshtml", dal.SelectRelatorioSemanaColaborador(login.id));
        }

        public ActionResult RelatorioMesColaborador()
        {
            LoginViewModel login = (LoginViewModel)Session["Usuario"];
            dal = new RelatorioDAL();

            return PartialView("~/Views/Relatorio/_RelatorioMesColaborador.cshtml", dal.SelectRelatorioMesColaborador(login.id));
        }

        public ActionResult RelatorioAtividadesColaborador()
        {
            LoginViewModel login = (LoginViewModel)Session["Usuario"];
            dal = new RelatorioDAL();

            return PartialView("~/Views/Relatorio/_RelatorioAtividadesColaborador.cshtml", dal.SelectRelatorioAtividadeColaborador(login.id));
        }

        public ActionResult RelatorioAtividadesGrupoColaborador()
        {
            LoginViewModel login = (LoginViewModel)Session["Usuario"];
            dal = new RelatorioDAL();

            return PartialView("~/Views/Relatorio/_RelatorioAtividadesGrupoColaborador.cshtml", dal.SelectRelatorioAtividadeGrupoColaborador(login.id));
        }

        #endregion

        #endregion

    }
}
