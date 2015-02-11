using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CliniKa.Models.DatabaseObject
{
    public class ServiceCenter
    {
        public int Id { set; get; }

        [Remote("IsNameAvailable","ServinceCenter")]
        [Required(ErrorMessage = "Name need to fill Up")]
        public string Name { set; get; }
        public string Code { set; get; }
        public string Password { set; get; }
        [DisplayName("Upazila")]
        [Required(ErrorMessage = "District need to fill Up")]
        public int DistrictId { set; get; }
        [DisplayName("District")]
        [Required(ErrorMessage = "Upazila need to fill Up")]
        public int UpazilaId { set; get; }
        public virtual District ADistrict { set; get; }
        public virtual Upazila AUpazila { set; get; }
        }
}