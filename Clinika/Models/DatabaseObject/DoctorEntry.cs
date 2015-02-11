using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Clinika.Models.DatabaseObject
{
    public class DoctorEntry
    {
        [Key]
        public int DoctorId { get; set; }

        //[Required]
        //[StringLength(50, ErrorMessage = "Doctor Name max length is 50")]
        //[Remote("IsAvailable", "Validation")]
        [Display(Name = "Doctor Name")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Degree is required")]
        //[StringLength(50, ErrorMessage = "Doctor Degree max length is 50")]
        [DisplayName("Degree")]
        public string Degree { get; set; }

        //[Required(ErrorMessage = "Specialization is required")]
        //[StringLength(100, ErrorMessage = "Doctor Specialization max length is 100")]
        [DisplayName("Specialization")]
        public string Specialization { get; set; }
       // public virtual ICollection<Treatments> Treatments { set; get; }
    }

}