using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Diagnostics;

namespace lab484.Pages
{
    public class MessagesModel : PageModel
    {
        [BindProperty]
        public string SelectedUsername { get; set; }
        public List<SelectListItem> Usernames { get; set; }

        public void OnGet()
        {
            Usernames = new List<SelectListItem>();

            // execute the userReader method from dbclass to load the usernames 
            using (SqlDataReader reader = DBClass.UserReader())
            {
                while (reader.Read())
                {
                    Usernames.Add(new SelectListItem
                    {
                        Value = reader["UserID"].ToString(),
                        Text = reader["Username"].ToString()
                    });
                }
                reader.Close();
                DBClass.DBConnection.Close();
            }
        }

        public IActionResult OnPost()
        {
            // sends the userID to the ViewMessages page in order to see the sent/received  messages 
            int userID = Convert.ToInt32(SelectedUsername);
            return RedirectToPage("ViewMessages", new { userID });
        }
    }
}
