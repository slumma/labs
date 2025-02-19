namespace lab484.Pages.Data_Classes
{
    public class Message
    {
        public int SenderID { get; set; }
        public string SenderUsername { get; set; }
        public string SubjectTitle { get; set; }
        public string Contents { get; set; }
        public DateTime SentTime { get; set; }
    }
}
