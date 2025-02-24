using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab484.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnGet(string logout)
        {
            if (logout == "true")
            {
                HttpContext.Session.Clear();
                ViewData["LoginMessage"] = "Successfully Logged Out!";
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            string loginQuery = "SELECT COUNT(*) FROM users WHERE Username = '";
            loginQuery += Username + "' AND Password='" + Password + "'";

            if (DBClass.LoginQuery(loginQuery) > 0)
            {
                HttpContext.Session.SetString("username", Username);
                DBClass.DBConnection.Close();

                return RedirectToPage("Index");
            }
            else
            {
                ViewData["LoginMessage"] = "Username and/or Password Incorrect";
                DBClass.DBConnection.Close();
                return Page();
            }
        }

        public IActionResult OnPostLogoutHandler()
        {
            HttpContext.Session.Clear();
            return Page();
        }
    }
}
