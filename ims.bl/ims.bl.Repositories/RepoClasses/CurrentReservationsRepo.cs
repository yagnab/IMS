using IMS.BL.DataModel;

namespace IMS.BL.Repositories
{
    class CurrentReservationsRepo : Repository<CurrentReservation>, ICurrentReservationsRepo
    {
        public CurrentReservationsRepo(InventoryContext context) : base(context)
        {
        }
    }
}
