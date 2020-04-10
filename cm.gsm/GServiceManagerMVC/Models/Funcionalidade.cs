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
    
    public partial class Funcionalidade
    {
        public Funcionalidade()
        {
            this.FuncionalidadeOrdems = new HashSet<FuncionalidadeOrdem>();
            this.FuncionalidadePerfils = new HashSet<FuncionalidadePerfil>();
            this.FuncionalidadeServicoes = new HashSet<FuncionalidadeServico>();
        }
    
        public long funcionalidadeId { get; set; }
        public long aplicativoId { get; set; }
        public int codigo { get; set; }
        public string nome { get; set; }
        public string uri { get; set; }
        public long usuarioIdInclusao { get; set; }
        public Nullable<long> usuarioIdAlteracao { get; set; }
        public System.DateTime dataInclusao { get; set; }
        public Nullable<System.DateTime> dataAlteracao { get; set; }
        public string status { get; set; }
    
        public virtual Aplicativo Aplicativo { get; set; }
        public virtual ICollection<FuncionalidadeOrdem> FuncionalidadeOrdems { get; set; }
        public virtual ICollection<FuncionalidadePerfil> FuncionalidadePerfils { get; set; }
        public virtual ICollection<FuncionalidadeServico> FuncionalidadeServicoes { get; set; }
    }
}