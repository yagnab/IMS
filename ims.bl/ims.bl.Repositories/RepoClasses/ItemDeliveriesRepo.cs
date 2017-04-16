using IMS.BL.DataModel;

namespace IMS.BL.Repositories
{
    class ItemDeliveriesRepo : Repository<ItemDelivery>, IItemDeliveriesRepo
    {
        public ItemDeliveriesRepo(InventoryContext context) : base(context)
        {
        }
    }
}