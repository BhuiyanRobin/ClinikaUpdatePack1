using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace CliniKa.Models.DatabaseObject
{
    public class District
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int Population { set; get; }
        public virtual ICollection<ServiceCenter> ServiceCenters { set; get; }
        
        }
}