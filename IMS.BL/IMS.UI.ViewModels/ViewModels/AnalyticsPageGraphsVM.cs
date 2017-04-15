using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using IMS.BL;
using IMS.BL.DataModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using OxyPlot.Series;
using OxyPlot.Axes;
using IMS.UI;
using System.Windows;


namespace IMS.UI.ViewModels
{
    public class AnalyticsPageGraphsVM : INotifyPropertyChanged
    {
        #region Properties
        private PlotModel _plotModel;
        public PlotModel plotModel
        {
            get
            {
                return _plotModel;
            }
            set
            {
                _plotModel = value;
                OnPropertyChanged();
            }
        }
        
        private List<Item> _items;
        public List<Item> items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructor
        public AnalyticsPageGraphsVM()
        {
            using (var dbContext = new InventoryContext())
            {
                //all items in db
                items = (from i in dbContext.Items
                        select i).ToList();

                plotModel = new DefaultPlotModel();
            }
            
        }

        //will tell view that a property has changed
        //may need to rerender a control
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
        #endregion

        #region Methods
        public void SetPlotModel(GraphTimePeriod timePeriod)
        {

            Dictionary<DateTime, int> _QuantityData;
            Dictionary<DateTime, decimal> _RevenueData;
            
            switch (timePeriod)
            {
                case GraphTimePeriod.PastHour:
                    _QuantityData = UIUtility.GetQuantityData(timePeriod);
                    _RevenueData = UIUtility.GetRevenueData(timePeriod);
                    plotModel = new PastHour( _QuantityData,  _RevenueData);
                    break;
                case GraphTimePeriod.PastDay:
                    _QuantityData = UIUtility.GetQuantityData(timePeriod);
                    _RevenueData = UIUtility.GetRevenueData(timePeriod);
                    plotModel = new PastDay(_QuantityData, _RevenueData);
                    break;
                case GraphTimePeriod.PastWeek:
                    _QuantityData = UIUtility.GetQuantityData(timePeriod);
                    _RevenueData = UIUtility.GetRevenueData(timePeriod);
                    plotModel = new PastWeek(_QuantityData, _RevenueData);
                    break;
                case GraphTimePeriod.PastYear:
                    _QuantityData = UIUtility.GetQuantityData(timePeriod);
                    _RevenueData = UIUtility.GetRevenueData(timePeriod);
                    plotModel = new PastYear(_QuantityData, _RevenueData);
                    break;
                default:
                    plotModel = null;
                    break;
            }

            
        }

        public void SetPlotModel(GraphTimePeriod timePeriod, Item item)
        {
            Dictionary<DateTime, int> _QuantityData;
            Dictionary<DateTime, decimal> _RevenueData;
            switch (timePeriod)
            {
                case GraphTimePeriod.PastHour:
                    _QuantityData = UIUtility.GetQuantityData(timePeriod, item);
                    _RevenueData = UIUtility.GetRevenueData(timePeriod, item);
                    plotModel = new PastHour(_QuantityData, _RevenueData);
                    break;
                case GraphTimePeriod.PastDay:
                    _QuantityData = UIUtility.GetQuantityData(timePeriod, item);
                    _RevenueData = UIUtility.GetRevenueData(timePeriod, item);
                    plotModel = new PastDay(_QuantityData, _RevenueData);
                    break;
                case GraphTimePeriod.PastWeek:
                    _QuantityData = UIUtility.GetQuantityData(timePeriod, item);
                    _RevenueData = UIUtility.GetRevenueData(timePeriod, item);
                    plotModel = new PastWeek(_QuantityData, _RevenueData);
                    break;
                case GraphTimePeriod.PastYear:
                    _QuantityData = UIUtility.GetQuantityData(timePeriod, item);
                    _RevenueData = UIUtility.GetRevenueData(timePeriod, item);
                    plotModel = new PastYear(_QuantityData, _RevenueData);
                    break;
                default:
                    plotModel = null;
                    break;
            }
        }
        #endregion
    }
}
