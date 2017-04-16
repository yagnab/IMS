using IMS.BL.DataModel;
using System.Data.Entity;

namespace IMS.BL.Repositories
{
    class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(DbContext context) : base(context)
        {
            
        }

        //public 
    }
}
