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
    
    public partial class ConfiguradorOrdemServico
    {
        public ConfiguradorOrdemServico()
        {
            this.ConfiguradorObjetoes = new HashSet<ConfiguradorObjeto>();
        }
    
        public long configuradorOrdemServicoId { get; set; }
        public Nullable<long> ordemId { get; set; }
        public long projetoId { get; set; }
        public Nullable<long> tipoOrdemServicoId { get; set; }
        public long faseId { get; set; }
        public long statusId { get; set; }
        public long usuario { get; set; }
        public Nullable<long> usuarioProximo { get; set; }
        public System.DateTime vigenciaInicio { get; set; }
        public Nullable<System.DateTime> vigenciaFim { get; set; }
        public Nullable<int> ordemExecucao { get; set; }
        public long usuarioIdInclusao { get; set; }
        public Nullable<long> usuarioIdAlteracao { get; set; }
        public System.DateTime dataInclusao { get; set; }
        public Nullable<System.DateTime> dataAlteracao { get; set; }
        public string status { get; set; }
    
        public virtual ICollection<ConfiguradorObjeto> ConfiguradorObjetoes { get; set; }
        public virtual Fase Fase { get; set; }
        public virtual Ordem Ordem { get; set; }
        public virtual Projeto Projeto { get; set; }
        public virtual Status Status1 { get; set; }
        public virtual Usuario Usuario1 { get; set; }
        public virtual Usuario Usuario2 { get; set; }
    }
}
