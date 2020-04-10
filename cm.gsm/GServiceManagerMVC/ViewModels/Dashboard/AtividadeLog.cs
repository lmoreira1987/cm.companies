using System;
using System.Collections.Generic;

namespace GServiceManagerMVC.ViewModels.Dashboard
{
    public class SalvarAtividadeLog
    {
        public long     atividadeId             {get;set;}
        public int      tempoEfetivoConsumido   {get;set;}
        public string   apontamento             {get;set;}
        public string   status                  {get;set;}
    }
}