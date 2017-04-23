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
using System.Windows.Shapes;
using MahApps.Metro.Controls;


namespace IMS.UI
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class UIWindow : MetroWindow
    {
        public UIWindow()
        {
            InitializeComponent();

            
        }
        
        private void MainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginService.Instance.currentUIWindow.pageHolder.Content = 
                UICreatePage.GetUserMenu(LoginService.Instance.currentUser);
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            
            var ui = new LoginWindow();
            ui.Show();
            LoginService.Instance.currentUIWindow.Close();
            LoginService.NullCurrentInstance();
        }
    }
}
