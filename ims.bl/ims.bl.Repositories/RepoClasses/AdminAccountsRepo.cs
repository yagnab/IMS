using IMS.BL.DataModel;

namespace IMS.BL.Repositories
{
    public class AdminAccountsRepo : Repository<AdminAccount>, IAdminAccountsRepo
    {
        public AdminAccountsRepo(InventoryContext context) : base(context)
        {
        }
    }
}
