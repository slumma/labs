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
        public void OnGet()
        {
            SqlDataReader projectReader = DBClass.ProjectReader();
            while (projectReader.Read())
            {
                projectList.Add(new ProjectSimple
                {
                    ProjectID = Int32.Parse(projectReader["ProjectID"].ToString()),
                    ProjectName = projectReader["ProjectName"].ToString(),
                    Amount = float.Parse(projectReader["Amount"].ToString()),  
                    DueDate = DateTime.Parse(projectReader["DueDate"].ToString())
                });
            }

            // Close your connection in DBClass
            DBClass.DBConnection.Close();
        }
    }
}

//test commit