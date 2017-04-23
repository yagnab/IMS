using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.BL;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using IMS.BL.DataModel;
using IMS.UI;
using IMS.UI.ViewModels;
using System.Data.Objects;
using IMS.BL.Repositories;
using System.Text.RegularExpressions;
using IMS.BL.Validation;

namespace IMS.BL.Testbed
{
    class Test
    {
        List<int> testList = new List<int>()
        {
            1,2,3
        };
        public List<int> TestList
        {
            get
            {
                return testList;
            }
            set
            {
                testList = value;
            }
        }

        public void PrinttestList()
        {
            testList.ForEach(i => Console.WriteLine(i));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Test();
            test.TestList.Add(12);
            test.PrinttestList();

            NoF5Needed();
        }
        public static void TestBoundryConditionsOfDescriptionLength()
        {
            string descriptionLower = "aaaa";
            string descriptionUpper = "";
            for(int x=0; x<301; x++)
            {
                descriptionUpper += "a";
            }

            var upperdescValidator = new DescriptionValidation(descriptionUpper);
            var lowerDescValidator = new DescriptionValidation(descriptionLower);

            Console.WriteLine(upperdescValidator.isDescriptionLengthOk);
            Console.WriteLine(lowerDescValidator.isDescriptionLengthOk);
        }
        public static void TestAddNewCurrentDelivery(DateTime expectedArrivalDate)
        {
            //getting all items
            IEnumerable<Item> allItems;
            using (var iRepo = new ItemRepository(new InventoryContext()))
            {
                allItems = iRepo.GetAll();
            }

            //getting vars
            
            //getting dict
            var itemsToQuantity = new Dictionary<Item, int>();
            foreach(Item item in allItems.Take(1))
            {
                itemsToQuantity.Add(item, 1);
            }
            //getting supplier
            Supplier supplier;
            using (var sRepo = new SupplierRepo(new InventoryContext()))
            {
                supplier = sRepo.GetByID(1);
                sRepo.Complete();
            }

            //adding new cd
            using (var cdRepo = new CurrentDeliveriesRepo(new InventoryContext()))
            {
                cdRepo.AddNewCurrentDelivery(itemsToQuantity, expectedArrivalDate, supplier);
                cdRepo.Complete();
            }
        }
        public static void CreateNewDelivery(DateTime expectedArrivalDate)
        {
            //vars needed
            Supplier supplier;

            //getting supplier
            using (var sRepo = new SupplierRepo(new InventoryContext()))
            {
                supplier = sRepo.GetByID(1);
            }

            //creating new delivery
            var NewCurrentDelivery = new CurrentDelivery()
            {
                Supplier = supplier,
                IsArrived = false,
                ExpectedArrivalDate = expectedArrivalDate
            };

            using (var cdRepo = new CurrentDeliveriesRepo(new InventoryContext()))
            {
                cdRepo.Add(NewCurrentDelivery);
                cdRepo.Complete();
            }

        }
        public static void TestGetAllCurrentDeliveries()
        {
            IEnumerable<CurrentDelivery> allCDs;
            using (var cdRepo = new CurrentDeliveriesRepo(new InventoryContext()))
            {
                allCDs = cdRepo.GetAll();
            }
            foreach(CurrentDelivery cd in allCDs)
            {
                Console.WriteLine(cd.DeliveryID.ToString() + " " + cd.ExpectedArrivalDate);
            }
        }
        public static void PrintGraphTimePeriodUsingExtensionMethods()
        {
            GraphTimePeriod pastDay = GraphTimePeriod.PastDay;
            GraphTimePeriod pastHour = GraphTimePeriod.PastHour;
            GraphTimePeriod pastWeek = GraphTimePeriod.PastWeek;
            GraphTimePeriod pastYear = GraphTimePeriod.PastYear;
            Console.WriteLine(pastDay.StringRepresentation());
            Console.WriteLine(pastHour.StringRepresentation());
            Console.WriteLine(pastWeek.StringRepresentation());
            Console.WriteLine(pastYear.StringRepresentation());
        }
        public static void PrintItemCatagoryUsingExtensionMethods()
        {
            ItemCatagory alchol = ItemCatagory.Alchol;
            ItemCatagory chilled = ItemCatagory.Chilled;
            ItemCatagory confectionary = ItemCatagory.Confectionery;
            ItemCatagory  softDrink = ItemCatagory.SoftDrink;
            Console.WriteLine(alchol.StringRepresentation());
            Console.WriteLine(chilled.StringRepresentation());
            Console.WriteLine(confectionary.StringRepresentation());
            Console.WriteLine(softDrink.StringRepresentation());
        }
        public static void AddItemToDbTest(Item item)
        {
            using(var iRepo = new ItemRepository(new InventoryContext()))
            {
                iRepo.Add(item);
                iRepo.Complete();
            }
        }
        public static bool test(Func<int, bool> func, int number)
        {
            return func(number);
        }

        static void CreateNewStaffAccountUsingUserAccount()
        {
            //Create new account
            string username = "testStaffUser";
            string password = UserAccount.stringToHashString("testStaffUser");
            bool isAnalyticsAllowed = true;
            bool isEditTablesAllowed = true;
            bool isAddDeliveryAllowed = true;

            UserAccount staff = StaffAccount.GetNewStaffAccount(username, password, isAnalyticsAllowed, isEditTablesAllowed, isAddDeliveryAllowed);

            //Commit to db
            using (var dbContext = new InventoryContext())
            {
                try
                {
                    dbContext.UserAccounts.Add(staff);
                    dbContext.SaveChanges();

                    //Did work
                    Console.WriteLine("It worked");
                }
                catch (Exception ex)
                {
                    //Didn't work
                    Console.WriteLine(ex.ToString());
                }

            }
        }
        static void NoF5Needed()
        {
            Console.Write("Press any key to continue . . .");
            Console.ReadKey();
        }
        static bool IsUserSure()
        {
            Console.Write("You you want to continue?");
            var key = Console.ReadKey();
            if (key.KeyChar == 'y')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void testIfNewAdminWorks(string username, string password)
        {
            bool isAdmin = true;
            AdminAccount newAdmin = new AdminAccount()
            {
                Username = username,
                Password = UserAccount.stringToHashString(password),
                IsAdmin = isAdmin,
            };

            using(var dbContext = new InventoryContext())
            {
                dbContext.UserAccounts.Add(newAdmin);
            }

        }
        
        //Testing if turning CurrentDelivery to PastDelivery works
        public static void TestIfCreatePastDeliveryWorks()
        {
            using (var dbContext = new InventoryContext())
            {
                //Creating a new PastDelivery object
                CurrentDelivery CurrDel = dbContext.Deliveries.OfType<CurrentDelivery>()
                                    .First();
                
                
                var ActualArrivalDate = DateTime.Now.AddDays(1);

                var PastDel = UIObjectCreator.CreatePastDelivery(CurrDel, ActualArrivalDate, dbContext);

                //Testing if it worked
                if(PastDel == null)
                {
                    Console.WriteLine("It's null again. :(");
                }
                else
                {
                    var PastDelFromDB = dbContext.Deliveries.OfType<PastDelivery>()
                                    .Where(pd => pd.ActualArrivalDate == ActualArrivalDate)
                                    .First();
                    Console.WriteLine(
                        string.Format("DeliveryID:{0}, ActualArrivalDate:{1}, IsArrived:{2}"
                        , PastDelFromDB.DeliveryID
                        , PastDelFromDB.ActualArrivalDate
                        , PastDelFromDB.IsArrived)
                        );
                }
            }
        }

        //This and ReduceItemQuantityStockLevel() works!
        public static void TestIfCreateNewReservationWorks()
        {
            using(var dbContext = new InventoryContext())
            {
                var allItems = dbContext.Items;
                var _ItemToQuantity = new Dictionary<Item, int>();
                var _ExpectedPickUpDate = new DateTime(2017, 12, 12, 23, 0, 0);

                foreach(Item item in allItems)
                {
                    _ItemToQuantity.Add(item, 1);
                }

                UIObjectCreator.CreateNewCurrentReservation(_ItemToQuantity, _ExpectedPickUpDate, dbContext);

                //check if it worked
                var currRes = (from r in dbContext.Reservations.OfType<CurrentReservation>()
                               where r.ExpectedPickUpDate == _ExpectedPickUpDate
                               select r).First();

                Console.WriteLine(
                    string.Format("ReservationID:{0}, IsPickedUp{1}, ExpectedPickUpDate:{2}"
                        ,currRes.ReservationID, currRes.IsPickedUp, currRes.ExpectedPickUpDate)
                        );
            }
        }

        //Yes it does work
        public static void TestIfCreateCurrDelWorks(DateTime _expectedArrivalDate)
        {
            using (var dbContext = new InventoryContext())
            {
                Dictionary<Item, int> _itemToItemQuantity = new Dictionary<Item, int>();
                var _supplier = dbContext.Suppliers.First();

                var allItems = dbContext.Items;

                foreach (Item item in allItems)
                {
                    _itemToItemQuantity.Add(item, 12);
                }

                UIObjectCreator.CreateNewCurrentDelivery(_itemToItemQuantity, _expectedArrivalDate, _supplier, dbContext);

                //checking if worked
                var currDel = (from del in dbContext.Deliveries.OfType<CurrentDelivery>()
                               where del.ExpectedArrivalDate == _expectedArrivalDate
                               select del).First();
                Console.WriteLine(
                    string.Format("DeliveryID:{0}, ExpectedArrivalDate:{1}, SupplierName:{2}, IsArrived:{3}"
                        , currDel.DeliveryID, currDel.ExpectedArrivalDate, currDel.Supplier.SupplierName, currDel.IsArrived)
                    );
            }
        }

        public static void PrintTenFastestItems()
        {
            using (var dbContext = new InventoryContext())
            {
                //reverses, so fastest selling first
                List<Item> orderedItems = dbContext.Items
                    .OrderBy(i => i.QuantityWeaklySaleRate)
                    .ToList();
                orderedItems.Reverse();

                foreach (Item item in orderedItems)
                {
                    Console.WriteLine(
                        string.Format("ItemID: {0}, Sale Rate: {1}", item.ItemID, item.QuantityWeaklySaleRate)
                        );
                }
            }
        }

        public static void CheckIfUpdateSaleRateWorks()
        {
            using (var dbContext = new InventoryContext())
            {
                UIUtility.UpdateAllItemSaleRate();
                foreach (Item item in dbContext.Items)
                {
                    Console.WriteLine(
                        string.Format("ItemID: {0}, SaleRate: {1}", item.ItemID, item.QuantityWeaklySaleRate)
                        );
                }

                Console.ReadKey();
            }
        }
        public static void UpdateQuantityWeaklySaleRate()
        {
            TimeSpan week = TimeSpan.FromDays(7 * 1);
            DateTime weekBeforeNow = DateTime.Now.Date.Subtract(week);

            using (var dbContext = new InventoryContext())
            {
                //select all items where a transaction happened in past week
                List<ItemTransaction> i_ts = (from i in dbContext.Items
                                              from i_t in i.ItemTransactions
                                              where i_t.Transaction.TimeOfTransaction >= weekBeforeNow
                                              select i_t).ToList();

                Console.WriteLine("All item sales rates (Should be 0)");
                //sets all sale rates to 0
                foreach (Item i in dbContext.Items)
                {
                    i.QuantityWeaklySaleRate = 0;
                    dbContext.SaveChanges();
                    Console.WriteLine(i.QuantityWeaklySaleRate);
                }

                Console.WriteLine("Incrementing each item sale rate\nShould only increment by 12\nOnly for item w/ ID == 1 || 2");
                //each time an item is sold, increment its sale rate
                //by quantity sold
                foreach (ItemTransaction i_t in i_ts)
                {
                    var tempItem = (from i in dbContext.Items
                                    where i.ItemID == i_t.ItemID
                                    select i).First();
                    tempItem.QuantityWeaklySaleRate += i_t.Quantity;
                    dbContext.SaveChanges();
                    Console.WriteLine(
                        string.Format("ID: {0}, WeaklySaleRate: {1}", tempItem.ItemID, tempItem.QuantityWeaklySaleRate));
                }
            }
        }
        public static void TransactionIncludingItemTransaction()
        {
            using (var dbContext = new InventoryContext())
            {
                var allTransactions = dbContext.Transactions.Include("ItemTransactions");

                foreach (Transaction t in allTransactions)
                {
                    Console.WriteLine(string.Format("Transaction ID: {0}", t.TransactionID));
                    for (int i = 0; i < t.ItemTransactions.Count; i++)
                    {
                        Console.WriteLine(string.Format("ItemID: {0}, Item Description: {1}, ItemQuantity: {2}", t.ItemTransactions[i].ItemID,
                            t.ItemTransactions[i].Item.Description,
                            t.ItemTransactions[i].Quantity));
                    }
                }
            }
            
            
        }

        /// <summary>
        /// Dangerous!!
        /// Don't use
        /// </summary>
        public static void CreateNewTransaction()
        {
            using (var dbContext = new InventoryContext())
            {
                var allItems = from item in dbContext.Items
                               where item.ItemID == 1 || item.ItemID == 2
                               select item;

                Dictionary<Item, int> itemToQuantity = new Dictionary<Item, int>();

                //buy quantity of 12 of all items
                foreach (Item item in allItems)
                {
                    itemToQuantity.Add(item, 12);
                }

                UIObjectCreator.CreateNewTransaction(itemToQuantity);
                Console.WriteLine((from transaction in dbContext.Transactions select transaction).First().TransactionID);

                var allI_Ts = from i_t in dbContext.ItemTransactions select i_t;
                foreach (ItemTransaction i_t in allI_Ts)
                {
                    Console.WriteLine(string.Format("TransactionID: {0}\nItemID: {1}\nQuantity{2}\n", i_t.TransactionID, i_t.ItemID, i_t.Quantity));
                }
            }

        }

        public static string returnDateTimeString()
        {
            var tstDate = new DateTime(2017, 04, 14);
            return tstDate.ToString("yyyy-MM-dd h:mm tt");
        }
    }
}