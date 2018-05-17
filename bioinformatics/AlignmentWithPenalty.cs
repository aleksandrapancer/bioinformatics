using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bioinformatics
{
    class AlignmentWithPenalty
    {
        int gap_penalty = -2;


        //example char[]
        char[] s1 = { 'T', 'G', 'G', 'T', 'G' };
        char[] s2 = { 'A', 'T', 'C', 'G', 'T' };

        public int[,] CheckAlignment()
        {
            int[,] score = new int[s1.Length + 1, s1.Length + 1];

            for (int i = 1; i < s1.Length + 1; i++)
            {
                for (int j = 1; j < s1.Length + 1; j++)
                {
                    if (s1[i - 1] == s2[j - 1]) { score[j, i] = 1; }
                    else if (s1[i - 1] != s2[j - 1]) { score[j, i] = -1; }
                    // Console.Write(score[i]); 
                }
            }


            int[,] arr = new int[s1.Length + 1, s2.Length + 1];


            for (int i = 0; i < s2.Length + 1; i++)
            {
                for (int j = 0; j < s1.Length + 1; j++)
                {

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
                        arr[i, j] = Math.Max(arr[i - 1, j - 1] + score[i, j], Math.Max(arr[i - 1, j] + gap_penalty, arr[i, j - 1] + gap_penalty));
                    }

                }
            }

            return arr; 
        }
    }
}
