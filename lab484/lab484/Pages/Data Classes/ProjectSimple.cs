using System.ComponentModel.DataAnnotations;

namespace lab484.Pages.Data_Classes
{
    public class ProjectSimple
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        [Required(ErrorMessage = "ProjectName is required")]
        public DateTime DueDate { get; set; }
        [Required(ErrorMessage = "DueDate is required")]
        public float Amount { get; set; }

    }
}
