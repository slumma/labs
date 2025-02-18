using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab484.Pages.Faculty
{
    public class AssociateFacModel : PageModel
    {

        public required List<ProjectStaff> staffList { get; set; } = new List<ProjectStaff>();
        public void OnGet(int ProjectID)
        {
            SqlDataReader facultyReader = DBClass.singleFacultyReader(ProjectID);
            while (facultyReader.Read())
            {
                staffList.Add(new ProjectStaff
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

            // Close your connection in DBClass
            DBClass.DBConnection.Close();
        }
    }
}
