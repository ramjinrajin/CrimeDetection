using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrimeDetectionClass.Infrastructure
{
    public static class SqlConnect
    {
        public static string GetConnection
        {
            get
            {
                string ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\Crime.mdf;Integrated Security=True";
                return ConnectionString;
            }
        }
    }
}