using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using CliniKa.Models.DatabaseObject;

namespace Clinika.Models.DatabaseObject
{
    public class TreatmentMedicinegiven
    {
        public int TreatmentMedicinegivenId { set; get; }
        public string VoterId { set; get; }
        [DisplayName("Disease")]
        public int DiseasesId { get; set; }
        [DisplayName("Medicien")]
        public int MedicineId { get; set; }
        [DisplayName("Dose")]
        public int DoseId { get; set; }
        [DisplayName("Meal")]
        public int MealId { set; get; }

        [DisplayName("Quantity Given")]
        public int QuantityGiven { get; set; }

        [DisplayName("Note")]
        public string Note { get; set; }
        public virtual Meal AMeal { set; get; }
        public virtual Dose ADose { get; set; }
        public virtual Medicine AMedicine { get; set; }
        public virtual Diseases ADiseases { get; set; }
      
    }
}