using IMS.BL.DataModel;

namespace IMS.BL.Repositories
{
    class ItemReservationsRepo : Repository<ItemReservation>, IItemReservationsRepo
    {
        public ItemReservationsRepo(InventoryContext context) : base(context)
        {
        }
    }
}