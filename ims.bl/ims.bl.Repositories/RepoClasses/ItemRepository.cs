using System;
using IMS.BL.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace IMS.BL.Repositories
{
    class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(InventoryContext context) : base(context)
        {
        }

        public void UpdateAllSaleRate()
        {
            
            var allItems = Context.Items;
            foreach (Item item in allItems)
            {
                item.QuantityWeaklySaleRate = 0;
            }

            using (var unitOfWork = new UnitOfWork(Context))
            {
                TimeSpan weekAgo = new TimeSpan(7, 0, 0);
                List<ItemTransaction> validI_Ts = unitOfWork.ItemTransactions.AllItemTransactionFrom(weekAgo)
                                                    .ToList();

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
}
