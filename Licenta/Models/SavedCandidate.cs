using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Licenta.Models
{
    [Table("SAVED_LIST")]
    public class SavedCandidate
    {
        [Key]
        public int Id_SavedCandidate { get; set; }

        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Country_to_work { get; set; }
        public string Skill { get; set; }
        public string Experience { get; set; }
        public string Status { get; set; }
        public string Process { get; set; }
        public string Email { get; set; }

    }
}