using System;
using System.IO;

namespace bioinformatics
{
    class Program
    {

        static void Main(string[] args)
        {
            char[] seq1 = null;
            char[] seq2 = null;

            try
            {
                using (StreamReader sr = new StreamReader("sequence1.txt"))
                {
                    String line = sr.ReadToEnd();
                    seq1 = line.ToCharArray();
                }
                using (StreamReader sr = new StreamReader("sequence2.txt"))
                {
                    String line = sr.ReadToEnd();
                    seq2 = line.ToCharArray();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }


            AlignmentWithPenalty withPenalty = new AlignmentWithPenalty();


            int[,] arr;
            arr = withPenalty.CheckAlignment();

            /*
            for (int x = 0; x < 6; x++) {
                Console.Write("\n"); 

                for (int y = 0; y < 6; y++) {
                    Console.Write(arr[x,y]);
                }
            }
            */

          
            int[,] backtrace = new int[arr.GetLength(0), arr.GetLength(1)];
            int i = arr.GetLength(0) - 1;
            int j = arr.GetLength(1) - 1;

            while (i>0 && j>0)
            {
                int min = Math.Max(arr[i - 1, j - 1], Math.Max(arr[i - 1, j], arr[i, j - 1]));

                if (min == arr[i - 1, j - 1]) { i -= 1; j -= 1; Console.WriteLine(arr[i, j]); }
                else if (min == arr[i - 1, j]) { i -= 1; Console.WriteLine(arr[i, j]); }
                else if (min == arr[i, j - 1]) { j -= 1; Console.WriteLine(arr[i, j]); }
            }
        }
    }
}



