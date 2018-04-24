using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models
{
    public partial class REGISTRATION_CANDIDATE
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string password { get; set; }
        public int id_candidate_registration { get; set; }
    }
}