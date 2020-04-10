using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GServiceManagerMVC.ViewModels.Fabrica
{
    public class AnexoViewModel
    {
        public long id { get; set; }
        public string descricao { get; set; }
        public string arquivo { get; set; }
        public DateTime data { get; set; }
        public bool editar { get; set; }
    }
}