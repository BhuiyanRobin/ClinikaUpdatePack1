namespace Clinika.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TreatmentMedicinegiven", "SevcieCenterCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TreatmentMedicinegiven", "SevcieCenterCode");
        }
    }
}
