using System;
using System.Collections.Generic;

namespace GServiceManagerMVC.ViewModels.Dashboard
{
    public class AtividadeLog
    {
        public AtividadeLog()
        {
            atividadeId = 0;
            atividadeNome = "";
            atividadeDescricao = "";
            tipoAtividadeNome = "";
            statusAtividadeNome = "";
            Apontamentos = new List<Apontamento>();
            Anexos = new List<Anexos>();
        }

        public long         atividadeId { get; set; }
        public string       atividadeNome { get; set; }
        public string       atividadeDescricao { get; set; }
        public DateTime?    atividadeDataEstimadaInicio { get; set; }
        public DateTime?    atividadeDataEstimadaFim { get; set; }
        public string       tipoAtividadeNome { get; set; }
        public long?        atividadePrazoEstimado { get; set; }
        public string       statusAtividadeNome { get; set; }
        public List<Apontamento> Apontamentos { get; set; }
        public List<Anexos> Anexos { get; set; }
    }

    public class Apontamento
    {
        public string usuarioLogin { get; set; }
        public DateTime dataInclusão { get; set; }
        public string apontamento { get; set; }
    }

    public class Anexos
    {
        public long sequenciaAnexo { get; set; }
		public string nome { get; set; }
		public string nomeInterno { get; set; }
		public DateTime? dataUpload { get; set; }
		public string caminho { get; set; }
		public long? servicoId { get; set; }
		public long? ordemId { get; set; }
		public long? parecerId { get; set; }
		public long usuarioIdInclusao { get; set; }
		public long? usuarioIdAlteracao { get; set; }
		public DateTime dataInclusao { get; set; }
		public DateTime? dataAlteracao { get; set; }
		public string status { get; set; }
		public long? atividadeId { get; set; }
		public long? logAtividadeId { get; set; }
    }
}