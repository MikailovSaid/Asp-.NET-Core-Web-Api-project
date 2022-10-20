using System.Security.Cryptography;
using System.Text;

namespace NewsApp.Utilities.Auth
{
    public class HashPassword
    {
        public static string HashPass(string password)
        {
            var sha1 = SHA1.Create();
            var step1 = Encoding.UTF8.GetBytes(password);
            var step2 = sha1.ComputeHash(step1);
            var encryted_password = Encoding.Default.GetString(step2);
            return encryted_password;
        }
    }
}
