using Microsoft.Data.SqlClient;
using RazorHotelDB25Kristian.Helpers;
using RazorHotelDB25Kristian.Interfaces;
using RazorHotelDB25Kristian.Models;
using System.Data;

namespace RazorHotelDB25Kristian.Services
{
    public class HotelService : IHotelService
    {
        private string connectionString = ConnectionManager.Connection;

        private string queryString = "SELECT Hotel_No, Name, Address FROM Hotel";

        private string queryByIDString = "SELECT Hotel_No, Name, Address FROM Hotel Where Hotel_No = @ID";
        private string queryByNameString = "SELECT Hotel_No, Name, Address FROM Hotel Where Hotel_Name = @Name";

        private string deleteString = "Delete From Hotel Where Hotel_No = @ID";

        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
                try
                {
                    {
                        await connection.OpenAsync();

                        string insertString = "Insert INTO Hotel Values(@ID, @Navn, @Adresse)";

                        SqlCommand insertCommand = new SqlCommand(insertString, connection);
                        insertCommand.Parameters.AddWithValue("@ID", hotel.HotelNr);
                        insertCommand.Parameters.AddWithValue("@Navn", hotel.Navn);
                        insertCommand.Parameters.AddWithValue("@Adresse", hotel.Adresse);

                        int noRows = await insertCommand.ExecuteNonQueryAsync(); //This must be done *after* the connection is opened i think

                        return noRows > 0;
                    }
                }
                catch (SqlException sqlx)
                {
                    Console.WriteLine(sqlx.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            return false;
        }

        public async Task<Hotel> DeleteHotelAsync(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {


                    Hotel toDelete = await GetHotelFromIdAsync(hotelNr);

                    await connection.OpenAsync();
                    SqlCommand deleteCommand = new SqlCommand(deleteString, connection);

                    deleteCommand.Parameters.AddWithValue("@ID", hotelNr);

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
            }
                
            return null;
        }

        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> result = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    await command.Connection.OpenAsync();

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int hotelNr = reader.GetInt32("Hotel_No");
                        string hotelNavn = reader.GetString("Name");
                        string hotelAdr = reader.GetString("Address");
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        result.Add(hotel);
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

        public async Task<Hotel> GetHotelFromIdAsync(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
                try
                {
                    {

                        int id = 0;
                        string navn = "N/A";
                        string adresse = "N/A";
                        await connection.OpenAsync();

                        SqlCommand findCommand = new SqlCommand(queryByIDString, connection);
                        findCommand.Parameters.AddWithValue("@ID", hotelNr);
                        SqlDataReader reader = await findCommand.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            id = reader.GetInt32("Hotel_No");
                            navn = reader.GetString("Name");
                            adresse = reader.GetString("Address");
                        }

                        return new Hotel(id, navn, adresse);
                    }
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

        public async Task<List<Hotel>> GetHotelsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr)
        {
            throw new NotImplementedException();
        }
    }
}
