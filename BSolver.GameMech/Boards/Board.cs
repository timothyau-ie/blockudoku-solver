//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BSolver.GameMech.Boards
//{
//    public class Board : BaseBoard
//    {
//        public Tile[,] Tiles;

//        public Board()
//        {
//            Tiles = new Tile[9,9];
//            for(int i = 0; i < 9; i++)
//            {
//                for (int j = 0; j < 9; j++)
//                {
//                    Tiles[i, j] = new Tile(0);
//                }
//            }
//            SelectedShapes = new List<Shape>();
//        }

//        public Board(Board board)
//        {
//            Tiles = new Tile[9,9];
//            for(int i = 0; i < 9; i++)
//            {
//                for (int j = 0; j < 9; j++)
//                {
//                    Tiles[i, j] = new Tile(board.Tiles[i, j]);
//                }
//            }
//            SelectedShapes = new List<Shape>(board.SelectedShapes);
//        }

//        //return erased count
//        public override int CheckForEraser()
//        {
//            bool[] row = new bool[9];
//            bool[] col = new bool[9];
//            bool[,] sq = new bool[3, 3];
//            for (int i = 0; i < 9; i++)
//            {
//                for (int j = 0; j < 9; j++)
//                {
//                    if (Tiles[i, j].Thickness == 0)
//                    {
//                        row[j] = true;
//                        col[i] = true;
//                        sq[i / 3, j / 3] = true;
//                    }
//                }
//            }

//            int erasedCount = 0;
//            // all with marked flags, reduce thickness
//            for (int i = 0; i < 9; i++)
//            {
//                for (int j = 0; j < 9; j++)
//                {
//                    if (!row[j]|| !col[i] || !sq[i / 3, j / 3])
//                    {
//                        Tiles[i, j].Thickness--;
//                        erasedCount++;
//                    }
//                }
//            }
//            return erasedCount;
//        }

//        ////return erased count
//        //public int CheckForEraser()
//        //{
//        //    //check columns
//        //    for (int i = 0; i < 9; i++)
//        //    {
//        //        bool columnFilled = true;
//        //        for (int j = 0; j < 9; j++)
//        //        {
//        //            if (Tiles[i, j].Thickness == 0)
//        //            {
//        //                columnFilled = false;
//        //                break;
//        //            }
//        //        }
//        //        if (columnFilled)
//        //        {
//        //            for (int j = 0; j < 9; j++)
//        //            {
//        //                Tiles[i, j].EraserFlag = true;
//        //            }
//        //        }
//        //    }
//        //    //check rows
//        //    for (int j = 0; j < 9; j++)
//        //    {
//        //        bool rowFilled = true;
//        //        for (int i = 0; i < 9; i++)
//        //        {
//        //            if (Tiles[i, j].Thickness == 0)
//        //            {
//        //                rowFilled = false;
//        //                break;
//        //            }
//        //        }
//        //        if (rowFilled)
//        //        {
//        //            for (int i = 0; i < 9; i++)
//        //            {
//        //                Tiles[i, j].EraserFlag = true;
//        //            }
//        //        }
//        //    }
//        //    //check square
//        //    for (int i = 0; i < 3; i++)
//        //    {
//        //        for (int j = 0; j < 3; j++)
//        //        {
//        //            bool squareFilled = true;
//        //            for (int ii = 0; ii < 3; ii++)
//        //            {
//        //                for (int jj = 0; jj < 3; jj++)
//        //                {
//        //                    if (Tiles[i * 3 + ii, j * 3 + jj].Thickness == 0)
//        //                    {
//        //                        squareFilled = false;
//        //                        break;
//        //                    }
//        //                }
//        //            }
//        //            if (squareFilled)
//        //            {
//        //                for (int ii = 0; ii < 3; ii++)
//        //                {
//        //                    for (int jj = 0; jj < 3; jj++)
//        //                    {
//        //                        Tiles[i * 3 + ii, j * 3 + jj].EraserFlag = true;
//        //                    }
//        //                }
//        //            }
//        //        }
//        //    }
//        //    int erasedCount = 0;
//        //    // all with marked flags, reduce thickness
//        //    for (int i = 0; i < 9; i++)
//        //    {
//        //        for (int j = 0; j < 9; j++)
//        //        {
//        //            if (Tiles[i, j].EraserFlag)
//        //            {
//        //                Tiles[i, j].EraserFlag = false;
//        //                Tiles[i, j].Thickness--;
//        //                erasedCount++;
//        //            }
//        //        }
//        //    }
//        //    return erasedCount;
//        //}

//        //// return: -1 if not allow, otherwise is erased count
//        //public int PlaceShape(int x, int y, int shapeIndex)
//        //{
//        //    int result = PlaceShape(x, y, SelectedShapes[shapeIndex]);
//        //    if (result != -1)
//        //    {
//        //        SelectedShapes.RemoveAt(shapeIndex);
//        //    }
//        //    return result;
//        //}

//        // return: -1 if not allow, otherwise is erased count
//        public override int PlaceShape(int x, int y, Shape shape)
//        {
//            for(int i = 0; i < shape.Blocks.Count; i++)
//            {
//                int displacedX = shape.Blocks[i].Item1 + x;
//                int displacedY = shape.Blocks[i].Item2 + y;
//                if (displacedX < 0 || displacedX > 8 || displacedY < 0 || displacedY > 8)
//                {
//                    return -1;
//                }
//                if (Tiles[displacedX, displacedY].Thickness > 0)
//                {
//                    return -1;
//                }
//            }

//            for (int i = 0; i < shape.Blocks.Count; i++)
//            {
//                int displacedX = shape.Blocks[i].Item1 + x;
//                int displacedY = shape.Blocks[i].Item2 + y;
//                Tiles[displacedX, displacedY].Thickness++;
               
//            }
           
//            int erasedCount = CheckForEraser();
//            return erasedCount;
//        }

//        public override int PlaceShapeUiMode(int x, int y, Shape shape)
//        {
//            for (int i = 0; i < shape.Blocks.Count; i++)
//            {
//                int displacedX = shape.Blocks[i].Item1 + x;
//                int displacedY = shape.Blocks[i].Item2 + y;
//                if (displacedX < 0 || displacedX > 8 || displacedY < 0 || displacedY > 8)
//                {
//                    return -1;
//                }
//                if (Tiles[displacedX, displacedY].Thickness > 0)
//                {
//                    return -1;
//                }
//            }



//            for (int i = 0; i < shape.Blocks.Count; i++)
//            {
//                int displacedX = shape.Blocks[i].Item1 + x;
//                int displacedY = shape.Blocks[i].Item2 + y;
//                Tiles[displacedX, displacedY].Thickness++;

//                Tiles[displacedX, displacedY].ColorFlag = true;
//            }
//            return 0;
//        }

//        public override void ClearColor()
//        {
//            for (int i = 0; i < 9; i++)
//            {
//                for (int j = 0; j < 9; j++)
//                {
//                    Tiles[i, j].ColorFlag = false;
//                }
//            }
//        }

//        public override void RecreateBoard(string[] rowPattern)
//        {
//            for (int j = 0; j < 9; j++)
//            {
//                for (int i = 0; i < 9; i++)
//                {
//                    char c = rowPattern[j][i];
//                    Tiles[i, j].Thickness = c == '-' ? 0 : int.Parse(c.ToString());
//                }
//            }
//        }

//        public override string ToString()
//        {
//            string s = Environment.NewLine;
//            for (int j = 0; j < 9; j++)
//            {
//                for (int i = 0; i < 9; i++)
//                {
//                    s += Tiles[i, j].ColorFlag? "X" : Tiles[i, j].Thickness == 0 ? "." : Tiles[i, j].Thickness.ToString();
//                }
//                s += Environment.NewLine;
//            }
//            return s;
//        }
//    }
//}
