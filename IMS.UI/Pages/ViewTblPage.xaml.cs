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
using IMS.BL;
using IMS.BL.DataModel;

namespace IMS.UI
{
    /// <summary>
    /// Interaction logic for ViewTblPage.xaml
    /// </summary>
    public partial class ViewTblPage : Page
    {
        private ViewTablesPageVM _dataContext = new ViewTablesPageVM(); 

        public ViewTblPage(List<object> _tableOptions)
        {
            InitializeComponent();
            ViewTablesPage_Loaded(_tableOptions);
        }

        private void tblComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //setting the combobox's current object as current table source
            string selectedItemType = this.tblComboBox.SelectedItem.ToString();
            using (var dbContext = new InventoryContext())
            {
                switch (selectedItemType)
                {
                    case "Item":
                        _dataContext.CurrentTable = getItemTable(dbContext);

                        string error = "";
                        foreach(dynamic item in _dataContext.CurrentTable)
                        {
                            error += item.Description.ToString() + "\n";
                        }
                        MessageBox.Show(error);

                        this.tblDatGrd.UpdateLayout();
                        break;
                    default:
                        MessageBox.Show("This didnt work");
                        break; 
                }
            }
                

        }

        private List<dynamic> getItemTable(InventoryContext dbContext)
        {
            var itemList = (from item in dbContext.Items
                            select item).ToList<dynamic>();
            return itemList; 
        }
        private void ViewTablesPage_Loaded(List<object> _tableOptions)
        {
            //set datacontext
            _dataContext.TableOptions = _tableOptions;
            
            this.DataContext = _dataContext;

            this.tblComboBox.SelectedIndex = 0;
        }
    }
}
