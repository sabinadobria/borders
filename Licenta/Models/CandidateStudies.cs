using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Licenta.Models
{
    [Table("STUDIES")]
    public class CandidateStudies
    {
        [Key]
        public int id_education { get; set; }
        [Display(Name = "University")]
        [Required]
        public string school { get; set; }
        [Display(Name = "Diploma")]
        public string diploma { get; set; }
        [Display(Name = "Starting date")]
        public string from_date { get; set; }
        [Display(Name = "End date")]
        public string to_date { get; set; }
        [Display(Name = "Section")]
        public string section { get; set; }
        public int id_candidate { get; set; }
       
    }
}