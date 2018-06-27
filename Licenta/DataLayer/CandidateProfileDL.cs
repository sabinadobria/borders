using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Licenta.DataLayer
{
    public class CandidateProfileDL
    {

        private string connString = ConfigurationManager.ConnectionStrings["NoBordersDB"].ConnectionString;
        //string query = "spLoadCandidateInfo";

        //public CandidateProfile getCandidateProfileById (int candidate_id)
        //{
        //    CandidateProfile candidate = null;
          
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connString))
        //        {
        //            SqlCommand cmd = new SqlCommand(query, conn);
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //            cmd.Parameters.AddWithValue("@id", candidate_id);
        //            SqlDataAdapter da = new SqlDataAdapter();
        //            da.SelectCommand = cmd;
        //            DataSet ds = new DataSet();
        //            da.Fill(ds);

        //            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            //{
        //                candidate = new CandidateProfile();
        //                candidate.Id_candidate = Convert.ToInt32(ds.Tables[0].Rows[0]["id_candidate"].ToString());
        //                candidate.Last_name = ds.Tables[0].Rows[0]["last_name"].ToString();
        //                candidate.First_name = ds.Tables[0].Rows[0]["first_name"].ToString();
        //                candidate.Email = ds.Tables[0].Rows[0]["email"].ToString();
        //                candidate.Phone = ds.Tables[0].Rows[0]["phone"].ToString();
        //                candidate.Address = ds.Tables[0].Rows[0]["address"].ToString();
        //                candidate.City = ds.Tables[0].Rows[0]["city"].ToString();
        //                candidate.Country = ds.Tables[0].Rows[0]["country"].ToString();
        //                candidate.AboutMe = ds.Tables[0].Rows[0]["aboutme"].ToString();
                        
        //            //}

        //            return candidate;
        //        }
        //    }
        //    catch
        //    {
        //        return candidate;
        //    }
        //}
        public int getCandidateId(string _email)
        {
           
            using (var cn = new SqlConnection(@"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=noBorders;Integrated Security=True"))
            {
                 string _sql = @"SELECT * FROM [candidates] " +
                       @"WHERE [email] = @u";
                var cmd = new SqlCommand(_sql, cn);
                cmd.Parameters
                    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                    .Value = _email;
               
                cn.Open();
                var reader = cmd.ExecuteReader();
              
                var _uID = 0;
                if (reader.Read())
                {
                    _uID = (int)reader["id_candidate"];
                }
                if (reader.HasRows)
                {

                    reader.Dispose();
                    cmd.Dispose();
                    return _uID;
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return 0;
                }
            }
        }

        public bool updateCandidateProfile(CandidateProfile candidate)
        {
            string query1 = "spCandidateContactData";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(query1, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_candidate", candidate.Id_candidate);
                cmd.Parameters.AddWithValue("@email", candidate.Email);
                cmd.Parameters.AddWithValue("@firstname", candidate.First_name);
                cmd.Parameters.AddWithValue("@lastname", candidate.Last_name);
                cmd.Parameters.AddWithValue("@phone", candidate.Phone);
                cmd.Parameters.AddWithValue("@address", candidate.Address);
                cmd.Parameters.AddWithValue("@city", candidate.City);
                cmd.Parameters.AddWithValue("@country", candidate.Country);
                cmd.Parameters.AddWithValue("@aboutme", candidate.AboutMe);

                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();

                if (i > 1)
                {
                    return true;
                }
                else
                    return false;
            }
        }
    }
}