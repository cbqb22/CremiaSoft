using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CremiaViewModel.Entity
{
    public class TwoCharacterPhraseFourEntity 
    {
        private char _CommonCharacter;
        public char CommonCharacter
        {
            get { return _CommonCharacter; }
            set { _CommonCharacter = value; }
        }

        private TwoCharacterPhraseEntity _FirstTCP;

        public TwoCharacterPhraseEntity FirstTCP
        {
            get { return _FirstTCP; }
            set { _FirstTCP = value; }
        }

        private TwoCharacterPhraseEntity _SecondTCP;

        public TwoCharacterPhraseEntity SecondTCP
        {
            get { return _SecondTCP; }
            set { _SecondTCP = value; }
        }

        private TwoCharacterPhraseEntity _ThirdTCP;

        public TwoCharacterPhraseEntity ThirdTCP
        {
            get { return _ThirdTCP; }
            set { _ThirdTCP = value; }
        }

        private TwoCharacterPhraseEntity _FourthTCP;

        public TwoCharacterPhraseEntity FourthTCP
        {
            get { return _FourthTCP; }
            set { _FourthTCP = value; }
        }


    }


    // Custom comparer for the Product class
    public class TwoCharacterPhraseFourEntityComparer : IEqualityComparer<TwoCharacterPhraseFourEntity>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(TwoCharacterPhraseFourEntity x, TwoCharacterPhraseFourEntity y)
        {


            //Check whether the compared objects reference the same data.

            
            if (
                    (x.FirstTCP == null ? null : x.FirstTCP.TwoCharacterPhrase) == (y.FirstTCP == null ? null : y.FirstTCP.TwoCharacterPhrase) &&
                    (x.SecondTCP == null ? null : x.SecondTCP.TwoCharacterPhrase) == (y.SecondTCP == null ? null : y.SecondTCP.TwoCharacterPhrase) &&
                    (x.ThirdTCP == null ? null : x.ThirdTCP.TwoCharacterPhrase) == (y.ThirdTCP == null ? null: y.ThirdTCP.TwoCharacterPhrase) &&
                    (x.FourthTCP == null ? null : x.FourthTCP.TwoCharacterPhrase) == (y.FourthTCP == null ? null : y.FourthTCP.TwoCharacterPhrase) &&
                    x.CommonCharacter == y.CommonCharacter
                )
            {
                return true;

            }

            return false;

        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(TwoCharacterPhraseFourEntity ent)
        {

            //Check whether the object is null
            if (Object.ReferenceEquals(ent, null)) return 0;

            int result = 0;

            //XORについて
            //bool a = true ^ false; aはtrueになる
            //bool b = true ^ true; bはfalseになる
            //int c = 201 ^ 92; // c は 149になる。 
            //(1100 1001 XOR 0101 1100 = 1001 0101)

            //Get hash code for the Name field if it is not null.
            result = result ^ (ent.FirstTCP == null ? 0 : ent.FirstTCP.TwoCharacterPhrase.GetHashCode());
            result = result ^ (ent.SecondTCP == null ? 0 : ent.SecondTCP.TwoCharacterPhrase.GetHashCode());
            result = result ^ (ent.ThirdTCP == null ? 0 : ent.ThirdTCP.TwoCharacterPhrase.GetHashCode());
            result = result ^ (ent.FourthTCP == null ? 0 : ent.FourthTCP.TwoCharacterPhrase.GetHashCode());
            result = result ^ (ent.CommonCharacter.GetHashCode());

            //Calculate the hash code for the product.
            return result;
        }

    }


}
