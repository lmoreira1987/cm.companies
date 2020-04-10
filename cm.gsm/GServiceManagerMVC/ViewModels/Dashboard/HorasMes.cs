using System;
using System.Collections.Generic;

namespace GServiceManagerMVC.ViewModels.Dashboard
{
    public class HorasMes
    {
        public List<int> diasMes { get; set; }
        public IEnumerable<string> nomesProjetosMes { get; set; }
        public List<ProjetoMes> projetosMes { get; set; }
    }

    public class ProjetoMes
    {
        public string projeto { get; set; }
        public List<int> tempoEfetivoConsumido { get; set; }
    }
}