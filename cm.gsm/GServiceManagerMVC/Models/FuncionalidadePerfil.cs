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
    
    public partial class FuncionalidadePerfil
    {
        public long funcionalidadeId { get; set; }
        public long perfilId { get; set; }
        public long usuarioIdInclusao { get; set; }
        public Nullable<long> usuarioIdAlteracao { get; set; }
        public System.DateTime dataInclusao { get; set; }
        public Nullable<System.DateTime> dataAlteracao { get; set; }
        public string status { get; set; }
    
        public virtual Funcionalidade Funcionalidade { get; set; }
    }
}
