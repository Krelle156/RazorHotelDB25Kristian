using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using RazorHotelDB25Kristian.Helpers;
using RazorHotelDB25Kristian.Interfaces;
using RazorHotelDB25Kristian.Models;
using System.Data;

namespace RazorHotelDB25Kristian.Services
{
    public class RoomService : IRoomService
    {
        private string connectionString = ConnectionManager.Connection;

        private string queryString = "SELECT Room_no, Hotel_No, Types, Price FROM Room";

        private string queryByHotelString = "SELECT Room_no, Hotel_No, Types, Price FROM Room Where Hotel_No = @hotelNo";
        private string queryByNameString = "SELECT Hotel_No, Name, Address FROM Hotel Where Hotel_Name = @Name";

        private string insertString = "Insert INTO Room Values(@Room_No, @Hotel_no, @Types, @Price)";
        private string deleteString = "Delete From Hotel Where Hotel_No = @ID";

        public async Task<List<Room>> GetAllRoomAsync()
        {
            List<Room> result = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    await command.Connection.OpenAsync();

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int roomNo = reader.GetInt32("Room_No");
                        int hotelNo = reader.GetInt32("Hotel_No");
                        string Type = reader.GetString("Types");
                        double price = reader.GetDouble("Price");
                        Room room = new Room(roomNo, hotelNo, Type, price);
                        result.Add(room);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return result;
        }
        public async Task<List<Room>> GetAllRoomInHotelAsync(int hotelNr)
        {
            List<Room> result = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryByHotelString, connection);
                    command.Parameters.AddWithValue("@hotelNo", hotelNr);
                    await command.Connection.OpenAsync();

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int roomNo = reader.GetInt32("Room_No");
                        int hotelNo = reader.GetInt32("Hotel_No");
                        string Type = reader.GetString("Types");
                        double price = reader.GetDouble("Price");
                        Room room = new Room(roomNo, hotelNo, Type, price);
                        result.Add(room);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return result;
        }

        public async Task<bool> AddRoom(Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand insertCommand = new SqlCommand(insertString, connection);
                    insertCommand.Parameters.AddWithValue("@Room_No",room.RoomNo);
                    insertCommand.Parameters.AddWithValue("@Hotel_No", room.HotelNo);
                    insertCommand.Parameters.AddWithValue("@Types", room.Type);
                    insertCommand.Parameters.AddWithValue("@Price", room.Price);

                    int noRows = await insertCommand.ExecuteNonQueryAsync();

                    return noRows > 0;
                }
                catch (SqlException sqlx)
                {
                    Console.WriteLine(sqlx.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            return false;
        }

        public async Task<Hotel> DeleteRoomAsync(int roomNo, int hotelNo)
        {
            throw new NotImplementedException();
        }



        public async Task<bool> UpdateRoomAsync(Room room, int roomNo, int hotelNo)
        {
            throw new NotImplementedException();
        }
    }
}
