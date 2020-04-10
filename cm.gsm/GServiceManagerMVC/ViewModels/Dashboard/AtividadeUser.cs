using System;
using System.Collections.Generic;

namespace GServiceManagerMVC.ViewModels.Dashboard
{
    public class AtividadeUser
    {
        public long grupoId { get; set; }
        public string grupoNome { get; set; }
        public long atividadeId { get; set; }
        public string atividadeNome { get; set; }
        public long statusAtividadeId { get; set; }
        public string statusNome { get; set; }
        public long projetoId { get; set; }
        public string projetoNome { get; set; }
        public long ordemId { get; set; }
        public long servicoId { get; set; }
        public DateTime? dataEstimadaIni { get; set; }
        public DateTime? dataEstimadaFim { get; set; }
        public int quantidadeHoras { get; set; }        
        }
}