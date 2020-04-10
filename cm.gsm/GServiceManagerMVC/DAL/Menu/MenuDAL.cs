using System.Collections.Generic;
using System.Linq;

using GServiceManagerMVC.ViewModels.Menu;
using GServiceManagerMVC.Models;

namespace GServiceManagerMVC.DAL.Menu
{
    public class MenuDAL
    {
        #region Public

        public List<MenuViewModel> SelectMenu(long idPerfil)
        {
            using (GSMEntities banco = new GSMEntities())
            {
                List<MenuViewModel> menu = (from men in banco.Menus
                                            join per in banco.PerfilAcessoes
                                            on men.IdMenu equals per.IdMenu
                                            where per.perfilId == idPerfil && men.IdMenuPai != null
                                            orderby men.Ordem
                                            select new MenuViewModel
                                            {
                                                id = men.IdMenu,
                                                idPai = men.IdMenuPai.Value,
                                                menu = men.Menu1,
                                                icone = men.Imagem,
                                                url = men.Url
                                            }).ToList();

                return menu;
            }
        }

        #endregion
    }
}