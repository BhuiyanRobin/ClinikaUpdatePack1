namespace Clinika.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientCount", "DistrictId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientCount", "DistrictId");
        }
    }
}
