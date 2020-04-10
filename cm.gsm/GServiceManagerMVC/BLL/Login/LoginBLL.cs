using System.Text;

using GServiceManagerMVC.BLL.Global;
using GServiceManagerMVC.ViewModels.Global;

namespace GServiceManagerMVC.BLL.Login
{
    public class LoginBLL
    {
        #region Propriedades

        CriptografiaBLL criptografiaBLL;

        #endregion


        #region Public
        public string GetCorpoEmail(LoginViewModel login, string site)
        {
            StringBuilder build = new StringBuilder();
            criptografiaBLL = new CriptografiaBLL();

            build.Append("<b>Alteração de Senha Global Service Manager</b>");
            build.Append("<p style='font-family: Arial, Helvetica, sans-serif;'>");
            build.Append("Nome: " + login.nome + "<br />");
            build.Append("Login: " + login.login + "<br />");

            login.login = criptografiaBLL.Criptografar(login.login);

            build.Append("<a href=\'" + site + "/Login/AlterarSenha?url=" + login.login + "'>Clique aqui para cadastrar uma nova senha</a>");
            build.Append("</p>");
            build.Append("<br />");
            build.Append("<br />");
            build.Append("<center>Esta mensagem é gerada automáticamente pelo GSM.</center>");

            return build.ToString();
        }

        #endregion
    }
}