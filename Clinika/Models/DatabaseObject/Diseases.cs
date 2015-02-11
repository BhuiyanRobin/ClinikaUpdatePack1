using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CliniKa.Models.DatabaseObject;

namespace Clinika.Models.DatabaseObject
{
    public class Diseases
    {
        public int DiseasesId { get; set; }

        [DisplayName("Disease Name")]
        [Required(ErrorMessage = "Disease name required")]
        public string Name { get; set; }
        [DisplayName("Disease Description")]
        [Required(ErrorMessage = "Disease description required")]
        public string Description { get; set; }

        [DisplayName("Treatments Procedure")]
        [Required(ErrorMessage = "Treatments procedure required")]
        public string TreatmentProcedure { get; set; }

        [DisplayName("Preferd Medicien")]
        [Required(ErrorMessage = "Preferd Medicien required")]
        public string PreferdMedicien { get; set; }

    }
}