using System.Windows;
using System.Windows.Controls;
using IMS.BL.DataModel;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using IMS.BL.Validation;

namespace IMS.UI
{
    /// <summary>
    /// Interaction logic for AdminDetailsPage.xaml
    /// </summary>
    public partial class AdminDetailsPage : Page
    {
        public AdminDetailsPage()
        {
            InitializeComponent();
        }
        //TODO Make sure ExpectedArrivalDate is unique
        private void usernameTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            var usernameTxt = (TextBox)sender;
            //make text blank
            //then remove this event
            usernameTxt.Text = "";
            usernameTxt.GotFocus -= usernameTxt_GotFocus;
        }

        private void passwordTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            var passwordTxt = (TextBox)sender;

            //remove the textbox, and focus on password box behind
            passwordTxt.Visibility = Visibility.Hidden;
            passwordBox.Focus();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            //Get inputted details
            string username = usernameTxt.Text;
            string password = UserAccount.stringToHashString(passwordBox.Password);
            var aV = new AdminValidation(username, password);

            if(aV.isAccountAdmin)
            {
                //Allow admin to pick access levels
                var ui = UICreatePage.CreateNewSetAccessPage();
                CreateNewAccountService.Instance.Window.Content = ui;
            }
            else
            {
                errorBlock.Text = aV.ErrorMessage;
            }
        }
    }
}