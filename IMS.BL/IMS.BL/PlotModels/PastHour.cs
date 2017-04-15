using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.Annotations;

namespace IMS.BL
{
    public class PastHour : PlotModelCreator
    {
        #region Constructors
        public PastHour(Dictionary<DateTime, int> _QuantityData, Dictionary<DateTime, decimal> _RevenueData) : base(_QuantityData, _RevenueData)
        {
            this.Title = "Past Hour";

            //pick max value from both set of dps
            AddAxes(GetMaxValue(_QuantityData, _RevenueData));
        }
        #endregion

        #region Methods
        void AddAxes(double maxValue)
        {
            var timeAxis = new DateTimeAxis()
            {
                Position = AxisPosition.Bottom,
                Title = "Time",
                StringFormat = "HH:mm",
            };

            var valueAxis = new LinearAxis()
            {
                Position = AxisPosition.Left,
                StartPosition = 0,
                Minimum = 0,
                //allows room at top of graph
                Maximum = maxValue + 1,
                Title = "Value"
            };

            this.Axes.Add(timeAxis);
            this.Axes.Add(valueAxis);
            
        }
        #endregion 
    }
}
