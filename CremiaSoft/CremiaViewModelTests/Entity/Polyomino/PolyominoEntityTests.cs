using Microsoft.VisualStudio.TestTools.UnitTesting;
using CremiaViewModel.Entity.Polyomino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CremiaViewModel.Entity.Polyomino.Tests
{
    [TestClass()]
    public class PolyominoEntityTests
    {
        [TestMethod()]
        public void Add90AngleTest()
        {
            List<CellEntity> list = new List<CellEntity>();
            CellEntity ce = new CellEntity(0,0,Color.White,false,true);
            CellEntity ce2 = new CellEntity(1, 2, Color.White, false, true);
            CellEntity ce3 = new CellEntity(-4, 3, Color.White, false, true);
            list.Add(ce);
            list.Add(ce2);
            list.Add(ce3);

            PolyominoEntity pe = new PolyominoEntity(list, 0);
            pe.Add90Angle();


            double x = 1;
            double y = 2;
            double degrees = 90;
            double x2 = -x * Math.Cos(degrees * (Math.PI / 180)) + y * Math.Sin(degrees * (Math.PI / 180));
            double y2 = -x * Math.Sin(degrees * (Math.PI / 180)) - y * Math.Cos(degrees * (Math.PI / 180));
            x2 = Math.Round(x2);
            y2 = Math.Round(y2);


            Assert.AreEqual((int)pe.PolyCells[0].X, 0);
            Assert.AreEqual((int)pe.PolyCells[0].Y, 0);

            Assert.AreEqual((int)pe.PolyCells[1].X, 2);
            Assert.AreEqual((int)pe.PolyCells[1].Y, -1);

            Assert.AreEqual((int)pe.PolyCells[2].X, 3);
            Assert.AreEqual((int)pe.PolyCells[2].Y, 4);

        }
    }
}