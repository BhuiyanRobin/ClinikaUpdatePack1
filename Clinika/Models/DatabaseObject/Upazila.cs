﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CliniKa.Models.DatabaseObject
{
    public class Upazila
    {
        public int Id { set; get; }
        public string Name { set; get; }
        
        public virtual ICollection<ServiceCenter> ServiceCenters { set; get; }

    }
}