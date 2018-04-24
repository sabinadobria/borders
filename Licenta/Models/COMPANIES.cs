using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models
{
    public partial class COMPANIES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public COMPANIES()
        {
            this.CANDIDATE_PROCESS = new HashSet<CANDIDATE_PROCESS>();
        }

        public int id_company { get; set; }
        public string company_name { get; set; }
        public string company_website { get; set; }
        public string CEO { get; set; }
        public Nullable<int> no_employees { get; set; }
        public string city { get; set; }
        public Nullable<System.DateTime> date_creation { get; set; }
        public Nullable<int> annual_income { get; set; }
        public string description { get; set; }
        public string technical_stack { get; set; }
        public int spoken_language { get; set; }
        public string social_network { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CANDIDATE_PROCESS> CANDIDATE_PROCESS { get; set; }
        public virtual LANGUAGES LANGUAGES { get; set; }
    }
}