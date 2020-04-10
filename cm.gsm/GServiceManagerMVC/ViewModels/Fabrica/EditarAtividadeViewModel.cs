using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GServiceManagerMVC.ViewModels.Fabrica
{
    public class EditarAtividadeViewModel
    {
        public string id { get; set; }
        public long idAti { get; set; }
        public long tipoAti { get; set; }
        public string atividade { get; set; }
        public string descricao { get; set; }
        public string datas { get; set; }
        public Nullable<DateTime> dataIni { get; set; }
        public Nullable<DateTime> dataFim { get; set; }
        public int prazo { get; set; }
        public int recurso { get; set; }
        public Nullable<long> recursoTec { get; set; }
        public List<Upload> uploads { get; set; }
        public List<Download> downloads { get; set; }
        public OSDescricaoViewModel osDescricao { get; set; }
        public List<long> deletar { get; set; }
    }
}