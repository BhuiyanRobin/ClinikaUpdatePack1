using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinika.Models.Relations
{
    public class VoterInformation
    {
        public string VoterId { set; get; }
        public string Name { set; get; }
        public string Address { set; get; }
        public string DateOfbirth { set; get; }
        public int Servicegiven { set; get; }
    }
}