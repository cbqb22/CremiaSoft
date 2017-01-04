using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CremiaViewModel.IO
{
    public static class DictionaryMaker
    {
        public static void OutputTwoCharacterPhraseFromCSV()
        {
            string readerPath = @"C:\Users\poohace\Documents\日本語辞書\edict.csv";
            string writerPath = @"C:\Users\poohace\Documents\日本語辞書\TwoCharacterPhrase.csv";


            using (StreamReader sr = new StreamReader(readerPath, Encoding.GetEncoding(932)))
            using (StreamWriter sw = new StreamWriter(writerPath, false, Encoding.GetEncoding(932)))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    var sepa = line.Split(',');

                    if (sepa.Count() < 1 || sepa[0].Length != 2)
                    {
                        continue;
                    }

                    sw.WriteLine(sepa[0]);
                }
            }
            
        }
    }
}
