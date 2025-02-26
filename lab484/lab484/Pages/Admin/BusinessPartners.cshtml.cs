using InventoryManagement.Pages.DB;
using System.Data.SqlClient;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;

namespace lab484.Pages.Admin
{
    public class BusinessPartnersModel : PageModel
    {
        public required List<BusinessPartner> bpList { get; set; } = new List<BusinessPartner>();
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }
            else if (HttpContext.Session.GetInt32("adminStatus") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You do not have permission to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }

            // populate BusinessPartners to be shown in view 
            SqlDataReader bpReader = DBClass.BPReader();
            while (bpReader.Read())
            {
                bpList.Add(new BusinessPartner
                {
                    UserID = Int32.Parse(bpReader["UserID"].ToString()),
                    FirstName = bpReader["FirstName"].ToString(),
                    LastName = bpReader["LastName"].ToString(),
                    Email = bpReader["Email"].ToString(),
                    Phone = bpReader["Phone"].ToString(),
                    HomeAddress = bpReader["HomeAddress"].ToString(),
                    CommunicationStatus = bpReader["CommunicationStatus"].ToString(),
                    SupplierID = Int32.Parse(bpReader["SupplierID"].ToString()),
                    SupplierName = bpReader["SupplierName"].ToString(),
                    OrgType = bpReader["OrgType"].ToString(),
                    StatusName = bpReader["StatusName"].ToString()
                });
            }
            DBClass.DBConnection.Close();
            return Page();
        }
    }
}

