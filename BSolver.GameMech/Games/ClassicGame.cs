using BSolver.GameMech.Think;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSolver.GameMech.Games
{
    public class ClassicGame : BaseGame
    {
        public ClassicGame() //: base()
        {
            Shapes = new List<Shape>();
            Board = new Boards.FlatBoard();
            Dictionary<string, string> shapeStrings = new Dictionary<string, string>();

            // 1 block
            shapeStrings.Add("dot", ".");

            // 2 blocks
            shapeStrings.Add("i2h", "..");
            shapeStrings.Add("i2v",".|.");
            shapeStrings.Add("slash2a", ".| .");
            shapeStrings.Add("slash2b", " .|.");

            // 3 blocks
            shapeStrings.Add("i3h", "...");
            shapeStrings.Add("i3v", ".|.|.");
            shapeStrings.Add("slash3a", ".| .|  .");
            shapeStrings.Add("slash3b", "  .| .|.");
            shapeStrings.Add("l3a", "..|.");
            shapeStrings.Add("l3b", "..| .");
            shapeStrings.Add("l3c", " .|..");
            shapeStrings.Add("l3d", ".|..");

            // 4 blocks
            shapeStrings.Add("o", "..|..");
            shapeStrings.Add("i4h", "....");
            shapeStrings.Add("i4v", ".|.|.|.");
            shapeStrings.Add("l4a", ".|.|..");
            shapeStrings.Add("l4b", "...|.");
            shapeStrings.Add("l4c", "..| .| .");
            shapeStrings.Add("l4d", "  .|...");
            shapeStrings.Add("l4e", ".|...");
            shapeStrings.Add("l4f", "..|.|.");
            shapeStrings.Add("l4g", "...|  .");
            shapeStrings.Add("l4h", " .| .|..");
            shapeStrings.Add("t4a", "...| .");
            shapeStrings.Add("t4b", " .|..| .");
            shapeStrings.Add("t4c", " .|...");
            shapeStrings.Add("t4d", ".|..|.");
            shapeStrings.Add("sa", " ..|..");
            shapeStrings.Add("sb", ".|..| .");
            shapeStrings.Add("za", " .|..|.");
            shapeStrings.Add("zb", "..| ..");

            // 5 blocks
            shapeStrings.Add("ua", ". .|...");
            shapeStrings.Add("ub", "..|. |..");
            shapeStrings.Add("uc", "...|. .");
            shapeStrings.Add("ud", "..| .|..");
            shapeStrings.Add("x", " .|...| .");
            shapeStrings.Add("i5h", ".....");
            shapeStrings.Add("i5v", ".|.|.|.|.");
            shapeStrings.Add("l5a", ".|.|...");
            shapeStrings.Add("l5b", "...|.|.");
            shapeStrings.Add("l5c", "...|  .|  .");
            shapeStrings.Add("l5d", "  .|  .|...");
            shapeStrings.Add("t5a", "...| .| .");
            shapeStrings.Add("t5b", "  .|...|  .");
            shapeStrings.Add("t5c", " .| .|...");
            shapeStrings.Add("t5d", ".|...|.");

            foreach (string key in shapeStrings.Keys)
            {
                Shapes.Add(new Shape(key, shapeStrings[key]));
            }
        }

        
    }
}
