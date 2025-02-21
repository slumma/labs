using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab484.Pages.Faculty
{
    public class EditGrantModel : PageModel
    {
        [BindProperty]
        public GrantSimple GrantToUpdate { get; set; }

        public void OnGet(int grantID)
        {
            // Fetch grant details using the grantID and populate GrantToUpdate
            SqlDataReader grantReader = DBClass.SingleGrantReader(grantID);

            if (grantReader.HasRows)
            {
                while (grantReader.Read())
                {
                    GrantToUpdate = new GrantSimple
                    {
                        GrantID = Int32.Parse(grantReader["GrantID"].ToString()),
                        ProjectID = grantReader["ProjectID"] != DBNull.Value ? (int?)Int32.Parse(grantReader["ProjectID"].ToString()) : null,
                        Supplier = grantReader["Supplier"].ToString(),
                        Project = grantReader["Project"] != DBNull.Value ? grantReader["Project"].ToString() : null,
                        Amount = float.Parse(grantReader["Amount"].ToString()),
                        Category = grantReader["Category"].ToString(),
                        Status = grantReader["StatusName"].ToString(),
                        Description = grantReader["descriptions"].ToString(),
                        SubmissionDate = DateTime.Parse(grantReader["SubmissionDate"].ToString()),
                        AwardDate = DateTime.Parse(grantReader["AwardDate"].ToString())
                    };
                }
            }

            grantReader.Close();
            DBClass.DBConnection.Close();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Update grant details
                DBClass.UpdateGrant(GrantToUpdate);
                return RedirectToPage("FacultyLanding");
            }

            return Page();
        }
    }
}
