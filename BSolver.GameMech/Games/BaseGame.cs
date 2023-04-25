using BSolver.GameMech.Boards;
using BSolver.GameMech.Think;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSolver.GameMech.Games
{
    public abstract class BaseGame
    {
        public FlatBoard Board;
        public List<Shape> Shapes;

        //public BaseGame()
        //{
        //    Board = new Board();
        //    Shapes = new List<Shape>();
        //}

        public void PickRandomSelectedShapes(Random random)
        {
            Board.SelectedShapes.Clear();
            for (int i = 0; i < 3; i++)
            {
                Board.SelectedShapes.Add(Shapes[random.Next(Shapes.Count)]);
            }
        }

        //public virtual ThinkResult Think(int cleverness)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
