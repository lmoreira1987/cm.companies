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
    
    public partial class PrioridadeOrdemlog
    {
        public long PrioridadeOrdemlogId { get; set; }
        public Nullable<long> ordemId { get; set; }
        public Nullable<int> prioridade { get; set; }
        public Nullable<long> usuarioIdInclusao { get; set; }
        public Nullable<long> usuarioIdAlteracao { get; set; }
        public Nullable<System.DateTime> dataInclusao { get; set; }
        public Nullable<System.DateTime> dataAlteracao { get; set; }
        public string status { get; set; }
    }
}
