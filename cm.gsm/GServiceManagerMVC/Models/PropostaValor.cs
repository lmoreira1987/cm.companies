//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GServiceManagerMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PropostaValor
    {
        public PropostaValor()
        {
            this.RecursoOrcadoes = new HashSet<RecursoOrcado>();
        }
    
        public long propostaValorId { get; set; }
        public int sequenciaPropostaValor { get; set; }
        public Nullable<long> tipoServicoId { get; set; }
        public Nullable<int> tempoTotal { get; set; }
        public Nullable<decimal> valorTotal { get; set; }
        public long ordemId { get; set; }
        public long usuarioIdInclusao { get; set; }
        public Nullable<long> usuarioIdAlteracao { get; set; }
        public System.DateTime dataInclusao { get; set; }
        public Nullable<System.DateTime> dataAlteracao { get; set; }
        public string status { get; set; }
    
        public virtual Ordem Ordem { get; set; }
        public virtual TipoServico TipoServico { get; set; }
        public virtual ICollection<RecursoOrcado> RecursoOrcadoes { get; set; }
    }
}