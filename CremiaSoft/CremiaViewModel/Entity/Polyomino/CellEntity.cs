using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CremiaViewModel.Entity.Polyomino
{
    /// <summary>
    /// CellのEntity
    /// </summary>
    public class CellEntity
    {
        private double _X;
        private double _Y;
        private Color _BackColor;
        private bool _IsFilled;
        private bool _Enable;


        public CellEntity(double x,double y,Color color,bool isfilled,bool enable)
        {
            X = x;
            Y = y;
            BackColor = color;
            IsFilled = isfilled;
            Enable = enable;
        }

        public bool Enable
        {
            get { return _Enable; }
            set { _Enable = value; }
        }

        public double X
        {
            get
            {
                return _X;
            }

            set
            {
                _X = value;
            }
        }
        public double Y
        {
            get
            {
                return _Y;
            }

            set
            {
                _Y = value;
            }
        }
        public Color BackColor
        {
            get
            {
                return _BackColor;
            }

            set
            {
                _BackColor = value;
            }
        }
        public bool IsFilled
        {
            get
            {
                return _IsFilled;
            }

            set
            {
                _IsFilled = value;
            }
        }
    }
}
