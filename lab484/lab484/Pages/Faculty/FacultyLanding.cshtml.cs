using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab484.Pages.Faculty
{
    public class FacultyLandingModel : PageModel
    {
        public required List<GrantSimple> grantList { get; set; } = new List<GrantSimple>();
        public void OnGet()
        {
            SqlDataReader grantReader = DBClass.GrantReader();
            while (grantReader.Read())
            {
                grantList.Add(new GrantSimple
                {
                    GrantID = Int32.Parse(grantReader["GrantID"].ToString()),
                    Supplier = grantReader["Supplier"].ToString(),
                    Project = grantReader["Project"].ToString(),
                    Amount = float.Parse(grantReader["Amount"].ToString()),  // fix
                    Category = grantReader["Category"].ToString(),
                    Description = grantReader["Description"].ToString(),
                    SubmissionDate = DateTime.Parse(grantReader["SubmissionDate"].ToString()),
                    AwardDate = DateTime.Parse(grantReader["AwardDate"].ToString())
                });
            }

            // Close your connection in DBClass
            DBClass.DBConnection.Close();
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("/Faculty/DetailedView");

        }
    }
}
