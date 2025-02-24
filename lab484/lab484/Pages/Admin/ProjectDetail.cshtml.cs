using InventoryManagement.Pages.DB;
using lab484.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Common;
using System.Data.SqlClient;

namespace lab484.Pages.Admin
{
    public class ProjectDetailModel : PageModel
    {
        public ProjectSimple project { get; set; }
        public List<User> userProjectList { get; set; } = new List<User>();
        public List<User> userTaskList { get; set; } = new List<User>();
        public List<TaskStaff> taskStaffList { get; set; } = new List<TaskStaff>();
        public List<Tasks> taskList { get; set; } = new List<Tasks>();
        public IActionResult OnGet(int projectID)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                HttpContext.Session.SetString("LoginError", "You must login to access that page!");
                return RedirectToPage("../Index"); // Redirect to login page
            }

            project = new ProjectSimple();

            // populates the project object with details 
            SqlDataReader singleProjectReader = DBClass.singleProjectReader(projectID);
            while (singleProjectReader.Read())
            {
                project.ProjectName = singleProjectReader["ProjectName"].ToString();
                project.DueDate = DateTime.Parse(singleProjectReader["DueDate"].ToString());
                project.Amount = float.Parse(singleProjectReader["Amount"].ToString());
            }
            DBClass.DBConnection.Close();

            // populates the staff list 
            SqlDataReader projectStaffReader = DBClass.projectStaffReader(projectID);
            while (projectStaffReader.Read())
            {
                userProjectList.Add(new User
                {
                    FirstName = projectStaffReader["FirstName"].ToString(),
                    LastName = projectStaffReader["LastName"].ToString(),
                    Phone = projectStaffReader["Phone"].ToString(),
                    Email = projectStaffReader["Email"].ToString()
                });
            }
            DBClass.DBConnection.Close();

            // populates the task list
            SqlDataReader taskStaffReader = DBClass.taskStaffReader(projectID);
            while (taskStaffReader.Read())
            {
                taskStaffList.Add(new TaskStaff
                {
                    TaskStaffID = Int32.Parse(taskStaffReader["TaskStaffID"].ToString()),
                    TaskID = Int32.Parse(taskStaffReader["TaskID"].ToString()),
                    AssigneeID = Int32.Parse(taskStaffReader["AssigneeID"].ToString()),
                    AssignerID = Int32.Parse(taskStaffReader["AssignerID"].ToString()),
                    DueDate = DateTime.Parse(taskStaffReader["DueDate"].ToString())
                });
                userTaskList.Add(new User
                {
                    FirstName = taskStaffReader["FirstName"].ToString(),
                    LastName = taskStaffReader["LastName"].ToString()
                });
            }
            DBClass.DBConnection.Close();

            SqlDataReader taskReader = DBClass.taskReader(projectID);
            while (taskReader.Read())
            {
                taskList.Add(new Tasks
                {
                    TaskID = Int32.Parse(taskReader["TaskID"].ToString()),
                    Objective = taskReader["Objective"].ToString(),
                    DueDate = DateTime.Parse(taskReader["DueDate"].ToString())
                });
            }
            DBClass.DBConnection.Close();

            return Page();
        }
    }
}
