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

        public void OnGet()
        {
            newProject = new ProjectSimple()
            {
                ProjectName = null,
                DueDate = DateTime.Now
            };
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
        }

        public void OnPost()
        {
            
        }

        public IActionResult OnPostAddProject()
        {
            if (assignedFacultyList != null && assignedFacultyList.Any())
            {
                DBClass.AddProject(newProject, assignedFacultyList);
            }
            else
            {
                ModelState.AddModelError("", "Please select at least one faculty member.");
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

        public IActionResult OnPostPopulate()
        {
            ModelState.Clear();
            newProject = new ProjectSimple
            {
                ProjectName = "Machine Project",
                DueDate = DateTime.Now,
            };

            Trace.WriteLine("Populated");

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
