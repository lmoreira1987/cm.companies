using System;
using System.Collections.Generic;

namespace GServiceManagerMVC.ViewModels.Dashboard
{
    public class DownloadAnexoViewModel
    {
        public long anexoId { get; set; }
        public string nomeDescricao { get; set; }
        public string nomeInternoArquivo { get; set; }
        public string caminho { get; set; }
        public long servicoId { get; set; }
        public long ordemId { get; set; }
        public long parecerId { get; set; }
        public DateTime dataUpload { get; set; }
        public long atividadeId { get; set; }
    }
}