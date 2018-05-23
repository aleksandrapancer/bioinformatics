using System;
using System.Text;

namespace bioinformatics
{
    class AlignmentWithPenalty
    {
        int gap_penalty = -1;

        public int[,] GetSimilarityMatrix(char[] s1, char[] s2) {
            int[,] score = new int[s2.Length, s1.Length];

            for (int i = 1; i < s2.Length; i++) {
                for (int j = 1; j < s1.Length; j++) {
                    if (s1[j] != s2[i]) {
                        score[i, j] = -1;
                    } else {
                        score[i, j] = 1;
                    }
                }
            }

            int[,] arr = new int[s1.Length, s2.Length];
            for (int i = 0; i < s2.Length; i++) {
                for (int j = 0; j < s1.Length; j++) {

                    if (i == 0 && j == 0) {
                        arr[i, j] = 0;
                    }
                    else if (j == 0) {
                        arr[i, j] = arr[i - 1, j] + gap_penalty;
                    }
                    else if (i == 0) {
                        arr[i, j] = arr[i, j - 1] + gap_penalty;
                    }
                    else if (i >= 1 && j >= 1) {
                        arr[i, j] = Math.Max(arr[i - 1, j - 1] + score[i, j], Math.Max(arr[i - 1, j] + gap_penalty, arr[i, j - 1] + gap_penalty));
                    }
                }
            }
            return arr;
        }


        public void GetBacktrace(int[,] arr, char[] s1, char[] s2)
        {
            int i = s1.Length - 1;
            int j = s2.Length - 1;

            StringBuilder alignmentA = new StringBuilder();
            StringBuilder alignmentB = new StringBuilder();

            while (i > 0 && j > 0)
            {
                int max = Math.Max(arr[i - 1, j - 1], Math.Max(arr[i - 1, j], arr[i, j - 1]));

                    if (max == arr[i - 1, j - 1])
                    {
                        alignmentA.Append(s2[i]);
                        alignmentB.Append(s1[j]);
                
                        i -= 1;
                        j -= 1;
                    }
                    else if (max == arr[i - 1, j])
                    {
                        alignmentA.Append(s2[i]);
                        alignmentB.Append('-');
                        i -= 1;
                    }
                    else if (max == arr[i, j - 1])
                    {
                        alignmentA.Append('-');
                        alignmentB.Append(s1[j]);
                        j -= 1;
                    }
            }

            char[] a = new char[alignmentA.Length];
            a = alignmentA.ToString().ToCharArray();
            Array.Reverse(a);
            char[] b = new char[alignmentB.Length];
            b = alignmentB.ToString().ToCharArray();
            Array.Reverse(b);


            Console.WriteLine(a);
            Console.WriteLine(b);
        }

    }
}
