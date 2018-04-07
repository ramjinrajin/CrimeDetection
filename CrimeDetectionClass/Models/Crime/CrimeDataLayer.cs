using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using CrimeDetectionClass.Infrastructure;
using CrimeDetectionClass.Models.User;

namespace CrimeDetectionClass.Models.Crime
{
    public class CrimeDataLayer
    {
        
        public bool SaveCrime(CrimeModel ObjCrimeObject, int IsWomensCrime)
        {
            using (SqlConnection con = new SqlConnection(SqlConnect.GetConnection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO tblCrime (Crime,Description,Criminal,Location,IsWomensCrime,UserId) VALUES (@Crime,@Description,@Criminal,@Location,@IsWomensCrime,@Userid)", con);
                    cmd.Parameters.AddWithValue("@Crime", ObjCrimeObject.Crime);
                    cmd.Parameters.AddWithValue("@Description", ObjCrimeObject.Description);
                    cmd.Parameters.AddWithValue("@Criminal", ObjCrimeObject.Criminal);
                    cmd.Parameters.AddWithValue("@Location", ObjCrimeObject.Location);
                    cmd.Parameters.AddWithValue("@IsWomensCrime", IsWomensCrime);
                    cmd.Parameters.AddWithValue("@Userid", UserDataLayer.GetUserIdByEmail(System.Web.HttpContext.Current.User.Identity.Name));
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


            return true;
        }



        public Location getCrimeRates(List<string> Queries)
        {
               Location loc = new Location();

            for (int i = 0; i < Queries.Count(); i++)
            {
                using (SqlConnection con = new SqlConnection(SqlConnect.GetConnection))
                {
                    try
                    {
                     
                        con.Open();
                        SqlCommand tvm = new SqlCommand(Queries[i], con);
                        SqlDataReader rdr = tvm.ExecuteReader();
                        while (rdr.Read())
                        {
                            if(i==0)
                            {
                                loc.Trivandrum = rdr[0].ToString();
                            }
                            if (i == 1)
                            {
                                loc.Kollam = rdr[0].ToString();
                            }
                            if (i ==2)
                            {
                                loc.Alappuzha = rdr[0].ToString();
                            }
                            if (i == 3)
                            {
                                loc.Thrissur = rdr[0].ToString();
                            }
                             
                        }

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
            }
                
         




            return loc;

        }

        public List<CrimeModel> GetCrimeDetails()
        {
            List<CrimeModel> listCrimes = new List<CrimeModel>();
            using(SqlConnection con = new SqlConnection(SqlConnect.GetConnection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select * from tblCrime order by 1 desc", con);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if(rdr.HasRows)
                    {
                        while(rdr.Read())
                        {
                            CrimeModel objCrimeModel = new CrimeModel
                            {
                                CrimeId =(int)rdr["CrimeId"],
                                Crime = rdr["Crime"].ToString(),
                                Criminal = rdr["Criminal"].ToString(),
                                Description = rdr["Description"].ToString(),
                                Location = rdr["Location"].ToString(),
                                Status = rdr["Status"].ToString(),
                                StatusCode = DefineStatus(rdr["Status"].ToString())
                            };

                            listCrimes.Add(objCrimeModel);
                        }
                    }

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



            return listCrimes;
        }



        internal CrimeModel GetCrimeDetailsbyCrimeId(int CrimeId)
        {
            CrimeModel objCrimeModel = new CrimeModel();
            using (SqlConnection con = new SqlConnection(SqlConnect.GetConnection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select * from tblCrime where CrimeId=@CrimeId order by 1 desc", con);
                    cmd.Parameters.AddWithValue("@CrimeId", CrimeId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            CrimeModel FetchCrime = new CrimeModel
                            {
                                CrimeId = (int)rdr["CrimeId"],
                                Crime = rdr["Crime"].ToString(),
                                Criminal = rdr["Criminal"].ToString(),
                                Description = rdr["Description"].ToString(),
                                Location = rdr["Location"].ToString(),
                                Status = rdr["Status"].ToString(),
                                StatusCode =DefineStatus(rdr["Status"].ToString()),
                                Comments=ReadComment(CrimeId) 
                            };

                            objCrimeModel = FetchCrime;
                        }
                    }

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



            return objCrimeModel;
        }

        private List<string> ReadComment(int CrimeId)
        {
            List<string> listComments = new List<string>();
            using (SqlConnection con = new SqlConnection(SqlConnect.GetConnection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select CommentDesc from CrimeComments where CrimeId=@CrimeId", con);
                    cmd.Parameters.AddWithValue("@CrimeId", CrimeId);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            listComments.Add(rdr["CommentDesc"].ToString());
                        }
                    }

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
            return listComments;
        }

        private string DefineStatus(string status)
        {
             if(status=="Pending")
             {
                 return "label-default";
             }
             if(status=="investigation")
             {
                 return "label-warning";
             }
             if(status=="Decline")
             {
                 return "label-danger";
             }
             if(status=="Completed")
             {
                 return "label-success";
             }

             return "label-default";
        }

        internal bool UpdateCrimeStatus(int CrimeId, string Status, string Comment)
        {
            bool IsSucess = false;
            using (SqlConnection con = new SqlConnection(SqlConnect.GetConnection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SpUpdateCrimeStatusAndComments", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CrimeId", CrimeId);
                    cmd.Parameters.AddWithValue("@Status", Status);
                    cmd.Parameters.AddWithValue("@Comment", Comment);
                    cmd.ExecuteNonQuery();
                    IsSucess = true;

                }
                catch  
                {
                    IsSucess = false;
                
                }
                finally
                {
                    con.Close();
                }
            }

            return IsSucess;
        }

      

        internal List<CrimeModel> GetCrimeDetailsByUserId(int Userid)
        {
            List<CrimeModel> listCrimes = new List<CrimeModel>();
            using (SqlConnection con = new SqlConnection(SqlConnect.GetConnection))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Select * from tblCrime Where Userid=@Userid order by 1 desc", con);
                    cmd.Parameters.AddWithValue("@Userid",Userid);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            CrimeModel objCrimeModel = new CrimeModel
                            {
                                CrimeId = (int)rdr["CrimeId"],
                                Crime = rdr["Crime"].ToString(),
                                Criminal = rdr["Criminal"].ToString(),
                                Description = rdr["Description"].ToString(),
                                Location = rdr["Location"].ToString(),
                                Status = rdr["Status"].ToString(),
                                StatusCode = DefineStatus(rdr["Status"].ToString())
                            };

                            listCrimes.Add(objCrimeModel);
                        }
                    }

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



            return listCrimes;
        }
    }
}