using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CremiaViewModel.Entity
{
    public class TwoCharacterPhraseEntity
    {

        public TwoCharacterPhraseEntity()
        {
        }


        public TwoCharacterPhraseEntity(char first,char second)
        {
            FirstChar = first;
            SecondChar = second;
        }

        private char _FirstChar;

        public char FirstChar
        {
            get { return _FirstChar; }
            set { _FirstChar = value; }
        }

        private char _SecondChar;

        public char SecondChar
        {
            get { return _SecondChar; }
            set { _SecondChar = value; }
        }


        public string TwoCharacterPhrase
        {
            get 
            {
                return _FirstChar.ToString() + _SecondChar.ToString(); 
            }
        }



    }
}
