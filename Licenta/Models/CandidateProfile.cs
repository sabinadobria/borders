using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Licenta.Models
{
    [Table("CANDIDATES")]
    public class CandidateProfile
    {
        //contact data properties
        
        [Key]
        public int Id_candidate { set; get; }

        public string Last_name { get; set; }
        public string First_name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string AboutMe { get; set; }

        
        public List<CandidateExperience> CandidateExperience { get; set; } 
        public List<CandidateStudies> CandidateStudies { get; set; }
        public List<CandidateTechnologies> CandidateTechnologies { get; set; }
        public List<CandidateLanguages> CandidateLanguages { get; set; }
    }
}