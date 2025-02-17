using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab484.Pages
{
    public class IndexModel : PageModel
    {
        public List<User> userList { get; set; }

        public IndexModel()
        {
            userList = new List<User>();

        }

        public void OnGet()
        {
        }
    }
}
