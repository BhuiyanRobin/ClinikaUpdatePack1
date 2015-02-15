using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinika.Models.Relations
{
    public class PatientCount
    {
        public int Id { set; get; }
        public string VoterId { set; get; }
        public DateTime DateTime { set; get; }
        public int DiseasesId { set; get; }
        public int DistrictId { set; get; }
    }
}