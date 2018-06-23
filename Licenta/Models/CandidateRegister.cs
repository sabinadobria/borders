using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Licenta.Models
{
    public partial class CandidateRegister
    {
        [Required]
        [Display (Name = "Email")]
        public string email { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string first_name { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string last_name { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string password { get; set; }
        public int id_candidate_registration { get; set; }
    }
}