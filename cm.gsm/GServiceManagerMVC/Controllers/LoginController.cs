using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web;

using GServiceManagerMVC.Filters;
using GServiceManagerMVC.ViewModels.Global;
using GServiceManagerMVC.BLL.Global;
using GServiceManagerMVC.DAL.Login;
using GServiceManagerMVC.BLL.Login;


namespace GServiceManagerMVC.Controllers
{
    public class LoginController : Controller
    {
        #region Propriedades

        CriptografiaBLL criptografiaBLL;
        EmailBLL emailBLL;
        LoginDAL dal;
        LoginBLL bll;

        #endregion

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region Logar

        [HttpPost]
        public JsonResult Logar(string login, string senha)
        {
            LoginViewModel logar = new LoginViewModel();
            logar.login = login;
            logar.senha = senha;

            if (String.IsNullOrEmpty(logar.login) || String.IsNullOrEmpty(logar.senha)
                || String.IsNullOrWhiteSpace(logar.login) || String.IsNullOrWhiteSpace(logar.senha))
                return Json(false, JsonRequestBehavior.AllowGet);

            Session["Usuario"] = null;
            Session["Controllers"] = null;

            criptografiaBLL = new CriptografiaBLL();
            logar.senha = criptografiaBLL.Criptografar(senha);

            dal = new LoginDAL();
            logar = dal.SelectUsuario(logar);

            if (logar != null)
            {
                Response.Cookies["Usuario"]["avatar"] = logar.avatar;
                Response.Cookies["Usuario"]["nome"] = logar.nome;
                Response.Cookies["Usuario"]["login"] = logar.login;
                Response.Cookies["Usuario"].Expires = DateTime.Now.AddDays(1);

                Session["Usuario"] = logar;

                List<string> listaControllers = dal.SelectControllers(logar.idPerfil.FirstOrDefault());

                Session["Controllers"] = listaControllers;

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        #region WebApi Comentado
        //[HttpPost]
        //public JsonResult LogarWebAPi(LoginViewModel login)
        //{
        //    if (String.IsNullOrEmpty(login.nome) || String.IsNullOrEmpty(login.senha)
        //        || String.IsNullOrWhiteSpace(login.nome) || String.IsNullOrWhiteSpace(login.senha))
        //        return Json("false", JsonRequestBehavior.AllowGet);

        //    Session["Usuario"] = null;

        //    crip = new Criptografia.Criptografia();
        //    string nome = login.nome;

        //    login.nome = crip.Criptografar(login.nome);
        //    login.senha = crip.Criptografar(login.senha);

        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri("http://ssp01apld001/WebAPILogin/");
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    var response = client.PostAsJsonAsync("api/autenticar/", login).Result;
        //    string check = response.Content.ReadAsAsync<string>().Result;

        //    if (check == "true")
        //    {
        //        Session["Usuario"] = nome;

        //        return Json(check, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(check, JsonRequestBehavior.AllowGet);
        //    }
        //}
        #endregion

        #endregion

        #region Desbloquear
        public ActionResult Desbloquear()
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

            Session["Usuario"] = null;

            var teste = Session["Usuario"];
            
            return View(login);
        }
        #endregion

        #region Esqueceu Senha

        [HttpPost]
        public JsonResult EsqueceuSenha(string objeto)
        {
            dal = new LoginDAL();
            bll = new LoginBLL();
            emailBLL = new EmailBLL();

            LoginViewModel login = dal.SelectUsuarioPorLogin(objeto);

            if (login == null)
                return Json(false, JsonRequestBehavior.AllowGet);

            string site = Request.Url.GetLeftPart(UriPartial.Authority);
            site += Request.Url.AbsolutePath.Contains("GSM_MVC") ? "/GSM_MVC" : "";

            string corpo = bll.GetCorpoEmail(login, site);

            bool result = emailBLL.EnviarEmail(login.email, "Cadastro Portal Gestão Service Manager", corpo);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AlterarSenha(string url)
        {
            dal = new LoginDAL();
            criptografiaBLL = new CriptografiaBLL();

            LoginViewModel login = new LoginViewModel();
            login.login = criptografiaBLL.Descriptografar(url);
            login = dal.SelectUsuarioPorLogin(login.login);

            return View(login);
        }

        public JsonResult SalvarNovaSenha(List<string> objeto)
        {
            dal = new LoginDAL();
            criptografiaBLL = new CriptografiaBLL();

            string mensagem = string.Empty;

            if (!String.IsNullOrEmpty(objeto[0]) && !String.IsNullOrEmpty(objeto[1]))
            {
                if (objeto[0] == objeto[1])
                {
                    if (dal.UpdateSenhaUsuario(objeto[3], criptografiaBLL.Criptografar(objeto[1])))
                    {
                        mensagem = "ok";

                        return Json(mensagem, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        mensagem = "Não foi possível alterar sua senha, por favor entrar em contato com o administrador do sistema.";

                        return Json(mensagem, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    mensagem = "Os campos Nova Senha não conferem!";

                    return Json(mensagem, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                mensagem = "Os campos Nova Senha são obrigatórios!";

                return Json(mensagem, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}
