using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab484.Pages.Admin
{
    public class AdminLandingModel : PageModel
    {
        public required List<ProjectSimple> projectList { get; set; } = new List<ProjectSimple>();
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }
            else if (HttpContext.Session.GetInt32("adminStatus") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You do not have permission to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }

            SqlDataReader projectReader = DBClass.ProjectReader();
            while (projectReader.Read())
            {
                // populate the projectList 
                projectList.Add(new ProjectSimple
                {
                    ProjectID = Int32.Parse(projectReader["ProjectID"].ToString()),
                    ProjectName = projectReader["ProjectName"].ToString(),
                    DueDate = DateTime.Parse(projectReader["DueDate"].ToString()),
                    Amount = 0f
                });
                // if the amount isnt a value, try converting it to a string then parsing a float from it 
                if (projectReader["Amount"] != DBNull.Value)
                {
                    if (projectReader["Amount"].ToString() != "")
                    {
                        projectList.Last().Amount = float.Parse(projectReader["Amount"].ToString());
                    }
                    else
                    {
                        projectList.Last().Amount = 0f;
                    }
                }
                else
                {
                    projectList.Last().Amount = 0f;
                }
                
            }

            // Close your connection in DBClass
            DBClass.DBConnection.Close();
            return Page();
        }
    }
}
