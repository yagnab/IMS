using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.DataModel;
using System.Collections;

namespace IMS.BL.Validation
{
    public class QuantityValidation : StringToIntegerValidation
    {
        public int intQuantity { get; private set; }
        public bool isQuantitySensible { get; private set; }
        public QuantityValidation() : base()
        {
        }
        /// <summary>
        /// Will return quantity from string if
        /// validation passes. Will update ErrorMessage 
        /// otherwise and return null
        /// </summary>
        /// <param name="integer"></param>
        /// <param name="item"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public int? ValidateQuantity(string integer, Item item, IEnumerable rows)
        {
            //Empties error message
            //So QuantityValidation.ErrorMessage only applies to one validation at a time
            ErrorMessage = "";

            int? quantityInt = GetIntegerFromString(integer);

            //First step validation failed
            if (quantityInt == null)
            {
                return null;
            }
            else
            {
                //If quantity is realistic
                if(quantityInt > 0 && quantityInt < 100)
                {
                    //If quantity meets quantityInStock
                    using(var dbContext = new InventoryContext())
                    {
                        quantityInt = IsQuantityStockLevelEnough(quantityInt, item, rows, dbContext);
                        if(quantityInt == null)
                        {
                            ErrorMessage += "There is not enough stock for that quantity";
                            return null;
                        }
                        else
                        {
                            //All validation passed
                            return quantityInt;
                        }
                    }
                }
                else
                {
                    ErrorMessage += "Quantity must be between 1 and 99";
                    return null;
                }
            }
        }
        int? IsQuantityStockLevelEnough(int? quantity, Item item, IEnumerable rows , InventoryContext dbContext)
        {
            int? totalQuantity = quantity;
            int quantityStockLevel = (from i in dbContext.Items
                                     where i.ItemID == item.ItemID
                                     select i).First().QuantityStockLevel;

            if(rows != null)
            {
                //Loop through all rows
                //Add quantity if it matches item quantity is for
                foreach (TillDataRow row in rows)
                {
                    if (row.Item == item)
                    {
                        totalQuantity += row.Quantity;
                    }
                }
            }
            
            //Quantity needed is more than quantity in stock
            if(totalQuantity > quantityStockLevel)
            {
                return null;
            }
            else
            {
                return quantity;
            }
        }
        public bool ValidateNewQuantity(string quantity)
        {
            ErrorMessage = "";

            if (StringValidation.IsStringNullOrWhiteSpace(quantity))
            {
                ErrorMessage += "Enter a quantity value.\n";
                return false;
            }
            if (!CanConvertToInt(quantity))
            {
                ErrorMessage += "Only digits allowed in quantity.\n";
                return false;
            }
            if (!IsQuantityInRange(quantity))
            {
                ErrorMessage += "Quantity must be between 0 and 999";
                return false;
            }

            //Validation worked
            Quantity = Convert.ToInt32(quantity);
            return true;

        }
        bool CanConvertToInt(string quantity)
        {
            try
            {
                int intQuantity = Convert.ToInt32(quantity);

                //workeds
                return true;
            }
            catch (Exception)
            {
                //cant convert to int
                return false;
            }
        }
        bool IsQuantityInRange(string quantity)
        {
            int intQuantity = Convert.ToInt32(quantity);
            
            if(intQuantity > 0 && intQuantity < 999)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
