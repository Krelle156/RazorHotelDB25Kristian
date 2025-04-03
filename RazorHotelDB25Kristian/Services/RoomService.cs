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
        private string querySpecificString = "SELECT Room_no, Hotel_No, Types, Price FROM Room Where Room_No =@roomNo AND Hotel_No = @hotelNo";

        private string insertString = "Insert INTO Room Values(@roomNo, @hotelNo, @type, @price)";
        private string deleteString = "Delete From Room Where Room_No = @roomNo AND Hotel_No = @hotelNo";

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

        public async Task<Room> GetOneRoomInHotelAsync(int roomNo, int hotelNo)
        {
            Room result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(querySpecificString, connection);
                    command.Parameters.AddWithValue("@roomNo", roomNo);
                    command.Parameters.AddWithValue("@hotelNo", hotelNo);
                    await command.Connection.OpenAsync();

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int saveRoomNo = reader.GetInt32("Room_No");
                        int savehotelNo = reader.GetInt32("Hotel_No");
                        string saveType = reader.GetString("Types");
                        double savePrice = reader.GetDouble("Price");

                        result = new Room(saveRoomNo, savehotelNo, saveType, savePrice);
                        return result;
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
            return null;
        }

        public async Task<bool> AddRoom(Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand insertCommand = new SqlCommand(insertString, connection);
                    insertCommand.Parameters.AddWithValue("@roomNo",room.RoomNo);
                    insertCommand.Parameters.AddWithValue("@hotelNo", room.HotelNo);
                    insertCommand.Parameters.AddWithValue("@types", room.Type);
                    insertCommand.Parameters.AddWithValue("@price", room.Price);

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

        public async Task<Room> DeleteRoomAsync(int roomNo, int hotelNo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {


                    Room toDelete = await GetOneRoomInHotelAsync(roomNo, hotelNo);

                    await connection.OpenAsync();
                    SqlCommand deleteCommand = new SqlCommand(deleteString, connection);

                    deleteCommand.Parameters.AddWithValue("@roomNo", roomNo);
                    deleteCommand.Parameters.AddWithValue("@hotelNo", hotelNo);

                    int noRows = await deleteCommand.ExecuteNonQueryAsync(); //This must be done *after* the connection is opened i think

                    return toDelete;

                }
                catch (SqlException sqlx)
                {
                    Console.WriteLine(sqlx.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return null;
            }

        }



        public async Task<bool> UpdateRoomAsync(Room room, int roomNo, int hotelNo)
        {
            throw new NotImplementedException();
        }
    }
}
