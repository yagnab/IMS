using IMS.BL.DataModel;

namespace IMS.BL.Repositories
{
    class PastReservationsRepo : Repository<PastReservation>, IPastReservationsRepo
    {
        public PastReservationsRepo(InventoryContext context) : base(context)
        {
        }
    }
}
