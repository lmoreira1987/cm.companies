using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GServiceManagerMVC.ViewModels.Fabrica
{
    public class PesquisaAtividadeViewModel
    {
        public int projeto { get; set; }
        public Nullable<DateTime> de { get; set; }
        public Nullable<DateTime> ate { get; set; }
        public string nome { get; set; }
        public long os { get; set; }
        public int pagina { get; set; }
        public int intervalo { get; set; }
    }
}