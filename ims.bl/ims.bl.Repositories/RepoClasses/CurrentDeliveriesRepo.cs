using IMS.BL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMS.BL.Repositories
{
    public class CurrentDeliveriesRepo : Repository<CurrentDelivery>, ICurrentDeliveriesRepo
    {
        public CurrentDeliveriesRepo(InventoryContext context) : base(context)
        {
        }
        /// <summary>
        /// Assumes items in itemToQuantity exist.
        /// </summary>
        /// <param name="itemsToQuantity"></param>
        /// <param name="expectedArrivalDate"></param>
        /// <param name="supplier"></param>
        public void AddNewCurrentDelivery(Dictionary<Item, int> itemsToQuantity, DateTime expectedArrivalDate, Supplier supplier)
        {
            //creating new delivery
            var NewCurrentDelivery = new CurrentDelivery()
            {
                Supplier = supplier,
                IsArrived = false,
                ExpectedArrivalDate = expectedArrivalDate
            };

            using (var cdRepo = new CurrentDeliveriesRepo(new InventoryContext()))
            {
                cdRepo.Add(NewCurrentDelivery);
                cdRepo.Complete();
            }

            //retrieving it
            using (var cdRepo = new CurrentDeliveriesRepo(new InventoryContext()))
            {
                NewCurrentDelivery = cdRepo.Find(cd => cd.ExpectedArrivalDate == expectedArrivalDate).First();
                cdRepo.Complete();
            }

            //adding ItemDeliveries
            //creating current deliveries from itemsToQuantity
            List<ItemDelivery> itemDeliveriesRange = new List<ItemDelivery>();
            foreach(KeyValuePair<Item, int> i_q in itemsToQuantity)
            {
                itemDeliveriesRange.Add(
                new ItemDelivery()
                {
                    Item = i_q.Key,
                    Delivery = NewCurrentDelivery,
                    Quantity = i_q.Value
                });
            }
            //adding range
            using (var idRepo = new ItemDeliveriesRepo(new InventoryContext()))
            {
                idRepo.AddRange(itemDeliveriesRange);
                idRepo.Complete();
            }
        }
    }
}
