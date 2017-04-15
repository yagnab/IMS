using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL.DataModel;
using System.Windows;
using IMS.BL.Validation;

namespace IMS.UI
{
    /// <summary>
    /// Another utility class
    /// This create complex objects
    /// Avoid circular dependencies between 
    /// IMS.BL + IMS.BL.DataModel
    /// </summary>
    public static class UIObjectCreator
    {
        /// <summary>
        /// Needs dictionary that
        /// Maps Item to its quntity needed
        /// </summary>
        /// <param name="itemToQuantity"></param>
        /// <returns></returns>
        public static Transaction CreateNewTransaction(Dictionary<Item, int> itemToQuantity)
        {
            using (var dbContext = new InventoryContext())
            {
                //converting items to item obj from dbContext
                //otherwise it will create new item in db
                Dictionary<Item, int> fixedItemToQuantity = new Dictionary<Item, int>();
                IQueryable<Item> allItemsFromDB = from item in dbContext.Items
                                     select item;
                 
                foreach(KeyValuePair<Item, int> itemQuantity in itemToQuantity )
                {
                    //searches all items (from above)
                    //matches the current keyvaluepairs item(key) to one from all items
                    //then add this to fixedItemToQuantity with quanity from itemToQuantity
                    Item itemFromDB = (from item in allItemsFromDB
                                      where item.ItemID == itemQuantity.Key.ItemID
                                      select item).First();
                    fixedItemToQuantity.Add(itemFromDB, itemQuantity.Value);
                }

                //sets current dataTime as transactions TimeOfTransaction
                var newTransaction = new Transaction()
                {
                    TimeOfTransaction = DateTime.Now
                };

                decimal itemTotValue = 0;
                
                //create many instances of itemTransaction and adds to db
                foreach (KeyValuePair<Item,int> itemQuantity in fixedItemToQuantity)
                {
                    //remove this item from stock
                    //add new I_T to ItemTransaction(tbl) and to the new transaction obj

                    ItemTransaction tempItemTransaction = new ItemTransaction
                                                            {
                                                                Transaction = newTransaction,
                                                                Item = itemQuantity.Key,
                                                                Quantity = itemQuantity.Value
                                                            };

                    dbContext.ItemTransactions.Add(tempItemTransaction);
                    

                    //adding value of each Item * Quantity of item
                    itemTotValue += (itemQuantity.Key.RRP * itemQuantity.Value);

                    
                }
                newTransaction.TotalValue = itemTotValue;
                dbContext.SaveChanges();

                return newTransaction;
            }

            
        }
        
        public static void RemoveItemQuantityFromDB(Item item, int quantity)
        {
            
        }
        
        /// <summary>
        /// Will return a new Item
        /// Which has been commited to database
        /// Assumes weaklyQuantitySaleRate 
        /// Is 0
        /// </summary>
        /// <param name="_Barcode"></param>
        /// <param name="_Description"></param>
        /// <param name="_RRP"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static Item CreateNewItem(string _Barcode, string _Description, decimal _RRP, int _QuantityStockLevel, ItemCatagory _Catagory)
        {
            using (var dbContext = new InventoryContext())
            {
                //makes sure description "AllRevenue" cannot be used for an item
                //this is a special case for combobox on analyticsgraph page
                if (_Description != "AllRevenue")
                {
                    var newItem = new Item()
                    {
                        Barcode = _Barcode,
                        Description = _Description,
                        RRP = _RRP,
                        QuantityStockLevel = _QuantityStockLevel,
                        QuantityWeaklySaleRate = 0,
                        Catagory = _Catagory
                    };

                    dbContext.Items.Add(newItem);
                    dbContext.SaveChanges();

                    return newItem;
                }
                else
                {
                    return null;
                }
                
            }
                
            
        }
        
        public static CurrentDelivery CreateNewCurrentDelivery(Dictionary<Item, int> ItemToItemQuantity, DateTime expectedArrivalDate, Supplier supplier, InventoryContext dbContext)
        {
            //Create new currentDelivery
            var currDel = new CurrentDelivery()
            {
                IsArrived = false,
                ExpectedArrivalDate = expectedArrivalDate,
                SupplierID = supplier.SupplierID,
            };
            try
            {
                //save to db
                dbContext.Deliveries.Add(currDel);
                dbContext.SaveChanges();
            }
            catch(Exception)
            {
                //Failure to commit new Delivery to database
                return null;
            }

            //if creating itemdeliveries didnt work
            if(!CreateItemDeliveries(ItemToItemQuantity, dbContext, currDel))
            {
                return null;
            }
            else
            {
                return currDel;
            }

        }
        public static CurrentReservation CreateNewCurrentReservation(Dictionary<Item, int> itemToItemQuantity , DateTime expectedPickUpDate, InventoryContext dbContext)
        {
            var currRes = new CurrentReservation()
            {
                IsPickedUp = false,
                ExpectedPickUpDate = expectedPickUpDate
            };
            try
            {
                dbContext.Reservations.Add(currRes);
                dbContext.SaveChanges();
            }
            catch(Exception)
            {
                //If the above commit to db didn't work
                return null;
            }

            if (!CreateItemReservations(itemToItemQuantity, dbContext, currRes))
            {
                return null;
            }
            else
            {
                return currRes;
            }
        }
        /*public PastReservation CreatePastReservation()
        {

        }*/
        public static PastDelivery CreatePastDelivery(CurrentDelivery CurrDel , DateTime ActualArrivalDate, InventoryContext dbContext)
        {
            try
            {
                Supplier Supplier = dbContext.Suppliers
                                .Where(s => s.SupplierID == CurrDel.SupplierID)
                                .First();
                var PastDel = new PastDelivery()
                {
                    IsArrived = true,
                    Supplier = Supplier,
                    ActualArrivalDate = ActualArrivalDate
                };

                dbContext.Deliveries.Add(PastDel);
                dbContext.SaveChanges();

                if (!AttachItemDeliveriesToPastDelivery(CurrDel, PastDel, dbContext))
                {
                    //It didn't work
                    return null;
                }
                else
                {
                    //It did work
                    //So remove CurrDelivery, which is no longer needed
                    dbContext.Deliveries.Remove(CurrDel);
                    return PastDel;
                }
            }
            catch(Exception ex)
            {
                //Add PastDel to database didn't work
                Console.WriteLine(ex.ToString());
                return null;
            }

            
        }

        public static bool AttachItemDeliveriesToPastDelivery(CurrentDelivery CurrDel, PastDelivery PastDel, InventoryContext dbContext)
        {
            bool isSuccessful;
            try
            {
                var relatedI_Ds = UIUtility.GetItemDeliveries(CurrDel, dbContext);
                
                Item _testItem;
                foreach (ItemDelivery i_d in relatedI_Ds)
                {
                    
                    _testItem = i_d.Item;
                    //Change each ItemDelivery to have a relation with the PastDel object
                    dbContext.ItemDeliveries.Add(new ItemDelivery()
                    {
                        Item = i_d.Item,
                        Delivery = PastDel,
                        Quantity = i_d.Quantity
                    });

                    if(!IncreaseItemQuantityStockLevel(i_d.Item, i_d.Quantity, dbContext))
                    {
                        isSuccessful = false;
                        return isSuccessful;
                    }

                    //Since everything worked, remove old ItemDelivery referencing CurrDel obj
                    dbContext.ItemDeliveries.Remove(i_d);
                }
                dbContext.SaveChanges();

                //It worked
                isSuccessful = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
                isSuccessful = false;
            }
            return isSuccessful;
        }
        /*
        public static PastReservation CreateNewPastReservation()
        {

        }*/

        public static bool CreateItemReservations(Dictionary<Item, int> itemToItemQuantity, InventoryContext dbContext, CurrentReservation currRes)
        {
            //If this transaction worked
            bool isSuccessful;
            try
            {
                foreach (KeyValuePair<Item, int> itemQuantity in itemToItemQuantity)
                {
                    dbContext.ItemReservations.Add(new ItemReservation()
                    {
                        Item = itemQuantity.Key,
                        Reservation = currRes,
                        Quantity = itemQuantity.Value
                    });
                }
                dbContext.SaveChanges();

                isSuccessful = true;
            }
            catch (Exception)
            {
                //If adding ItemReservations fails, remove currRes
                dbContext.Reservations.Remove(currRes);
                dbContext.SaveChanges();
                isSuccessful = false;
                
            }
            
            return isSuccessful;

        }
        public static bool CreateItemDeliveries(Dictionary<Item, int> ItemToItemQuantity, InventoryContext dbContext, CurrentDelivery currDel)
        {
            //Whether this transaction worked
            bool isSuccessful;

            try
            {
                foreach (KeyValuePair<Item, int> itemQuantity in ItemToItemQuantity)
                {
                    dbContext.ItemDeliveries.Add(new ItemDelivery()
                    {
                        Item = itemQuantity.Key,
                        Delivery = currDel,
                        Quantity = itemQuantity.Value
                    });
                }
                dbContext.SaveChanges();

                isSuccessful = true;
            }
            catch (Exception)
            {
                //If adding ItemDelivries fails, remove currDel
                dbContext.Deliveries.Remove(currDel);
                dbContext.SaveChanges();
                isSuccessful = false;
                
            }
            return isSuccessful;
        }
        public static bool IncreaseItemQuantityStockLevel(Item item, int QuantityToAdd, InventoryContext dbContext)
        {
            bool isSuccessful;

            try
            {
                item.QuantityStockLevel += QuantityToAdd;
                dbContext.SaveChanges();

                //It worked
                isSuccessful = true;
            }
            catch (Exception ex)
            {
                //Didn't work
                Console.WriteLine(ex.ToString());
                isSuccessful = false;
            }
            
            return isSuccessful;
        }
        public static bool ReduceItemQuantityStockLevel(Item item, InventoryContext dbContext, int quantityToRemove)
        {
            bool isSuccessful;
            try
            {
                Item itemFromDb = (from i in dbContext.Items
                                   where i.ItemID == item.ItemID
                                   select i).First();

                itemFromDb.QuantityStockLevel -= quantityToRemove;
                dbContext.SaveChanges();
                isSuccessful = true;
            }
            catch
            {
                isSuccessful = false;
            }

            return isSuccessful;
        }
        /// <summary>
        /// Will assumes username and password have already been validated
        /// Will then validate string to bool values
        /// Will check if string inputs are valid according to
        /// StringValidation.IsStringNullOrWhiteSpace()
        /// Will then create an account and return if it was successful
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="IsAdmin"></param>
        /// <param name="IsAnalyticsAllowed"></param>
        /// <param name="IsEditTablesAllowed"></param>
        /// <param name="IsAddNewDeliveryAllowed"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool CommitNewAccountToDB(InventoryContext dbContext, string username, string password, bool isAdmin, bool isAnalyticsAllowed, bool isEditTablesAllowed, bool isAddNewDeliveriesAllowed)
        {
            UserAccount newAccount = GetNewAccount(username, password, isAdmin, isAnalyticsAllowed, isEditTablesAllowed, isAnalyticsAllowed);

            try
            {
                dbContext.UserAccounts.Add(newAccount);
                dbContext.SaveChanges();

                //It worked
                return true;
            }
            catch (Exception)
            {
                //Comitting to db didn't work
                return false;
            }
        }
        
        static UserAccount GetNewAccount(string username, string password, bool isAdmin, bool isAnalyticsAllowed, bool isEditTablesAllowed, bool isAddDeliveryAllowed)
        {
            if (isAdmin)
            {
                return AdminAccount.CreateNewAdminAccount(username, password);
            }
            else
            {
                return StaffAccount.GetNewStaffAccount(username, password, isAnalyticsAllowed, isEditTablesAllowed, isAddDeliveryAllowed);
            }
        } 
    }
}
