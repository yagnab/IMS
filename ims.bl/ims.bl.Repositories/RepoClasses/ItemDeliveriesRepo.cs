using IMS.BL.DataModel;
using System;
using System.Collections.Generic;

namespace IMS.BL.Repositories
{
    public class ItemDeliveriesRepo : Repository<ItemDelivery>, IItemDeliveriesRepo
    {
        public ItemDeliveriesRepo(InventoryContext context) : base(context)
        {
        }
    }
}