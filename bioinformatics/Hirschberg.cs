using System;
using System.Linq;


namespace bioinformatics
{
    class Hirschberg
    {
        int gap_penalty = -2;

        public int[] GetNWScore(char[] s1, char[] s2, int side)
        {
            int[,] score = new int[s2.Length, s1.Length];
            int[] last = new int[s1.Length];

            for (int i = 1; i < s2.Length; i++)
            {
                for (int j = 1; j < s1.Length; j++)
                {
                    if (s1[j] != s2[i])
                    {
                        score[i, j] = -1;
                    }
                    else
                    {
                        score[i, j] = 1;
                    }
                }
            }

            int[,] arr = new int[s1.Length, s2.Length];
            arr[0, 0] = 0;

            switch (side)
            {
                case -1:
                    for (int j = 1; j < s2.Length - 1; j++)
                    {
                        arr[0, j] = arr[0, j - 1];
                    }

                    for (int i = 1; i < s1.Length - 1; i++)
                    {
                        arr[i, 0] = arr[i - 1, 0];

                        for (int j = 1; j < s2.Length - 1; j++)
                        {
                            arr[i, j] = Math.Max(arr[i - 1, j - 1] + score[i, j], Math.Max(arr[i - 1, j] + gap_penalty, arr[i, j - 1] + gap_penalty));
                        }
                    }

                    for (int j = 0; j < s2.Length - 1; j++)
                    {
                        last[j] = arr[s1.Length - 1, j];
                    }
                    break;
                case 1:
                    for (int j = s2.Length - 1; j > s1.Length / 2; j--)
                    {
                        arr[0, j] = arr[0, j - 1];
                    }

                    for (int i = s1.Length - 1; i > s1.Length / 2; i--)
                    {
                        arr[i, 0] = arr[i - 1, 0];

                        for (int j = s2.Length - 1; j > s2.Length / 2; j--)
                        {
                            arr[i, j] = Math.Max(arr[i - 1, j - 1] + score[i, j], Math.Max(arr[i - 1, j] + gap_penalty, arr[i, j - 1] + gap_penalty));
                        }
                    }

                    for (int j = s2.Length - 1; j > s2.Length / 2; j--)
                    {
                        last[j] = arr[s1.Length - 1, j];
                    }
                    break;
            }
            return last;
        }

        public void GetAlignment(char[] s1, char[] s2, int s1_start, int s1_stop, int s2_start, int s2_stop)
        {
            int[] scoreL = new int[s1.Length / 2];
            int[] scoreR = new int[s1.Length / 2];
            int[] score = new int[s2.Length - 1];

            scoreL = GetNWScore(s1,s2,-1);
            scoreR = GetNWScore(s1,s2, 1);

            int max = 0;
            int vmax = scoreL[0] + scoreR[0];
            
            for (int j = s2_start; j < s2_stop; j++)
            {
                score[j] = scoreL[j] + scoreR[j];

                if (score[j] > vmax)
                {
                    vmax = score[];
                    max = j;
                }
            }

            GetAlignment(s1, s2, s1_start, s1_stop, s2_start, s2_stop);
            GetAlignment(s1, s2, s1_start, s1_stop, s2_start, s2_stop);
        }

    }
}

