using IMS.BL.DataModel;

namespace IMS.BL.Repositories
{
    class PastDeliveriesRepo : Repository<PastDelivery>, IPastDeliveriesRepo
    {
        public PastDeliveriesRepo(InventoryContext context) : base(context)
        {
        }
    }
}
