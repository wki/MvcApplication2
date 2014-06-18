using System;
using System.Configuration;
using System.Xml;

namespace MvcApplication2.Repository.EF
{
    public class ConfigurationService
    {
        private static bool isInitialized;
        private static string connectionString;

        public static string ConnectionString
        {
            get
            {
                if (!isInitialized)
                {
                    Initialize();
                }
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }

        private static void Initialize()
        {
            var cs = ConfigurationManager.ConnectionStrings;
            Console.WriteLine("Connection Strings: " + cs.ToString());

            connectionString = "Hello";

            //var c = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            //if (c != null)
            //{
            //    Console.WriteLine("Connection String: " + c);
            //    connectionString = c;
            //}
        }
    }
}
