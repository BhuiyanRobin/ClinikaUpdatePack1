using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CliniKa.Models.DatabaseObject;
using Clinika.Models.DatabaseObject;
using Clinika.Models.Relations;

namespace Clinika.Models.Gateway
{
    public class Gateway : DbContext
    {
        public Gateway()
            : base("connection")
        {
            Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<District> Districts { set; get; }
        public DbSet<Upazila> Upazilas { set; get; }
        public DbSet<DistrictUpazila> DistrictUpazilaRelation { set; get; }
        public DbSet<ServiceCenter> ServiceCenters { set; get; }
        public DbSet<Medicine> Medicines { set; get; }
        public DbSet<Diseases> Diseaseses { set; get; }
        public DbSet<DoctorEntry> Doctors { set; get; }
        public DbSet<Dose> Doses { set; get; }
        public DbSet<Meal> Meals { set; get; }
        public DbSet<AllocateMedicine> AllocateMedicines { set; get; }
        public DbSet<PatientCount> PatientList { set; get; }
        public DbSet<PatientInformation> PatientInformations { set; get; }
        public DbSet<TreatmentMedicinegiven> TreatmentMedicinegivens { set; get; }
        public DbSet<TreatmentRelation> TreatmentRelations { set; get; }
        public DbSet<Treatments> Treatmentses { set; get; } 
        public System.Data.Entity.DbSet<Clinika.Models.DatabaseObject.DieaseAffectInfo> DieaseAffectInfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}