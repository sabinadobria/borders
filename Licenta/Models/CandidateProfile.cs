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

        [Display(Name ="Last Name")]
        public string Last_name { get; set; }

        [Display(Name = "First Name")]
        public string First_name { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        [Display(Name = "About Me")]
        public string AboutMe { get; set; }

        public string Country_To_Work { get; set; }
        public string Interest { get; set; }

        public List<CandidateExperience> CandidateExperience { get; set; } 
        public List<CandidateStudies> CandidateStudies { get; set; }
        public List<CandidateTechnologies> CandidateTechnologies { get; set; }
        public List<CandidateLanguages> CandidateLanguages { get; set; }

    }
}