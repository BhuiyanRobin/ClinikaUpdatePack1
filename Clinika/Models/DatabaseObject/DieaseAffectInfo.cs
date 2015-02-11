using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinika.Models.DatabaseObject
{
    public class DieaseAffectInfo
    {
        public int Id { set; get; }
        public int DistrictId { set; get; }
        public int DiseasesId { set; get; }
        public DateTime FromDateTime { set; get; }
        public DateTime ToDateTime { set; get; }
    }
}