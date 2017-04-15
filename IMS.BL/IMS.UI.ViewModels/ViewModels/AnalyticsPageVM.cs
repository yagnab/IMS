using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;

namespace IMS.UI.ViewModels
{
    public class AnalyticsPageVM
    {
        public AnalyticsPageGraphsVM graphsVM { get; set; }

        public AnalyticsPageVM(AnalyticsPageGraphsVM _graphsVM)
        {
            graphsVM = _graphsVM;
        }
    }
}
