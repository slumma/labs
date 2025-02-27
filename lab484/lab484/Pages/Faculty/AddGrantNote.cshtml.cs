using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab484.Pages.Faculty
{
    public class AddGrantNoteModel : PageModel
    {
        public GrantNote GrantNote { get; set; } = new GrantNote();
        public void OnGet()
        {
        }
    }
}
