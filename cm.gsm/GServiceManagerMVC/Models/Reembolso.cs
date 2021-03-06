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
    
    public partial class Reembolso
    {
        public long ReembolsoId { get; set; }
        public long DespesaId { get; set; }
        public long ProjetoId { get; set; }
        public long UsuarioId { get; set; }
        public System.DateTime dtReembolso { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public System.DateTime DataCriacao { get; set; }
        public Nullable<short> Status { get; set; }
    
        public virtual Despesa Despesa { get; set; }
        public virtual Projeto Projeto { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
