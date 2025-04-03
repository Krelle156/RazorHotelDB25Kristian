using Microsoft.Data.SqlClient;
using RazorHotelDB25Kristian.Helpers;
using RazorHotelDB25Kristian.Interfaces;
using RazorHotelDB25Kristian.Models;

namespace RazorHotelDB25Kristian.Services
{
    public class UserService : IUserService
    {
        private string connectionString = ConnectionManager.Connection;

        private string insertString = "Insert INTO Hotel25Users Values(@userName, @hash)";

        private string loginstring = "SELECT Hotel_No, Name, Address FROM Hotel Where Hotel_No = @ID";

        public async Task<bool> RegisterAsync(string newUserName, string newCode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand insertCommand = new SqlCommand(insertString, connection);

                    insertCommand.Parameters.AddWithValue("@userName", newUserName);
                    insertCommand.Parameters.AddWithValue("@hash", await SimpleHash.CreateHashStringAsync(newCode));

                    int noRows = await insertCommand.ExecuteNonQueryAsync();

                    return noRows > 0;
                }
                catch (SqlException sqlx)
                {
                    throw sqlx;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return false;
        }

        public Task<bool> LoginAsync(string userName, string code)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
