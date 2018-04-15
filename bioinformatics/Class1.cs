using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bioinformatics
{
    class Algorithm
    {

        public int[,] setArrays(char[] seq1, char[] seq2) {
            int[,] values = new int[seq1.Length + 1, seq2.Length + 1];
            for (int i = 0; i <= seq1.Length; i++)
            {
                values[i, 0] = i;
            };
            for (int j = 0; j <= seq2.Length; j++)
            {
                values[0, j] = j;
            };
            return values; 
        }


        //Levenshtein distance ---- odległość edycyjna
        public void getDistance(char[] seq1,char[] seq2){
            int[,] values = setArrays(seq1,seq2);
            int cost;
            for (int i = 1; i <= seq1.Length; i++)
            {
                for (int j = 1; j <= seq2.Length; j++)
                {
                    if (seq1[i - 1] == seq2[j - 1]) { cost = 0; } else { cost = 1; }
                    values[i, j] = Math.Min(Math.Min(values[i - 1, j] + 1, values[i, j - 1] + 1), values[i - 1, j - 1] + cost);
                }
            }

            Console.WriteLine("Funkcja edycyjna: " + values[seq1.Length, seq2.Length]);
        }


        //DO SPRAWDZENIA!!!
        //similarity of sequences funtion ---- funkcja podobieństwa
        public void getSimilarityFunc(char[] seq1, char[] seq2)
        {
            int[,] values = setArrays(seq1, seq2);
            int cost;
            for (int i = 1; i <= seq1.Length; i++)
            {
                for (int j = 1; j <= seq2.Length; j++)
                {
                    if (seq1[i - 1] == seq2[j - 1]) { cost = 0; } else { cost = 1; }
                    values[i, j] = Math.Max(Math.Max(values[i - 1, j] + 1, values[i, j - 1] + 1), values[i - 1, j - 1] + cost);
                }
            }

            Console.WriteLine("Funkcja podobieństwa: " + values[seq1.Length, seq2.Length]);
        }
                

        //sequence alignment ---- dopasowanie sekwencji
        public void getSeqAlignment(char[] seq1,char[] seq2){
                int cost;
                int[,] values = setArrays(seq1, seq2); 
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
                foreach (var item in align)
                {
                    Console.Write(item.ToString());
                }
                Console.WriteLine("Dopasowanie sekwencji:" +max);
        }
    }
}
