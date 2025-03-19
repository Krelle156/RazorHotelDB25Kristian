namespace RazorHotelDB25Kristian.Helpers
{
    public class ConnectionManager
    {

        private static string _connectionString = "";
        public static string Connection {get { return _connectionString; } }
    }
}
