using System;
using System.Collections.Generic;

namespace GServiceManagerMVC.ViewModels.Dashboard
{
    public class informacoesUser
    {
        public HorasMes horasMes { get; set; }
        public List<AtividadeUser> atividadesUser { get; set; }
        public List<AtividadeGrupo> atividadesdoGrupo { get; set; }
    }
}