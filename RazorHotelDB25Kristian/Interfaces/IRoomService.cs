using RazorHotelDB25Kristian.Models;

namespace RazorHotelDB25Kristian.Interfaces
{
    public interface IRoomService
    {
        /// <summary>
        /// henter alle værelser fra databasen
        /// </summary>
        /// <returns>Liste af værelser</returns>
        Task<List<Room>> GetAllRoomAsync();

        /// <summary>
        /// Henter alle værelser fra et specifikt hotel fra database 
        /// </summary>
        /// <param name="hotelNr">Udpeger det hotel værelserne skal tilhøre</param>
        /// <returns>Liste af fundne værelser eller null hvis hotellet ikke findes</returns>
        Task<List<Room>> GetAllRoomInHotelAsync(int hotelNr);

        /// <summary>
        /// Indsætter et nyt værelse i databasen
        /// </summary>
        /// <param name="room">værelset der skal indsættes</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> AddRoom(Room room);

        /// <summary>
        /// Opdaterer et værelse i databasen
        /// </summary>
        /// <param name="room">De nye værdier til værelset</param>
        /// <param name="roomNo">Værelsets oprindelige nummer</param>
        /// <param name="hotelNo">Nummer på den hotel værelset tilhører</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> UpdateRoomAsync(Room room, int roomNo, int hotelNo);

        /// <summary>
        /// Sletter et værelse fra databasen
        /// </summary>
        /// <param name="roomNo">Nummer på det værelse der skal slettes</param>
        /// <param name="hotelNo">Nummer på det hotel værelsest skal slettes fra</param>
        /// <returns>Det værelse der er slettet fra databasen, returnere null hvis værelset ikke findes</returns>
        Task<Hotel> DeleteRoomAsync(int roomNo, int hotelNo);
    }
}
