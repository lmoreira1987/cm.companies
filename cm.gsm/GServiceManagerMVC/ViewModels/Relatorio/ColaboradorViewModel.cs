using System;

namespace GServiceManagerMVC.ViewModels.Relatorio
{
    public class ColaboradorViewModel
    {
        public string usuario { get; set; }
        public long ordem { get; set; }
        public long servico { get; set; }
        public string atividade { get; set; }
        public string projeto { get; set; }
        public string tipoAtividade { get; set; }
        public string statusAtividade { get; set; }
        public string statusOS { get; set; }
        public DateTime dataOcorrencia { get; set; }
        public int tempoEfetivo { get; set; }
        public int prazoEstimado { get; set; }
        public string apontamento { get; set; }
        public Nullable<DateTime> dataEstimativaIni { get; set; }
        public Nullable<DateTime> dataEstimativaFim { get; set; }
        public Nullable<DateTime> dataEfetivaIni { get; set; }
        public Nullable<DateTime> dataEfetivaFim { get; set; }
    }
}