using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab484.Pages
{
    public class SendMessagesModel : PageModel
    {
        // not done yet 
        public int UserID { get; set; }
        public void OnGet(int userID)
        {
            this.UserID = userID;
        }
    }
}
