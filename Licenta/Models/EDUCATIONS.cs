﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models
{
    public partial class EDUCATIONS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EDUCATIONS()
        {
            this.CANDIDATES = new HashSet<CANDIDATES>();
        }

        public int id_education { get; set; }
        public string school { get; set; }
        public string diploma { get; set; }
        public System.DateTime from_date { get; set; }
        public System.DateTime to_date { get; set; }
        public string description { get; set; }
        public int id_candidate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CANDIDATES> CANDIDATES { get; set; }
        public virtual CANDIDATES CANDIDATES1 { get; set; }
    }
}