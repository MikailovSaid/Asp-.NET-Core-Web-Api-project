namespace NewsApp.Core.Models
{
    public class LoginLog
    {
        public int Id { get; set; }
        public DateTime? DateTime { get; set; }
        public int UserId { get; set; }
        public string IP { get; set; }
    }
}
