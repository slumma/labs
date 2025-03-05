using lab484.Pages.Data_Classes;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace lab484.Pages.DB
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
                    m.RecipientID = @UserID
                ORDER BY m.SentTime DESC;";
            cmdMessageReader.Parameters.AddWithValue("@UserID", UserID);

            cmdMessageReader.Connection.Open();
            SqlDataReader tempReader = cmdMessageReader.ExecuteReader();
            return tempReader;
        }

        public static void InsertProjectStaff(User u, int projectID)
        {

            SqlConnection connection = new SqlConnection(DBConnString);

            String sqlQuery = "INSERT INTO projectStaff (UserID, ProjectID, Leader, Active) VALUES(@UserID, @ProjectID, 0, 1);";
            SqlCommand cmdInsertProjectStaff = new SqlCommand(sqlQuery, connection);

            // Add parameters to the command
            cmdInsertProjectStaff.Parameters.AddWithValue("@UserID", u.UserID);
            cmdInsertProjectStaff.Parameters.AddWithValue("@ProjectID", projectID);

            connection.Open();
            int rowsAffected = cmdInsertProjectStaff.ExecuteNonQuery();

            connection.Close();
        }

        public static void InsertGrantStaff(User u, int grantID)
        {

            SqlConnection connection = new SqlConnection(DBConnString);

            String sqlQuery = "INSERT INTO grantStaff (UserID, grantID) VALUES (@UserID, @ProjectID);";
            SqlCommand cmdInsertGrantStaff = new SqlCommand(sqlQuery, connection);

            // Add parameters to the command
            cmdInsertGrantStaff.Parameters.AddWithValue("@UserID", u.UserID);
            cmdInsertGrantStaff.Parameters.AddWithValue("@ProjectID", grantID);

            connection.Open();
            int rowsAffected = cmdInsertGrantStaff.ExecuteNonQuery();

            connection.Close();
        }

        public static void InsertGrantNote(GrantNote newNote)
        {
            SqlConnection connection = new SqlConnection(DBConnString);

            String sqlQuery = "INSERT INTO grantNotes(GrantID, Content, AuthorID) VALUES (@GrantID, @Content, @AuthorID);";
            SqlCommand cmdInsertGrantNote = new SqlCommand(sqlQuery, connection);

            cmdInsertGrantNote.Parameters.AddWithValue("@GrantID", newNote.GrantID);
            cmdInsertGrantNote.Parameters.AddWithValue("@Content", newNote.Content);
            cmdInsertGrantNote.Parameters.AddWithValue("@AuthorID", newNote.AuthorID);

            connection.Open();
            cmdInsertGrantNote.ExecuteNonQuery();
            connection.Close();
        }

        
        public static SqlDataReader GrantNoteReader(int GrantID)
        {
            SqlCommand cmdViewNotes = new SqlCommand(DBConnString);
            cmdViewNotes.Connection = DBConnection;
            cmdViewNotes.Connection.ConnectionString = DBConnString;
            cmdViewNotes.CommandText = @"SELECT * FROM grantNotes JOIN users ON grantNotes.AuthorID = users.UserID WHERE GrantID = @GrantID;";

            cmdViewNotes.Parameters.AddWithValue("@GrantID", GrantID);

            cmdViewNotes.Connection.Open();

            SqlDataReader tempReader = cmdViewNotes.ExecuteReader();

            return tempReader;
        }

        public static SqlDataReader ProjectNoteReader(int ProjectID)
        {
            SqlCommand cmdViewNotes = new SqlCommand(DBConnString);
            cmdViewNotes.Connection = DBConnection;
            cmdViewNotes.Connection.ConnectionString = DBConnString;
            cmdViewNotes.CommandText = @"SELECT * FROM projectNotes JOIN users ON projectNotes.AuthorID = users.UserID WHERE projectID = @ProjectID;";

            cmdViewNotes.Parameters.AddWithValue("@ProjectID", ProjectID);

            cmdViewNotes.Connection.Open();

            SqlDataReader tempReader = cmdViewNotes.ExecuteReader();

            return tempReader;
        }

        public static void InsertProjectNote(ProjectNote newNote)
        {
            SqlConnection connection = new SqlConnection(DBConnString);

            String sqlQuery = "INSERT INTO projectNotes(ProjectID, Content, AuthorID) VALUES (@GrantID, @Content, @AuthorID);";
            SqlCommand cmdInsertGrantNote = new SqlCommand(sqlQuery, connection);

            cmdInsertGrantNote.Parameters.AddWithValue("@GrantID", newNote.ProjectID);
            cmdInsertGrantNote.Parameters.AddWithValue("@Content", newNote.Content);
            cmdInsertGrantNote.Parameters.AddWithValue("@AuthorID", newNote.AuthorID);

            connection.Open();
            cmdInsertGrantNote.ExecuteNonQuery();
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
                                                    userMessage.RecipientID = @UserID
                                                ORDER BY SentTime DESC;";

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

       /* string insertProjectQuery = "INSERT INTO dbo.project (ProjectName, DueDate) VALUES (@ProjectName, @DueDate); SELECT SCOPE_IDENTITY();";
        string insertProjectStaffQuery = "INSERT INTO dbo.projectStaff (ProjectID, UserID, Leader, Active) VALUES (@ProjectID, @UserID, @Leader, @Active)";

            using (SqlConnection connection = new SqlConnection(DBClass.DBConnString))
            {
                connection.Open();

                using (SqlCommand cmdProjectInsert = new SqlCommand(insertProjectQuery, connection))
                {
                    cmdProjectInsert.Parameters.AddWithValue("@ProjectName", project.ProjectName);
                    cmdProjectInsert.Parameters.AddWithValue("@DueDate", project.DueDate);

                    project.ProjectID = Convert.ToInt32(cmdProjectInsert.ExecuteScalar()); 
                }*/
public static void InsertGrant(GrantSimple g, int supplierID, int projectID, int userID)
        {
            String insertGrantQuery = "INSERT INTO grants (SupplierID, GrantName, ProjectID, StatusName, Category, SubmissionDate, descriptions, AwardDate, Amount) " +
                              "VALUES (@SupplierID, @GrantName, @ProjectID, @StatusName, @Category, @SubmissionDate, @Descriptions, @AwardDate, @Amount); SELECT SCOPE_IDENTITY();";
            String insertGrantStaffQuery = "INSERT INTO grantStaff (GrantID, UserID) VALUES (@GrantID, @UserID);";

            int GrantID;


            // used AI to help implement the grants into the DB without the grantStaff freaking out 
            using (SqlCommand cmdInsertGrant = new SqlCommand(insertGrantQuery, DBConnection))
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
                GrantID = Convert.ToInt32(cmdInsertGrant.ExecuteScalar());
                cmdInsertGrant.Connection.Close();
            }
            using (SqlCommand cmdInsertGrantStaff = new SqlCommand(insertGrantStaffQuery, DBConnection))
            {
                cmdInsertGrantStaff.Parameters.AddWithValue("@GrantID", GrantID);
                cmdInsertGrantStaff.Parameters.AddWithValue("@UserID", userID);

                cmdInsertGrantStaff.Connection.Open();
                cmdInsertGrantStaff.ExecuteNonQuery();
                cmdInsertGrantStaff.Connection.Close();
            }
        }



        public static void InsertUser(User user)
        {
            String sqlQuery = "INSERT INTO users (Username, Password, FirstName, LastName, Email, Phone, HomeAddress) " +
                              "VALUES (@Username, @Password, @FirstName, @LastName, @Email, @Phone, @HomeAddress)";


            // helped with AI to generate the insertion queries 
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

        public static int loggedInUser(string findUserID)
        {
            SqlCommand cmdUserID = new SqlCommand();
            cmdUserID.Connection = DBConnection;
            cmdUserID.Connection.ConnectionString = DBConnString;
            cmdUserID.CommandText = findUserID;
            cmdUserID.Connection.Open();
            // ExecuteScalar() returns back data type Object
            // Use a typecast to convert this to an int.
            // Method returns first column of first row.
            int userID = Convert.ToInt32(cmdUserID.ExecuteScalar());
            return userID;
        }

        public static void InsertUserMessage(int? senderID, int recipientID, string subjectTitle, string contents)
        {
            String sqlQuery = "INSERT INTO UserMessage (SenderID, RecipientID, SubjectTitle, Contents, SentTime) " +
                              "VALUES (@SenderID, @RecipientID, @SubjectTitle, @Contents, GETDATE())";

            // helped with AI for insertion statemetns so it doesnt break
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
        public static int employeeCheck(int userID)
        {
            SqlCommand cmdCheck = new SqlCommand();
            cmdCheck.Connection = DBConnection;
            cmdCheck.Connection.ConnectionString = DBConnString;
            cmdCheck.CommandText = "SELECT EmployeeStatus FROM users WHERE UserID = @UserID;";
            cmdCheck.Parameters.AddWithValue("@UserID", userID);
            cmdCheck.Connection.Open();
            int status = Convert.ToInt32(cmdCheck.ExecuteScalar());
            return status;

        }

        public static int adminCheck(int userID)
        {
            SqlCommand cmdCheck = new SqlCommand();
            cmdCheck.Connection = DBConnection;
            cmdCheck.Connection.ConnectionString = DBConnString;
            cmdCheck.CommandText = "SELECT AdminStatus FROM users WHERE UserID = @UserID;";
            cmdCheck.Parameters.AddWithValue("@UserID", userID);
            cmdCheck.Connection.Open();
            int status = Convert.ToInt32(cmdCheck.ExecuteScalar());
            return status;
        }

        public static int facultyCheck(int userID)
        {
            SqlCommand cmdCheck = new SqlCommand();
            cmdCheck.Connection = DBConnection;
            cmdCheck.Connection.ConnectionString = DBConnString;
            cmdCheck.CommandText = "SELECT FacultyStatus FROM users WHERE UserID = @UserID;";
            cmdCheck.Parameters.AddWithValue("@UserID", userID);
            cmdCheck.Connection.Open();
            int status = Convert.ToInt32(cmdCheck.ExecuteScalar());
            return status;
        }

        public static int nonFacultyCheck(int userID)
        {
            SqlCommand cmdCheck = new SqlCommand();
            cmdCheck.Connection = DBConnection;
            cmdCheck.Connection.ConnectionString = DBConnString;
            cmdCheck.CommandText = "SELECT NonFacultyStatus FROM users WHERE UserID = @UserID;";
            cmdCheck.Parameters.AddWithValue("@UserID", userID);
            cmdCheck.Connection.Open();
            int status = Convert.ToInt32(cmdCheck.ExecuteScalar());
            return status;
        }


    }
}

