using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab484.Pages.Faculty
{
    public class AssociateFacModel : PageModel
    {
        public int GrantID { get; set; }
        public int ProjectID { get; set; }

        // make variable so we can access it w/o reading list 
        public string ProjectName { get; set; }
        public required List<ProjectStaff> StaffList { get; set; } = new List<ProjectStaff>();
        public List<User> UserList { get; set; } = new List<User>();

        public void OnGet(int ProjectID, int GrantID)
        {
            this.ProjectID = ProjectID;  // sets the ProjectID
            this.GrantID = GrantID;      // sets the GrantID



            // populates the ProjectName variable so it can be used
            // checks if there are any staff in list first
            SqlDataReader projectReader = DBClass.singleProjectReader(ProjectID);
            if (projectReader.Read())
            {
                ProjectName = projectReader["ProjectName"].ToString();
            }
            DBClass.DBConnection.Close();

            SqlDataReader facultyReader = DBClass.singleFacultyReader(ProjectID);
            while (facultyReader.Read())
            {
                StaffList.Add(new ProjectStaff
                {
                    UserID = Int32.Parse(facultyReader["UserID"].ToString()),
                    Username = facultyReader["Username"].ToString(),
                    FirstName = facultyReader["FirstName"].ToString(),
                    LastName = facultyReader["LastName"].ToString(),
                    Email = facultyReader["Email"].ToString(),
                    Phone = facultyReader["Phone"].ToString(),
                    HomeAddress = facultyReader["HomeAddress"].ToString(),
                    ProjectID = Int32.Parse(facultyReader["ProjectID"].ToString()),
                    ProjectName = facultyReader["ProjectName"].ToString(),
                    Leader = bool.Parse(facultyReader["Leader"].ToString()),
                    Active = bool.Parse(facultyReader["Active"].ToString())
                });
            }
            DBClass.DBConnection.Close();

            using (SqlDataReader userReader = DBClass.UserReader())
            {
                while (userReader.Read())
                {
                    UserList.Add(new User
                    {
                        UserID = Int32.Parse(userReader["UserID"].ToString()),
                        UserName = userReader["Username"].ToString()
                    });
                }
            }

            // Close your connection in DBClass
            DBClass.DBConnection.Close();
        }
    }
}
