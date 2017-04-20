using IMS.BL.DataModel;
using System;
using System.Collections.Generic;

namespace IMS.BL.Repositories
{
    public interface ICurrentDeliveriesRepo : IRepository<CurrentDelivery>
    {
        void AddNewCurrentDelivery(Dictionary<Item, int> itemsToQuantity, DateTime expectedArrivalDate, Supplier supplier);
    }
}
