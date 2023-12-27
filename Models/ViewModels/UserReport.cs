namespace HMS.Models.ViewModels
{
    public class UserReport
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Photo { get; set; }
        public string Role { get; set; }
        public bool Inactive { set; get; }
    }
}
