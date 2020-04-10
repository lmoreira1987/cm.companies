using System.Collections.Generic;

namespace GServiceManagerMVC.ViewModels.Global
{
    public class LoginViewModel
    {
        public long id { get; set; }
        public IEnumerable<long> idPerfil { get; set; }
        public string nome { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }
    }
}