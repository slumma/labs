namespace lab484.Pages.Data_Classes
{
    public class User
    {
        public int UserID { get; set; }
        public String? UserName { get; set; }
        public String? Password { get; set; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public String? Email { get; set; }
        public String? Phone { get; set; }
        public String? HomeAddress { get; set; }
    }

    public class Employee : User
    {
        public Boolean? AdminStatus { get; set; }

    }

    public class BPrep : User
    {
        public String? CommunicationStatus { get; set; }
    }

    public class Faculty : User
    {
    }

    public class NonFaculty : User
    {
    }
}
