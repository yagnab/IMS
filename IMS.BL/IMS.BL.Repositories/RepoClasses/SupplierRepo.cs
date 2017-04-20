using IMS.BL.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Repositories
{
    public class SupplierRepo : Repository<Supplier>, ISupplierRepo
    {
        public SupplierRepo(InventoryContext context) : base(context)
        {
        }
    }
}
