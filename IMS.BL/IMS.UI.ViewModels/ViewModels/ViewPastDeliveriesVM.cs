using System.Collections.Generic;
using System.Linq;
using IMS.BL.DataModel;
using IMS.BL;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IMS.UI.ViewModels
{
    public class ViewPastDeliveriesVM
    {
        #region Properties

        private PastDelivery _SelectedDelivery;
        public PastDelivery SelectedDelivery
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

        public List<PastDelivery> AllPastDeliveries { get; set; }

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

        public ViewPastDeliveriesVM()
        {
            using (var dbContext = new InventoryContext())
            {
                AllPastDeliveries = dbContext.Deliveries.OfType<PastDelivery>().ToList();
            }
        }

        #endregion

        #region Methods

        public void ChangeProperties(int DeliveryID, InventoryContext dbContext)
        {
            SelectedDelivery = (from del in dbContext.Deliveries.OfType<PastDelivery>()
                                where del.DeliveryID == DeliveryID
                                select del).First();
            
            //Changing SelectedItemDisplays at once
            //To avoid calling OnPropertyChanged too many times
            List<ItemDisplay> itemDisplays = new List<ItemDisplay>();
            foreach (ItemDelivery i_d in SelectedDelivery.ItemDeliveries)
            {
                itemDisplays.Add(new ItemDisplay(i_d.Item));
            }

            SelectedItemDisplays = itemDisplays;
        }

        #endregion
    }
}
