namespace IMS.BL.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTPTUserAccountAndReservation : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Reservations", newName: "Reservation");
            RenameTable(name: "dbo.UserAccounts", newName: "UserAccount");
            CreateTable(
                "dbo.CurrentReservation",
                c => new
                    {
                        ReservationID = c.Int(nullable: false),
                        ExpectedPickUpDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationID)
                .ForeignKey("dbo.Reservation", t => t.ReservationID)
                .Index(t => t.ReservationID);
            
            CreateTable(
                "dbo.PastReservation",
                c => new
                    {
                        ReservationID = c.Int(nullable: false),
                        ActualPickUpDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationID)
                .ForeignKey("dbo.Reservation", t => t.ReservationID)
                .Index(t => t.ReservationID);
            
            CreateTable(
                "dbo.AdminAccount",
                c => new
                    {
                        UserAccountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserAccountID)
                .ForeignKey("dbo.UserAccount", t => t.UserAccountID)
                .Index(t => t.UserAccountID);
            
            CreateTable(
                "dbo.StaffAccount",
                c => new
                    {
                        UserAccountID = c.Int(nullable: false),
                        IsAddDeliveryAllowed = c.Boolean(nullable: false),
                        IsEditTablesAllowed = c.Boolean(nullable: false),
                        IsAnalyticsAllowed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserAccountID)
                .ForeignKey("dbo.UserAccount", t => t.UserAccountID)
                .Index(t => t.UserAccountID);
            
            DropColumn("dbo.Reservation", "ExpectedPickUpDate");
            DropColumn("dbo.Reservation", "ActualPickUpDate");
            DropColumn("dbo.UserAccount", "IsAddDeliveryAllowed");
            DropColumn("dbo.UserAccount", "IsEditTablesAllowed");
            DropColumn("dbo.UserAccount", "IsAnalyticsAllowed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserAccount", "IsAnalyticsAllowed", c => c.Boolean());
            AddColumn("dbo.UserAccount", "IsEditTablesAllowed", c => c.Boolean());
            AddColumn("dbo.UserAccount", "IsAddDeliveryAllowed", c => c.Boolean());
            AddColumn("dbo.Reservation", "ActualPickUpDate", c => c.DateTime());
            AddColumn("dbo.Reservation", "ExpectedPickUpDate", c => c.DateTime());
            DropForeignKey("dbo.StaffAccount", "UserAccountID", "dbo.UserAccount");
            DropForeignKey("dbo.AdminAccount", "UserAccountID", "dbo.UserAccount");
            DropForeignKey("dbo.PastReservation", "ReservationID", "dbo.Reservation");
            DropForeignKey("dbo.CurrentReservation", "ReservationID", "dbo.Reservation");
            DropIndex("dbo.StaffAccount", new[] { "UserAccountID" });
            DropIndex("dbo.AdminAccount", new[] { "UserAccountID" });
            DropIndex("dbo.PastReservation", new[] { "ReservationID" });
            DropIndex("dbo.CurrentReservation", new[] { "ReservationID" });
            DropTable("dbo.StaffAccount");
            DropTable("dbo.AdminAccount");
            DropTable("dbo.PastReservation");
            DropTable("dbo.CurrentReservation");
            RenameTable(name: "dbo.UserAccount", newName: "UserAccounts");
            RenameTable(name: "dbo.Reservation", newName: "Reservations");
        }
    }
}
