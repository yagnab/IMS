using System.Data.Entity;
using IMS.BL;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace IMS.BL.DataModel
{
    public class InventoryContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //establish tpt(table per type) for delivery
            modelBuilder.Entity<Delivery>().ToTable("Delivery");
            modelBuilder.Entity<CurrentDelivery>().ToTable("CurrentDelivery");
            modelBuilder.Entity<PastDelivery>().ToTable("PastDelivery");

            //establish TPT for reservations
            modelBuilder.Entity<Reservation>().ToTable("Reservation");
            modelBuilder.Entity<CurrentReservation>().ToTable("CurrentReservation");
            modelBuilder.Entity<PastReservation>().ToTable("PastReservation");

            //estblish TPT for UserAccount
            modelBuilder.Entity<UserAccount>().ToTable("UserAccount");
            modelBuilder.Entity<AdminAccount>().ToTable("AdminAccount");
            modelBuilder.Entity<StaffAccount>().ToTable("StaffAccount");

            //Making UserAccount.Username a unique constraint
            
            
        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ItemDelivery> ItemDeliveries { get; set; }
        public DbSet<ItemReservation> ItemReservations { get; set; }
        public DbSet<ItemTransaction> ItemTransactions { get; set; }
    }
}
