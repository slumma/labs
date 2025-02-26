﻿using lab484.Pages.Data_Classes;
using System.Data.SqlClient;
using System.Diagnostics;

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
            "Server=Localhost;Database=Lab2;Trusted_Connection=True";

        //Connection Methods:

        //Basic Product Reader
        public static SqlDataReader UserReader()
        {
            SqlCommand cmdUserReader = new SqlCommand();
            cmdUserReader.Connection = DBConnection;
            cmdUserReader.Connection.ConnectionString = DBConnString;
            cmdUserReader.CommandText = "SELECT * FROM users ORDER BY Username";
            cmdUserReader.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdUserReader.ExecuteReader();

            return tempReader;
        }

        public static SqlDataReader facReader()
        {
            SqlCommand cmdFacReader = new SqlCommand();
            cmdFacReader.Connection = DBConnection;
            cmdFacReader.Connection.ConnectionString = DBConnString;
            cmdFacReader.CommandText = "SELECT u.* FROM users u WHERE FacultyStatus = 1;";
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
                                            users u ON ps.UserID = u.UserID
                                        JOIN 
                                            project p ON ps.ProjectID = p.ProjectID
                                        WHERE ps.ProjectID = @ProjectID AND u.FacultyStatus = 1;";

            cmdFacultyReader.Parameters.AddWithValue("@ProjectID", ProjectID);

            cmdFacultyReader.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdFacultyReader.ExecuteReader();

            return tempReader;
        }

        public static SqlDataReader projectStaffReader(int ProjectID)
        {
            SqlCommand cmdProjectStaffReader = new SqlCommand();
            cmdProjectStaffReader.Connection = DBConnection;
            cmdProjectStaffReader.Connection.ConnectionString = DBConnString;
            cmdProjectStaffReader.CommandText = @"SELECT DISTINCT
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
                                            users u ON ps.UserID = u.UserID
                                        JOIN 
                                            project p ON ps.ProjectID = p.ProjectID
                                        WHERE ps.ProjectID = @ProjectID AND FacultyStatus = 1;";

            cmdProjectStaffReader.Parameters.AddWithValue("@ProjectID", ProjectID);

            cmdProjectStaffReader.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdProjectStaffReader.ExecuteReader();

            return tempReader;
        }
        


        public static SqlDataReader taskStaffReader(int projectID)
        {
            SqlCommand cmdTaskStaffRead = new SqlCommand();
            cmdTaskStaffRead.Connection = DBConnection;
            cmdTaskStaffRead.Connection.ConnectionString = DBConnString;

            cmdTaskStaffRead.CommandText = "SELECT * from taskStaff\r\njoin task on task.taskid = taskstaff.taskid\r\n" +
                "join users on taskstaff.assigneeID = users.UserID\r\nWHERE ProjectID = 1";
            cmdTaskStaffRead.Parameters.AddWithValue("@ProjectID", projectID);
            cmdTaskStaffRead.Connection.Open();
            SqlDataReader tempReader = cmdTaskStaffRead.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader taskReader(int projectID)
        {
            SqlCommand cmdTaskRead = new SqlCommand();
            cmdTaskRead.Connection = DBConnection;
            cmdTaskRead.Connection.ConnectionString = DBConnString;

            cmdTaskRead.CommandText = "SELECT * from task WHERE ProjectID = @ProjectID";
            cmdTaskRead.Parameters.AddWithValue("@ProjectID", projectID);
            cmdTaskRead.Connection.Open();
            SqlDataReader tempReader = cmdTaskRead.ExecuteReader();
            return tempReader;
        }

        public static SqlDataReader SingleGrantReader(int GrantID)
        {
            SqlCommand cmdSingleGrantRead = new SqlCommand();
            cmdSingleGrantRead.Connection = DBConnection;
            cmdSingleGrantRead.Connection.ConnectionString = DBConnString;

            cmdSingleGrantRead.CommandText = @"SELECT 
                                            g.GrantID,
                                            g.GrantName,
                                            p.ProjectID,
                                            s.SupplierName AS Supplier, 
                                            p.ProjectName AS Project, 
                                            g.Amount,
                                            g.Category,
                                            g.StatusName, 
                                            g.descriptions,
                                            g.SubmissionDate, 
                                            g.AwardDate
                                        FROM grants g
                                        JOIN grantSupplier s ON g.SupplierID = s.SupplierID
                                        LEFT JOIN project p ON g.ProjectID = p.ProjectID
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
                                            g.GrantName,
                                            p.ProjectID,
                                            s.SupplierName AS Supplier, 
                                            p.ProjectName AS Project, 
                                            g.Amount,
                                            g.Category,
                                            g.GrantStatus, 
                                            g.descriptions,
                                            g.SubmissionDate, 
                                            g.AwardDate
                                        FROM grants g
                                        JOIN grantSupplier s ON g.SupplierID = s.SupplierID
                                        LEFT JOIN project p ON g.ProjectID = p.ProjectID
                                        ORDER BY g.AwardDate";


            cmdGrantReader.Connection.Open();
            SqlDataReader tempReader = cmdGrantReader.ExecuteReader();
            return tempReader;
        }


        public static SqlDataReader singleProjectReader(int ProjectID)
        {
            SqlCommand cmdProjectRead = new SqlCommand();
            cmdProjectRead.Connection = DBConnection;
            cmdProjectRead.Connection.ConnectionString = DBConnString;
            cmdProjectRead.CommandText = "SELECT project.ProjectID, project.ProjectName, project.DueDate, sum(grants.amount) AS Amount from project" +
                                            " JOIN grants on project.ProjectID = grants.ProjectID where project.ProjectID = @ProjectID" +
                                            " group by project.ProjectID, project.ProjectName, project.duedate;";
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
            cmdProjectRead.CommandText = "SELECT project.ProjectID, project.ProjectName, project.DueDate, sum(grants.amount) AS Amount\r\nfrom project\r\nLEFT JOIN grants on project.ProjectID = grants.ProjectID\r\ngroup by project.ProjectID, project.ProjectName, project.duedate;";
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

        public static void AddProject(ProjectSimple project, List<int> assignedFacultyList)
        {
            string insertProjectQuery = "INSERT INTO dbo.project (ProjectName, DueDate) VALUES (@ProjectName, @DueDate); SELECT SCOPE_IDENTITY();";
            string insertProjectStaffQuery = "INSERT INTO dbo.projectStaff (ProjectID, UserID, Leader, Active) VALUES (@ProjectID, @UserID, @Leader, @Active)";

            using (SqlConnection connection = new SqlConnection(DBClass.DBConnString))
            {
                connection.Open();

                using (SqlCommand cmdProjectInsert = new SqlCommand(insertProjectQuery, connection))
                {
                    cmdProjectInsert.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                    cmdProjectInsert.Parameters.AddWithValue("@DueDate", project.DueDate);

                    project.ProjectID = Convert.ToInt32(cmdProjectInsert.ExecuteScalar()); 
                }

                foreach (var userID in assignedFacultyList)
                {
                    using (SqlCommand cmdProjectStaffInsert = new SqlCommand(insertProjectStaffQuery, connection))
                    {
                        cmdProjectStaffInsert.Parameters.AddWithValue("@ProjectID", project.ProjectID); 
                        cmdProjectStaffInsert.Parameters.AddWithValue("@UserID", userID);
                        cmdProjectStaffInsert.Parameters.AddWithValue("@Leader", 0);
                        cmdProjectStaffInsert.Parameters.AddWithValue("@Active", 1);

                        cmdProjectStaffInsert.ExecuteNonQuery();
                    }
                }
            }
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
                                      "StatusName = @StatusName, " +
                                      "descriptions = @Description, " +
                                      "SubmissionDate = @SubmissionDate, " +
                                      "AwardDate = @AwardDate " +
                                      "WHERE GrantID = @GrantID; ";

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


        public static SqlDataReader singleSenderReader(int UserID)
        {
            SqlCommand cmdsingleSenderReader = new SqlCommand();
            cmdsingleSenderReader.Connection = DBConnection;
            cmdsingleSenderReader.Connection.ConnectionString = DBConnString;
            cmdsingleSenderReader.CommandText = @"SELECT 
                                                    userMessage.*,
                                                    sender.Username AS SenderUsername,
                                                    recipient.Username AS RecipientUsername
                                                FROM 
                                                    userMessage
                                                JOIN 
                                                    Users AS sender ON userMessage.SenderID = sender.UserID
                                                JOIN 
                                                    Users AS recipient ON userMessage.RecipientID = recipient.UserID
                                                WHERE 
                                                    userMessage.SenderID = @UserID;";

            cmdsingleSenderReader.Parameters.AddWithValue("@UserID", UserID);

            cmdsingleSenderReader.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdsingleSenderReader.ExecuteReader();

            return tempReader;
        }



        public static SqlDataReader singleRecipientReader(int UserID)
        {
            SqlCommand cmdsingleSenderReader = new SqlCommand();
            cmdsingleSenderReader.Connection = DBConnection;
            cmdsingleSenderReader.Connection.ConnectionString = DBConnString;
            cmdsingleSenderReader.CommandText = @"SELECT 
                                                    userMessage.*,
                                                    sender.Username AS SenderUsername,
                                                    recipient.Username AS RecipientUsername
                                                FROM 
                                                    userMessage
                                                JOIN 
                                                    Users AS sender ON userMessage.SenderID = sender.UserID
                                                JOIN 
                                                    Users AS recipient ON userMessage.RecipientID = recipient.UserID
                                                WHERE 
                                                    userMessage.RecipientID = @UserID;";

            cmdsingleSenderReader.Parameters.AddWithValue("@UserID", UserID);

            cmdsingleSenderReader.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdsingleSenderReader.ExecuteReader();

            return tempReader;
        }

        public static SqlDataReader SingleUserReader(String username)
        {
            SqlCommand cmdsingleSenderReader = new SqlCommand();
            cmdsingleSenderReader.Connection = DBConnection;
            cmdsingleSenderReader.Connection.ConnectionString = DBConnString;
            cmdsingleSenderReader.CommandText = @"select * from users where username = @username;";

            cmdsingleSenderReader.Parameters.AddWithValue("@username", username);

            cmdsingleSenderReader.Connection.Open(); // Open connection here, close in Model!

            SqlDataReader tempReader = cmdsingleSenderReader.ExecuteReader();

            return tempReader;
        }


        public static void InsertGrant(GrantSimple g, int supplierID, int projectID)
        {
            String sqlQuery = "INSERT INTO grants (SupplierID, GrantName, ProjectID, StatusName, Category, SubmissionDate, descriptions, AwardDate, Amount) " +
                              "VALUES (@SupplierID, @GrantName, @ProjectID, @StatusName, @Category, @SubmissionDate, @Descriptions, @AwardDate, @Amount)";

            using (SqlCommand cmdInsertGrant = new SqlCommand(sqlQuery, DBConnection))
            {
                cmdInsertGrant.Connection.ConnectionString = DBConnString;

                cmdInsertGrant.Parameters.AddWithValue("@SupplierID", supplierID);
                cmdInsertGrant.Parameters.AddWithValue("@GrantName", g.GrantName);
                cmdInsertGrant.Parameters.AddWithValue("@ProjectID", projectID);
                cmdInsertGrant.Parameters.AddWithValue("@StatusName", g.Status);
                cmdInsertGrant.Parameters.AddWithValue("@Category", g.Category);
                cmdInsertGrant.Parameters.AddWithValue("@SubmissionDate", g.SubmissionDate);
                cmdInsertGrant.Parameters.AddWithValue("@Descriptions", g.Description);
                cmdInsertGrant.Parameters.AddWithValue("@AwardDate", g.AwardDate);
                cmdInsertGrant.Parameters.AddWithValue("@Amount", g.Amount);

                
                cmdInsertGrant.Connection.Open();
                cmdInsertGrant.ExecuteNonQuery();
                cmdInsertGrant.Connection.Close();
            }
        }



        public static void InsertUser(User user)
        {
            String sqlQuery = "INSERT INTO users (Username, Password, FirstName, LastName, Email, Phone, HomeAddress) " +
                              "VALUES (@Username, @Password, @FirstName, @LastName, @Email, @Phone, @HomeAddress)";

            using (SqlCommand cmdInsertUser = new SqlCommand(sqlQuery, DBConnection))
            {
                cmdInsertUser.Connection.ConnectionString = DBConnString;

                cmdInsertUser.Parameters.AddWithValue("@Username", user.UserName);
                cmdInsertUser.Parameters.AddWithValue("@Password", user.Password);
                cmdInsertUser.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmdInsertUser.Parameters.AddWithValue("@LastName", user.LastName);
                cmdInsertUser.Parameters.AddWithValue("@Email", user.Email);
                cmdInsertUser.Parameters.AddWithValue("@Phone", user.Phone);
                cmdInsertUser.Parameters.AddWithValue("@HomeAddress", user.HomeAddress);

                cmdInsertUser.Connection.Open();
                cmdInsertUser.ExecuteNonQuery();
                cmdInsertUser.Connection.Close();
            }
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


        public static void InsertUserMessage(int? senderID, int recipientID, string subjectTitle, string contents)
        {
            String sqlQuery = "INSERT INTO UserMessage (SenderID, RecipientID, SubjectTitle, Contents, SentTime) " +
                              "VALUES (@SenderID, @RecipientID, @SubjectTitle, @Contents, GETDATE())";

            using (SqlCommand cmdInsertUserMessage = new SqlCommand(sqlQuery, DBConnection))
            {
                cmdInsertUserMessage.Connection.ConnectionString = DBConnString;

                cmdInsertUserMessage.Parameters.AddWithValue("@SenderID", senderID);
                cmdInsertUserMessage.Parameters.AddWithValue("@RecipientID", recipientID);
                cmdInsertUserMessage.Parameters.AddWithValue("@SubjectTitle", subjectTitle);
                cmdInsertUserMessage.Parameters.AddWithValue("@Contents", contents);

                cmdInsertUserMessage.Connection.Open();
                cmdInsertUserMessage.ExecuteNonQuery();
                cmdInsertUserMessage.Connection.Close();
            }
        }



    }
}

