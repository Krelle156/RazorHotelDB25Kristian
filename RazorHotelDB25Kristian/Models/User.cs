namespace RazorHotelDB25Kristian.Models
{
    public class User
    {

        public string Username { get; set; }
        public string HashCode { get; set; }
        public string? ImagePath { get; set; }
        public int AccessLevel { get; private set; }

        public User(string username, string hash)
        {
            Username = username;
            HashCode = hash;
            AccessLevel = 0;
        }

        public void SetAccess(AccessLevel level)
        {
            AccessLevel = (int)level;
        }

    }
}
