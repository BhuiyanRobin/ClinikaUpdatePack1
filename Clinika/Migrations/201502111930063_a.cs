namespace Clinika.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AllocateMedicine",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceCenterId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        MedicineId = c.Int(nullable: false),
                        DistrictUpazilaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicine", t => t.MedicineId, cascadeDelete: true)
                .ForeignKey("dbo.ServiceCenter", t => t.ServiceCenterId, cascadeDelete: true)
                .Index(t => t.ServiceCenterId)
                .Index(t => t.MedicineId);
            
            CreateTable(
                "dbo.Medicine",
                c => new
                    {
                        MedicineId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Power = c.String(nullable: false),
                        PowerUnit = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MedicineId);
            
            CreateTable(
                "dbo.ServiceCenter",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(),
                        Password = c.String(),
                        DistrictId = c.Int(nullable: false),
                        UpazilaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.District", t => t.DistrictId, cascadeDelete: true)
                .ForeignKey("dbo.Upazila", t => t.UpazilaId, cascadeDelete: true)
                .Index(t => t.DistrictId)
                .Index(t => t.UpazilaId);
            
            CreateTable(
                "dbo.District",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Population = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Upazila",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DieaseAffectInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistrictId = c.Int(nullable: false),
                        DiseasesId = c.Int(nullable: false),
                        FromDateTime = c.DateTime(nullable: false),
                        ToDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Diseases",
                c => new
                    {
                        DiseasesId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        TreatmentProcedure = c.String(nullable: false),
                        PreferdMedicien = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DiseasesId);
            
            CreateTable(
                "dbo.DistrictUpazila",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistrictId = c.Int(nullable: false),
                        UpazilaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DoctorEntry",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Degree = c.String(),
                        Specialization = c.String(),
                    })
                .PrimaryKey(t => t.DoctorId);
            
            CreateTable(
                "dbo.Dose",
                c => new
                    {
                        DoseId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DoseId);
            
            CreateTable(
                "dbo.Meal",
                c => new
                    {
                        MealId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.MealId);
            
            CreateTable(
                "dbo.PatientInformation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoterId = c.String(),
                        Name = c.String(),
                        Address = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PatientCount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoterId = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        DiseasesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TreatmentMedicinegiven",
                c => new
                    {
                        TreatmentMedicinegivenId = c.Int(nullable: false, identity: true),
                        VoterId = c.String(),
                        DiseasesId = c.Int(nullable: false),
                        MedicineId = c.Int(nullable: false),
                        DoseId = c.Int(nullable: false),
                        MealId = c.Int(nullable: false),
                        QuantityGiven = c.Int(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.TreatmentMedicinegivenId)
                .ForeignKey("dbo.Diseases", t => t.DiseasesId, cascadeDelete: true)
                .ForeignKey("dbo.Dose", t => t.DoseId, cascadeDelete: true)
                .ForeignKey("dbo.Meal", t => t.MealId, cascadeDelete: true)
                .ForeignKey("dbo.Medicine", t => t.MedicineId, cascadeDelete: true)
                .Index(t => t.DiseasesId)
                .Index(t => t.MedicineId)
                .Index(t => t.DoseId)
                .Index(t => t.MealId);
            
            CreateTable(
                "dbo.TreatmentRelation",
                c => new
                    {
                        TreatmentRelationId = c.Int(nullable: false, identity: true),
                        VoterId = c.String(),
                        Observation = c.String(),
                        DateOfObservation = c.DateTime(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        ServiceCenterCode = c.String(),
                    })
                .PrimaryKey(t => t.TreatmentRelationId);
            
            CreateTable(
                "dbo.Treatments",
                c => new
                    {
                        TreatmentsId = c.Int(nullable: false, identity: true),
                        VoterId = c.String(),
                        Name = c.String(),
                        Address = c.String(),
                        DateOfBirht = c.DateTime(nullable: false),
                        ServiceGiven = c.Int(nullable: false),
                        Observation = c.String(),
                        Date = c.DateTime(nullable: false),
                        DoctorEntryId = c.Int(nullable: false),
                        DiseasesId = c.Int(nullable: false),
                        MedicineId = c.Int(nullable: false),
                        DoseId = c.Int(nullable: false),
                        MealId = c.Int(nullable: false),
                        QuantityGiven = c.String(),
                        Note = c.String(),
                        ADoctorEntry_DoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.TreatmentsId)
                .ForeignKey("dbo.Diseases", t => t.DiseasesId, cascadeDelete: true)
                .ForeignKey("dbo.DoctorEntry", t => t.ADoctorEntry_DoctorId)
                .ForeignKey("dbo.Dose", t => t.DoseId, cascadeDelete: true)
                .ForeignKey("dbo.Meal", t => t.MealId, cascadeDelete: true)
                .ForeignKey("dbo.Medicine", t => t.MedicineId, cascadeDelete: true)
                .Index(t => t.DiseasesId)
                .Index(t => t.MedicineId)
                .Index(t => t.DoseId)
                .Index(t => t.MealId)
                .Index(t => t.ADoctorEntry_DoctorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Treatments", "MedicineId", "dbo.Medicine");
            DropForeignKey("dbo.Treatments", "MealId", "dbo.Meal");
            DropForeignKey("dbo.Treatments", "DoseId", "dbo.Dose");
            DropForeignKey("dbo.Treatments", "ADoctorEntry_DoctorId", "dbo.DoctorEntry");
            DropForeignKey("dbo.Treatments", "DiseasesId", "dbo.Diseases");
            DropForeignKey("dbo.TreatmentMedicinegiven", "MedicineId", "dbo.Medicine");
            DropForeignKey("dbo.TreatmentMedicinegiven", "MealId", "dbo.Meal");
            DropForeignKey("dbo.TreatmentMedicinegiven", "DoseId", "dbo.Dose");
            DropForeignKey("dbo.TreatmentMedicinegiven", "DiseasesId", "dbo.Diseases");
            DropForeignKey("dbo.AllocateMedicine", "ServiceCenterId", "dbo.ServiceCenter");
            DropForeignKey("dbo.ServiceCenter", "UpazilaId", "dbo.Upazila");
            DropForeignKey("dbo.ServiceCenter", "DistrictId", "dbo.District");
            DropForeignKey("dbo.AllocateMedicine", "MedicineId", "dbo.Medicine");
            DropIndex("dbo.Treatments", new[] { "ADoctorEntry_DoctorId" });
            DropIndex("dbo.Treatments", new[] { "MealId" });
            DropIndex("dbo.Treatments", new[] { "DoseId" });
            DropIndex("dbo.Treatments", new[] { "MedicineId" });
            DropIndex("dbo.Treatments", new[] { "DiseasesId" });
            DropIndex("dbo.TreatmentMedicinegiven", new[] { "MealId" });
            DropIndex("dbo.TreatmentMedicinegiven", new[] { "DoseId" });
            DropIndex("dbo.TreatmentMedicinegiven", new[] { "MedicineId" });
            DropIndex("dbo.TreatmentMedicinegiven", new[] { "DiseasesId" });
            DropIndex("dbo.ServiceCenter", new[] { "UpazilaId" });
            DropIndex("dbo.ServiceCenter", new[] { "DistrictId" });
            DropIndex("dbo.AllocateMedicine", new[] { "MedicineId" });
            DropIndex("dbo.AllocateMedicine", new[] { "ServiceCenterId" });
            DropTable("dbo.Treatments");
            DropTable("dbo.TreatmentRelation");
            DropTable("dbo.TreatmentMedicinegiven");
            DropTable("dbo.PatientCount");
            DropTable("dbo.PatientInformation");
            DropTable("dbo.Meal");
            DropTable("dbo.Dose");
            DropTable("dbo.DoctorEntry");
            DropTable("dbo.DistrictUpazila");
            DropTable("dbo.Diseases");
            DropTable("dbo.DieaseAffectInfo");
            DropTable("dbo.Upazila");
            DropTable("dbo.District");
            DropTable("dbo.ServiceCenter");
            DropTable("dbo.Medicine");
            DropTable("dbo.AllocateMedicine");
        }
    }
}
