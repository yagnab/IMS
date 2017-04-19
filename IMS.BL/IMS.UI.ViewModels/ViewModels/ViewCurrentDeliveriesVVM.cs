using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.DataModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IMS.UI.ViewModels.ViewModels
{
    class ViewCurrentDeliveriesVVM
    {
        public List<CurrentDelivery> CurrentDeliveries { get; private set; }

        CurrentDelivery _CurrentDelivery;
        public CurrentDelivery CurrentDelivery
        {
            get
            {
                return _CurrentDelivery;
            }
            set
            {
                _CurrentDelivery = value;
                OnPropertyChanged();
            }
        }

        List<ItemDelivery> _CurrentItemsDeliveries;
        public List<ItemDelivery> CurrentItemsDeliveries
        {
            get
            {
                return _CurrentItemsDeliveries;
            }
            set
            {
                _CurrentItemsDeliveries = value;
                OnPropertyChanged();
            }
        }

        #region OnPropertyChanged Members
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
