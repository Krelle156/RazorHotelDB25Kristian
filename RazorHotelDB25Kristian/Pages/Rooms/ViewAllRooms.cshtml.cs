using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25Kristian.Interfaces;
using RazorHotelDB25Kristian.Models;

namespace RazorHotelDB25Kristian.Pages.Rooms
{
    public class ViewAllRoomsModel : PageModel
    {
        private IRoomService _internalService;
        private IHotelService _hotelService;

        public List<Room> Rooms { get; set; }

        public Hotel Hotel { get; set; }

        public ViewAllRoomsModel(IRoomService roomService, IHotelService hotelService)
        {
            _internalService = roomService;
            _hotelService = hotelService;
        }


        public async Task OnGet()
        {
            Rooms = await _internalService.GetAllRoomAsync();
        }

        public async Task OnGetWHotel(int HotelNo)
        {
            Hotel = await _hotelService.GetHotelFromIdAsync(HotelNo);
            Rooms = await _internalService.GetAllRoomInHotelAsync(HotelNo);
        }
    }
}
