using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Clinika.Models.Relations
{
    public class DiseaseWiseReport
    {
        public string DistrictName { set; get; }
        public int TotalPatient { set; get; }
        public double PercentOverPopulation { set; get; }
    }
}