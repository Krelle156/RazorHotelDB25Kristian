using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25Kristian.Interfaces;

namespace RazorHotelDB25Kristian.Pages.Users
{
    public class LoginModel : PageModel
    {
        private IUserService _internalService;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        public LoginModel(IUserService userService)
        {
            _internalService = userService;
            Message = "";
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (await _internalService.LoginAsync(Username, Password) != null)
            {
                Message = "Login Succesful";
                return Page();
            }
            Message = "Login Failed";
            return Page();
        }
    }
}
