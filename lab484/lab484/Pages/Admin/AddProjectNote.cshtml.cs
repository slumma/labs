using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace lab484.Pages.Admin
{
    public class AddProjectNoteModel : PageModel
    {
        [BindProperty]
        public ProjectNote newProjectNote { get; set; } = new ProjectNote();
        public ProjectSimple project { get; set; } = new ProjectSimple();
        public User user { get; set; } = new User();
        public int ProjectID { get; set; }
        public IActionResult OnGet(int ProjectID)
        {
            if (HttpContext.Session.GetInt32("loggedIn") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }
            else if (HttpContext.Session.GetInt32("facultyStatus") != 1 && HttpContext.Session.GetInt32("adminStatus") != 1)
            {
                HttpContext.Session.SetString("LoginError", "You do not have permission to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }

            this.ProjectID = ProjectID;

            SqlDataReader projectReader = DBClass.singleProjectReader(ProjectID);
            while (projectReader.Read())
            {
                project.ProjectName = projectReader["ProjectName"].ToString();
            }
            DBClass.DBConnection.Close();

            user = DBClass.GetUserByID(Convert.ToInt32(HttpContext.Session.GetInt32("userID")));
            DBClass.DBConnection.Close();

            newProjectNote.ProjectID = ProjectID;
            newProjectNote.AuthorFirst = user.FirstName;
            newProjectNote.AuthorLast = user.LastName;
            newProjectNote.AuthorID = user.UserID;
            newProjectNote.TimeAdded = DateTime.Now;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                DBClass.InsertProjectNote(newProjectNote);
                return RedirectToPage("ProjectDetail", new { ProjectID = newProjectNote.ProjectID });
            }
            return Page();
        }

        public IActionResult OnPostClear()
        {
            ModelState.Clear();
            newProjectNote = new ProjectNote();
            return Page();
        }
    }
}
