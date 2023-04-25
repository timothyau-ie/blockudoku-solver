using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSolver.GameMech.Think
{
    public class Step
    {
        public Shape Shape;
        public Tuple<int, int> Position;

        public Step(Shape shape, Tuple<int, int> position)
        {
            Shape = shape;
            Position = new Tuple<int, int>(position.Item1, position.Item2);
        }
        public Step(Step step)
        {
            Shape = step.Shape;
            Position = new Tuple<int, int>(step.Position.Item1, step.Position.Item2);
        }
    }
}
