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
    
    public partial class LogAtividade
    {
        public LogAtividade()
        {
            this.Checklists = new HashSet<Checklist>();
        }
    
        public long logAtividadeId { get; set; }
        public int sequenciaLogAtividade { get; set; }
        public System.DateTime dataOcorrencia { get; set; }
        public Nullable<int> tempoEfetivoConsumido { get; set; }
        public string apontamento { get; set; }
        public Nullable<long> recurso { get; set; }
        public Nullable<long> atividadeId { get; set; }
        public long usuarioIdInclusao { get; set; }
        public Nullable<long> usuarioIdAlteracao { get; set; }
        public System.DateTime dataInclusao { get; set; }
        public Nullable<System.DateTime> dataAlteracao { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> DataInicio { get; set; }
        public Nullable<System.DateTime> DataFim { get; set; }
        public Nullable<bool> Retrabalho { get; set; }
    
        public virtual Atividade Atividade { get; set; }
        public virtual ICollection<Checklist> Checklists { get; set; }
        public virtual Recurso Recurso1 { get; set; }
    }
}
