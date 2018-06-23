using NoBordersConnectionDb;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicentaBUS.BusinessLayer
{
    public class Candidate
    {
       public static IEnumerable<CANDIDATE> GetCandidates()
        {
            

           using ( var db = new NoBordersConnectionDB())
            {
                var sql = Sql.Builder.Select("*")
                                     .From("Candidates");
             
                return db.Fetch<CANDIDATE>(sql);
            }
           
        }

        public static CANDIDATE GetCandidateById(int id) 
        {


            using (var db = new NoBordersConnectionDB())
            {
                var sql = Sql.Builder.Select("*")
                                     .From("Candidates")
                                     .Where("id_candidate = @0", id);
                return db.SingleOrDefault<CANDIDATE>(sql);
            }

        }

        public static CANDIDATE Delete(int id)
        {


            using (var db = new NoBordersConnectionDB())
            {
                var sql = Sql.Builder.Select("*")
                                     .From("Candidates")
                                     .Where("id_candidate = @0", id);
                return db.SingleOrDefault<CANDIDATE>(sql);
                //dataContext.Delete<Employee>(id);
            }

        }


        //public static IEnumerable<CANDIDATE> GetCandidates()
        //{
        //    var db = new NoBordersConnectionDB();
        //    IEnumerable<CANDIDATE> candidates = db.Query<CANDIDATE>("select * from candidates");
        //    return candidates;
        //}

        //public static IEnumerable<CANDIDATE> GetCandidates()
        //{
        //    var db = new NoBordersConnectionDB();
        //    IEnumerable<CANDIDATE> candidates = db.Query<CANDIDATE>("select * from candidates");
        //    return candidates;
        //}
    }
}
