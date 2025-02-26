using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace lab484.Pages
{
    public class MessagesModel : PageModel
    {
        [BindProperty]
        public string SelectedUsername { get; set; }
        public List<SelectListItem> Usernames { get; set; }

        public User activeUser { get; set; }

        public string usr { get; set; } // Declare usr as a property of the class

        public required List<Message> receivedList { get; set; } = new List<Message>();

        public IActionResult OnGet()
        {
            // Retrieve the username from the session here
            usr = HttpContext.Session.GetString("username");

            if (usr == null)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("Index"); // Redirect to login page
            }

            Usernames = new List<SelectListItem>();

            // Execute the userReader method from DBClass to load the usernames 
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

            using (SqlDataReader singleUserReader = DBClass.SingleUserReader(usr))
            {
                if (singleUserReader != null)
                {
                    while (singleUserReader.Read())
                    {
                        activeUser = new User
                        {
                            UserID = int.Parse(singleUserReader["UserID"].ToString()),
                            UserName = singleUserReader["Username"].ToString(),
                            FirstName = singleUserReader["FirstName"].ToString(),
                            LastName = singleUserReader["Lastname"].ToString(),
                            Email = singleUserReader["Email"].ToString()
                        };
                    }
                    singleUserReader.Close();
                }
            }

            DBClass.DBConnection.Close();

            // Retrieve received messages
            using (SqlDataReader receivedReader = DBClass.singleRecipientReader(activeUser.UserID))
            {
                while (receivedReader.Read())
                {
                    receivedList.Add(new Message
                    {
                        SenderID = int.Parse(receivedReader["SenderID"].ToString()),
                        SenderUsername = receivedReader["SenderUsername"].ToString(),
                        RecipientID = int.Parse(receivedReader["RecipientID"].ToString()),
                        RecipientUsername = receivedReader["RecipientUsername"].ToString(),
                        SubjectTitle = receivedReader["SubjectTitle"].ToString(),
                        Contents = receivedReader["Contents"].ToString(),
                        MessageID = int.Parse(receivedReader["MessageID"].ToString()),
                        SentTime = DateTime.Parse(receivedReader["SentTime"].ToString())
                    });
                }
                receivedReader.Close();
            }

            DBClass.DBConnection.Close();

            return Page();
        }

        public IActionResult OnPost()
        {
            // Sends the userID to the ViewMessages page in order to see the sent/received messages 
            int userID = Convert.ToInt32(SelectedUsername);
            return RedirectToPage("ViewMessages", new { userID });
        }
    }
}
