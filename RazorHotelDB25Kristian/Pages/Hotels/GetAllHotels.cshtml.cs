using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using RazorHotelDB25Kristian.Interfaces;
using RazorHotelDB25Kristian.Models;
using RazorHotelDB25Kristian.Helpers;

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

        public async Task<IActionResult> OnGetUpdateAsync(string query)
        {
            Hotels = await _hotelService.GetAllHotelAsync();
            if (String.IsNullOrEmpty(query)) return new JsonResult(Hotels);

            return new JsonResult(DLStringComparer<Hotel>.Matches(Hotels, x=>x.Navn, query, 1));
        }

        public async Task<IActionResult> OnPostDeleteAsync(int deleteNo)
        {
            await _hotelService.DeleteHotelAsync(deleteNo);
            return RedirectToPage("GetAllHotels");
        }
    }
}
