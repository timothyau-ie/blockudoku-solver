using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace BSolver.GameMech.Boards
{
    public class FlatBoard : BaseBoard
    {
        //BigInteger boardBits;
        public int[] RowBits;
        public int[] RowBitsColor;
        static int[] singleBits;
        static int[] tripleBits;
        const int FULL_ROW_BITS = 0b_0000_0001_1111_1111;
        const int SINGLE_BIT = 0b_0000_0001_0000_0000;

        //static private List<BigInteger> _eraserAreas;
        static public void Init()
        {
            //int singleBit = 0b_0000_0001_0000_0000;
            singleBits = new int[9];
            for (int i = 0; i < 9; i++)
            {
                singleBits[i] = SINGLE_BIT >> i;
            }
            int tripleBit = 0b_0000_0001_1100_0000;
            tripleBits = new int[3];
            for (int i = 0; i < 3; i++)
            {
                tripleBits[i] = tripleBit >> i * 3;
            }
            //BigInteger lastRow = 0b_1_1111_1111;
            //for (int i = 0; i < 9; i++)
            //{
            //    _eraserAreas.Add(lastRow << (9 * i));
            //}
            //BigInteger lastDot = 1;
            //BigInteger lastColumn = 0;
            //for (int i = 0; i < 9; i++)
            //{
            //    lastColumn += (lastDot << (9 * i));
            //}
            //for (int i = 0; i < 9; i++)
            //{
            //    _eraserAreas.Add(lastColumn << (9 * i));
            //}
            //BigInteger lastSquare = 0;
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //    {
            //        lastSquare += (lastDot << (i + j * 9));
            //    }
            //}
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //    {
            //        _eraserAreas.Add(lastColumn << (i + j * 9 * 3));
            //    }
            //}
        }

        public FlatBoard()
        {
            //boardBits = 0;
            RowBits = new int[9];
            RowBitsColor = new int[9];
            SelectedShapes = new List<Shape>();
        }

        public FlatBoard(FlatBoard board)
        {
            RowBits = board.RowBits.ToArray();
            RowBitsColor = board.RowBitsColor.ToArray();
            SelectedShapes = new List<Shape>(board.SelectedShapes);
        }

        //return erased count
        public override int CheckForEraser()
        {
            int[] eraseBits = new int[9];
            for (int i = 0; i < 9; i++)
            {
                if (RowBits[i] == FULL_ROW_BITS)
                {
                    eraseBits[i] = FULL_ROW_BITS;
                }
                int singleBit = singleBits[i];
                if ((RowBits[0] & singleBit) != 0
                    && (RowBits[1] & singleBit) != 0
                    && (RowBits[2] & singleBit) != 0
                    && (RowBits[3] & singleBit) != 0
                    && (RowBits[4] & singleBit) != 0
                    && (RowBits[5] & singleBit) != 0
                    && (RowBits[6] & singleBit) != 0
                    && (RowBits[7] & singleBit) != 0
                    && (RowBits[8] & singleBit) != 0)
                {
                    eraseBits[0] |= singleBit;
                    eraseBits[1] |= singleBit;
                    eraseBits[2] |= singleBit;
                    eraseBits[3] |= singleBit;
                    eraseBits[4] |= singleBit;
                    eraseBits[5] |= singleBit;
                    eraseBits[6] |= singleBit;
                    eraseBits[7] |= singleBit;
                    eraseBits[8] |= singleBit;
                }
            }
            for (int i = 0; i < 9; i += 3)
            {
                for (int j = 0; j < 3; j ++)
                {
                    if ((RowBits[i] & tripleBits[j]) == tripleBits[j]
                    && (RowBits[i + 1] & tripleBits[j]) == tripleBits[j]
                    && (RowBits[i + 2] & tripleBits[j]) == tripleBits[j])
                    {
                        eraseBits[i] |= tripleBits[j];
                        eraseBits[i + 1] |= tripleBits[j];
                        eraseBits[i + 2] |= tripleBits[j];
                    }
                }
            }
            int totalErased = 0;
            for (int i = 0; i < 9; i++)
            {
                RowBits[i] &= ~eraseBits[i];
                totalErased += CountBitsInteger(eraseBits[i]);
            }
            return totalErased;

            //BigInteger clearBits = 0;
            //for (int i = 0; i < _eraserAreas.Count; i++)
            //{
            //    if ((boardBits & _eraserAreas[i]) == _eraserAreas[i])
            //    {
            //        clearBits |= _eraserAreas[i];
            //    }
            //}
            //if (clearBits != 0)
            //{
            //    boardBits = boardBits & (~clearBits);
            //}
            //return CountBits(clearBits);
        }

        //private int CountBits(BigInteger b)
        //{
        //    return CountBitsInteger((int)(b >> 64)) + CountBitsInteger((int)((b >> 32) & 0xFFFFFFFF)) + CountBitsInteger((int)(b & 0xFFFFFFFF));
        //}

        private int CountBitsInteger(int i)
        {
            
            i = i - ((i >> 1) & 0x55555555);        // add pairs of bits
            i = (i & 0x33333333) + ((i >> 2) & 0x33333333);  // quads
            i = (i + (i >> 4)) & 0x0F0F0F0F;        // groups of 8
            return (i * 0x01010101) >> 24;          // horizontal sum of bytes
        }

        // return: -1 if not allow, otherwise is erased count
        public override int PlaceShape(int x, int y, Shape shape)
        {
            for (int j = 0; j < shape.Size.Item2; j++)
            {
                if ((RowBits[j + y] & (shape.RowBits[j] >> x)) != 0)
                {
                    return -1;
                }
            }
            for (int j = 0; j < shape.Size.Item2; j++)
            {
                RowBits[j + y] |= (shape.RowBits[j] >> x);
            }

            //for (int i = 0; i < shape.Blocks.Count; i++)
            //{
            //    int displacedX = shape.Blocks[i].Item1 + x;
            //    int displacedY = shape.Blocks[i].Item2 + y;
            //    if (displacedX < 0 || displacedX > 8 || displacedY < 0 || displacedY > 8)
            //    {
            //        return -1;
            //    }
            //    if (Tiles[displacedX, displacedY].Thickness > 0)
            //    {
            //        return -1;
            //    }
            //}

            //for (int i = 0; i < shape.Blocks.Count; i++)
            //{
            //    int displacedX = shape.Blocks[i].Item1 + x;
            //    int displacedY = shape.Blocks[i].Item2 + y;
            //    Tiles[displacedX, displacedY].Thickness++;

            //}

            int erasedCount = CheckForEraser();
            return erasedCount;
        }

        public override int PlaceShapeUiMode(int x, int y, Shape shape)
        {
            for (int j = 0; j < shape.Size.Item2; j++)
            {
                if ((RowBits[j + y] & (shape.RowBits[j] >> x)) != 0)
                {
                    return -1;
                }
            }
            for (int j = 0; j < shape.Size.Item2; j++)
            {
                RowBits[j + y] |= (shape.RowBits[j] >> x);
                RowBitsColor[j + y] |= (shape.RowBits[j] >> x);
            }

            //for (int i = 0; i < shape.Blocks.Count; i++)
            //{
            //    int displacedX = shape.Blocks[i].Item1 + x;
            //    int displacedY = shape.Blocks[i].Item2 + y;
            //    if (displacedX < 0 || displacedX > 8 || displacedY < 0 || displacedY > 8)
            //    {
            //        return -1;
            //    }
            //    if (Tiles[displacedX, displacedY].Thickness > 0)
            //    {
            //        return -1;
            //    }
            //}



            //for (int i = 0; i < shape.Blocks.Count; i++)
            //{
            //    int displacedX = shape.Blocks[i].Item1 + x;
            //    int displacedY = shape.Blocks[i].Item2 + y;
            //    Tiles[displacedX, displacedY].Thickness++;

            //    Tiles[displacedX, displacedY].ColorFlag = true;
            //}
            return 0;
        }

        public override void ClearColor()
        {
            for (int i = 0; i < 9; i++)
            {
                //for (int j = 0; j < 9; j++)
                //{
                //    Tiles[i, j].ColorFlag = false;
                //}
                RowBitsColor[i] = 0;
            }
        }

        public override void RecreateBoard(string[] rowPattern)
        {
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    char c = rowPattern[j][i];
                    //Tiles[i, j].Thickness = c == '-' ? 0 : int.Parse(c.ToString());
                    if (c != '-')
                    {
                        RowBits[j] |= (SINGLE_BIT >> i);
                    }
                }
            }
        }

        public override string ToString()
        {
            string s = Environment.NewLine;
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    //s += Tiles[i, j].ColorFlag ? "X" : Tiles[i, j].Thickness == 0 ? "." : Tiles[i, j].Thickness.ToString();

                    if ((RowBitsColor[j] & (SINGLE_BIT >> i)) != 0)
                    {
                        s += "X";
                    }
                    else if ((RowBits[j] & (SINGLE_BIT >> i)) != 0)
                    {
                        s += "1";
                    }
                    else
                    {
                        s += ".";
                    }
                }
                s += Environment.NewLine;
            }
            return s;
        }
    }
}
