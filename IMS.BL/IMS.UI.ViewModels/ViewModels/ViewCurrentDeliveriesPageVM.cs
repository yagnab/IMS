using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IMS.BL.DataModel;

namespace IMS.UI.ViewModels
{
    public class ViewCurrentDeliveriesPageVM
    {
        #region Properties

        private CurrentDelivery _SelectedDelivery;
        public CurrentDelivery SelectedDelivery
        {
            get
            {
                return _SelectedDelivery;
            }
            set
            {
                _SelectedDelivery = value;
                OnPropertyChanged();
            }
        }

        private List<ItemDisplay> _SelectedItemDisplays;
        public List<ItemDisplay> SelectedItemDisplays
        {
            get
            {
                return _SelectedItemDisplays;
            }
            set
            {
                _SelectedItemDisplays = value;
                OnPropertyChanged();
            }
            
        }

        public List<CurrentDelivery> AllCurrentDeliveries { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
        #endregion

        #region Constructor

        public ViewCurrentDeliveriesPageVM()
        {
            using (var dbContext = new InventoryContext())
            {
                AllCurrentDeliveries = dbContext.Deliveries.OfType<CurrentDelivery>().ToList();
            }
        }

        #endregion

        #region Methods

        public void ChangeProperties(int DeliveryID, InventoryContext dbContext)
        {
            SelectedDelivery = (from del in dbContext.Deliveries.OfType<CurrentDelivery>()
                               where del.DeliveryID == DeliveryID
                               select del).First();

            //Changing SelectedItemDisplays at once
            //To avoid calling OnPropertyChanged too many times
            List<ItemDisplay> itemDisplays = new List<ItemDisplay>();
            foreach(ItemDelivery i_d in SelectedDelivery.ItemDeliveries)
            {
                itemDisplays.Add( new ItemDisplay(i_d.Item) );
            }

            SelectedItemDisplays = itemDisplays;
        }

        #endregion
    }
}
