using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models
{
    public class CandidateStudies
    {
        public int Id { get; set; }
        public string School { get; set; }
        public string Diploma { get; set; }
        public DateTime From_date { get; set; }
        public DateTime To_date { get; set; }
        public string Description { get; set; }
    }
}