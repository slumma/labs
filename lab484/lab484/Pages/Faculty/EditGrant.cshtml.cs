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

        public EditGrantModel()
        {
            GrantToUpdate = new GrantSimple();
        }

        public void OnGet(int grantID)
        {
            SqlDataReader grantReader = DBClass.SingleGrantReader(grantID);

            while (grantReader.Read())
            {
                GrantToUpdate.GrantID = Int32.Parse(grantReader["GrantID"].ToString());
                GrantToUpdate.Supplier = grantReader["Supplier"].ToString();
                GrantToUpdate.Project = grantReader["Project"].ToString();
                GrantToUpdate.Amount = float.Parse(grantReader["Amount"].ToString());
                GrantToUpdate.Category = grantReader["Category"].ToString();
                GrantToUpdate.Status = grantReader["StatusName"].ToString();
                GrantToUpdate.Description = grantReader["descriptions"].ToString();
                GrantToUpdate.SubmissionDate = DateTime.Parse(grantReader["SubmissionDate"].ToString());
                GrantToUpdate.AwardDate = DateTime.Parse(grantReader["AwardDate"].ToString());

            }

            grantReader.Close();

            DBClass.DBConnection.Close();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            DBClass.UpdateGrant(GrantToUpdate);
            return RedirectToPage("DetailedView", new { grantID = GrantToUpdate.GrantID });
        }


        public IActionResult OnPostClear()
        {
            GrantToUpdate.Supplier = string.Empty;
            GrantToUpdate.Project = string.Empty;
            GrantToUpdate.Amount = 0;
            GrantToUpdate.Category = string.Empty;
            GrantToUpdate.Status = string.Empty;
            GrantToUpdate.Description = string.Empty;

            ModelState.Clear(); // clear the model state to refresh the form

            return Page();
        }

    }
}
