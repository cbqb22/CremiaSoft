using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CremiaViewModel.Entity.Polyomino
{
    public class PolyominoSet
    {
        private DrawCellAreaEntity _DrawCellAreaEntity;

        public DrawCellAreaEntity DrawCellAreaEntity
        {
            get
            {
                return _DrawCellAreaEntity;
            }

            set
            {
                _DrawCellAreaEntity = value;
            }
        }

        public List<PolyominoEntity> UsedPolyominoEntityList
        {
            get
            {
                return _UsedPolyominoEntityList;
            }

            set
            {
                _UsedPolyominoEntityList = value;
            }
        }

        private List<PolyominoEntity> _UsedPolyominoEntityList;
    }
}
