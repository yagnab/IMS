using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.Validation;

namespace IMS.UI.Models
{
 /*   public class AddUserDetailsPageM : IDataErrorInfo, INotifyPropertyChanged
    {
        #region Properties
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }
        private string confirmPassword;
        public string ConfirmPassword
        {
            get
            {
                return confirmPassword;
            }
            set
            {
                confirmPassword = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructor
        public AddUserDetailsPageM()
        {
            //Prompting user to enter a Username
            username = "Username";
        }
        #endregion

        #region INotifyPropertyChanged members
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

        #region IDataErrorInfo members
        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return ValidateProperty(propertyName);
            }
        }

        //Will not be used
        string IDataErrorInfo.Error
        {
            get
            {
                return null;
            }
        }
        #endregion

        #region Validation methods
        static readonly string[] propertiesToValidate =
        {
            "Username",
            "Password",
            "ConfirmPassword"
        };

        //Check if all properties have valid values
        public bool IsValid
        {
            get
            {
                foreach (string property in propertiesToValidate)
                {
                    if (ValidateProperty(property) != null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        string ValidateProperty(string propertyName)
        {
            string errorMessage = "";

            #region Carry out validation on Properties
            switch (propertyName)
            {
                case "Username":
                    errorMessage += UsernameValidation.ValidateUsername(Username);
                    break;
                case "Password":
                    errorMessage += PasswordValidation.ValidatePassword(Password);
                    break;
                case "ConfirmPassword":
                    errorMessage += ConfirmPasswordValidation.ValidateConfirmPassword(Password, ConfirmPassword);
                    break;
                default:
                    break;
            }
            #endregion

            return IsValidationError(errorMessage);
        }

        string IsValidationError(string errorMessage)
        {
            //If there is a validation error
            if (errorMessage != "")
            {
                return errorMessage;
            }
            else
            {
                //No validation error found
                return null;
            }
        }
        #endregion
    }*/
}