using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Licenta.Models
{
    public class NoBordersDB : DbContext
    {
        public DbSet<CandidateProfile> CandidateProfiles { get; set; }
        public DbSet<CandidateExperience> CandidateExperiences { get; set; }
    }
}