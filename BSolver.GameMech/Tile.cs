using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSolver.GameMech
{
    public class Tile
    {
        public int Thickness;
        //public bool EraserFlag;
        public bool ColorFlag;
        public Tile(int thickness)
        {
            Thickness = thickness;
            //EraserFlag = false;
            ColorFlag = false;
        }
        public Tile(Tile tile)
        {
            Thickness = tile.Thickness;
            //EraserFlag = tile.EraserFlag;
            ColorFlag = tile.ColorFlag;
        }
    }
}
