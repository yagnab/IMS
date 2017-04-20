using IMS.BL.DataModel;

namespace IMS.BL.Repositories
{
    public class DeliveryRepo : Repository<Delivery>, IDeliveryRepo
    {
        public DeliveryRepo(InventoryContext context) : base(context)
        {
        }
    }
}
