using IMS.BL.DataModel;

namespace IMS.BL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly InventoryContext Context;
        //repos
        public IItemRepository Items { get; private set; }
        public IItemTransactionsRepo ItemTransactions { get; private set; }
        public IAdminAccountsRepo AdminAccounts { get; private set; }
        public ICurrentDeliveriesRepo CurrentDeliveries { get; private set; }
        public ICurrentReservationsRepo CurrentReservations { get; private set; }
        public IItemDeliveriesRepo ItemDeliveries { get; private set; }
        public IItemReservationsRepo ItemReservations { get; private set; }
        public IPastDeliveriesRepo PastDeliveries { get; private set; }
        public IPastReservationsRepo PastReservations { get; private set; }
        public IStaffAccountsRepo StaffAccounts { get; private set; }
        
        //constructor
        public UnitOfWork()
        {
            Context = new InventoryContext();
            Items = new ItemRepository(Context);
            ItemTransactions = new ItemTransactionsRepo(Context);
            AdminAccounts = new AdminAccountsRepo(Context);
            CurrentDeliveries = new CurrentDeliveriesRepo(Context);
            CurrentReservations = new CurrentReservationsRepo(Context);
            ItemDeliveries = new ItemDeliveriesRepo(Context);
            ItemReservations = new ItemReservationsRepo(Context);
            PastDeliveries = new PastDeliveriesRepo(Context);
            PastReservations = new PastReservationsRepo(Context);
            StaffAccounts = new StaffAccountsRepo(Context);
        }
        
        //allows the use of using(UnitOfWork){}
        //saves changes to database before it is disposed
        public void Dispose()
        {
            Context.SaveChanges();
            Context.Dispose();
        }
    }
}
