﻿using lab484.Pages.Data_Classes;
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
            SqlCommand cmdUserReader = new SqlCommand();
            cmdUserReader.Connection = DBConnection;
            cmdUserReader.Connection.ConnectionString = DBConnString;
            cmdUserReader.CommandText = "SELECT * FROM users";
            cmdUserReader.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdUserReader.ExecuteReader();

            return tempReader;
        }

        public static SqlDataReader facReader()
        {
            SqlCommand cmdFacReader = new SqlCommand();
            cmdFacReader.Connection = DBConnection;
            cmdFacReader.Connection.ConnectionString = DBConnString;
            cmdFacReader.CommandText = "SELECT u.* FROM users u INNER JOIN  faculty f ON u.UserID = f.UserID;";
            cmdFacReader.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdFacReader.ExecuteReader();

            return tempReader;
        }

        public static SqlDataReader singleFacultyReader(int ProjectID)
        {
            SqlCommand cmdFacultyReader = new SqlCommand();
            cmdFacultyReader.Connection = DBConnection;
            cmdFacultyReader.Connection.ConnectionString = DBConnString;
            cmdFacultyReader.CommandText = @"SELECT DISTINCT
                                            u.UserID,
                                            u.Username,
                                            u.FirstName,
                                            u.LastName,
                                            u.Email,
                                            u.Phone,
                                            u.HomeAddress,
                                            ps.ProjectID,
                                            p.ProjectName,
                                            ps.Leader,
                                            ps.Active
                                        FROM 
                                            projectStaff ps
                                        JOIN 
                                            faculty f ON ps.UserID = f.UserID
                                        JOIN 
                                            users u ON ps.UserID = u.UserID
                                        JOIN 
                                            project p ON ps.ProjectID = p.ProjectID
                                        WHERE ps.ProjectID = @ProjectID;";

            cmdFacultyReader.Parameters.AddWithValue("@ProjectID", ProjectID);

            cmdFacultyReader.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdFacultyReader.ExecuteReader();

            return tempReader;
        }

        public static SqlDataReader SingleGrantReader(int GrantID)
        {
            SqlCommand cmdSingleGrantRead = new SqlCommand();
            cmdSingleGrantRead.Connection = DBConnection;
            cmdSingleGrantRead.Connection.ConnectionString = DBConnString;

            cmdSingleGrantRead.CommandText = @"SELECT 
                                                g.GrantID, 
                                                p.ProjectID,
                                                s.SupplierName AS Supplier, 
                                                p.ProjectName AS Project, 
                                                g.Amount,
                                                g.Category,
                                                gstat.StatusName, 
                                                g.descriptions,
                                                g.SubmissionDate, 
                                                g.AwardDate
                                            FROM grants g
                                            JOIN grantSupplier s ON g.SupplierID = s.SupplierID
                                            JOIN grantStatus gstat ON g.GrantID = gstat.GrantID
                                            JOIN project p ON g.ProjectID = p.ProjectID
                                            LEFT JOIN grantStatus gs ON g.GrantID = gs.GrantID
                                            WHERE g.GrantID = @GrantID;";

                                    

            cmdSingleGrantRead.Parameters.AddWithValue("@GrantID", GrantID);

            cmdSingleGrantRead.Connection.Open();
            SqlDataReader tempReader = cmdSingleGrantRead.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader GrantReader()
        {
            SqlCommand cmdGrantReader = new SqlCommand();
            cmdGrantReader.Connection = DBConnection;
            cmdGrantReader.Connection.ConnectionString = DBConnString;
            cmdGrantReader.CommandText = @"SELECT 
                                        g.GrantID, 
                                        p.ProjectID,
                                        s.SupplierName AS Supplier, 
                                        p.ProjectName AS Project, 
                                        g.Amount,
                                        g.Category,
                                        gstat.StatusName, 
                                        g.descriptions,
                                        g.SubmissionDate, 
                                        g.AwardDate
                                    FROM grants g
                                    JOIN grantSupplier s ON g.SupplierID = s.SupplierID
                                    JOIN grantStatus gstat ON g.GrantID = gstat.GrantID
                                    JOIN project p ON g.ProjectID = p.ProjectID
                                    LEFT JOIN grantStatus gs ON g.GrantID = gs.GrantID;";


            cmdGrantReader.Connection.Open();
            SqlDataReader tempReader = cmdGrantReader.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader singleProjectReader(int ProjectID)
        {
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = DBConnection;
            cmdProjectRead.Connection.ConnectionString = DBConnString;
            cmdProjectRead.CommandText = "SELECT * from project WHERE ProjectID = @ProjectID;";
            cmdProjectRead.Parameters.AddWithValue("@ProjectID", ProjectID);
            cmdProjectRead.Connection.Open();
            SqlDataReader tempReader = cmdProjectRead.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader ProjectReader()
        {
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = DBConnection;
            cmdProjectRead.Connection.ConnectionString = DBConnString;
            cmdProjectRead.CommandText = "SELECT project.ProjectID, project.ProjectName, project.DueDate, sum(grants.amount) AS Amount\r\nfrom project\r\nJOIN grants on project.ProjectID = grants.ProjectID\r\ngroup by project.ProjectID, project.ProjectName, project.duedate;";
            cmdProjectRead.Connection.Open();
            SqlDataReader tempReader = cmdProjectRead.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader BPrepReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString = DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM BPrep\r\nJOIN users on users.userid = bprep.userid;";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader GrantSupplierReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString = DBConnString;
            cmdProductRead.CommandText = "SELECT * FROM grantSupplier;";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader BPReader()
        {
            SqlCommand cmdProductRead = new SqlCommand();
            cmdProductRead.Connection = DBConnection;
            cmdProductRead.Connection.ConnectionString = DBConnString;
            cmdProductRead.CommandText = "SELECT grantSupplier.SupplierID, grantSupplier.SupplierName, OrgType, " +
                "  grantSupplier.BusinessAddress, bprep.UserID, CommunicationStatus, FirstName,\r\nLastName, email, " +
                "phone, homeaddress, statusname\r\nfrom grantSupplier\r\nJOIN bprep on grantSupplier.SupplierID = bprep.supplierid" +
                "\r\nJOIN users on users.userid = bprep.userid " +
                "\r\nJOIN supplierStatus on grantsupplier.SupplierID = supplierStatus.SupplierID;";
            cmdProductRead.Connection.Open();
            SqlDataReader tempReader = cmdProductRead.ExecuteReader();
            return tempReader;
        }
        public static void UpdateGrant(GrantSimple grant)
        {

            // queries *parameterized*
            // checks relative information such as supplier to ensure its there 
            // query to insert a supplier if it is not already in db
            string checkSupplierQuery = "SELECT SupplierID FROM grantSupplier WHERE SupplierName = @Supplier";
            string insertSupplierQuery = "INSERT INTO grantSupplier (SupplierName) OUTPUT INSERTED.SupplierID VALUES (@Supplier)";
            string updateGrantQuery = "UPDATE grants SET " +
                                      "SupplierID = @SupplierID, " +
                                      "Amount = @Amount, " +
                                      "Category = @Category, " +
                                      "descriptions = @Description, " +
                                      "SubmissionDate = @SubmissionDate, " +
                                      "AwardDate = @AwardDate " +
                                      "WHERE GrantID = @GrantID; " +
                                      "UPDATE grantStatus SET " +
                                      "StatusName = @StatusName " +
                                      "WHERE GrantID = @GrantID;";

            // db connection
            using (SqlConnection connection = new SqlConnection(DBClass.DBConnString))
            {
                connection.Open();

                // check if supplier already exists 
                var supplierId = new SqlCommand(checkSupplierQuery, connection) { 
                    Parameters = { new SqlParameter("@Supplier", grant.Supplier) } 
                }.ExecuteScalar();


                // if supplier does NOT exist, insert it 
                if (supplierId == null)
                {
                    supplierId = new SqlCommand(insertSupplierQuery, connection) { 
                        Parameters = { new SqlParameter("@Supplier", grant.Supplier) } 
                    }.ExecuteScalar();
                }

                // insert parameters into the query 
                using (SqlCommand cmdGrantUpdate = new SqlCommand(updateGrantQuery, connection))
                {
                    cmdGrantUpdate.Parameters.AddWithValue("@SupplierID", supplierId);
                    cmdGrantUpdate.Parameters.AddWithValue("@Amount", grant.Amount);
                    cmdGrantUpdate.Parameters.AddWithValue("@Category", grant.Category);
                    cmdGrantUpdate.Parameters.AddWithValue("@StatusName", grant.Status);
                    cmdGrantUpdate.Parameters.AddWithValue("@Description", grant.Description);
                    cmdGrantUpdate.Parameters.AddWithValue("@SubmissionDate", grant.SubmissionDate);
                    cmdGrantUpdate.Parameters.AddWithValue("@AwardDate", grant.AwardDate);
                    cmdGrantUpdate.Parameters.AddWithValue("@GrantID", grant.GrantID);

                    cmdGrantUpdate.ExecuteNonQuery();
                }
            }
        }


        public static SqlDataReader LoadMessages(int UserID)
        {
            SqlCommand cmdMessageReader = new SqlCommand();
            cmdMessageReader.Connection = DBConnection;
            cmdMessageReader.Connection.ConnectionString = DBConnString;

            cmdMessageReader.CommandText = @"
                SELECT 
                    m.SenderID,
                    u.Username AS SenderUsername,
                    m.SubjectTitle,
                    m.Contents,
                    m.SentTime
                FROM 
                    userMessage m
                JOIN 
                    users u ON m.SenderID = u.UserID
                WHERE 
                    m.RecipientID = @UserID";
            cmdMessageReader.Parameters.AddWithValue("@UserID", UserID);

            cmdMessageReader.Connection.Open();
            SqlDataReader tempReader = cmdMessageReader.ExecuteReader();
            return tempReader;
        }


        // insert user into ProjectStaff 
            /*public static void InsertProjectStaff(int UserID, int projectID)
            {
                String sqlQuery = "INSERT INTO projectStaff (UserID, ProjectID, Leader, Active) VALUES(@UserID, @ProjectID, 0, 1);";

                SqlConnection connection = new SqlConnection(DBConnString);
                SqlCommand cmdInsertProduct = new SqlCommand(sqlQuery, connection);

                // Add parameters to the command
                cmdInsertProduct.Parameters.AddWithValue("@UserID", UserID);
                cmdInsertProduct.Parameters.AddWithValue("@ProjectID", projectID);

                connection.Open();
                cmdInsertProduct.ExecuteNonQuery();
                connection.Close();
            }*/

        public static void InsertProjectStaff(User u, int projectID)
        {

            SqlConnection connection = new SqlConnection(DBConnString);

            String sqlQuery = "INSERT INTO projectStaff (UserID, ProjectID, Leader, Active) VALUES(@UserID, @ProjectID, 0, 1);";
            SqlCommand cmdInsertProduct = new SqlCommand(sqlQuery, connection);

            // Add parameters to the command
            cmdInsertProduct.Parameters.AddWithValue("@UserID", u.UserID);
            cmdInsertProduct.Parameters.AddWithValue("@ProjectID", projectID);

            connection.Open();
            int rowsAffected = cmdInsertProduct.ExecuteNonQuery();

            connection.Close();
        }



        public static User GetUserByID(int userID)
        {
            User user = null;
            String sqlQuery = "SELECT * FROM users WHERE UserID = @UserID";

            SqlConnection connection = new SqlConnection(DBConnString);
            SqlCommand cmdGetUser = new SqlCommand(sqlQuery, connection);
            cmdGetUser.Parameters.AddWithValue("@UserID", userID);

            connection.Open();
            SqlDataReader reader = cmdGetUser.ExecuteReader();

            if (reader.Read())
            {
                user = new User
                {
                    UserID = Int32.Parse(reader["UserID"].ToString()),
                    UserName = reader["Username"].ToString(),
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString(),
                    Email = reader["Email"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    HomeAddress = reader["HomeAddress"].ToString()
                };
            }

            reader.Close();
            connection.Close();

            return user;
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

        
    }
}

