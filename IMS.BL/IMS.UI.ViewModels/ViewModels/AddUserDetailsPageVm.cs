using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL;
using System.Runtime.CompilerServices;
using IMS.UI.Models;

namespace IMS.UI.ViewModels
{
    /*
    public class AddUserDetailsPageVM : INotifyPropertyChanged
    {
        #region Properties
        public AddUserDetailsPageM model { get; private set; }
        public string Username
        {
            get
            {
                return model.Username;
            }
            set
            {
                model.Username = value;
                OnPropertyChanged();
            }
        }
        
        public string Password
        {
            get
            {
                return model.Password;
            }
            set
            {
                model.Password = value;
                OnPropertyChanged();
            }
        }
        
        public string ConfirmPassword
        {
            get
            {
                return model.ConfirmPassword;
            }
            set
            {
                model.ConfirmPassword = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructor
        public AddUserDetailsPageVM()
        {
            model = new AddUserDetailsPageM();
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
    }*/
}
