using System.Data.SqlClient;

namespace InventoryManagement.Pages.DB
{
    public class DBClass
    {
        // Use this class to define methods that make connecting to
        // and retrieving data from the DB easier.

        // Connection Object at Data Field Level
        public static SqlConnection DBConnection = new SqlConnection();

        // Connection String - How to find and connect to DB
        private static readonly String? DBConnString = 
            "Server=Localhost;Database=labs;Trusted_Connection=True";

        //Connection Methods:

        //Basic Product Reader
        public static SqlDataReader UserReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString = DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM users";
            cmdProductRead.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;
        }

        public static SqlDataReader SingleUser(int UserID)
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString =
            DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM users WHERE UserID = " + UserID;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }

        /*
        public static void UpdateProduct(Product p)
        {
            String sqlQuery = "UPDATE Product SET ";
            sqlQuery += "ProductName='" + p.ProductName + "',";
            sqlQuery += "ProductCost=" + p.ProductCost + ",";
            sqlQuery += "ProductDescription='" + p.ProductDescription + "' WHERE ProductID=" + p.ProductID;
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString = DBConnString;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();
        }

        public static void InsertProduct(Product p)
        {
            String sqlQuery = "INSERT INTO Product(ProductName, ProductCost, ProductDescription) VALUES('";
            sqlQuery += p.ProductName + "',";
            sqlQuery += p.ProductCost + ",'";
            sqlQuery += p.ProductDescription + "')";

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString = DBConnString;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();

            cmdProductRead.ExecuteNonQuery();
        }
        */

        public static SqlDataReader GeneralReaderQuery(string sqlQuery)
        {

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString = DBConnString;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();

            return tempReader;

        }

        public static void InsertQuery(string sqlQuery)
        {

            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString = DBConnString;
            cmdProductRead.CommandText = sqlQuery;
            cmdProductRead.Connection.Open();
            cmdProductRead.ExecuteNonQuery();

        }

        public static int LoginQuery(string loginQuery)
        {
            // This method expects to receive an SQL SELECT
            // query that uses the COUNT command.
            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = DBConnection;
            cmdLogin.Connection.ConnectionString = DBConnString;
            cmdLogin.CommandText = loginQuery;
            cmdLogin.Connection.Open();
            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            int rowCount = (int)cmdLogin.ExecuteScalar();
            return rowCount;
        }
        public static int SecureLogin(string Username, string Password)
        {
            string loginQuery =
            "SELECT COUNT(*) FROM Credentials where Username = @Username and Password = @Password";
            SqlCommand cmdLogin = new SqlCommand();
            cmdLogin.Connection = DBConnection;
            cmdLogin.Connection.ConnectionString = DBConnString;
            cmdLogin.CommandText = loginQuery;
            cmdLogin.Parameters.AddWithValue("@Username", Username);
            cmdLogin.Parameters.AddWithValue("@Password", Password);
            cmdLogin.Connection.Open();
            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            int rowCount = (int)cmdLogin.ExecuteScalar();
            return rowCount;
        }
    }
}

