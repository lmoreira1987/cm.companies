using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GServiceManagerMVC.ViewModels.Fabrica
{
    public class SalvarAtividadeViewModel
    {
        public string id { get; set; }
        public int tipoAti { get; set; }
        public string atividades { get; set; }
        public string descricao { get; set; }
        public string datas { get; set; }
        public DateTime dataIni { get; set; }
        public DateTime dataFim { get; set; }
        public int prazo { get; set; }
        public int recurso { get; set; }
        public long recursoTec { get; set; }
        public List<Upload> uploads { get; set; }
        public List<Download> downloads { get; set; }
    }

    public class Upload
    {
        public string descricao { get; set; }
        public string anexo { get; set; }
        public DateTime dtUpload { get; set; }
    }

    public class Download
    {
        public long id { get; set; }
        public string descricao { get; set; }
        public string anexo { get; set; }
        public DateTime dtUpload { get; set; }
    }
}