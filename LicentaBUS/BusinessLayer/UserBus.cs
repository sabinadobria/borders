using NoBordersConnectionDb;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicentaBUS.BusinessLayer
{
    public class UserBus
    {
        public bool IsValid(string _username, string _password,int _utype)
        {
            using (var cn = new SqlConnection(@"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=noBorders;Integrated Security=True"))
            {
                string _sql = @"SELECT * FROM [users] " +
                       @"WHERE [email] = @u AND [Password] = @p";
                var cmd = new SqlCommand(_sql, cn);
                cmd.Parameters
                    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                    .Value = _username;
                cmd.Parameters
                    .Add(new SqlParameter("@p", SqlDbType.NVarChar))
                    .Value = _password;
                
                //.Value = Helpers.SHA1.Encode(_password);
                cn.Open();
                var reader = cmd.ExecuteReader();

                _utype = 0;
                //SqlDataReader rdr = cmd.ExecuteReader();
                if (reader.Read())
                {
                    _utype = (int)reader["usertypeId"];
                }
                if (reader.HasRows)
                {
                   
                    reader.Dispose();
                    cmd.Dispose();
                    return true;
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return false;
                }
            }
        }
        public int userTypeId(string _username, string _password)
        {
            using (var cn = new SqlConnection(@"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=noBorders;Integrated Security=True"))
            {
                string _sql = @"SELECT * FROM [users] " +
                       @"WHERE [email] = @u AND [Password] = @p";
                var cmd = new SqlCommand(_sql, cn);
                cmd.Parameters
                    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                    .Value = _username;
                cmd.Parameters
                    .Add(new SqlParameter("@p", SqlDbType.NVarChar))
                    .Value = _password;

                cn.Open();
                var reader = cmd.ExecuteReader();

                var  _utype = 0;
                if (reader.Read())
                {
                    _utype = (int)reader["usertypeId"];
                }
                if (reader.HasRows)
                {

                    reader.Dispose();
                    cmd.Dispose();
                    return _utype ;
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return 0;
                }
            }
        }
    }
}
