using Microsoft.Data.SqlClient;
using RazorHotelDB25Kristian.Helpers;
using RazorHotelDB25Kristian.Interfaces;
using RazorHotelDB25Kristian.Models;
using System.Data;

namespace RazorHotelDB25Kristian.Services
{
    public class UserService : IUserService
    {
        private string connectionString = ConnectionManager.Connection;

        private string insertString = "Insert INTO Hotel25Users Values(@userName, @hash)";

        private string loginString = "SELECT UserName, HashPass FROM Hotel25Users Where UserName = @userName AND HashPass = @hashPass";
        private string findString = "SELECT UserName FROM Hotel25Users Where UserName = @userName";

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

        public async Task<User> LoginAsync(string userName, string code)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    User result = null;
                    SqlCommand loginCommand = new SqlCommand(loginString, connection);

                    loginCommand.Parameters.AddWithValue("@userName", userName);
                    loginCommand.Parameters.AddWithValue("@hashPass", await SimpleHash.CreateHashStringAsync(code));

                    await connection.OpenAsync();

                    SqlDataReader reader = await loginCommand.ExecuteReaderAsync();
                    
                    while(reader.Read())
                    {
                        string name = reader.GetString("UserName");
                        string hash = reader.GetString("HashPass");
                        result = new User(name, hash);
                    }

                    reader.Close();


                    return result;
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
            return null;
        }

        public async Task<bool> UserExistsAsync(string userName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                    SqlCommand searchCommand = new SqlCommand(findString, connection);

                    searchCommand.Parameters.AddWithValue("@userName", userName);
                    await connection.OpenAsync();

                    SqlDataReader reader = await searchCommand.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        if (reader.GetString("UserName") != null) return true;
                    }

                    reader.Close();
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

        public async Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
