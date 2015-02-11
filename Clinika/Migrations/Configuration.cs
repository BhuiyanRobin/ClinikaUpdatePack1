using System.Collections.Generic;
using Clinika.Models.DatabaseObject;
using Clinika.Models.Relations;

namespace Clinika.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Clinika.Models.Gateway.Gateway>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Clinika.Models.Gateway.Gateway context)
        {
           //List<TreatmentRelation>treatmentRelations=new List<TreatmentRelation>()
           //{
           //    new TreatmentRelation(){VoterId = "5644309456813",DateOfObservation = Convert.ToDateTime("12/11/2012"), DoctorId = 1,Observation = "Its harmful",ServiceCenterCode = "Ha",}
           //};
           //treatmentRelations.ForEach(s => context.TreatmentRelations.AddOrUpdate(s));
           //context.SaveChanges();
            //List<PatientInformation>patientInformations=new List<PatientInformation>()
            //{
            //    new PatientInformation(){Address = "Dhaka",DateOfBirth =Convert.ToDateTime("12/11/2012"),VoterId ="5644309456813",Name = "Asif Zaman"}
            //};
            //patientInformations.ForEach(s => context.PatientInformations.AddOrUpdate(s));
            //context.SaveChanges();
        }
    }
}
