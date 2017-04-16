using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.Repositories
{
    public interface IUnitOfWork 
    {
        //Repos
        IItemRepository Items { get; }
        IItemTransactionsRepo ItemTransactions { get; }
        IAdminAccountsRepo AdminAccounts { get; }
        ICurrentDeliveriesRepo CurrentDeliveries { get; }
        ICurrentReservationsRepo CurrentReservations { get; }
        IItemDeliveriesRepo ItemDeliveries { get; }
        IItemReservationsRepo ItemReservations { get; }
        IPastDeliveriesRepo PastDeliveries { get; }
        IPastReservationsRepo PastReservations { get; }
        IStaffAccountsRepo StaffAccounts { get; }
        void Complete();
        void Dispose();
    }
}
