using RazorHotelDB25Kristian.Models;

namespace RazorHotelDB25Kristian.Interfaces
{
    public interface IUserService
    {

        Task<bool> RegisterAsync(string newUserName, string newCode);

        Task<User> LoginAsync(string userName, string Code);

        /// <summary>
        /// Checks if a given username exists in the database.
        /// </summary>
        /// <param name="UserName">The name and primary key of the user we are looking for</param>
        /// <returns>True if there is a user with that username and false if not</returns>
        Task<bool> UserExistsAsync(string userName);

        Task<List<User>> GetAllUsers();
    }
}
