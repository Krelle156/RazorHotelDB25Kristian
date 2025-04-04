using RazorHotelDB25Kristian.Models;

namespace RazorHotelDB25Kristian.Interfaces
{
    public interface IUserService
    {

        Task<bool> RegisterAsync(string newUserName, string newCode);

        Task<User> LoginAsync(string UserName, string Code);

        Task<List<User>> GetAllUsers();
    }
}
