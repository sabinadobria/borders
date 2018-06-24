using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models
{
    public class CandidateProfile
    {
        public int Id { set; get; }
        public string Last_name { get; set; }
        public string First_name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string AboutMe { get; set; }

        public IList<CandidateExperience> CandidateExperiences { get; set; }
        public IList<CandidateStudies> CandidateStudies { get; set; }
        public IList<CandidateTechnologies> CandidateTechnologies { get; set; }
        public IList<CandidateLanguages> CandidateLanguages { get; set; }

        public IList<CandidateExperience> GetCandidateExperiences()
        {
            return CandidateExperiences;
        }
    }
}