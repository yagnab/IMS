using IMS.BL.DataModel;

namespace IMS.BL.Repositories
{
    class StaffAccountsRepo : Repository<StaffAccount>, IStaffAccountsRepo
    {
        public StaffAccountsRepo(InventoryContext context) : base(context)
        {
        }
    }
}