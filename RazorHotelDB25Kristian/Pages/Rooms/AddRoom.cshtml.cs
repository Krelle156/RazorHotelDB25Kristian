using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25Kristian.Interfaces;
using RazorHotelDB25Kristian.Models;

namespace RazorHotelDB25Kristian.Pages.Rooms
{
    public class AddRoomModel : PageModel
    {
        private IRoomService _internalService;

        [BindProperty]
        public int RoomNo { get; set; }

        public int HotelNo { get; set; }

        [BindProperty]
        public string Type { get; set; }

        [BindProperty]
        public double Price { get; set; }

        public string Warning { get; set; }

        public string? SessionUsername { get; private set; }

        public AddRoomModel(IRoomService roomService)
        {
            _internalService = roomService;
        }
        public IActionResult OnGet(int TheHotelNo)
        {
            //Logincheck
            SessionUsername = HttpContext.Session.GetString("Username");
            if (String.IsNullOrEmpty(SessionUsername)) return RedirectToPage("/Users/Login");
            //Logincheck

            HotelNo = TheHotelNo;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int HotelNo)
        {
            if(await _internalService.AddRoom(new Room(RoomNo, HotelNo, Type, Price)))
            {
                return RedirectToPage("ViewAllRooms", new { handler = "WHotel", HotelNo = HotelNo});
            }
            return Page();
        }
    }
}
