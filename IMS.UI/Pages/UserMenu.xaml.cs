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

namespace IMS.UI
{
    /// <summary>
    /// Interaction logic for UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Page
    {
        public UserMenu()
        {
            InitializeComponent();
        }

        //Displaying pages on currently open UIWindow
        private void viewTblsBtn_Click(object sender, RoutedEventArgs e)
        {
            var viewTblsPage = UICreatePage.GetViewTblPage();
            LoginService.Instance.currentUIWindow.pageHolder.Content = viewTblsPage;
        }

        private void viewTransactionsBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewTransactionsPage viewTransactions = UICreatePage.GetViewTransactionsPage();
            LoginService.Instance.currentUIWindow.pageHolder.Content = viewTransactions;
        }

        private void tillModeBtn_Click(object sender, RoutedEventArgs e)
        {
            TillModePage tillModePage = UICreatePage.GetTillModePage();
            LoginService.Instance.currentUIWindow.pageHolder.Content = tillModePage;
        }

        private void analyticsBtn_Click(object sender, RoutedEventArgs e)
        {
            var ui = UICreatePage.GetAnalyticsPage();
            LoginService.Instance.currentUIWindow.pageHolder.Content = ui;
        }

        private void viewItemsBtn_Click(object sender, RoutedEventArgs e)
        {
            var ui = UICreatePage.GetItemsPage();
            LoginService.Instance.currentUIWindow.pageHolder.Content = ui;
        }

        private void viewCurrDelBtn_Click(object sender, RoutedEventArgs e)
        {
            var ui = UICreatePage.GetCurrentDeliveryPage();
            LoginService.Instance.currentUIWindow.pageHolder.Content = ui;
        }

        private void viewPastDelBtn_Click(object sender, RoutedEventArgs e)
        {
            var ui = UICreatePage.GetNewPastDeliveriesPage();
            LoginService.Instance.currentUIWindow.pageHolder.Content = ui;
        }

        private void lowStockBtn_Click(object sender, RoutedEventArgs e)
        {
            var ui = UICreatePage.GetNewLowStockPage();
            LoginService.Instance.currentUIWindow.pageHolder.Content = ui;
        }

        private void addDeliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            var ui = new AddDeliveryPage();
            LoginService.Instance.currentUIWindow.pageHolder.Content = ui;
        }
    }
}
