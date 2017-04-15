using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL;
using IMS.BL.DataModel;

namespace IMS.UI.ViewModels
{
    public class LowStockPageVM
    {
        public List<Item> LowestStock { get; set; }

        public LowStockPageVM()
        {
            using(var dbContext = new InventoryContext())
            {
                //If less or equal to 10 items in db
                if (dbContext.Items.Count() <= 10)
                {
                    //OrderBy sorts accendingly
                    LowestStock = LowestStock = dbContext.Items.OrderBy(i => i.QuantityStockLevel).ToList();
                }
                else
                {
                    List<Item> temp = dbContext.Items.OrderBy(i => i.QuantityStockLevel).ToList();

                    for (int i = 0; i < 10; i++)
                    {
                        LowestStock.Add(temp[i]);
                    }
                }
            }
        }
    }
}
