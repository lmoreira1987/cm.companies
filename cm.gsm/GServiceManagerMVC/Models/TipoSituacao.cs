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
    
    public partial class TipoSituacao
    {
        public TipoSituacao()
        {
            this.Contratoes = new HashSet<Contrato>();
            this.Projetoes = new HashSet<Projeto>();
        }
    
        public long tipoSituacaoId { get; set; }
        public int codigo { get; set; }
        public string nome { get; set; }
        public long usuarioIdInclusao { get; set; }
        public Nullable<long> usuarioIdAlteracao { get; set; }
        public System.DateTime dataInclusao { get; set; }
        public Nullable<System.DateTime> dataAlteracao { get; set; }
        public string status { get; set; }
    
        public virtual ICollection<Contrato> Contratoes { get; set; }
        public virtual ICollection<Projeto> Projetoes { get; set; }
    }
}
