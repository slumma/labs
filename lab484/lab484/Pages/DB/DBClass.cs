using lab484.Pages.Data_Classes;
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

        public static SqlDataReader SingleGrantReader(int GrantID)
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString =
            DBConnString;
            cmdProductRead.CommandText = "SELECT \r\n    g.GrantID, \r\n    s.SupplierName AS Supplier, \r\n    p.ProjectID AS Project, \r\n    g.Amount, \r\n    g.StatusName, \r\n    g.descriptions,\r\n    g.SubmissionDate, \r\n    g.AwardDate\r\nFROM grants g\r\n\r\nJOIN grantSupplier s ON g.SupplierID = s.SupplierID\r\nJOIN project p ON g.ProjectID = p.ProjectID\r\nLEFT JOIN grantStatus gs ON g.GrantID = gs.GrantID WHERE g.GrantID =" + GrantID + ";";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader GrantReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString = DBConnString;
            cmdProductRead.CommandText = "SELECT \r\n    g.GrantID, \r\n    s.SupplierName AS Supplier, \r\n    p.ProjectID AS Project, \r\n    g.Amount, \r\n    g.StatusName, \r\n    g.descriptions,\r\n    g.SubmissionDate, \r\n    g.AwardDate\r\nFROM grants g\r\n\r\nJOIN grantSupplier s ON g.SupplierID = s.SupplierID\r\nJOIN project p ON g.ProjectID = p.ProjectID\r\nLEFT JOIN grantStatus gs ON g.GrantID = gs.GrantID;\r\n";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader ProjectReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString = DBConnString;
            cmdProductRead.CommandText = "SELECT project.ProjectID, project.ProjectName, project.DueDate, sum(grants.amount) AS Amount\r\nfrom project\r\nJOIN grants on project.ProjectID = grants.ProjectID\r\ngroup by project.ProjectID, project.ProjectName, project.duedate;";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }

        public static void UpdateGrant(GrantSimple grant)
        {
            // define the SQL query with parameters
            string sqlQuery = "UPDATE grants SET " +
                              "SupplierID = (SELECT SupplierID FROM grantSupplier WHERE SupplierName = @Supplier), " +
                              "ProjectID = (SELECT ProjectID FROM project WHERE ProjectID = @Project), " +
                              "Amount = @Amount, " +
                              "StatusName = (SELECT StatusName FROM grantStatus WHERE GrantID = @GrantID), " +
                              "Description = @Description, " +
                              "SubmissionDate = @SubmissionDate, " +
                              "AwardDate = @AwardDate " +
                              "WHERE GrantID = @GrantID";

            // Create a new *Parameterized* SQL command
            using (SqlCommand cmdGrantUpdate = new SqlCommand(sqlQuery, DBClass.DBConnection))
            {
                // Define and add the parameters
                cmdGrantUpdate.Parameters.AddWithValue("@Supplier", grant.Supplier);
                cmdGrantUpdate.Parameters.AddWithValue("@Project", grant.Project);
                cmdGrantUpdate.Parameters.AddWithValue("@Amount", grant.Amount);
                cmdGrantUpdate.Parameters.AddWithValue("@Category", grant.Category);
                cmdGrantUpdate.Parameters.AddWithValue("@Description", grant.Description);
                cmdGrantUpdate.Parameters.AddWithValue("@SubmissionDate", grant.SubmissionDate);
                cmdGrantUpdate.Parameters.AddWithValue("@AwardDate", grant.AwardDate);
                cmdGrantUpdate.Parameters.AddWithValue("@GrantID", grant.GrantID);

                // Open the connection if it is not already open
                if (cmdGrantUpdate.Connection.State != System.Data.ConnectionState.Open)
                {
                    cmdGrantUpdate.Connection.ConnectionString = DBClass.DBConnString;
                    cmdGrantUpdate.Connection.Open();
                }

                // Execute the query
                cmdGrantUpdate.ExecuteNonQuery();
            }
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

