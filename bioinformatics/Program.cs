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


            //example char[]
            // char[] s11 = { 'T', 'G', 'G', 'T', 'G' };
            //char[] s22 = { 'A', 'T', 'C', 'G', 'T' };                   
            char[] s11 = { 'G', 'C', 'A', 'T', 'G','C','U' };
            char[] s22 = { 'G', 'A', 'T', 'T', 'A','C','A' };            

            char[] s1 = new char[s11.Length + 1];
            char[] s2 = new char[s22.Length + 1];
            s1[0] = ' ';    
            s2[0] = ' ';
            s11.CopyTo(s1,1);
            s22.CopyTo(s2,1);


            Console.WriteLine("Needleman-Wunsch algorithm with gap penalty");
            AlignmentWithPenalty withPenalty = new AlignmentWithPenalty();
            int[,] arr = withPenalty.GetSimilarityMatrix(s1,s2);
            withPenalty.GetBacktrace(arr,s1,s2);

            Console.WriteLine("Hirschberg algorithm without gap penalty");
            Hirschberg hirschberg = new Hirschberg();
            hirschberg.GetAlignment(s1,s2); 
        }
    }
}



