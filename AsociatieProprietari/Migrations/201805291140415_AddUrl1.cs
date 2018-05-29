namespace AsociatieProprietari.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FlatId = c.Int(nullable: false),
                        Value = c.Single(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.EmployeeModels", "AddDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.InvoiceModels", "InvoiceDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.InvoiceModels", "Paid", c => c.Single(nullable: false));
            AddColumn("dbo.WaterConsumptionModels", "Month", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WaterConsumptionModels", "Month");
            DropColumn("dbo.InvoiceModels", "Paid");
            DropColumn("dbo.InvoiceModels", "InvoiceDate");
            DropColumn("dbo.EmployeeModels", "AddDate");
            DropTable("dbo.PaymentsModels");
        }
    }
}
