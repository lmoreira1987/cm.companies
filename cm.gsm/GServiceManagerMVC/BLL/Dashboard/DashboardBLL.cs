using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using GServiceManagerMVC.ViewModels.Dashboard;

namespace GServiceManagerMVC.BLL.Dashboard
{
    public class DashboardBLL
    {
        #region Public

        public int GetSemana(List<TempoViewModel> tempo)
        {
            int controler = 0;

            switch(DateTime.Now.DayOfWeek.ToString().ToLower())
            {
                case "tuesday":
                    controler = 1;
                    break;
                case "wednesday":
                    controler = 2;
                    break;
                case "thursday":
                    controler = 3;
                    break;
                case "friday":
                    controler = 4;
                    break;
                case "saturday":
                    controler = 5;
                    break;
                case "sunday":
                    controler = 6;
                    break;
            }

            DateTime data = DateTime.Now.Date.AddDays(-controler);

            return tempo.Where(x => x.dataOcorrencia.Date >= data.Date)
                    .Select(x => x.tempo).Sum();
        }

        public int GetMes(List<TempoViewModel> tempo)
        {
            return tempo.Select(x => x.tempo).Sum();
        }

        #endregion
    }
}