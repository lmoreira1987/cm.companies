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
    
    public partial class Empresa
    {
        public Empresa()
        {
            this.Areas = new HashSet<Area>();
            this.Clientes = new HashSet<Cliente>();
            this.GrupoUsuarios = new HashSet<GrupoUsuario>();
            this.Perfils = new HashSet<Perfil>();
            this.Usuarios = new HashSet<Usuario>();
        }
    
        public long empresaId { get; set; }
        public int codigo { get; set; }
        public string nome { get; set; }
        public long usuarioIdInclusao { get; set; }
        public Nullable<long> usuarioIdAlteracao { get; set; }
        public System.DateTime dataInclusao { get; set; }
        public Nullable<System.DateTime> dataAlteracao { get; set; }
        public string status { get; set; }
    
        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<GrupoUsuario> GrupoUsuarios { get; set; }
        public virtual ICollection<Perfil> Perfils { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}