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
        public User user { get; set; }
        public List<TaskStaff> taskStaffList { get; set; } = new List<TaskStaff>();
        public List<Tasks> taskList { get; set; } = new List<Tasks>();
        public void OnGet(int projectID)
        {
            project = new ProjectSimple();

            SqlDataReader singleProjectReader = DBClass.singleProjectReader(projectID);
            while (singleProjectReader.Read())
            {
                project.ProjectName = singleProjectReader["ProjectName"].ToString();
                project.DueDate = DateTime.Parse(singleProjectReader["DueDate"].ToString());
                project.Amount = float.Parse(singleProjectReader["Amount"].ToString());
            }
            DBClass.DBConnection.Close();

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

            
        }
    }
}
/*
        public int TaskStaffID { get; set; }
        public int TaskID { get; set; }
        public int AssigneeID { get; set; }
        public int AssignerID { get; set; }
        public DateTime DueDate { get; set; }*/