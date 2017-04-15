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
using OxyPlot;
using IMS.UI.ViewModels;
using IMS.BL;
using IMS.BL.DataModel;

namespace IMS.UI.Views
{
    /// <summary>
    /// Interaction logic for AnalyticsGraphsView.xaml
    /// </summary>
    public partial class AnalyticsGraphsView : UserControl
    {
        AnalyticsPageGraphsVM _dataContext;
        public AnalyticsGraphsView()
        {
            InitializeComponent();

            //set data context
            _dataContext = new AnalyticsPageGraphsVM();
            DataContext = _dataContext;
            
        }

        private void GraphOptsCmbBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //hides or shows items combobox
            if (GraphOptsCmbBx.SelectedIndex == 1)
            {
                ItemChoiceCmbBx.Visibility = Visibility.Visible;
            }
            else
            {
                ItemChoiceCmbBx.Visibility = Visibility.Hidden;
            }
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            //assuming all needed values are filled in
            //will change graphs
            if(GraphOptsCmbBx.SelectedIndex > -1 && TimePeriodCmbBX.SelectedIndex > -1)
            {
                //user wants to show single item graph
                if(GraphOptsCmbBx.SelectedIndex == 1)
                {
                    //item has been chosen
                    if(ItemChoiceCmbBx.SelectedIndex > -1)
                    {
                        string timePeriod = TimePeriodCmbBX.SelectedItem.ToString()
                                                .Split(' ')[1] + " " +
                                            TimePeriodCmbBX.SelectedItem.ToString().Split(' ')[2];
                        Item item = ItemChoiceCmbBx.SelectedItem as Item;
                        
                        switch (timePeriod)
                        {
                            case "Past Hour":
                                _dataContext.SetPlotModel(GraphTimePeriod.PastHour, item);
                                break;
                            case "Past Day":
                                _dataContext.SetPlotModel(GraphTimePeriod.PastDay, item);
                                break;
                            case "Past Week":
                                _dataContext.SetPlotModel(GraphTimePeriod.PastWeek, item);
                                break;
                            case "Past Year":
                                _dataContext.SetPlotModel(GraphTimePeriod.PastYear, item);
                                break;
                            default:
                                break;

                        }

                        //rerender the graph
                        graph.InvalidatePlot(true);
                    }
                    else
                    {
                        ErrorLbl.Content = "You need to select an item";
                    }
                }
                else
                {
                    //formats string properly
                    string timePeriod = TimePeriodCmbBX.SelectedItem.ToString()
                                                .Split(' ')[1] + " " +
                                            TimePeriodCmbBX.SelectedItem.ToString().Split(' ')[2];
                    switch (timePeriod)
                    {
                        case "Past Hour":
                            _dataContext.SetPlotModel(GraphTimePeriod.PastHour);
                            break;
                        case "Past Day":
                            _dataContext.SetPlotModel(GraphTimePeriod.PastDay);
                            break;
                        case "Past Week":
                            _dataContext.SetPlotModel(GraphTimePeriod.PastWeek);
                            break;
                        case "Past Year":
                            _dataContext.SetPlotModel(GraphTimePeriod.PastYear);
                            break;
                        default:
                            break;
                    }

                    //rerender the graph
                    graph.InvalidatePlot(true);
                }
            }
            else
            {
                ErrorLbl.Content = "Please enter pick all the values";
            }

        }
    }
}
