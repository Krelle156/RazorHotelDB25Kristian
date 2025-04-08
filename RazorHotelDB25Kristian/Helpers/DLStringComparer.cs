namespace RazorHotelDB25Kristian.Helpers
{
    public class DLStringComparer
    {
        private static int _dLCost = 2;
        public static int DLCost { get { return _dLCost; } set { _dLCost = value; } }


        /// <summary>
        /// A function using the Damerau-Levensthein Algorithm to provide a value signifying the similarty between two strings
        /// </summary>
        /// <param name="a">The first string</param>
        /// <param name="b">The second string</param>
        /// <returns>An int representing the similarity between the strings.
        /// The lower the number is, the closer the strings are. Identical strings return a value of 0.</returns>
        public async static Task<int> CompareAsync(string a, string b)
        {
            int[,] matrix = new int[a.Length + 1, b.Length + 1];
            int cost = 0;

            for(int i = 0; i<= a.Length; i++)
            {
                matrix[i, 0] = i;
                //Values from 0 to the number of characters in the string "a"
                //Visualize as the values on the x-axis at the top of the matrix
            }

            for(int i = 0; i<=b.Length;  i++)
            {
                matrix[0, i] = i;
                //Same as above, but for y-axis using the length of string "b"
            }

            for(int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    //This nested loop goes through the entire matrix.
                    //This is used to compare values to the "preceeding" values
                    if (a[i - 1] == b[j - 1])
                    {
                        cost = 0;
                        //Because the characters are the same the cost of "replacement" becomes zero.
                        //This is because replacement is now "inaction", the correct letter is already in the correct place.
                    }
                    else cost = DLCost;


                    matrix[i, j] = // We select the lowers/cheapest of the following:
                        Math.Min(matrix[i-1, j] + 1, //Cost  of deletion, this is the cost of the "tile" to the "left".
                        Math.Min(matrix[i, j - 1], //Cost of insertion, this is the cost of the "tile" "above".
                        matrix[i-1, j-1] + cost)); // The cost is the value to the left and one up. Cost of replaceing a letter.

                    if (i > 1 && j > 1 && a[i - 1] == b[j - 2] && a[i - 2] == b[j - 1]) //We can only swap neightbouring letters and only if we are on letter nr. 2 in both strings.
                    {
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + 1); //If two letters can be swapped the cost is matrix[i - 2, j - 2] + 1
                    }


                }
            }
            return matrix[a.Length, b.Length];
        }
    }



}
