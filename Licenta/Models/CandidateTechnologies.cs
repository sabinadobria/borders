using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Licenta.Models
{
    [Table("TECH")]
    public class CandidateTechnologies
    {
        [Key]
        public int id_tech { get; set; }
        [Display (Name = "Technology")]
        public string tech_name { get; set; }
        [Display (Name = "Level")]
        public string tech_level { get; set; }
        public int id_candidate { get; set; }
    }
}