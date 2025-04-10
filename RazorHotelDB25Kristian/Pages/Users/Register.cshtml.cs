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
        private IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public IFormFile Image {get; set;}

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }
        public string? SessionUsername { get; private set; }

        public RegisterModel(IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _internalService = userService;
            _webHostEnvironment = webHostEnvironment;
        }

        public void OnGet(string Username, string Password)
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            SessionUsername = HttpContext.Session.GetString("Username");
            if (!SessionUsername.IsNullOrEmpty())
            {
                Message = "You are already logged in :)";
                return Page(); //Just a hopefully temporary check
            }

            if(await _internalService.UserExistsAsync(Username))
            {
                Message = "The username is already taken";
                return Page();
            }

            string? imagePath = null;

            if(Image != null)
            {
                imagePath = ProcessUploadedFile();
            }

            await _internalService.RegisterAsync(Username, Password, imagePath);
            return RedirectToPage("/Index");
        }


        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/memberimages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
