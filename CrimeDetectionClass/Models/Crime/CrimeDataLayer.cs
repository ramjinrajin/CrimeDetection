using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using CrimeDetectionClass.Infrastructure;

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
                    SqlCommand cmd = new SqlCommand("INSERT INTO tblCrime (Crime,Description,Criminal,Location,IsWomensCrime) VALUES (@Crime,@Description,@Criminal,@Location,@IsWomensCrime)", con);
                    cmd.Parameters.AddWithValue("@Crime", ObjCrimeObject.Crime);
                    cmd.Parameters.AddWithValue("@Description", ObjCrimeObject.Description);
                    cmd.Parameters.AddWithValue("@Criminal", ObjCrimeObject.Criminal);
                    cmd.Parameters.AddWithValue("@Location", ObjCrimeObject.Location);
                    cmd.Parameters.AddWithValue("@IsWomensCrime", IsWomensCrime);
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
                                loc.Trivandrum = "0."+rdr[0].ToString();
                            }
                            if (i == 1)
                            {
                                loc.Kollam = "0."+rdr[0].ToString();
                            }
                            if (i ==2)
                            {
                                loc.Alappuzha = "0."+rdr[0].ToString();
                            }
                            if (i == 3)
                            {
                                loc.Thrissur = "0."+rdr[0].ToString();
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




    }
}