namespace IMS.BL.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedCurrentDeliveryTblNameTypo : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DurrentDelivery", newName: "CurrentDelivery");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CurrentDelivery", newName: "DurrentDelivery");
        }
    }
}
