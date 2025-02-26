using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab484.Pages.Admin
{
    public class AddProjectModel : PageModel
    {
        [BindProperty]
        public ProjectSimple newProject { get; set; }

        public List<User> facultyList { get; set; } = new List<User>();
        [BindProperty] public List<int> assignedFacultyList { get; set; } = new List<int>();

        // creates new project object and adds faculty to list
        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetString("username") == null)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }

            newProject = new ProjectSimple()
            {
                ProjectName = null,
                DueDate = DateTime.Now
            };
            // resets the facList
            facultyList.Clear();

            SqlDataReader facultyReader = DBClass.facReader();

            while (facultyReader.Read())
            {
                facultyList.Add(new User
                {
                    FirstName = facultyReader["FirstName"].ToString(),
                    LastName = facultyReader["LastName"].ToString(),
                    UserID = Int32.Parse(facultyReader["UserID"].ToString())
                });
            }
            DBClass.DBConnection.Close();
            return Page();
        }

        public void OnPost()
        {
            
        }
         
        // add project button 
        public IActionResult OnPostAddProject()
        {
            // adds the project if it has employees selected to work on it 
            if (assignedFacultyList != null && assignedFacultyList.Any())
            {
                DBClass.AddProject(newProject, assignedFacultyList);
            }
            else
            {
                facultyList.Clear();
                SqlDataReader facultyReader = DBClass.facReader();
                while (facultyReader.Read())
                {
                    facultyList.Add(new User
                    {
                        FirstName = facultyReader["FirstName"].ToString(),
                        LastName = facultyReader["LastName"].ToString(),
                        UserID = Int32.Parse(facultyReader["UserID"].ToString())
                    });
                }
                DBClass.DBConnection.Close();
                return Page();
            }
            DBClass.DBConnection.Close();
            return RedirectToPage("AdminLanding");
        }

        // populates the fields 
        public IActionResult OnPostPopulate()
        {
            ModelState.Clear();
            newProject = new ProjectSimple
            {
                ProjectName = "Machine Project",
                DueDate = DateTime.Now,
            };

            //repopulate facultyList
            SqlDataReader facultyReader = DBClass.facReader();
            while (facultyReader.Read())
            {
                facultyList.Add(new User
                {
                    FirstName = facultyReader["FirstName"].ToString(),
                    LastName = facultyReader["LastName"].ToString(),
                    UserID = Int32.Parse(facultyReader["UserID"].ToString())
                });
            }
            DBClass.DBConnection.Close();

            return Page();
        }

        public IActionResult OnPostClear()
        {
            // Clear the ModelState
            ModelState.Clear();

            // Reset the model properties
            newProject = new ProjectSimple
            {
                DueDate = DateTime.Now
            };
            assignedFacultyList = new List<int>();

            //repopulate facultyList
            SqlDataReader facultyReader = DBClass.facReader();
            while (facultyReader.Read())
            {
                facultyList.Add(new User
                {
                    FirstName = facultyReader["FirstName"].ToString(),
                    LastName = facultyReader["LastName"].ToString(),
                    UserID = Int32.Parse(facultyReader["UserID"].ToString())
                });
            }
            DBClass.DBConnection.Close();

            return Page();
        }
    }
}
