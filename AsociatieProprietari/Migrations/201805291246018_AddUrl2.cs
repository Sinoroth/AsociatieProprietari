namespace AsociatieProprietari.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InvoiceModels", "Paid", c => c.Boolean(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InvoiceModels", "Paid", c => c.Single(nullable: true));
        }
    }
}
