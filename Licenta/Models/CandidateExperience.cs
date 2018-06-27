using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Licenta.Models
{

    [Table("EXPERIENCES")]
    public class CandidateExperience
    {
        [Key]
        public int Id_experience { get; set; }


        public int Id_candidate { get; set; }

        [Display(Name ="Company Name")]
        public string company_name { get; set; }

        [Display(Name ="From")]
        public DateTime from_date { get; set; }

        [Display(Name ="Until")]
        public DateTime to_date { get; set; }

        [Display(Name ="Job Description")]
        public string description { get; set; }

        [Display(Name ="Job Name")]
        public string position { get; set; }
    }
}