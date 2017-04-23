using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using IMS.BL.DataModel;
using System.Collections.ObjectModel;

namespace IMS.UI.ViewModels
{
    public class TillModeViewVM : INotifyPropertyChanged
    {

        private string barcode;
        public string Barcode
        {
            get
            {
                return barcode;
            }
            set
            {
                barcode = value;
            }
        }

        ObservableCollection<TillDataRow> rows = new ObservableCollection<TillDataRow>();
        public ObservableCollection<TillDataRow> Rows
        {
            get
            {
                return rows;
            }
            set
            {
                rows = value;
                OnPropertyChanged();
            }
        }

        #region INofityPropertyChanged members
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
    }
}
