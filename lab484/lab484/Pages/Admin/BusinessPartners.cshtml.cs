using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab484.Pages.Admin
{
    public class BusinessPartnersModel : PageModel
    {
        public required List<BPrep> projectList { get; set; } = new List<BPrep>();
        public void OnGet()
        {

        }
    }
}
