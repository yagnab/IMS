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
using IMS.BL.DataModel;
using IMS.BL.Validation;

namespace IMS.UI
{
    /// <summary>
    /// Interaction logic for AddUserDetailsPage.xaml
    /// </summary>
    public partial class AddUserDetailsPage : Page
    {
        public AddUserDetailsPage()
        {
            InitializeComponent();
        }

        #region Input events
        private void UsernameTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            var usernameTxt = (TextBox)sender;
            //make text blank
            //then remove this event
            usernameTxt.Text = "";
            usernameTxt.GotFocus -= UsernameTxt_GotFocus;
        }

        private void PasswordTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            var passwordTxt = (TextBox)sender;

            //remove the textbox, and focus on password box behind
            passwordTxt.Visibility = Visibility.Hidden;
            passwordBox.Visibility = Visibility.Visible;
            passwordBox.Focus();
        }

        private void ConfirmPasswordTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            var confirmPasswordTxt = (TextBox)sender;

            //remove the textbox, and focus on password box behind
            confirmPasswordTxt.Visibility = Visibility.Hidden;
            ConfirmPasswordBox.Visibility = Visibility.Visible;
            ConfirmPasswordBox.Focus();
        }
        #endregion

        private void SignupBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTxt.Text;
            string password = passwordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;
            var naV = new NewAccountValidation(username, password, confirmPassword);

            //validation passed
            if(naV.isNewAccountValid)
            {
                //stores username and hashed password
                CreateNewAccountService.Instance.username = username;
                CreateNewAccountService.Instance.password = UserAccount.stringToHashString(password);

                //show next window
                var ui = UICreatePage.GetAdminDetailsPage();
                CreateNewAccountService.Instance.Window.Content = ui;
            }
            else
            {
                ErrorTxt.Text = naV.ErrorMessage;
            }
            /*
            string errorMessage = ValidateInputs(username, password, confirmPassword);

            //Take action depending on validation from above
            IsInputValid(errorMessage, username, password);*/
        }
        /*
        #region Validation
        string ValidateInputs(string username, string password, string confirmPassword)
        {
            string errorMessage = "";
            var naV = new NewAccountValidation(username, password, confirmPassword);
            errorMessage += UsernameValidation.ValidateUsername(username);
            errorMessage += PasswordValidation.ValidatePassword(password);
            errorMessage += ConfirmPasswordValidation.ValidateConfirmPassword(password, confirmPassword);
            return errorMessage;
        }
        void IsInputValid(string errorMessage, string username, string password)
        {
            //If no error is found
            if (errorMessage == "")
            {
                //stores username and hashed password
                CreateNewAccountService.Instance.username = username;
                CreateNewAccountService.Instance.password = UserAccount.stringToHashString(password);

                var ui = UICreatePage.GetAdminDetailsPage();
                CreateNewAccountService.Instance.Window.Content = ui;
            }
            else
            {
                ErrorTxt.Text = errorMessage;
                ClearInputFields();
            }
        }
        #endregion*/

        #region Utility methods
        void ClearInputFields()
        {
            //Clear all fields on the view
            UsernameTxt.Clear();
            passwordBox.Clear();
            ConfirmPasswordBox.Clear();
        }
        #endregion
    }
}
