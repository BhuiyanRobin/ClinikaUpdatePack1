using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using CliniKa.Models.DatabaseObject;

namespace Clinika.Models.DatabaseObject
{
    public class PatientInformation
    {
        public int Id { get; set; }

        [DisplayName("Voter ID")]
        public string VoterId { get; set; }
        [DisplayName("Name")]
        public string Name { set; get; }
        [DisplayName("Address")]
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }
       
    }
}