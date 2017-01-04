using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CremiaViewModel.Entity.Polyomino
{
    /// <summary>
    /// ポリオミノを敷き詰めるセルのエリア
    /// </summary>
    public class DrawCellAreaEntity
    {



        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x">x軸の個数</param>
        /// <param name="y">y軸の個数</param>
        public DrawCellAreaEntity(int x, int y,List<Tuple<int,int>> disableArea)
        {
            Cells = new List<CellEntity>();

            for (int iy = 0; iy < y; iy++)
            {
                for (int ix = 0; ix < x; ix++)
                {
                    //利用可能領域か
                    bool enable = true;
                    var query = disableArea.Where((t) => {
                        if (t.Item1 == ix && t.Item2 == iy)
                            return true;
                        else
                            return false;
                    });
                    enable = query.Count() == 0 ? true : false;

                    CellEntity ce = new CellEntity(ix,iy,System.Drawing.Color.White,false,enable);
                    ce.X = ix;
                    ce.Y = iy;
                    ce.BackColor = System.Drawing.Color.White;

                    Cells.Add(ce);
                }
            }
        }

        /// <summary>
        /// セルを格納するリストｓ
        /// </summary>
        private List<CellEntity> _Cells;
        public List<CellEntity> Cells
        {
            get
            {
                return _Cells;
            }

            set
            {
                _Cells = value;
            }
        }

        /// <summary>
        /// 全てのセルが埋まっているかまたは利用不可
        /// </summary>
        public bool IsAllCellFilledOrDisabled
        {
            get
            {
                bool flag = true;
                Cells.ForEach((c) =>
                        {
                            if (!c.IsFilled && c.Enable)
                                flag = false;
                        });
                return flag;
            }

        }

        /// <summary>
        /// 最も左上の空白セルを返す
        /// </summary>
        /// <returns></returns>
        public CellEntity EmptyCellLeftUpper()
        {
            CellEntity result = new CellEntity(-1,-1,System.Drawing.Color.White,false,true);
            result.X = -1;
            result.Y = -1;

            foreach (var cell in Cells)
            {
                //利用不可の場所はcontinue
                if (!cell.Enable)
                {
                    continue;
                }

                if (cell.IsFilled)
                    continue;

                if (result.X == -1 || result.Y == -1)
                {
                    result = cell;
                    continue;
                }

                if (cell.X < result.X)
                {
                    result = cell;
                }
                else if (cell.X == result.X)
                {
                    if (cell.Y < result.Y)
                    {
                        result = cell;
                    }
                }
                else
                {
                    continue;
                }
            }

            return result;

        }


        /// <summary>
        /// 指定の座標のセルを返す
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public CellEntity GetCellByXY(double X, double Y)
        {
            var result = Cells.Where((c) =>
            {
                if (c.X == X && c.Y == Y)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });

            if (result.Count() == 0)
            {
                return null;
            }
            else if(1 < result.Count())
            {
                throw new Exception("対応するセルが２つ以上あり、不正です。");
            }

            return result.First();


        }

    }
}
