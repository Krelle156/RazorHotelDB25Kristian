using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25Kristian.Interfaces;
using RazorHotelDB25Kristian.Models;

namespace RazorHotelDB25Kristian.Pages.Users
{
    public class ViewAllUsersModel : PageModel
    {
        private IUserService _internalService;

        public List<User> UserList { get; set; }

        public ViewAllUsersModel(IUserService userService)
        {
            _internalService = userService;
        }

        public async Task OnGet()
        {
            UserList = await _internalService.GetAllUsers();
        }
    }
}
