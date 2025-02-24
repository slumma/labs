using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;

namespace lab484.Pages.Faculty
{
    public class AssociateFacModel : PageModel
    {
        [BindProperty]
        public int GrantID { get; set; }
        [BindProperty]
        public int ProjectID { get; set; }

        // make variable so we can access it w/o reading list 
        public string ProjectName { get; set; }
        public required List<ProjectStaff> StaffList { get; set; } = new List<ProjectStaff>();
        public List<User> UserList { get; set; } = new List<User>();

        public IActionResult OnGet(int ProjectID, int GrantID)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }

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

            // as long as there are faculty in the db for the method to read from the user will be added to the userList 
            using (SqlDataReader facReader = DBClass.facReader())
            {
                while (facReader.Read())
                {
                    UserList.Add(new User
                    {
                        UserID = Int32.Parse(facReader["UserID"].ToString()),
                        UserName = facReader["Username"].ToString()
                    });
                }
            }

            // Close your connection in DBClass
            DBClass.DBConnection.Close();
            return Page();
        }

        public IActionResult OnPostAddFaculty(int UserID)
        {
            // Retrieve the User object based on UserID
            User user = DBClass.GetUserByID(UserID);

            Trace.WriteLine($"ProjectID: {this.ProjectID}");
            Trace.WriteLine($"UserID: {UserID}");

            if (user != null)
            {
                // why does ProjectID keep showing as 0 omg 
                DBClass.InsertProjectStaff(user, this.ProjectID);
            }


            // reloads the page 
            return RedirectToPage(new
            {
                ProjectID = this.ProjectID,
                GrantID = this.GrantID
            });

        }
    }
}
