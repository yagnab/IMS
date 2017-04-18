using System;
using IMS.BL.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace IMS.BL.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(InventoryContext context) : base(context)
        {
        }
        public IEnumerable<Item> ItemByBarcode(string barcode)
        {
            var item = from i in Context.Items
                       where i.Barcode == barcode
                       select i;
            return item.ToList();
        }
        public void UpdateAllSaleRate()
        {
            
            var allItems = Context.Items;
            foreach (Item item in allItems)
            {
                item.QuantityWeaklySaleRate = 0;
            }

            TimeSpan weekAgo = new TimeSpan(7, 0, 0);
            List<ItemTransaction> validI_Ts = new List<ItemTransaction>();
            using (var itsRepo = new ItemTransactionsRepo(new InventoryContext()))
            {
                validI_Ts = itsRepo.AllItemTransactionFrom(weekAgo)
                                                    .ToList();
                itsRepo.Complete();
            }
            
            //each time an item is sold, increment its sale rate
            //by quantity sold
            foreach (ItemTransaction i_t in validI_Ts)
            {
                var currItem = i_t.Item;
                currItem.QuantityWeaklySaleRate += i_t.Quantity;
            }
        }
    }
}
