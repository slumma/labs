namespace lab484.Pages.Data_Classes
{
    public class GrantSimple
    {
        public int GrantID { get; set; }
        public String? Supplier { get; set; }
        public String? Project { get; set; }
        public float Amount { get; set; }
        public String? Category { get; set; }
        public String? Description { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime AwardDate { get; set; }
    }
}
