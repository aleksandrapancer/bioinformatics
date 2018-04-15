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


            //without gap penalty 
            Algorithm a = new Algorithm();
            a.getDistance(seq1,seq2);
            a.getSimilarityFunc(seq1, seq2);
            a.getSeqAlignment(seq1,seq2);
        }
    }
}



