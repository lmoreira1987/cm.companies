using System;

namespace GServiceManagerMVC.ViewModels.Global
{
    public class Anexo
    {
        public int          sequenciaAnexo		{ get; set; }
        public string		nome			    { get; set; }
        public string		nulableNomeInterno		{ get; set; }
        public DateTime?	dataUpload			{ get; set; }
        public DateTime		dataInclusao		{ get; set; }
        public DateTime?	dataAlteracao		{ get; set; }
        public string		nulableCaminho			{ get; set; }
        public long?		servicoId			{ get; set; }
        public long?		ordemId			{ get; set; }
        public long?		parecerId			{ get; set; }
        public long			usuarioIdInclusao	{ get; set; }
        public long?		usuarioIdAlteracao	{ get; set; }
        public char			status				{ get; set; }
        public long?		atividadeId		{ get; set; }
        public long?		logAtividadeId		{ get; set; }
    }
}