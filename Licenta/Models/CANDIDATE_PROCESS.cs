using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models
{
    public partial class CANDIDATE_PROCESS
    {
        public int id_candidate { get; set; }
        public int id_company { get; set; }
        public int id_status { get; set; }
        public int id_process { get; set; }

        public virtual CANDIDATE_STATUS CANDIDATE_STATUS { get; set; }
        public virtual CANDIDATES CANDIDATES { get; set; }
        public virtual COMPANIES COMPANIES { get; set; }
    }
}