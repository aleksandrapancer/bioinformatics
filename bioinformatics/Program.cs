using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace bioinformatics
{
    class Program
    {

        static void Main(string[] args)
        {

            //sample char sequences FOR TEST ONLY
            //char[] seq1 = new char[] { '-', 'A', 'T', 'A', 'A', 'G', 'C', '-' };
            //char[] seq2 = new char[] { 'A', 'A', 'A', 'A', 'A', '-', 'C', 'G' };
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
                //Levenshtein distance ---- odległość edycyjna
                int cost;
                int[,] values = new int[seq1.Length + 1, seq2.Length + 1];
                for (int i = 0; i <= seq1.Length; i++)
                {
                    values[i, 0] = i;
                };
                for (int j = 0; j <= seq2.Length; j++)
                {
                    values[0, j] = j;
                };

                for (int i = 1; i <= seq1.Length; i++)
                {
                    for (int j = 1; j <= seq2.Length; j++)
                    {
                        if (seq1[i - 1] == seq2[j - 1]) { cost = 0; } else { cost = 1; }
                        values[i, j] = Math.Min(Math.Min(values[i - 1, j] + 1, values[i, j - 1] + 1), values[i - 1, j - 1] + cost);
                    }
                }

                Console.WriteLine("Funkcja edycyjna: " + values[seq1.Length, seq2.Length]);


                //DO SPRAWDZENIA!!!
                //similarity of sequences funtion ---- funkcja podobieństwa
                for (int i = 1; i <= seq1.Length; i++)
                {
                    for (int j = 1; j <= seq2.Length; j++)
                    {
                        if (seq1[i - 1] == seq2[j - 1]) { cost = 0; } else { cost = 1; }
                        values[i, j] = Math.Max(Math.Max(values[i - 1, j] + 1, values[i, j - 1] + 1), values[i - 1, j - 1] + cost);
                    }
                }

                Console.WriteLine("Funkcja podobieństwa: " + values[seq1.Length, seq2.Length]);

                //sequence alignment ---- dopasowanie sekwencji
                int[,] align = new int[seq1.Length + 1, seq2.Length + 1];
                for (int i = 0; i <= seq1.Length; i++)
                {
                    align[i, 0] = 0;
                };
                for (int j = 0; j <= seq2.Length; j++)
                {
                    align[0, j] = 0;
                };


                for (int i = 1; i <= seq1.Length; i++)
                {
                    for (int j = 1; j <= seq2.Length; j++)
                    {
                        if (seq1[i - 1] == seq2[j - 1]) { cost = 0; } else { cost = 1; }
                        align[i, j] = Math.Max(Math.Max(values[i - 1, j] + 1, values[i, j - 1] + 1), values[i - 1, j - 1] + cost);
                    }
                }

                int max = align.Cast<int>().Max();
                Console.WriteLine(max);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}



