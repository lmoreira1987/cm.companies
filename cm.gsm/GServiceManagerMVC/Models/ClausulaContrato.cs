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
    
    public partial class ClausulaContrato
    {
        public ClausulaContrato()
        {
            this.ClausulaContrato1 = new HashSet<ClausulaContrato>();
        }
    
        public long clausulaContratoId { get; set; }
        public string identificador { get; set; }
        public string nome { get; set; }
        public Nullable<long> clausulaRelacionada { get; set; }
        public Nullable<decimal> valorAplicavel { get; set; }
        public string observacao { get; set; }
        public Nullable<long> aditivoContratoId { get; set; }
        public Nullable<long> contratoId { get; set; }
        public long usuarioIdInclusao { get; set; }
        public Nullable<long> usuarioIdAlteracao { get; set; }
        public System.DateTime dataInclusao { get; set; }
        public Nullable<System.DateTime> dataAlteracao { get; set; }
        public string status { get; set; }
    
        public virtual AditivoContrato AditivoContrato { get; set; }
        public virtual ICollection<ClausulaContrato> ClausulaContrato1 { get; set; }
        public virtual ClausulaContrato ClausulaContrato2 { get; set; }
        public virtual Contrato Contrato { get; set; }
    }
}