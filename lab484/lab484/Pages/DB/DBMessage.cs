using lab484.Pages.Data_Classes;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace lab484.Pages.DB
{
    public class DBMessage
    {
        public static SqlConnection DBConnection = new SqlConnection();

        // Connection String - How to find and connect to DB
        private static readonly String? DBConnString =
            "Server=Localhost;Database=Lab2;Trusted_Connection=True";
    }
}
