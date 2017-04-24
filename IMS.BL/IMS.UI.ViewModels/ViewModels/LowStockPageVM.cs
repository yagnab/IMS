using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.DataModel;

namespace IMS.UI.ViewModels
{
    public class LowStockPageVM
    {
        public List<Item> LowestStock { get; set; }

        public LowStockPageVM()
        {
            //initialising LowestStock

            LowestStock = new List<Item>();

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

                    //assigns increment the smaller value out of
                    //length of temp, or 10
                    int increment = temp.Count >= 10 ? 10 : temp.Count; 
                    for (int i = 0; i < increment; i++)
                    {
                        LowestStock.Add(temp[i]);
                    }
                }
            }
        }
    }
}
