using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GServiceManagerMVC.ViewModels.Fabrica
{
    public class OSDescricaoViewModel
    {
        public long id { get; set; }
        public string os { get; set; }
        public string assunto { get; set; }
        public Nullable<DateTime> dataEstimativaIni { get; set; }
        public Nullable<DateTime> dataEstimativaFim { get; set; }
        public string tipo { get; set; }
        public string projeto { get; set; }
    }
}