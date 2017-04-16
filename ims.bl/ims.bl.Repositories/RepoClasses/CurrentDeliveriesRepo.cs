using IMS.BL.DataModel;

namespace IMS.BL.Repositories
{
    public class CurrentDeliveriesRepo : Repository<CurrentDelivery>, ICurrentDeliveriesRepo
    {
        public CurrentDeliveriesRepo(InventoryContext context) : base(context)
        {
        }
    }
}
