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
    
    public partial class ProjetoAdministrativo
    {
        public long projetoAdministrativoId { get; set; }
        public long projetoId { get; set; }
        public long usuarioId { get; set; }
    
        public virtual Projeto Projeto { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}