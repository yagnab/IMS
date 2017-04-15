using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace IMS.BL.DataModel
{
    public class DefaultPlotModel : PlotModelCreator
    {
        #region Constructor
        public DefaultPlotModel(Dictionary<DateTime, int> _QuantityData = null, Dictionary<DateTime, decimal> _RevenueData = null)
        {
            this.Title = "";
            AddLegend();
            AddAxes();

            var qSampleData = GetQuantitySampleData();
            var rSampleData = GetRevenueSampleData();

            CreateSeries(qSampleData, rSampleData);
        }
        #endregion

        #region Methods
        Dictionary<DateTime, int> GetQuantitySampleData()
        {
            var sampleDate = new Dictionary<DateTime, int>();
            var now = DateTime.Now;

            sampleDate.Add(now, 0);
            sampleDate.Add(now.AddMinutes(1), 1);
            sampleDate.Add(now.AddMinutes(2), 2);
            sampleDate.Add(now.AddMinutes(3), 3);
            sampleDate.Add(now.AddMinutes(4), 4);
            return sampleDate;
        }

        Dictionary<DateTime, decimal> GetRevenueSampleData()
        {
            var sampleDate = new Dictionary<DateTime, decimal>();
            var now = DateTime.Now;

            sampleDate.Add(now, 0m);
            sampleDate.Add(now.AddMinutes(1), 2m);
            sampleDate.Add(now.AddMinutes(2), 3m);
            sampleDate.Add(now.AddMinutes(3), 4m);
            sampleDate.Add(now.AddMinutes(4), 5m);
            return sampleDate;
        }
        void AddAxes()
        {
            var keyAxis = new DateTimeAxis()
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
                Maximum = 6,
                Title = "Value"
            };

            this.Axes.Add(keyAxis);
            this.Axes.Add(valueAxis);

        }
        #endregion
    }
}
