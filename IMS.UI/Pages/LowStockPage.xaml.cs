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
using IMS.UI.ViewModels;
using IMS.BL.DataModel;

namespace IMS.UI
{
    /// <summary>
    /// Interaction logic for LowStockPage.xaml
    /// </summary>
    public partial class LowStockPage : Page
    {
        LowStockPageVM _dataContext;
        public LowStockPage()
        {
            InitializeComponent();

            //Setting DataContext
            _dataContext = new LowStockPageVM();
            DataContext = _dataContext;
        }
    }
}
