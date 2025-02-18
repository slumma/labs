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
            //  SQL queries with parameters
            string checkSupplierQuery = "SELECT SupplierID FROM grantSupplier WHERE SupplierName = @Supplier";
            string insertSupplierQuery = "INSERT INTO grantSupplier (SupplierName) OUTPUT INSERTED.SupplierID VALUES (@Supplier)";
            string updateGrantQuery = "UPDATE grants SET " +
                                      "SupplierID = @SupplierID, " +
                                      "ProjectID = @Project, " +
                                      "Amount = @Amount, " +
                                      "StatusName = @Category, " +
                                      "descriptions = @Description, " +
                                      "SubmissionDate = @SubmissionDate, " +
                                      "AwardDate = @AwardDate " +
                                      "WHERE GrantID = @GrantID";

            // create a new SQL command to check if the supplier exists
            using (SqlCommand cmdCheckSupplier = new SqlCommand(checkSupplierQuery, DBClass.DBConnection))
            {
                // Define and add the parameters for the check supplier query
                cmdCheckSupplier.Parameters.AddWithValue("@Supplier", grant.Supplier);

                cmdCheckSupplier.Connection.ConnectionString = DBClass.DBConnString;
                cmdCheckSupplier.Connection.Open();

                // Execute the query to check if the supplier exists
                var supplierId = cmdCheckSupplier.ExecuteScalar();

                if (supplierId == null)
                {
                    // If the supplier does not exist, insert it and get the new SupplierID
                    using (SqlCommand cmdInsertSupplier = new SqlCommand(insertSupplierQuery, DBClass.DBConnection))
                    {
                        cmdInsertSupplier.Parameters.AddWithValue("@Supplier", grant.Supplier);
                        supplierId = cmdInsertSupplier.ExecuteScalar();
                    }
                }

                // Create a new SQL command to update the grant
                using (SqlCommand cmdGrantUpdate = new SqlCommand(updateGrantQuery, DBClass.DBConnection))
                {
                    // Define and add the parameters for the update grant query
                    cmdGrantUpdate.Parameters.AddWithValue("@SupplierID", supplierId);
                    cmdGrantUpdate.Parameters.AddWithValue("@Project", grant.Project);
                    cmdGrantUpdate.Parameters.AddWithValue("@Amount", grant.Amount);
                    cmdGrantUpdate.Parameters.AddWithValue("@Category", grant.Category);
                    cmdGrantUpdate.Parameters.AddWithValue("@Description", grant.Description);
                    cmdGrantUpdate.Parameters.AddWithValue("@SubmissionDate", grant.SubmissionDate);
                    cmdGrantUpdate.Parameters.AddWithValue("@AwardDate", grant.AwardDate);
                    cmdGrantUpdate.Parameters.AddWithValue("@GrantID", grant.GrantID);

                    // Execute the update query
                    cmdGrantUpdate.ExecuteNonQuery();
                }
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

