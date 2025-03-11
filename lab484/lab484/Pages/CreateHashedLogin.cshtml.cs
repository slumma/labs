using lab484.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InventoryManagement.Pages.Practice
{
    public class CreateHashedLoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Perform Validation First on Form
            // then...

            DBClass.CreateHashedUser(Username, Password);
            DBClass.DBConnection.Close();

            // Perform actual logic to check if user was successfully
            //  added in your projects but for demo purposes we can say:

            ViewData["UserCreate"] = "User Successfully Created!";

            return RedirectToPage("HashedLogin");
        }
    }
}
