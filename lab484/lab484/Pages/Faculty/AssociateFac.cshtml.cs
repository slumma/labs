using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab484.Pages.Faculty
{
    public class AssociateFacModel : PageModel
    {
    
        public int ProjectID { get; set; }
        public void OnGet(int ProjectID)
        {
            ProjectID = ProjectID;
        }
    }
}
