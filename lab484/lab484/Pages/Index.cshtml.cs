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

            string loginError = HttpContext.Session.GetString("LoginError");
            if (!string.IsNullOrEmpty(loginError))
            {
                ViewData["LoginMessage"] = loginError;
                HttpContext.Session.Remove("LoginError"); // Clear after displaying
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            string loginQuery = "SELECT COUNT(*) FROM users WHERE Username = '";
            loginQuery += Username + "' AND Password='" + Password + "'";

            string findUserID = "SELECT UserID FROM users WHERE Username = '";
            findUserID += Username + "' AND Password='" + Password + "'";

            if (DBClass.LoginQuery(loginQuery) > 0)
            {
                HttpContext.Session.SetInt32("loggedIn", 1);

                DBClass.DBConnection.Close();
                HttpContext.Session.SetString("username", Username);
                
                //get userID into session state
                int userID = DBClass.loggedInUser(findUserID);
                DBClass.DBConnection.Close();
                HttpContext.Session.SetInt32("userID", userID);

                //check user permissions
                int adminStatus = DBClass.adminCheck(userID);
                DBClass.DBConnection.Close();

                if (adminStatus == 1)
                {
                    HttpContext.Session.SetInt32("adminStatus", 1);
                    return RedirectToPage("/Admin/AdminLanding");
                }
                else
                {
                    int facultyStatus = DBClass.facultyCheck(userID);
                    DBClass.DBConnection.Close();

                    if (facultyStatus == 1)
                    {
                        HttpContext.Session.SetInt32("facultyStatus", 1);
                        return RedirectToPage("/Faculty/FacultyLanding");
                    }
                    else
                    {
                        return RedirectToPage("/NoPermissions");
                    }
                }
                

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
