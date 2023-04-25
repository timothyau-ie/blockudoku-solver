using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSolver.GameMech.Boards
{
    public abstract class BaseBoard
    {
        public List<Shape> SelectedShapes;


        //return erased count
        public virtual int CheckForEraser()
        {
            throw new NotImplementedException();
        }

        // return: -1 if not allow, otherwise is erased count
        public virtual int PlaceShape(int x, int y, Shape shape)
        {
            throw new NotImplementedException();
        }

        public virtual int PlaceShapeUiMode(int x, int y, Shape shape)
        {
            throw new NotImplementedException();
        }

        public virtual void ClearColor()
        {
            throw new NotImplementedException();
        }

        public virtual void RecreateBoard(string[] rowPattern)
        {
            throw new NotImplementedException();
        }

    }
}
