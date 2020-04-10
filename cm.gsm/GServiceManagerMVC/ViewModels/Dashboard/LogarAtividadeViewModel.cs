using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GServiceManagerMVC.ViewModels.Dashboard
{
    public class LogarAtividadeViewModel
    {
        public string atividadeId { get; set; }
        public List<Upload> uploads { get; set; }
    }

    public class Upload
    {
        public string descricao { get; set; }
        public string anexo { get; set; }
        public DateTime dtUpload { get; set; }
    }
}