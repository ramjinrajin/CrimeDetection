using CrimeDetectionClass.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CrimeDetectionClass.Models.User
{
    public class UserDataLayer
    {
        public string UserRegistration(UserModel objUserModel)
        {
             
            string EncryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(objUserModel.Password, "SHA1");
            string Response = "";
            using (SqlConnection con = new SqlConnection(SqlConnect.GetConnection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SpRegisterUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", objUserModel.EmailId);
                    cmd.Parameters.AddWithValue("@Password", EncryptedPassword);
                    cmd.Parameters.AddWithValue("@ContactNo", objUserModel.ContactNo);
                    cmd.Parameters.AddWithValue("@District", objUserModel.District);
                    cmd.Parameters.AddWithValue("@IsPolice", objUserModel.EmailId.Contains("indianpolice.com")?1:0);
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


        internal bool AuthenticateUser(string username, string password)
        {
          
            // SqlConnection is in System.Data.SqlClient namespace
            using (SqlConnection con = new SqlConnection(SqlConnect.GetConnection))
            {
                SqlCommand cmd = new SqlCommand("spAuthenticateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // FormsAuthentication is in System.Web.Security
                string EncryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
                // SqlParameter is in System.Data namespace
                SqlParameter paramUsername = new SqlParameter("@UserName", username);
                SqlParameter paramPassword = new SqlParameter("@Password", EncryptedPassword);//we are not using authentiacated password ,use EncryptedPassword to use authenticated password 

                cmd.Parameters.Add(paramUsername);
                cmd.Parameters.Add(paramPassword);

                con.Open();
                int ReturnCode = (int)cmd.ExecuteScalar();
                return ReturnCode == 1;
            }
        }

        internal bool GetIsPoliceStatus(string EmailId)
        {
            
            using (SqlConnection con = new SqlConnection(SqlConnect.GetConnection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select TOP 1 [IsPolice] From CrimeUsers where EmailId=@EmailId AND IsPolice=1" , con);
                    cmd.Parameters.AddWithValue("@EmailId", EmailId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    return rdr.HasRows;
                }
                catch (Exception)
                {

                    return false;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public static int GetUserIdByEmail(string Email)
        {
            int UserId = 0;
            using (SqlConnection con = new SqlConnection(SqlConnect.GetConnection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select TOP 1 [UserId] From CrimeUsers where EmailId=@EmailId AND IsPolice=1", con);
                    cmd.Parameters.AddWithValue("@EmailId", Email);
                    SqlDataReader rdr = cmd.ExecuteReader();
                   if(rdr.HasRows)
                   {
                       while(rdr.Read())
                       {
                           UserId = (int)rdr[0];
                       }
                   }
                }
                catch (Exception)
                {

                    UserId = 0;
                }
                finally
                {
                    con.Close();
                }
                return UserId;
            }
        }
    }
}