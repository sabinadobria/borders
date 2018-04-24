using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models
{
    public partial class CANDIDATES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CANDIDATES()
        {
            this.CANDIDATE_PROCESS = new HashSet<CANDIDATE_PROCESS>();
            this.EDUCATIONS1 = new HashSet<EDUCATIONS>();
            this.EXPERIENCES1 = new HashSet<EXPERIENCES>();
        }

        public int id_candidate { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string skills { get; set; }
        public string email { get; set; }
        public int language { get; set; }
        public int experience { get; set; }
        public int education { get; set; }
        public string hobbies { get; set; }
        public Nullable<int> saved_profile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CANDIDATE_PROCESS> CANDIDATE_PROCESS { get; set; }
        public virtual EDUCATIONS EDUCATIONS { get; set; }
        public virtual EXPERIENCES EXPERIENCES { get; set; }
        public virtual LANGUAGES LANGUAGES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EDUCATIONS> EDUCATIONS1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXPERIENCES> EXPERIENCES1 { get; set; }
    }
}