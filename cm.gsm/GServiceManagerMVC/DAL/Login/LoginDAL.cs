using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

using GServiceManagerMVC.ViewModels.Global;
using GServiceManagerMVC.Models;

namespace GServiceManagerMVC.DAL.Login
{
    public class LoginDAL
    {
        #region Public

        public LoginViewModel SelectUsuario(LoginViewModel login)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                return (from usu in banco.Usuarios
                        where usu.login == login.login
                        && usu.senha == login.senha
                        select new LoginViewModel
                        {
                            id = usu.usuarioId,
                            idPerfil = usu.UsuarioPerfils.Select(x => x.perfilId),
                            login = usu.login,
                            nome = usu.nome,
                            avatar = usu.foto
                        }).FirstOrDefault();
            }
        }

        public List<string> SelectControllers(long idPerfil)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                return (from men in banco.Menus
                        join per in banco.PerfilAcessoes
                        on men.IdMenu equals per.IdMenu
                        where per.perfilId == idPerfil
                        select men.Menu1.ToLower()).ToList();
            }
        }

        public LoginViewModel SelectUsuarioPorLogin(string login)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                return (from usu in banco.Usuarios
                        where usu.login == login
                        select new LoginViewModel
                        {
                            login = usu.login,
                            nome = usu.nome,
                            email = usu.email,
                            senha = usu.senha
                        }).FirstOrDefault();
            }
        }

        public bool UpdateSenhaUsuario(string senha, string novoSenha)
        {
            using(TransactionScope scope = new TransactionScope())
            {
                using(GSMEntities banco = new GSMEntities())
                {
                    try
                    {
                        Usuario usuario = banco.Usuarios.Where(x => x.senha == senha).FirstOrDefault();
                        usuario.senha = novoSenha;

                        banco.SaveChanges();

                        scope.Complete();

                        return true;
                    }
                    catch(Exception)
                    {
                        return false;
                    }
                }
            }
        }

        #endregion
    }
}