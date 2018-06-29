using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Licenta.Models
{
    [Table ("LANGUAGES")]
    public class CandidateLanguages
    {
        [Key]
        public int id_language { get; set; }
        [Display (Name ="Language")]
        public string language { get; set; }
        [Display (Name = "Level")]
        public string language_level { get; set; }
        public int id_candidate { get; set; }
    }
}