using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BSolver.GameMech
{
    public class Shape
    {
        public string Name;
        public List<Tuple<int, int>> Blocks;
        public Tuple<double, double> Centre;
        public List<Tuple<double, double>> BlockRelativePositions;
        public Tuple<int, int> Size;

        // flat board
        public int[] RowBits;


        public Shape(string name, string pattern)
        {
            Name = name;
            Blocks = new List<Tuple<int, int>>();

            int x = 0;
            int y = 0;
            int i = 0;

            int totalX = 0;
            int totalY = 0;
            int maxX = 0;
            int maxY = 0;
            while(i < pattern.Length)
            {
                switch (pattern[i])
                {
                    case '.':
                        AddBlock(x, y);
                        totalX += x;
                        totalY += y;
                        if (x > maxX)
                        {
                            maxX = x;
                        }
                        if (y > maxY)
                        {
                            maxY = y;
                        }
                        x++;
                        break;
                    case ' ':
                        x++;
                        break;
                    case '|':
                        y++;
                        x = 0;
                        break;
                    default:
                        break;
                }
                i++;
            }

            int blockCount = Blocks.Count;
            Centre = new Tuple<double, double>(totalX * 1.0 / blockCount, totalY * 1.0 / blockCount);

            BlockRelativePositions = new List<Tuple<double, double>>();
            for (int ii = 0; ii < blockCount; ii++)
            {
                BlockRelativePositions.Add(new Tuple<double, double>(Blocks[ii].Item1 - Centre.Item1, Blocks[ii].Item2 - Centre.Item2));
            }

            Size = new Tuple<int, int>(maxX + 1, maxY + 1);

            RowBits = new int[maxY + 1];
            for (int ii = 0; ii < blockCount; ii++)
            {
                int singleBit = 0b_0000_0001_0000_0000;
                RowBits[Blocks[ii].Item2] |= singleBit >> Blocks[ii].Item1;
            }
        }

        private void AddBlock(int x, int y)
        {
            Blocks.Add(new Tuple<int, int>(x, y));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
