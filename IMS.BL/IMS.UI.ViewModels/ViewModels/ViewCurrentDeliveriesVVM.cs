using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.DataModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IMS.BL.Repositories;

namespace IMS.UI.ViewModels
{
    public class ViewCurrentDeliveriesVVM
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

        List<CurrentItemDeliveryDisplay> _CurrentItemDeliveryDisplays;
        public List<CurrentItemDeliveryDisplay> CurrentItemDeliveryDisplays
        {
            get
            {
                return _CurrentItemDeliveryDisplays;
            }
            set
            {
                _CurrentItemDeliveryDisplays = value;
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

        public ViewCurrentDeliveriesVVM()
        {
            using (var cdRepo = new CurrentDeliveriesRepo(new InventoryContext()))
            {
                CurrentDeliveries = cdRepo.GetAll().ToList();
                cdRepo.Complete();
            }
            //sets a default current delivery to display on loadup
            //also sets default currentDeliveryDisplay
            SelectionChanged(CurrentDeliveries[0]);
        }
        public void SelectionChanged(CurrentDelivery newCurrentDelivery)
        {
            CurrentDelivery = newCurrentDelivery;
            CurrentItemDeliveryDisplays = GetCurrentItemDeliveryDisplays();
        }
        List<CurrentItemDeliveryDisplay> GetCurrentItemDeliveryDisplays()
        {
            List<CurrentItemDeliveryDisplay> temp = new List<CurrentItemDeliveryDisplay>();
            foreach(ItemDelivery i_d in CurrentDelivery.ItemDeliveries)
            {
                var newCIDDisplay = CurrentItemDeliveryDisplay.GetCurrentItemDeliveryDisplay(i_d, CurrentDelivery, i_d.Item);
            }
            return temp;
        }
    }
}
