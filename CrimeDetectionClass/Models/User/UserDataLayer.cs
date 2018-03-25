using CrimeDetectionClass.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CrimeDetectionClass.Models.User
{
    public class UserDataLayer
    {
        public string UserRegistration(UserModel objUserModel)
        {
            string Response = "";
            using (SqlConnection con = new SqlConnection(SqlConnect.GetConnection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SpRegisterUser", con);
                    cmd.Parameters.AddWithValue("@EmailId", objUserModel.EmailId);
                    cmd.Parameters.AddWithValue("@Password", objUserModel.Password);
                    cmd.Parameters.AddWithValue("@ContactNo", objUserModel.ContactNo);
                    cmd.Parameters.AddWithValue("@District", objUserModel.District);
 
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    con.Close();
                }

            }


            return Response;
        }

    }
}