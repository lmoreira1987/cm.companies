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
    
    public partial class TipoOrdemServico
    {
        public TipoOrdemServico()
        {
            this.Ordems = new HashSet<Ordem>();
            this.TipoOrdemServicoPerguntas = new HashSet<TipoOrdemServicoPergunta>();
        }
    
        public long tipoOrdemServicoId { get; set; }
        public int codigo { get; set; }
        public string nome { get; set; }
        public bool demandaServico { get; set; }
        public long usuarioIdInclusao { get; set; }
        public Nullable<long> usuarioIdAlteracao { get; set; }
        public System.DateTime dataInclusao { get; set; }
        public Nullable<System.DateTime> dataAlteracao { get; set; }
        public string status { get; set; }
    
        public virtual ICollection<Ordem> Ordems { get; set; }
        public virtual ICollection<TipoOrdemServicoPergunta> TipoOrdemServicoPerguntas { get; set; }
    }
}
