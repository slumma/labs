using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace lab484.Pages.Faculty
{
    public class DetailedViewModel : PageModel
    {
        public GrantSimple grant { get; set; }
        public void OnGet(int grantID)
        {
            grant = new GrantSimple(); // Initialize the grant object
            SqlDataReader grantReader = DBClass.SingleGrantReader(grantID);

            while (grantReader.Read())
            {
                grant.GrantID = Int32.Parse(grantReader["GrantID"].ToString());
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
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("DetailedView");
        }
    }
}