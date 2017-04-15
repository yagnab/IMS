using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace IMS.BL
{
    public abstract class PlotModelCreator : PlotModel
    {
        //for DefaultPlotModel class
        public PlotModelCreator()
        {
            AddLegend();
        }
        public PlotModelCreator(Dictionary<DateTime, int> _QuantityData, Dictionary<DateTime, decimal> _RevenueData)
        {
            AddLegend();
            CreateSeries(_QuantityData, _RevenueData);
        }
        #region Methods for child classes
        protected void AddLegend()
        {
            this.LegendTitle = "Key";
            this.LegendOrientation = LegendOrientation.Horizontal;
            this.LegendPlacement = LegendPlacement.Outside;
            this.LegendPosition = LegendPosition.TopRight;
            this.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            this.LegendBorder = OxyColors.Black;
        }

        protected void CreateSeries(Dictionary<DateTime, int> _QuantityData, Dictionary<DateTime, decimal> _RevenueData)
        {
            var quantitySeries = new LineSeries()
            {
                Color = OxyColor.FromRgb(153, 255, 0),
                Title = "Quantity Sold"
                
            };
            var revenueSeries = new LineSeries()
            {
                Color = OxyColor.FromRgb(25, 0, 255),
                Title = "Revenue"
            };

            //add data to series'
            foreach (KeyValuePair<DateTime, int> dp in _QuantityData)
            {
                quantitySeries.Points.Add( new DataPoint(DateTimeAxis.ToDouble(dp.Key), (double)dp.Value ) );
            }
            
            foreach (KeyValuePair<DateTime, decimal> dp in _RevenueData)
            {
                revenueSeries.Points.Add( new DataPoint(DateTimeAxis.ToDouble(dp.Key), (double)dp.Value) );
            }

            //add series to PlotModel
            this.Series.Add(quantitySeries);
            this.Series.Add(revenueSeries);
            
        }
        
        protected double GetMaxValue(Dictionary<DateTime, int> _QuantityData, Dictionary<DateTime, decimal> _RevenueData)
        {
            int quantityMax = _QuantityData.Values.Max();
            decimal revenueMax = _RevenueData.Values.Max();
            return revenueMax > quantityMax ? (double)revenueMax : (double)quantityMax;
        }
        #endregion
    }
}
