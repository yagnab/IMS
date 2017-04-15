using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IMS.BL;
using IMS.BL.DataModel;

namespace IMS.UI.ViewModels
{
    public class AnalyticsPageFSVM
    {
        #region Properties
        private List<Item> _fastItems;
        public List<Item> fastItems
        {
            get
            {
                return _fastItems;
            }
            set
            {
                _fastItems = value;
                OnPropertyChanged();
            }

        }

        private List<Item> _slowItems;
        public List<Item> slowItems
        {
            get
            {
                return _slowItems;
            }
            set
            {
                _slowItems = value;
                OnPropertyChanged();
            }

        }

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

        #region Constructor
        public AnalyticsPageFSVM()
        {
            using (var dbContext = new InventoryContext())
            {
                fastItems = GetFastItems(dbContext);
                slowItems = GetSlowItems(dbContext);
            }
            
        }
        #endregion

        #region Methods

        
        List<Item> GetFastItems(InventoryContext dbContext)
        {
            
           
            //reverses, so fastest selling first
            List<Item> orderedItems = dbContext.Items
                .OrderBy(i => i.QuantityWeaklySaleRate)
                .ToList();
            orderedItems.Reverse();

            List<Item> fastest = new List<Item>();

            //if lenght of items < 10, then only iterate through
            //length of item times
            int listLenght = orderedItems.Count < 10 ? orderedItems.Count : 10;
            for(int i = 0; i < listLenght; i++)
            {
                fastest.Add(orderedItems[i]);
            }

            return fastest;
            
            
        }

        List<Item> GetSlowItems(InventoryContext dbContext)
        {
            
            //slowest selling first
            List<Item> orderedItems = dbContext.Items
                .OrderBy(i => i.QuantityWeaklySaleRate)
                .ToList();
                
            List<Item> slowest = new List<Item>();

            //if lenght of items < 10, then only iterate through
            //length of item times
            int listLenght = orderedItems.Count < 10 ? orderedItems.Count : 10;
            for (int i = 0; i < listLenght; i++)
            {
                slowest.Add(orderedItems[i]);
            }

            return slowest;
            
        }

        #endregion
    }
}
