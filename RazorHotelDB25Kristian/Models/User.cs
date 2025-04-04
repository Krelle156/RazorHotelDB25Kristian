namespace RazorHotelDB25Kristian.Models
{
    public class User
    {
        private string _userName;
        private string _hashCode;

        public User(string user, string hash)
        {
            _userName = user;
            _hashCode = hash;
        }


    }
}
