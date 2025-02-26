﻿using InventoryManagement.Pages.DB;
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

        public User activeUser { get; set; }

        public String usr { get; set; } // Declare usr as a property of the class

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

            
            using (SqlDataReader singleUserReader = DBClass.SingleUserReader(usr))
            {
                if (singleUserReader != null)
                {
                    while (singleUserReader.Read())
                    {
                        activeUser = new User
                        {
                            UserID = Int32.Parse(singleUserReader["UserID"].ToString()),
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
            SqlDataReader receivedReader = DBClass.singleRecipientReader(activeUser.UserID);
            while (receivedReader.Read())
            {
                receivedList.Add(new Message
                {
                    SenderID = Int32.Parse(receivedReader["SenderID"].ToString()),
                    SenderUsername = receivedReader["SenderUsername"].ToString(),
                    RecipientID = Int32.Parse(receivedReader["RecipientID"].ToString()),
                    RecipientUsername = receivedReader["RecipientUsername"].ToString(),
                    SubjectTitle = receivedReader["SubjectTitle"].ToString(),
                    Contents = receivedReader["Contents"].ToString(),
                    MessageID = Int32.Parse(receivedReader["MessageID"].ToString()),
                    SentTime = DateTime.Parse(receivedReader["SentTime"].ToString())
                });
            }

            // Close your connection in DBClass
            DBClass.DBConnection.Close();

            return Page();
        }

        public IActionResult OnPost()
        {
            // sends the userID to the ViewMessages page in order to see the sent/received messages 
            int userID = Convert.ToInt32(SelectedUsername);
            return RedirectToPage("ViewMessages", new { userID });
        }
    }
}
