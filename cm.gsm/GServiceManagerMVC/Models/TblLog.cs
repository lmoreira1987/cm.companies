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
    
    public partial class TblLog
    {
        public int IdLog { get; set; }
        public string dsStackTrace { get; set; }
        public string dsErrorMessage { get; set; }
        public string dsInnerException { get; set; }
        public string dsResponsibleMethod { get; set; }
        public string dsExceptionType { get; set; }
        public string dsMachineName { get; set; }
        public System.DateTime dtOccured { get; set; }
        public string dsUserHostAddress { get; set; }
        public string dsBrowser { get; set; }
        public string dsUrlWhereErrorOccured { get; set; }
        public string dsRefereer { get; set; }
        public string dsSession { get; set; }
        public string dsQueryString { get; set; }
        public string dsForm { get; set; }
        public string dsCookie { get; set; }
        public string dsApplication { get; set; }
        public string dsServerVariables { get; set; }
    }
}
