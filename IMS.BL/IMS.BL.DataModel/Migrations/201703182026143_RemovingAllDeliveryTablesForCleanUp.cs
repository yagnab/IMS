namespace IMS.BL.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingAllDeliveryTablesForCleanUp : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Delivery", newName: "Deliveries");
            DropForeignKey("dbo.CurrentDelivery", "DeliveryID", "dbo.Delivery");
            DropForeignKey("dbo.PastDelivery", "DeliveryID", "dbo.Delivery");
            DropIndex("dbo.CurrentDelivery", new[] { "DeliveryID" });
            DropIndex("dbo.PastDelivery", new[] { "DeliveryID" });
            AddColumn("dbo.Deliveries", "ExpectedArrivalDate", c => c.DateTime());
            AddColumn("dbo.Deliveries", "ActualArrivalDate", c => c.DateTime());
            AddColumn("dbo.Deliveries", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.CurrentDelivery");
            DropTable("dbo.PastDelivery");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PastDelivery",
                c => new
                    {
                        DeliveryID = c.Int(nullable: false),
                        ActualArrivalDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DeliveryID);
            
            CreateTable(
                "dbo.CurrentDelivery",
                c => new
                    {
                        DeliveryID = c.Int(nullable: false),
                        ExpectedArrivalDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DeliveryID);
            
            DropColumn("dbo.Deliveries", "Discriminator");
            DropColumn("dbo.Deliveries", "ActualArrivalDate");
            DropColumn("dbo.Deliveries", "ExpectedArrivalDate");
            CreateIndex("dbo.PastDelivery", "DeliveryID");
            CreateIndex("dbo.CurrentDelivery", "DeliveryID");
            AddForeignKey("dbo.PastDelivery", "DeliveryID", "dbo.Delivery", "DeliveryID");
            AddForeignKey("dbo.CurrentDelivery", "DeliveryID", "dbo.Delivery", "DeliveryID");
            RenameTable(name: "dbo.Deliveries", newName: "Delivery");
        }
    }
}
