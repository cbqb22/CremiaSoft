using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CremiaViewModel.Entity.Polyomino;

namespace CremiaViewModel.Const.Polyomino
{
    public class PolyominoConst
    {

        /// <summary>
        /// ペントミノ全１２種類
        /// </summary>
        public static class Pentomino
        {
            public static readonly PolyominoEntity PentominoF = new PolyominoEntity(
                                                                    new List<CellEntity>() {
                                                                    new CellEntity(0, 0, System.Drawing.Color.YellowGreen, false, true),
                                                                    new CellEntity(1, 0, System.Drawing.Color.YellowGreen, false, true),
                                                                    new CellEntity(1, 1, System.Drawing.Color.YellowGreen, false, true),
                                                                    new CellEntity(2, 1, System.Drawing.Color.YellowGreen, false, true),
                                                                    new CellEntity(1, 2, System.Drawing.Color.YellowGreen, false, true),
                                                                            }, 0, false);


            public static readonly PolyominoEntity PentominoL = new PolyominoEntity(
                                                            new List<CellEntity>() {
                                                                    new CellEntity(0, 0, System.Drawing.Color.Orange, false, true),
                                                                    new CellEntity(0, 1, System.Drawing.Color.Orange, false, true),
                                                                    new CellEntity(0, 2, System.Drawing.Color.Orange, false, true),
                                                                    new CellEntity(0, 3, System.Drawing.Color.Orange, false, true),
                                                                    new CellEntity(1, 3, System.Drawing.Color.Orange, false, true),
                                                                    }, 0, false);


            public static readonly PolyominoEntity PentominoN = new PolyominoEntity(
                                                            new List<CellEntity>() {
                                                                    new CellEntity(0, 0, System.Drawing.Color.Black, false, true),
                                                                    new CellEntity(1, 0, System.Drawing.Color.Black, false, true),
                                                                    new CellEntity(2, 0, System.Drawing.Color.Black, false, true),
                                                                    new CellEntity(2, 1, System.Drawing.Color.Black, false, true),
                                                                    new CellEntity(3, 1, System.Drawing.Color.Black, false, true),
                                                                    }, 0, false);


            public static readonly PolyominoEntity PentominoP = new PolyominoEntity(
                                                            new List<CellEntity>() {
                                                                    new CellEntity(0, 0, System.Drawing.Color.SkyBlue, false, true),
                                                                    new CellEntity(1, 0, System.Drawing.Color.SkyBlue, false, true),
                                                                    new CellEntity(0, 1, System.Drawing.Color.SkyBlue, false, true),
                                                                    new CellEntity(1, 1, System.Drawing.Color.SkyBlue, false, true),
                                                                    new CellEntity(2, 1, System.Drawing.Color.SkyBlue, false, true),
                                                                    }, 0, false);


            public static readonly PolyominoEntity PentominoY = new PolyominoEntity(
                                                            new List<CellEntity>() {
                                                                    new CellEntity(2, 0, System.Drawing.Color.Brown, false, true),
                                                                    new CellEntity(0, 1, System.Drawing.Color.Brown, false, true),
                                                                    new CellEntity(1, 1, System.Drawing.Color.Brown, false, true),
                                                                    new CellEntity(2, 1, System.Drawing.Color.Brown, false, true),
                                                                    new CellEntity(3, 1, System.Drawing.Color.Brown, false, true),
                                                                    }, 0, false);


            public static readonly PolyominoEntity PentominoZ = new PolyominoEntity(
                                                            new List<CellEntity>() {
                                                                    new CellEntity(0, 0, System.Drawing.Color.Gray, false, true),
                                                                    new CellEntity(1, 0, System.Drawing.Color.Gray, false, true),
                                                                    new CellEntity(1, 1, System.Drawing.Color.Gray, false, true),
                                                                    new CellEntity(1, 2, System.Drawing.Color.Gray, false, true),
                                                                    new CellEntity(2, 2, System.Drawing.Color.Gray, false, true),
                                                                    }, 0, false);


            public static readonly PolyominoEntity PentominoT = new PolyominoEntity(
                                                            new List<CellEntity>() {
                                                                    new CellEntity(0, 0, System.Drawing.Color.LightGreen, false, true),
                                                                    new CellEntity(1, 0, System.Drawing.Color.LightGreen, false, true),
                                                                    new CellEntity(2, 0, System.Drawing.Color.LightGreen, false, true),
                                                                    new CellEntity(1, 1, System.Drawing.Color.LightGreen, false, true),
                                                                    new CellEntity(1, 2, System.Drawing.Color.LightGreen, false, true),
                                                                    }, 0, false);



            public static readonly PolyominoEntity PentominoU = new PolyominoEntity(
                                                            new List<CellEntity>() {
                                                                    new CellEntity(0, 0, System.Drawing.Color.Yellow, false, true),
                                                                    new CellEntity(2, 0, System.Drawing.Color.Yellow, false, true),
                                                                    new CellEntity(0, 1, System.Drawing.Color.Yellow, false, true),
                                                                    new CellEntity(1, 1, System.Drawing.Color.Yellow, false, true),
                                                                    new CellEntity(2, 1, System.Drawing.Color.Yellow, false, true),
                                                                    }, 0, false);



            public static readonly PolyominoEntity PentominoV = new PolyominoEntity(
                                                            new List<CellEntity>() {
                                                                    new CellEntity(0, 0, System.Drawing.Color.Blue, false, true),
                                                                    new CellEntity(0, 1, System.Drawing.Color.Blue, false, true),
                                                                    new CellEntity(0, 2, System.Drawing.Color.Blue, false, true),
                                                                    new CellEntity(1, 2, System.Drawing.Color.Blue, false, true),
                                                                    new CellEntity(2, 2, System.Drawing.Color.Blue, false, true),
                                                                    }, 0, false);



            public static readonly PolyominoEntity PentominoW = new PolyominoEntity(
                                                            new List<CellEntity>() {
                                                                    new CellEntity(0, 0, System.Drawing.Color.Pink, false, true),
                                                                    new CellEntity(0, 1, System.Drawing.Color.Pink, false, true),
                                                                    new CellEntity(1, 1, System.Drawing.Color.Pink, false, true),
                                                                    new CellEntity(1, 2, System.Drawing.Color.Pink, false, true),
                                                                    new CellEntity(2, 2, System.Drawing.Color.Pink, false, true),
                                                                    }, 0, false);



            public static readonly PolyominoEntity PentominoI = new PolyominoEntity(
                                                            new List<CellEntity>() {
                                                                    new CellEntity(0, 0, System.Drawing.Color.DarkGray, false, true),
                                                                    new CellEntity(1, 0, System.Drawing.Color.DarkGray, false, true),
                                                                    new CellEntity(2, 0, System.Drawing.Color.DarkGray, false, true),
                                                                    new CellEntity(3, 0, System.Drawing.Color.DarkGray, false, true),
                                                                    new CellEntity(4, 0, System.Drawing.Color.DarkGray, false, true),
                                                                    }, 0, false);



            public static readonly PolyominoEntity PentominoX = new PolyominoEntity(
                                                            new List<CellEntity>() {
                                                                    new CellEntity(1, 0, System.Drawing.Color.Red, false, true),
                                                                    new CellEntity(0, 1, System.Drawing.Color.Red, false, true),
                                                                    new CellEntity(1, 1, System.Drawing.Color.Red, false, true),
                                                                    new CellEntity(2, 1, System.Drawing.Color.Red, false, true),
                                                                    new CellEntity(1, 2, System.Drawing.Color.Red, false, true),
                                                                    }, 0, false);

        }

    }
}
