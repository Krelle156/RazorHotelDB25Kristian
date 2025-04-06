using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25Kristian.Models;
using RazorHotelDB25Kristian.Interfaces;
using RazorHotelDB25Kristian.Services;
using Microsoft.IdentityModel.Tokens;

namespace RazorHotelDB25Kristian.Pages.Users
{
    public class RegisterModel : PageModel
    {
        private IUserService _internalService;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        public RegisterModel(IUserService userService)
        {
            _internalService = userService;
        }

        public void OnGet(string Username, string Password)
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!HttpContext.Session.GetString("Username").IsNullOrEmpty())
            {
                Message = "You are already logged in :)";
                return Page(); //Just a hopefully temporary check
            }

            if(await _internalService.UserExistsAsync(Username))
            {
                Message = "The username is already taken";
                return Page();
            }

            await _internalService.RegisterAsync(Username, Password);
            return RedirectToPage("/Index");
        }
    }
}
