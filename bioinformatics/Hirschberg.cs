using System;
using System.Linq;
using System.Text;

namespace bioinformatics
{
    class Hirschberg
    {

        public void GetAlignment(char[] s1, char[] s2) {
            StringBuilder Z = new StringBuilder();
            StringBuilder W = new StringBuilder();

            if (s1.Length == 0)
            {
                for (int i = 1; i < s2.Length - 1; i++)
                {
                    Z.Append("-");
                    W.Append(s2[i]);
                }
            }
            else if (s2.Length == 0)
            {
                for (int i = 1; i < s2.Length - 1; i++)
                {
                    Z.Append(s1[i]);
                    W.Append("-");
                }
            }
            else if (s1.Length == 1 || s2.Length == 1)
            {
                AlignmentWithPenalty withPenalty = new AlignmentWithPenalty();
                int[,] arr = withPenalty.GetSimilarityMatrix(s1, s2);
                withPenalty.GetBacktrace(arr, s1, s2);
            }
            else {
                int xlen = s1.Length;
                int ylen = s2.Length;
                int xmid = xlen / 2;

                int[] prefScore = GetNWScore(s1,s2,xmid,"pref");
                int[] suffScore = GetNWScore(s1,s2,xmid,"suff");
                int[] score = new int[prefScore.Length];

                for (int i = 0; i < s2.Length; i++) {
                    score[i] = prefScore[i] + suffScore[i];
                }

                int max = score.Max();
                int ymid = Array.IndexOf(score, max);

                char[] pref_s1 = new char[xmid];
                char[] suff_s1 = new char[s1.Length - xmid];
                char[] pref_s2 = new char[ymid];
                char[] suff_s2 = new char[s2.Length - ymid];

                Array.Copy(s1, 0, pref_s1, 0, xmid);
                Array.Copy(s2, 0, pref_s2, 0, ymid);
                Array.Copy(s1, xmid, suff_s1, 0, s1.Length - xmid);
                Array.Copy(s1, ymid, suff_s2, 0, s2.Length - ymid);

                GetAlignment(pref_s1, pref_s2);
                if (ymid != 0)
                {
                    GetAlignment(suff_s1, suff_s2);
                }
            }
        }


        public int[] GetNWScore(char[] s1, char[] s2, int xmid, string align) {
            int gap_penalty = -2;


            char[] seq1 = new char[s1.Length + 1];
            char[] seq2 = new char[s2.Length + 1];
            seq1[0] = ' ';
            seq2[0] = ' ';
            s1.CopyTo(seq1, 1);
            s2.CopyTo(seq2, 1);

            int[] last = new int[s2.Length];
            int[,] similarity = GetSimilarityMatrix(seq1, seq2);
            int[,] score = new int[s1.Length, s2.Length];

            switch (align)
            {
                case "pref":
                    for (int j = 1; j < xmid; j++) {
                        score[0, j] = score[0, j - 1];
                    }

                    for (int i = 1; i < s2.Length - 1; i++) {
                        score[i, 0] = score[i-1, 0];

                        for (int j = 1; j < xmid; j++)  {
                            score[i, j] = Math.Max(score[i - 1, j - 1] + score[i, j], Math.Max(score[i - 1, j]+gap_penalty, score[i, j - 1]+gap_penalty));
                        }
                    }

                    for (int j = 0; j < s2.Length - 1; j++)   {
                        last[j] = score[xmid, j];
                    }

                    break;
                case "suff":

                    for (int j = s1.Length - 1; j > xmid; j--) {
                        score[0, j] = score[0, j - 1];
                    }
                    for (int i = s1.Length-2; i > 1; i--)   {
                        score[i, 0] = score[i - 1, 0];

                        for (int j = s1.Length-1; j > xmid; j--)   {
                            score[i, j] = Math.Max(score[i - 1, j - 1] + score[i, j], Math.Max(score[i - 1, j]+gap_penalty, score[i, j - 1]+gap_penalty));
                        }
                    }

                    for (int i = s2.Length - 1; i > 0; i--) {
                        last[i] = score[i, xmid];
                    }
                    break;
            }
            return last;
        }


        public int[,] GetSimilarityMatrix(char[] s1, char[]s2) {
        
            int[,] similar = new int[s2.Length, s1.Length];
            for (int i = 1; i < s2.Length; i++) {
                for (int j = 1; j < s1.Length; j++) {
                    if (s1[j] != s2[i]) {
                        similar[i, j] = -1;
                    } else {
                        similar[i, j] = 1;
                    }
                }
            }

            return similar;
        }
    }
}