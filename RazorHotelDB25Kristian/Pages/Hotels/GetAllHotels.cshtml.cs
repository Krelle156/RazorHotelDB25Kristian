using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using RazorHotelDB25Kristian.Interfaces;
using RazorHotelDB25Kristian.Models;

namespace RazorHotelDB25Kristian.Pages.Hotels
{
    public class GetAllHotelsModel : PageModel
    {
        private IHotelService _hotelService;

        public List<Hotel> Hotels { get; set; }

        public string? SessionUsername { get; private set; }

        public GetAllHotelsModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            SessionUsername = HttpContext.Session.GetString("Username");
            if (String.IsNullOrEmpty(SessionUsername)) return RedirectToPage("/Users/Login");

            Hotels = await _hotelService.GetAllHotelAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int DeleteNo)
        {
            await _hotelService.DeleteHotelAsync(DeleteNo);
            return RedirectToPage("GetAllHotels");
        }
    }
}
