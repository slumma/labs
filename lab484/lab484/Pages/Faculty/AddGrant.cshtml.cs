using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab484.Pages.Faculty
{
    public class AddGrantModel : PageModel
    {
        [BindProperty]
        public GrantSimple newGrant { get; set; }
        
        public static void OnGet()
        {

        }

        /*
        public IActionResult OnPost()
        {
            
            DBClass.InsertGrant(newGrant);
            DBClass.DBConnection.Close();s
            return RedirectToPage("FacultyLanding"); 
        }*/

    }
}


