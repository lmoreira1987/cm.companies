using System.Web.Mvc;
using System.Linq;
using System;

using GServiceManagerMVC.Filters;
using GServiceManagerMVC.ViewModels.Menu;
using GServiceManagerMVC.ViewModels.Global;
using GServiceManagerMVC.DAL.Menu;
using System.Collections.Generic;
using GServiceManagerMVC.BLL.Global;

namespace GServiceManagerMVC.Controllers
{
    public class MenuController : Controller
    {
        #region Propriedades

        MenuDAL dal;

        #endregion

        #region Menu
        public ActionResult _Menu()
        {
            LoginViewModel login = (LoginViewModel)Session["Usuario"];
            dal = new MenuDAL();

            if (login == null)
                return RedirectToAction("Desbloquear", "Login");

            long idPerfil = login.idPerfil.FirstOrDefault();

            List<MenuViewModel> menu = dal.SelectMenu(idPerfil);

            #region ViewBag

            if (string.IsNullOrEmpty(login.avatar))
                login.avatar = "Sombra.jpg";

            ViewBag.user = login;

            #endregion

            return PartialView("~/Views/Shared/_Menu.cshtml", menu);
        }

        public FileContentResult ManualOperacaoGSMPDF()
        {
            Bytes bytes = new Bytes();

            byte[] doc = bytes.GetBytesFromFile(AppDomain.CurrentDomain.BaseDirectory + "PDF/" + "ManualOperacaoGSM.pdf");

            string mimeType = "application/pdf";

            Response.AppendHeader("Content-Disposition", "inline; filename=ManualOperacaoGSM.pdf");

            return File(doc, mimeType);
        }

        public ActionResult Redirecionar()
        {
            Session["Usuario"] = null;

            return RedirectToAction("Index", "Login");
        }

        #endregion
    }
}