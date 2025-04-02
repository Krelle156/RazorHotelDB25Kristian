namespace RazorHotelDB25Kristian.Models
{
    public class Room
    {
        public int RoomNo { get; set; }
        public int HotelNo { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }

        public Room(int no, int hotelNo, string type, double price)
        {
            RoomNo = no;
            HotelNo = hotelNo;
            Type = type;
            Price = price;
        }

        public override string ToString()
        {
            return $"{RoomNo} tilhører hotel nummer: {HotelNo}. Har typen: {Type} og koster {Price:0.00} per overnatning";
        }
    }
}
