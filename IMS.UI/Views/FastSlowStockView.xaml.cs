using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IMS.BL;
using IMS.UI.ViewModels;

namespace IMS.UI.Views
{
    /// <summary>
    /// Interaction logic for FastSlowStockView.xaml
    /// </summary>
    public partial class FastSlowStockView : UserControl
    {
        AnalyticsPageFSVM _dataContext;
        public FastSlowStockView()
        {
            InitializeComponent();

            //refreshes all the weaklysalerate
            UIUtility.UpdateAllItemSaleRate();

            //Setting DataContext
            _dataContext = new AnalyticsPageFSVM();
            DataContext = _dataContext;
            
        }
    }
}
