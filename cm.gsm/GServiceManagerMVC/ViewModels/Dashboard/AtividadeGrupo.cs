using System;
using System.Collections.Generic;

namespace GServiceManagerMVC.ViewModels.Dashboard
{
    public class AtividadeGrupo
    {
        public long atividadeId { get; set; }
        public string nomeProjeto { get; set; }
        public string nomeAtividade { get; set; }
        public string dataEstimadaInicio { get; set; }
        public string dataEstimadaFim { get; set; }
        public string tipoAtividadeNome { get; set; }
        public long? atividadePrazoEstimado { get; set; }
        public string atividadeDescricao { get; set; }
    }
}