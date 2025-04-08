using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25Kristian.Interfaces;
using RazorHotelDB25Kristian.Models;

namespace RazorHotelDB25Kristian.Pages.Hotels
{
    public class CreateHotelModel : PageModel
    {
        private IHotelService _internalService;

        [BindProperty]
        public int HotelNo { get; set; }

        [BindProperty]
        public string HotelName { get; set; }

        [BindProperty]
        public string HotelAddress { get; set; }

        public string? SessionUsername { get; private set; }


        public CreateHotelModel(IHotelService hotelService)
        {
            _internalService = hotelService;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            SessionUsername = HttpContext.Session.GetString("Username");
            if (String.IsNullOrEmpty(SessionUsername)) return RedirectToPage("/Users/Login");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _internalService.CreateHotelAsync(new Hotel(HotelNo, HotelName, HotelAddress));
            return RedirectToPage("GetAllHotels") ;
        }
    }
}
