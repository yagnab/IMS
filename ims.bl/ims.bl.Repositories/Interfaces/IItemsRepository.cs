using IMS.BL.DataModel;
using System.Collections.Generic;

namespace IMS.BL.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
        void UpdateAllSaleRate();
        IEnumerable<Item> ItemByBarcode(string barcode);
    }
}