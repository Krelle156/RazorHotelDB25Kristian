using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25Kristian.Models;
using RazorHotelDB25Kristian.Interfaces;
using RazorHotelDB25Kristian.Services;

namespace RazorHotelDB25Kristian.Pages.Users
{
    public class RegisterModel : PageModel
    {
        private IUserService _internalService;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public RegisterModel(IUserService userService)
        {
            _internalService = userService;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _internalService.RegisterAsync(Username, Password);
            return RedirectToPage("/Index");
        }
    }
}
