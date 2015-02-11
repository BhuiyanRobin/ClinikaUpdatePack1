using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CliniKa.Models.DatabaseObject
{
    public class Medicine
    {
        public int MedicineId { set; get; }
        [Required(ErrorMessage = "Please insert Medicine Name")]
        [Remote("IsAvialable", "Medicine",ErrorMessage = "come")]
        public string Name { set; get; }
        [Required(ErrorMessage = "Please insert Medicine Name")]
        public string Power { set; get; }
        [Required(ErrorMessage = "Please insert Medicine Name")]
        
        public string PowerUnit { set; get; }
         
    }
}