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

            int gap_penalty = -2;


            //example char[]
            char[] s1 = { 'T', 'G', 'G', 'T', 'G' };
            char[] s2 = { 'A', 'T', 'C', 'G', 'T' };


            int[,] score = new int[s1.Length+1, s1.Length + 1];

            for (int i = 1; i< s1.Length + 1; i++) {
                for (int j = 1; j < s1.Length + 1; j++)
                {
                    if (s1[i-1] == s2[j-1]) { score[j,i] = 1; }
                    else if (s1[i - 1] != s2[j - 1]) { score[j,i] = -1; }
                    // Console.Write(score[i]); 
                }
            }


            int[,] arr = new int[s1.Length + 1, s2.Length + 1];


            for (int i = 0; i < s2.Length + 1; i++){
                for (int j = 0; j < s1.Length + 1; j++) { 

                    if (i == 0 && j == 0)
                    {
                        arr[i, j] = 0;
                    }
                    else if (j == 0)
                    {
                        arr[i, j] = arr[i - 1, j] + gap_penalty;
                    }
                    else if (i == 0)
                    {
                        arr[i, j] = arr[i, j - 1] + gap_penalty;
                    }

                    if (i >= 1 && j >= 1)
                    {

                        int a = arr[i - 1, j - 1] + score[i,j];
                        int b = arr[i - 1, j] + gap_penalty;
                        int c = arr[i, j - 1] + gap_penalty;

                        arr[i,j] = Math.Max(a,Math.Max(b,c));
                    }
                }
            }


            for (int x = 0; x < 6; x++) {
                Console.Write("\n"); 

                for (int y = 0; y < 6; y++) {
                    Console.Write(arr[x,y]);
                }
            }

        }
    }
}



