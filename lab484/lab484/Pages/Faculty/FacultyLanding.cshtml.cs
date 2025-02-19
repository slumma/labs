using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace lab484.Pages.Faculty
{
    public class FacultyLandingModel : PageModel
    {
        public required List<GrantSimple> grantList { get; set; } = new List<GrantSimple>();

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        public string CurrentSortOrder { get; set; }

        public void OnGet()
        {
            SqlDataReader grantReader = DBClass.GrantReader();
            while (grantReader.Read())
            {
                grantList.Add(new GrantSimple
                {
                    GrantID = Int32.Parse(grantReader["GrantID"].ToString()),
                    ProjectID = Int32.Parse(grantReader["ProjectID"].ToString()),
                    Supplier = grantReader["Supplier"].ToString(),
                    Project = grantReader["Project"].ToString(),
                    Amount = float.Parse(grantReader["Amount"].ToString()),
                    Category = grantReader["Category"].ToString(),
                    Status = grantReader["StatusName"].ToString(),
                    Description = grantReader["descriptions"].ToString(),
                    SubmissionDate = DateTime.Parse(grantReader["SubmissionDate"].ToString()),
                    AwardDate = DateTime.Parse(grantReader["AwardDate"].ToString())
                });
            }

            // Close your connection in DBClass
            DBClass.DBConnection.Close();

            // links up to AI usage on the view, this switch statement allows the program to sort the grants by the selected sort order
            switch (SortOrder)
            {
                case "amount_asc":
                    grantList = grantList.OrderBy(g => g.Amount).ToList();
                    break;
                case "amount_desc":
                    grantList = grantList.OrderByDescending(g => g.Amount).ToList();
                    break;
                case "date_asc":
                    grantList = grantList.OrderBy(g => g.AwardDate).ToList();
                    break;
                case "date_desc":
                    grantList = grantList.OrderByDescending(g => g.AwardDate).ToList();
                    break;
                case "name_desc":
                    grantList = grantList.OrderByDescending(g => g.GrantID).ToList();
                    break;
                default:
                    grantList = grantList.OrderBy(g => g.GrantID).ToList();
                    break;
            }

            CurrentSortOrder = SortOrder;
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("DetailedView");
        }
    }
}
