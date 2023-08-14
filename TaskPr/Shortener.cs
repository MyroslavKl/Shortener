using System.Security.Cryptography;
using System.Text;

namespace TaskPr
{
    public class Shortener
    {
        public string ShortenUrlInternal(string url)
        {
            string hash = ComputeHash(url);
            string shortUrl = hash.Substring(0, 6);
            return $"http://short.url/{shortUrl}";
        }

        public static string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
