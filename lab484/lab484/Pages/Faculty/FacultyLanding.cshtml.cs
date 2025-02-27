using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;



// where all users land when logging in as faculty
namespace lab484.Pages.Faculty
{
    public class FacultyLandingModel : PageModel
    {
        // initialize lists and variables to be used 


        public bool testerOpposite;
        public bool testerCurrent;


        public required List<GrantSimple> grantList { get; set; } = new List<GrantSimple>();

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        public string CurrentSortOrder { get; set; }

        [BindProperty]
        public bool DisplayAll { get; set; }
        [BindProperty]
        public String TableButton { get; set; } = "Expand";
        
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }
            else if (HttpContext.Session.GetInt32("facultyStatus") != 1 && HttpContext.Session.GetInt32("adminStatus") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You do not have permission to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }
            HttpContext.Session.SetInt32("DisplayAll", 0);

            if (HttpContext.Session.GetInt32("adminStatus") == 1)
            {
                SqlDataReader grantReader = DBClass.adminGrantReader();
                while (grantReader.Read())
                {
                    grantList.Add(new GrantSimple
                    {
                        GrantID = Convert.ToInt32(grantReader["GrantID"]),
                        GrantName = grantReader["GrantName"].ToString(),
                        ProjectID = grantReader["ProjectID"] != DBNull.Value ? Convert.ToInt32(grantReader["ProjectID"]) : (int?)null, // Handle NULL ProjectID
                        Supplier = grantReader["Supplier"].ToString(),
                        Project = grantReader["Project"].ToString(), // Handle NULL Project
                        Amount = Convert.ToSingle(grantReader["Amount"]),
                        Category = grantReader["Category"].ToString(),
                        Status = grantReader["GrantStatus"].ToString(),
                        Description = grantReader["descriptions"].ToString(),
                        SubmissionDate = Convert.ToDateTime(grantReader["SubmissionDate"]),
                        AwardDate = Convert.ToDateTime(grantReader["AwardDate"])
                    });
                }
            }
            else if (HttpContext.Session.GetInt32("facultyStatus") == 1)
            {
                // reads the db for grants for specific user
                int currentUserID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));
                SqlDataReader grantReader = DBClass.facGrantReader(currentUserID);
                while (grantReader.Read())
                {
                    grantList.Add(new GrantSimple
                    {
                        GrantID = Convert.ToInt32(grantReader["GrantID"]),
                        GrantName = grantReader["GrantName"].ToString(),
                        ProjectID = grantReader["ProjectID"] != DBNull.Value ? Convert.ToInt32(grantReader["ProjectID"]) : (int?)null, // Handle NULL ProjectID
                        Supplier = grantReader["Supplier"].ToString(),
                        Project = grantReader["Project"].ToString(), // Handle NULL Project
                        Amount = Convert.ToSingle(grantReader["Amount"]),
                        Category = grantReader["Category"].ToString(),
                        Status = grantReader["GrantStatus"].ToString(),
                        Description = grantReader["descriptions"].ToString(),
                        SubmissionDate = Convert.ToDateTime(grantReader["SubmissionDate"]),
                        AwardDate = Convert.ToDateTime(grantReader["AwardDate"])
                    });
                }
            }
            

            // Close your connection in DBClass
            DBClass.DBConnection.Close();

            // links up to AI usage on the view, this switch statement allows the program to sort the grants by the selected sort order
            // allows for the columns to be sorted 
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
            return Page();
        }


        public IActionResult OnPost()
        {
            // redirects to the detailed view page with the specific grant using asp-route
            return RedirectToPage("DetailedView");
        }

        public IActionResult OnPostToggleTable()
        {
            bool DisplayAll = (HttpContext.Session.GetInt32("DisplayAll") == 1);
            

            if (DisplayAll)
            {
                TableButton = "Expand";
                HttpContext.Session.SetInt32("DisplayAll", 0);
            }
            else
            {
                TableButton = "Collapse";
                HttpContext.Session.SetInt32("DisplayAll", 1);
            }

            if (HttpContext.Session.GetString("username") == null)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }

            if (HttpContext.Session.GetInt32("adminStatus") == 1)
            {
                SqlDataReader grantReader = DBClass.adminGrantReader();
                while (grantReader.Read())
                {
                    grantList.Add(new GrantSimple
                    {
                        GrantID = Convert.ToInt32(grantReader["GrantID"]),
                        GrantName = grantReader["GrantName"].ToString(),
                        ProjectID = grantReader["ProjectID"] != DBNull.Value ? Convert.ToInt32(grantReader["ProjectID"]) : (int?)null, // Handle NULL ProjectID
                        Supplier = grantReader["Supplier"].ToString(),
                        Project = grantReader["Project"].ToString(), // Handle NULL Project
                        Amount = Convert.ToSingle(grantReader["Amount"]),
                        Category = grantReader["Category"].ToString(),
                        Status = grantReader["GrantStatus"].ToString(),
                        Description = grantReader["descriptions"].ToString(),
                        SubmissionDate = Convert.ToDateTime(grantReader["SubmissionDate"]),
                        AwardDate = Convert.ToDateTime(grantReader["AwardDate"])
                    });
                }
            }
            else if (HttpContext.Session.GetInt32("facultyStatus") == 1)
            {
                // reads the db for grants for specific user
                int currentUserID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));
                SqlDataReader grantReader = DBClass.facGrantReader(currentUserID);
                while (grantReader.Read())
                {
                    grantList.Add(new GrantSimple
                    {
                        GrantID = Convert.ToInt32(grantReader["GrantID"]),
                        GrantName = grantReader["GrantName"].ToString(),
                        ProjectID = grantReader["ProjectID"] != DBNull.Value ? Convert.ToInt32(grantReader["ProjectID"]) : (int?)null, // Handle NULL ProjectID
                        Supplier = grantReader["Supplier"].ToString(),
                        Project = grantReader["Project"].ToString(), // Handle NULL Project
                        Amount = Convert.ToSingle(grantReader["Amount"]),
                        Category = grantReader["Category"].ToString(),
                        Status = grantReader["GrantStatus"].ToString(),
                        Description = grantReader["descriptions"].ToString(),
                        SubmissionDate = Convert.ToDateTime(grantReader["SubmissionDate"]),
                        AwardDate = Convert.ToDateTime(grantReader["AwardDate"])
                    });
                }
            }
            // Close your connection in DBClass
            DBClass.DBConnection.Close();

            // links up to AI usage on the view, this switch statement allows the program to sort the grants by the selected sort order
            // allows for the columns to be sorted 
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

            return Page();
        }
    }
}
