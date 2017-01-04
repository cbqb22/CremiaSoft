using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CremiaViewModel.Entity.Polyomino
{
    /// <summary>
    /// ポリオミノのEntity
    /// </summary>
    public class PolyominoEntity
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="polycells"></param>
        /// <param name="angle"></param>
        public PolyominoEntity(List<CellEntity> polycells,double angle)
        {
            PolyCells = polycells;
            Angle = angle;
        }


        /// <summary>
        /// コンストラクタ　MirrorModeつき
        /// </summary>
        /// <param name="polycells"></param>
        /// <param name="angle"></param>
        /// <param name="isMirror"></param>
        public PolyominoEntity(List<CellEntity> polycells, double angle,bool isMirror)
        {
            PolyCells = polycells;
            Angle = angle;
            IsMirror = isMirror;
        }


        /// <summary>
        /// 左右反転
        /// 裏表対応
        /// </summary>
        private bool _IsMirror;
        public bool IsMirror
        {
            get { return _IsMirror; }
            set { _IsMirror = value; }
        }

        private List<CellEntity> _PolyCells;
        private double _Angle;
        public double Angle
        {
            get
            {
                return _Angle;
            }

            set
            {
                _Angle = value;
            }
        }

        public List<CellEntity> PolyCells
        {
            get
            {
                return _PolyCells;
            }

            set
            {
                _PolyCells = value;
            }
        }


        /// <summary>
        /// http://www.cuc.ac.jp/~miyata/classes/prg2.H24/12/rotate.html
        /// θ度、右回転する場合の新座標
        /// x' = -xcosθ+ysinθ
        /// y' = -xsinθ-ycosθ
        /// 
        /// c#で三角関数ではθにラジアンを使う
        /// http://kuroeveryday.blogspot.jp/2015/03/Trigonometry.html
        /// </summary>
        private void AddAngle(double addangle)
        {
            Angle += addangle;

            //Angle -= ((int)Angle / 360) * 360; //360度は0度と同じなので、0～360の範囲に戻す

            double radian = AngleToRadian(addangle);
            foreach (var cell in PolyCells)
            {
                double x = cell.X;
                double y = cell.Y;
                //cell.X = y;
                //cell.Y = -x;

                cell.X = -x * Math.Cos(radian) + y * Math.Sin(radian);
                cell.X = Math.Round(cell.X);
                cell.Y = -x * Math.Sin(radian) - y * Math.Cos(radian);
                cell.Y = Math.Round(cell.Y);

            }
        }

        /// <summary>
        /// 度数をラジアンへ変換
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        private double AngleToRadian(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        /// <summary>
        /// ９０度右に回転
        /// 座標と角度を変更
        /// </summary>
        public void Add90Angle()
        {
            AddAngle(90d);
        }

        /// <summary>
        /// ０度に強制的に戻す
        /// </summary>
        public void ToAngle0()
        {
            //double a = Angle % 360;
            //AddAngle(360 - a);
            AddAngle(-Angle);
        }


        public void MirrorModeOn()
        {
            if (!IsMirror)
            {
                ToMirror();
                IsMirror = true;
            }
        }

        public void MirrorModeOff()
        {
            if (IsMirror)
            {
                ToMirror();
                IsMirror = false;
            }
        }


        /// <summary>
        /// X軸で反転
        /// </summary>
        private void ToMirror()
        {
            foreach (var cell in PolyCells)
            {
                cell.X *= -1;
                cell.Y = cell.Y; //Y軸はそのまま

            }

        }

    }
}
