using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Licenta.Models;
namespace Licenta.DataLayer
{
    public class CandidateRegisterDL
    {

        private string connString = ConfigurationManager.ConnectionStrings["NoBordersConnection"].ConnectionString;
        private string query = "spCandidateRegister";

        public bool registerCandidate(CandidateRegister candidate)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", candidate.email);
                cmd.Parameters.AddWithValue("@firstname", candidate.first_name);
                cmd.Parameters.AddWithValue("@lastname", candidate.last_name);
                cmd.Parameters.AddWithValue("@password", candidate.password);

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