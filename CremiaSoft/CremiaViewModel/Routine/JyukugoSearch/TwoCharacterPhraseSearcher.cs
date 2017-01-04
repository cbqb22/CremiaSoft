using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CremiaViewModel.Entity;
using CremiaViewModel.Const;

namespace CremiaViewModel.Routine
{
    public static class TwoCharacterPhraseSearcher
    {
        public static List<TwoCharacterPhraseFourEntity> Search(TwoCharacterPhraseEntity A, TwoCharacterPhraseEntity B, TwoCharacterPhraseEntity C, TwoCharacterPhraseEntity D, TwoCharacterPhraseLocationEnum emptyLocation)
        {
            //辞書を読込み
            List<string> jisho = new List<string>();

#if DEBUG
            string readerPath = @"C:\Users\poohace\Documents\日本語辞書\TwoCharacterPhrase.csv";
#else
            string readerPath = Path.Combine(Const.CremiaConst.CremiaDatapath, "TwoCharacterPhrase.csv");
#endif

            using (StreamReader sr = new StreamReader(readerPath, Encoding.GetEncoding(932)))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    jisho.Add(line);
                }

            }


            //文字の配列は
            // A
            //B D
            // C

            //A?
            var firstQuery = (from x in jisho
                              where
                                 x.Substring(0, 1) == A.FirstChar.ToString()
                              select x).ToList();


            //B?
            var secondQuery = (from x in jisho
                               where
                                 x.Substring(0, 1) == B.FirstChar.ToString()
                               select x).ToList();

            //?C
            var thirdQuery = (from x in jisho
                              where
                                 x.Substring(1, 1) == C.SecondChar.ToString()
                              select x).ToList();

            //?D
            var fourthQuery = (from x in jisho
                               where
                                 x.Substring(1, 1) == D.SecondChar.ToString()
                               select x).ToList();


            Dictionary<TwoCharacterPhraseEntity, List<string>> dic = new Dictionary<TwoCharacterPhraseEntity, List<string>>();
            dic.Add(A, firstQuery);
            dic.Add(B, secondQuery);
            dic.Add(C, thirdQuery);
            dic.Add(D, fourthQuery);

            if (emptyLocation == TwoCharacterPhraseLocationEnum.A || emptyLocation == TwoCharacterPhraseLocationEnum.B)
            {
                return KeyWordSearchForEmptyB(dic, A, B, C, D,emptyLocation);
            }
            else if (emptyLocation == TwoCharacterPhraseLocationEnum.C || emptyLocation == TwoCharacterPhraseLocationEnum.D)
            {
                return KeyWordSearchForEmptyD(dic, A, B, C, D, emptyLocation);
            }
            else
            {
                return KeyWordSearch(dic, A, B, C, D, emptyLocation);
            }



        }

        /// <summary>
        /// 共通文字の検索
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <returns></returns>
        private static List<TwoCharacterPhraseFourEntity> KeyWordSearch(Dictionary<TwoCharacterPhraseEntity, List<string>> dic, TwoCharacterPhraseEntity A, TwoCharacterPhraseEntity B, TwoCharacterPhraseEntity C, TwoCharacterPhraseEntity D, TwoCharacterPhraseLocationEnum emptyLocation)
        {

            List<TwoCharacterPhraseFourEntity> result = new List<TwoCharacterPhraseFourEntity>();

            //次のAを検索
            foreach (var wordA in dic[A])
            {
                string 共通文字 = wordA.Replace(A.FirstChar.ToString(), "");


                //Bを検索
                foreach (var wordB in dic[B])
                {
                    if (wordB.Substring(1, 1) != 共通文字)
                    {
                        continue;
                    }


                    //Cを検索
                    foreach (var wordC in dic[C])
                    {

                        if (wordC.Substring(0, 1) != 共通文字)
                        {
                            continue;
                        }

                        //Dを検索
                        foreach (var wordD in dic[D])
                        {
                            if (wordD.Substring(0, 1) != 共通文字)
                            {
                                continue;
                            }

                            TwoCharacterPhraseFourEntity ent = new TwoCharacterPhraseFourEntity();
                            ent.FirstTCP = new TwoCharacterPhraseEntity();
                            ent.FirstTCP.FirstChar = A.FirstChar;
                            ent.FirstTCP.SecondChar = 共通文字.ToCharArray(0, 1)[0];

                            ent.SecondTCP = new TwoCharacterPhraseEntity();
                            ent.SecondTCP.FirstChar = B.FirstChar;
                            ent.SecondTCP.SecondChar = 共通文字.ToCharArray(0, 1)[0];

                            ent.ThirdTCP = new TwoCharacterPhraseEntity();
                            ent.ThirdTCP.FirstChar = 共通文字.ToCharArray(0, 1)[0];
                            ent.ThirdTCP.SecondChar = C.SecondChar; ;

                            ent.FourthTCP = new TwoCharacterPhraseEntity();
                            ent.FourthTCP.FirstChar = 共通文字.ToCharArray(0, 1)[0];
                            ent.FourthTCP.SecondChar = D.SecondChar;

                            ent.CommonCharacter = 共通文字.ToCharArray(0, 1)[0];
                            result.Add(ent);

                        }

                    }

                }

            }


            //result.Add(result[0]);
            //result.Add(result[0]);

            //重複削除
            IEqualityComparer<TwoCharacterPhraseFourEntity> customComparer = new TwoCharacterPhraseFourEntityComparer();
            result = result.Distinct(new TwoCharacterPhraseFourEntityComparer()).ToList();

#if DEBUG
            result.ForEach(x =>
                System.Diagnostics.Debug.WriteLine(string.Format("A:{0} B:{1} C:{2} D:{3}"
                    , x.FirstTCP.TwoCharacterPhrase
                    , x.SecondTCP.TwoCharacterPhrase
                    , x.ThirdTCP.TwoCharacterPhrase
                    , x.FourthTCP.TwoCharacterPhrase
                    ))
            );

#endif

            return result;

        }


        /// <summary>
        /// Bが空白の場合の検索
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <returns></returns>
        private static List<TwoCharacterPhraseFourEntity> KeyWordSearchForEmptyB(Dictionary<TwoCharacterPhraseEntity, List<string>> dic, TwoCharacterPhraseEntity A, TwoCharacterPhraseEntity B, TwoCharacterPhraseEntity C, TwoCharacterPhraseEntity D, TwoCharacterPhraseLocationEnum emptyLocation)
        {

            List<TwoCharacterPhraseFourEntity> result = new List<TwoCharacterPhraseFourEntity>();

            //次のAを検索
            foreach (var wordA in emptyLocation == TwoCharacterPhraseLocationEnum.A ? dic[B] : dic[A])
            {
                string 共通文字 = wordA.Replace(emptyLocation == TwoCharacterPhraseLocationEnum.A ? B.FirstChar.ToString() : A.FirstChar.ToString() , "");



                //Cを検索
                foreach (var wordC in dic[C])
                {

                    if (wordC.Substring(0, 1) != 共通文字)
                    {
                        continue;
                    }

                    //Dを検索
                    foreach (var wordD in dic[D])
                    {
                        if (wordD.Substring(0, 1) != 共通文字)
                        {
                            continue;
                        }

                        TwoCharacterPhraseFourEntity ent = new TwoCharacterPhraseFourEntity();

                        if (emptyLocation == TwoCharacterPhraseLocationEnum.A)
                        {
                            ent.SecondTCP = new TwoCharacterPhraseEntity();
                            ent.SecondTCP.FirstChar = B.FirstChar;
                            ent.SecondTCP.SecondChar = 共通文字.ToCharArray(0, 1)[0];
                        }
                        else if (emptyLocation == TwoCharacterPhraseLocationEnum.B)
                        {
                            ent.FirstTCP = new TwoCharacterPhraseEntity();
                            ent.FirstTCP.FirstChar = A.FirstChar;
                            ent.FirstTCP.SecondChar = 共通文字.ToCharArray(0, 1)[0];
                        }




                        ent.ThirdTCP = new TwoCharacterPhraseEntity();
                        ent.ThirdTCP.FirstChar = 共通文字.ToCharArray(0, 1)[0];
                        ent.ThirdTCP.SecondChar = C.SecondChar; ;

                        ent.FourthTCP = new TwoCharacterPhraseEntity();
                        ent.FourthTCP.FirstChar = 共通文字.ToCharArray(0, 1)[0];
                        ent.FourthTCP.SecondChar = D.SecondChar;

                        ent.CommonCharacter = 共通文字.ToCharArray(0, 1)[0];
                        result.Add(ent);

                    }

                }

                //}

            }


            //result.Add(result[0]);
            //result.Add(result[0]);

            //重複削除
            IEqualityComparer<TwoCharacterPhraseFourEntity> customComparer = new TwoCharacterPhraseFourEntityComparer();
            result = result.Distinct(new TwoCharacterPhraseFourEntityComparer()).ToList();

#if DEBUG
            result.ForEach(x =>
                System.Diagnostics.Debug.WriteLine(string.Format("A:{0} B:{1} C:{2} D:{3}"
                    , emptyLocation == TwoCharacterPhraseLocationEnum.A ? "" : x.FirstTCP.TwoCharacterPhrase
                    , emptyLocation == TwoCharacterPhraseLocationEnum.B ? "" : x.SecondTCP.TwoCharacterPhrase
                    , x.ThirdTCP.TwoCharacterPhrase
                    , x.FourthTCP.TwoCharacterPhrase
                    ))
            );

#endif

            return result;

        }

        /// <summary>
        /// Dが空白の場合の検索
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <returns></returns>
        private static List<TwoCharacterPhraseFourEntity> KeyWordSearchForEmptyD(Dictionary<TwoCharacterPhraseEntity, List<string>> dic, TwoCharacterPhraseEntity A, TwoCharacterPhraseEntity B, TwoCharacterPhraseEntity C, TwoCharacterPhraseEntity D, TwoCharacterPhraseLocationEnum emptyLocation)
        {

            List<TwoCharacterPhraseFourEntity> result = new List<TwoCharacterPhraseFourEntity>();

            //次のAを検索
            foreach (var wordA in dic[A])
            {
                string 共通文字 = wordA.Replace(A.FirstChar.ToString(), "");


                //Bを検索
                foreach (var wordB in dic[B])
                {
                    if (wordB.Substring(1, 1) != 共通文字)
                    {
                        continue;
                    }


                    //CorDを検索
                    foreach (var wordC in emptyLocation == TwoCharacterPhraseLocationEnum.C ? dic[D] : dic[C])
                    {

                        if (wordC.Substring(0, 1) != 共通文字)
                        {
                            continue;
                        }


                        TwoCharacterPhraseFourEntity ent = new TwoCharacterPhraseFourEntity();
                        ent.FirstTCP = new TwoCharacterPhraseEntity();
                        ent.FirstTCP.FirstChar = A.FirstChar;
                        ent.FirstTCP.SecondChar = 共通文字.ToCharArray(0, 1)[0];

                        ent.SecondTCP = new TwoCharacterPhraseEntity();
                        ent.SecondTCP.FirstChar = B.FirstChar;
                        ent.SecondTCP.SecondChar = 共通文字.ToCharArray(0, 1)[0];

                        if (emptyLocation == TwoCharacterPhraseLocationEnum.C)
                        {
                            ent.FourthTCP = new TwoCharacterPhraseEntity();
                            ent.FourthTCP.FirstChar = 共通文字.ToCharArray(0, 1)[0];
                            ent.FourthTCP.SecondChar = D.SecondChar;
                        }
                        else if (emptyLocation == TwoCharacterPhraseLocationEnum.D)
                        {
                            ent.ThirdTCP = new TwoCharacterPhraseEntity();
                            ent.ThirdTCP.FirstChar = 共通文字.ToCharArray(0, 1)[0];
                            ent.ThirdTCP.SecondChar = C.SecondChar; ;
                        }


                        ent.CommonCharacter = 共通文字.ToCharArray(0, 1)[0];
                        result.Add(ent);

                        //}

                    }

                }

            }


            //result.Add(result[0]);
            //result.Add(result[0]);

            //重複削除
            IEqualityComparer<TwoCharacterPhraseFourEntity> customComparer = new TwoCharacterPhraseFourEntityComparer();
            result = result.Distinct(new TwoCharacterPhraseFourEntityComparer()).ToList();

#if DEBUG
            result.ForEach(x =>
                System.Diagnostics.Debug.WriteLine(string.Format("A:{0} B:{1} C:{2} D:{3}"
                    , x.FirstTCP.TwoCharacterPhrase
                    , x.SecondTCP.TwoCharacterPhrase
                    , emptyLocation == TwoCharacterPhraseLocationEnum.C ? "-" :  x.ThirdTCP.TwoCharacterPhrase
                    , emptyLocation == TwoCharacterPhraseLocationEnum.D ? "-" : x.FourthTCP.TwoCharacterPhrase
                    ))
            );

#endif

            return result;

        }


        private static Dictionary<int, string> max2(Dictionary<int, string> x, Dictionary<int, string> y)
        {
            Dictionary<int, string> max = x;
            if (x.Count < y.Count)
            {
                max = y;
            }

            return x;




        }



    }
}
