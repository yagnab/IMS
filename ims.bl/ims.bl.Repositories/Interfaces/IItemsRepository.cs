using IMS.BL.DataModel;

namespace IMS.BL.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
        void UpdateAllItemSaleRate();
        
    }
}