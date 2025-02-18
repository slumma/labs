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
                GrantToUpdate.Status = grantReader["StatusName"].ToString();
                GrantToUpdate.Description = grantReader["descriptions"].ToString();
                GrantToUpdate.SubmissionDate = DateTime.Parse(grantReader["SubmissionDate"].ToString());
                GrantToUpdate.AwardDate = DateTime.Parse(grantReader["AwardDate"].ToString());
            }
            DBClass.DBConnection.Close();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) // if the inputs are not valid (either null or unallowed) it wont save
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
            GrantToUpdate.Status = string.Empty;
            GrantToUpdate.Description = string.Empty;

            ModelState.Clear(); // Clear the model state to refresh the form

            return Page();
        }
    }
}
