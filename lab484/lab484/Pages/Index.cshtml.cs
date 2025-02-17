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
            SqlDataReader usrReader = DBClass.UserReader();
            while (usrReader.Read())
            {
                userList.Add(new User
                {
                    UserID = Int32.Parse(usrReader["UserID"].ToString()),
                    UserName = usrReader["UserName"].ToString(),
                    Password = usrReader["Password"].ToString(),
                    FirstName = usrReader["FirstName"].ToString(),
                    LastName = usrReader["LastName"].ToString()
                });
            }

            // Close your connection in DBClass
            DBClass.DBConnection.Close();
        }
    }
}
