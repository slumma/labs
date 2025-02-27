namespace lab484.Pages.Data_Classes
{
    public class GrantNote
    {
        public int GrantID { get; set; }
        public string Content { get; set; }
        public DateTime TimeAdded { get; set; }
        public int AuthorID { get; set; }
        public int AuthorLast { get; set; }
        public int AuthorFirst { get; set; }
    }
}
