namespace RazorHotelDB25Kristian.Helpers
{
    public class DLStringScoreWithObject<T>
    {
        public T Data { get; private set; }

        public int Score { get; set; }

        public string DLString { get; set; }

        public DLStringScoreWithObject(T data, int score, string dLString)
        {
            Data = data;
            Score = score;
            DLString = dLString;
        }
    }
}
