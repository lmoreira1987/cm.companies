using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GServiceManagerMVC.ViewModels.Menu
{
    public class MenuViewModel
    {
        public int id { get; set; }
        public int idPai { get; set; }
        public string menu { get; set; }
        public string icone { get; set; }
        public string url { get; set; }
    }
}