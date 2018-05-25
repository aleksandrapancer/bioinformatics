using System;
using System.IO;
using System.Text;

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

            //sample sequences  
            //char[] s11 = { 'G', 'C', 'A', 'T', 'G','C','U' };
            //char[] s22 = { 'G', 'A', 'T', 'T', 'A','C','A' };            

            char[] s1 = new char[seq1.Length + 1];
            char[] s2 = new char[seq2.Length + 1];
            s1[0] = ' ';    
            s2[0] = ' ';
            seq1.CopyTo(s1,1);
            seq2.CopyTo(s2,1);


            Console.WriteLine("Needleman-Wunsch algorithm with gap penalty");
            AlignmentWithPenalty withPenalty = new AlignmentWithPenalty();
            int[,] arr = withPenalty.GetSimilarityMatrix(s1,s2);
            withPenalty.GetBacktrace(arr,s1,s2);

            Console.WriteLine("\nHirschberg algorithm without gap penalty");
            Hirschberg hirschberg = new Hirschberg();
            hirschberg.GetAlignment(seq1,seq2); 
        }
    }
}



