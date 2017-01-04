using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CremiaViewModel.Entity.Polyomino;
using CremiaViewModel.Const.Polyomino;


namespace CremiaViewModel.Routine.Polyomino
{
    public class PolyominoMaker
    {
        public static PolyominoSet MakePentominoQuestion(int x, int y, int removeCellsCount, bool includingMirror, bool includingRotation)
        {

            int totalCells = (x * y - removeCellsCount);

            if (totalCells % 5 != 0)
            {
                return null;
            }

            int polyominoCount = totalCells / 5;


            var polyominoList = MakeRandomUsePolyominoList(polyominoCount);
            var randomRemoveCells = MakeRandoRemoveCells(y, y, removeCellsCount);

            DrawCellAreaEntity drawCellEntity = new DrawCellAreaEntity(x, y, randomRemoveCells);


            var result = PolyominoAnalysis.Analysis(polyominoList, drawCellEntity, includingMirror, includingMirror);

            return result;

        }

        public static List<Tuple<int, int>> MakeRandoRemoveCells(int MaxX, int MaxY, int removeCellsCount)
        {
            List<Tuple<int, int>> useNo = new List<Tuple<int, int>>();
            List<Tuple<int, int>> removableCellList = new List<Tuple<int, int>>();

            for (int i = 0; i < MaxX; i++)
            {
                Tuple<int, int> tp = new Tuple<int, int>(0, i);
                removableCellList.Add(tp);
            }
            for (int i = 1; i < MaxY; i++) //0,0は飛ばす
            {
                Tuple<int, int> tp = new Tuple<int, int>(i, 0);
                removableCellList.Add(tp);
            }


            int rNumX = 0;
            int rNumY = 0;
            int seeds = DateTime.Now.Millisecond;
            int seeds2 = (int)DateTime.Now.Ticks;
            int counter = 0;
            Random randomX = new Random(seeds++);
            Random randomY = new Random(seeds2++);
            do
            {

                rNumX = randomX.Next(0, MaxX);
                rNumY = randomY.Next(0, MaxY);


 
                bool canRemovable = false;
                removableCellList.ForEach(x =>
                {
                    if (x.Item1 == rNumX && x.Item2 == rNumY)
                        canRemovable = true;
                });
                if (!canRemovable)
                    continue;


                bool find = false;
                useNo.ForEach(x =>
                {
                    if (x.Item1 == rNumX && x.Item2 == rNumY)
                        find = true;
                });


                if (find)
                    continue;

                //隣接したセルを除去可能セルに追加（前後左右）
                Tuple<int, int> tp = new Tuple<int, int>(rNumX + 1, rNumY);
                Tuple<int, int> tp2 = new Tuple<int, int>(rNumX, rNumY + 1);
                Tuple<int, int> tp3 = new Tuple<int, int>(rNumX - 1, rNumY);
                Tuple<int, int> tp4 = new Tuple<int, int>(rNumX, rNumY - 1);

                if(0 < tp.Item1 && 0 < tp.Item2)
                {
                    if (removableCellList.Find(x => x.Item1 == tp.Item1 && x.Item2 == tp.Item2) == null)
                    {
                        removableCellList.Add(tp);
                    }

                }
                if (0 < tp2.Item1 && 0 < tp2.Item2)
                {
                    if (removableCellList.Find(x => x.Item1 == tp2.Item1 && x.Item2 == tp2.Item2) == null)
                    {
                        removableCellList.Add(tp2);
                    }
                }
                if (0 < tp3.Item1 && 0 < tp3.Item2)
                {
                    if (removableCellList.Find(x => x.Item1 == tp3.Item1 && x.Item2 == tp3.Item2) == null)
                    {
                        removableCellList.Add(tp3);
                    }
                }
                if (0 < tp4.Item1 && 0 < tp4.Item2)
                {
                    if (removableCellList.Find(x => x.Item1 == tp4.Item1 && x.Item2 == tp4.Item2) == null)
                    {
                        removableCellList.Add(tp4);
                    }
                }


                useNo.Add(new Tuple<int, int>(rNumX, rNumY));
                counter++;
            }
            while (counter < removeCellsCount);

            return useNo;

        }

        public static List<PolyominoEntity> MakeRandomUsePolyominoList(int polyominoCount)
        {

            List<PolyominoEntity> polyominoList = new List<PolyominoEntity>();

            int counter = 0;
            Dictionary<int, PolyominoEntity> dic = new Dictionary<int, PolyominoEntity>() {
                { counter++,PolyominoConst.Pentomino.PentominoF },
                { counter++,PolyominoConst.Pentomino.PentominoL },
                { counter++,PolyominoConst.Pentomino.PentominoN },
                { counter++,PolyominoConst.Pentomino.PentominoP },
                { counter++,PolyominoConst.Pentomino.PentominoY },
                { counter++,PolyominoConst.Pentomino.PentominoZ },
                { counter++,PolyominoConst.Pentomino.PentominoT },
                { counter++,PolyominoConst.Pentomino.PentominoU },
                { counter++,PolyominoConst.Pentomino.PentominoV },
                { counter++,PolyominoConst.Pentomino.PentominoW },
                { counter++,PolyominoConst.Pentomino.PentominoI },
                { counter++,PolyominoConst.Pentomino.PentominoX },
            };

            List<int> useNo = new List<int>();
            Random random = new Random(DateTime.Now.Millisecond);
            int rNum = 0;
            do
            {

                rNum = random.Next(0, 12);
                //System.Diagnostics.Debug.WriteLine("PolyominoNum:{0}", rNum);

                if (!useNo.Contains(rNum))
                {
                    useNo.Add(rNum);
                    polyominoList.Add(dic[rNum]);
                }
            }
            while (polyominoList.Count < polyominoCount);

            return polyominoList;


        }

    }
}
