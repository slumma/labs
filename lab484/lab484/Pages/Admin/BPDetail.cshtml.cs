using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab484.Pages.Admin
{
    public class BPDetailModel : PageModel
    {
        public BusinessPartner BP {  get; set; }


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

            SqlDataReader BPReader = DBClass.BPReader();
            BP = new BusinessPartner();

            while (BPReader.Read())
            {
                BP.SupplierID = Convert.ToInt32(BPReader["SupplierID"].ToString());
            }
            DBClass.DBConnection.Close();

            return Page();
        }
    }
}
