namespace NewsApp.Core.Dtos
{
    public class LoginReqDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? IP { get; set; }
    }
}
