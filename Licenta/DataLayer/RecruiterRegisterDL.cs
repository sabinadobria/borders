using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Licenta.DataLayer
{
    public class RecruiterRegisterDL
    {
        private string connString = ConfigurationManager.ConnectionStrings["NoBordersConnection"].ConnectionString;
        private string query = "spCompanyRegister";

        public bool registerRecruiter(CompanyRegister company)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@companyname", company.company_name);
                cmd.Parameters.AddWithValue("@email", company.email);
                cmd.Parameters.AddWithValue("@firstname", company.first_name);
                cmd.Parameters.AddWithValue("@lastname", company.last_name);
                cmd.Parameters.AddWithValue("@password", company.password);

                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();

                if (i > 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}