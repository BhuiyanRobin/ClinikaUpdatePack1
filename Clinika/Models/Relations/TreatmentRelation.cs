using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Antlr.Runtime.Tree;
using Clinika.Models.DatabaseObject;

namespace Clinika.Models.Relations
{
    public class TreatmentRelation
    {
        public int TreatmentRelationId { set; get; }
        public string VoterId { set; get; }
        public string Observation { get; set; }

        [DisplayName("Observation Date")]
        [DataType(DataType.Date)]
        public DateTime DateOfObservation { get; set; }
        [DisplayName("Doctor")]
        public int DoctorId { get; set; }
        public string ServiceCenterCode { set; get; }
    }
}