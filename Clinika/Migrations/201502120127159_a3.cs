namespace Clinika.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PatientCount", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PatientCount", "Date", c => c.DateTime(nullable: false));
        }
    }
}
