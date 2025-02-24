using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace lab484.Pages.Faculty
{
    public class DetailedViewModel : PageModel
    {
        // empty grant object to populate it
        public GrantSimple grant { get; set; }
        public IActionResult OnGet(int grantID)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }
            // fills the grant object with the info in the db so the user can see and edit it 
            grant = new GrantSimple(); // Initialize the grant object
            SqlDataReader grantReader = DBClass.SingleGrantReader(grantID);

            while (grantReader.Read())
            {
                grant.GrantID = Int32.Parse(grantReader["GrantID"].ToString());
                grant.GrantName = grantReader["GrantName"].ToString();
                grant.ProjectID = grantReader["ProjectID"] != DBNull.Value ? Convert.ToInt32(grantReader["ProjectID"]) : (int?)null; // Handle NULL ProjectID
                grant.Supplier = grantReader["Supplier"].ToString();
                grant.Project = grantReader["Project"].ToString();
                grant.Amount = float.Parse(grantReader["Amount"].ToString());
                grant.Category = grantReader["Category"].ToString();
                grant.Status = grantReader["StatusName"].ToString();
                grant.Description = grantReader["descriptions"].ToString();
                grant.SubmissionDate = DateTime.Parse(grantReader["SubmissionDate"].ToString());
                grant.AwardDate = DateTime.Parse(grantReader["AwardDate"].ToString());
            }
            DBClass.DBConnection.Close();
            return Page();
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("DetailedView");
        }
    }
}