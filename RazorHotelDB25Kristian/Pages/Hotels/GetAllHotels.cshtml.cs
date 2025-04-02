using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25Kristian.Interfaces;
using RazorHotelDB25Kristian.Models;

namespace RazorHotelDB25Kristian.Pages.Hotels
{
    public class GetAllHotelsModel : PageModel
    {
        private IHotelService _hotelService;

        public List<Hotel> Hotels { get; set; }

        public GetAllHotelsModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task OnGetAsync()
        {
            Hotels = await _hotelService.GetAllHotelAsync();
        }

        public async Task<IActionResult> OnPostDelete(int DeleteNo)
        {
            await _hotelService.DeleteHotelAsync(DeleteNo);
            return RedirectToPage("GetAllHotels");
        }
    }
}
