using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models
{
    public partial class CANDIDATE_STATUS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CANDIDATE_STATUS()
        {
            this.CANDIDATE_PROCESS = new HashSet<CANDIDATE_PROCESS>();
        }

        public int id_status { get; set; }
        public string status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CANDIDATE_PROCESS> CANDIDATE_PROCESS { get; set; }
    }
}