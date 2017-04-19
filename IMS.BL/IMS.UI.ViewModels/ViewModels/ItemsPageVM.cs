using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IMS.BL.DataModel;
using IMS.UI;
using System.Data.Entity;
using IMS.BL.Repositories;

namespace IMS.UI.ViewModels
{
    public class ItemsPageVM
    {
        #region Properties
        public List<Item> items { get; set; }

        Item _item;
        Item item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }


        private List<CurrentItemDeliveryDisplay> _currItemDeliveriesDisplays;
        public List<CurrentItemDeliveryDisplay> currItemDeliveriesDisplays
        {
            get
            {
                return _currItemDeliveriesDisplays;
            }
            set
            {
                _currItemDeliveriesDisplays = value;
                OnPropertyChanged();
            }
        }

        private List<ItemReservationDisplay> _currItemReservationsDisplays;
        public List<ItemReservationDisplay> currItemReservationsDisplays
        {
            get
            {
                return _currItemReservationsDisplays;
            }
            set
            {
                _currItemReservationsDisplays = value;
                OnPropertyChanged();
            }
        }

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
        public ItemsPageVM()
        {
            using(var dbContext = new InventoryContext())
            {
                items = dbContext.Items.ToList();
            }
        }
        #endregion

        #region Methods
        //item must include all the stuffs
        public void SetNewProperties(int ItemID)
        {
            using(var dbContext = new InventoryContext())
            {
                //selects new propety values
                this.item = UIUtility.GetItemFromItemID(ItemID, dbContext);
                currItemDeliveriesDisplays = CreateCurrentItemDeliveriesDisplays(this.item, dbContext);
                currItemReservationsDisplays = CreateItemReservationsDisplays(this.item, dbContext);
            }
        }
        
        List<CurrentItemDeliveryDisplay> CreateCurrentItemDeliveriesDisplays(Item item, InventoryContext dbContext)
        {
            List<CurrentItemDeliveryDisplay> displays = new List<CurrentItemDeliveryDisplay>();

            //Create new ItemDeliveryDisplay object foreach ItemDelivery
            //corrispoding to the passed item
            foreach(ItemDelivery i_d in item.ItemDeliveries)
            {
                //Only display current deliveries to user
                if (!i_d.Delivery.IsArrived)
                {
                    //getting delivery for i_d
                    using (var cdRepo = new CurrentDeliveriesRepo(new InventoryContext()))
                    {
                        var currDel = cdRepo.GetByID(i_d.DeliveryID);
                        displays.Add(CurrentItemDeliveryDisplay.GetCurrentItemDeliveryDisplay(i_d, currDel , item));
                        cdRepo.Complete();
                    }
                        
                }
            }

            return displays;
        }
        List<ItemReservationDisplay> CreateItemReservationsDisplays(Item item, InventoryContext dbContext)
        {
            List<ItemReservationDisplay> displays = new List<ItemReservationDisplay>();

            //Create new ItemReservationDisplay object foreach ItemTransaction
            //corrispoding to the passed item
            foreach (ItemReservation i_r in item.ItemReservations)
            {
                //Only show current reservations to user
                if (!i_r.Reservation.IsPickedUp)
                {
                    displays.Add(UIUtility.CreateNewItemReservationDisplay(i_r, dbContext));
                }
            }

            return displays;
        }
        #endregion
    }
}
