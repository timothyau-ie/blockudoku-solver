//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BSolver.GameMech.Boards;

//namespace BSolver.GameMech.Think
//{
//    public class BoardState
//    {
//        public Board Board;
//        public ThinkResult ThinkResult;
//        public int Erased;
//        public int HoleCount_2window;
//        public int HoleCount_1window;
//        public int HoleCount_0window;

//        public double Heuristic;
        


//        public int SweepPlaces;
//        public bool SweepErase;

//        public BoardState(Board board, ThinkResult result)
//        {
//            Board = board;
//            ThinkResult = result;
//            Erased = 0;
//            HoleCount_2window = 0;
//            HoleCount_1window = 0;
//            HoleCount_0window = 0;
//            SweepPlaces = 0;
//            SweepErase = false;
//        }

//        public BoardState(BoardState state)
//        {
//            Board = new Board(state.Board);
//            ThinkResult = new ThinkResult(state.ThinkResult);
//            Erased = state.Erased;
//            SweepPlaces = state.SweepPlaces;
//            SweepErase = state.SweepErase;
//            HoleCount_2window = state.HoleCount_2window;
//            HoleCount_1window = state.HoleCount_1window;
//            HoleCount_0window = state.HoleCount_0window;
//        }

//        public override string ToString()
//        {
//            return Board.ToString();
//        }

//        public void CalcHoles()
//        {
//            HoleCount_2window = 0;
//            HoleCount_1window = 0;
//            HoleCount_0window = 0;
//            for (int i = 0; i < 9; i++)
//            {
//                for(int j = 0; j < 9; j++)
//                {
//                    if (Board.Tiles[i,j].Thickness == 0)
//                    {
//                        int windowCount = 0;
//                        // check left
//                        if (i > 0 && Board.Tiles[i - 1, j].Thickness == 0)
//                        {
//                            windowCount++;
//                        }
//                        // check right
//                        if (i < 8 && Board.Tiles[i + 1, j].Thickness == 0)
//                        {
//                            windowCount++;
//                        }
//                        // check up
//                        if (windowCount < 2 && j > 0 && Board.Tiles[i, j - 1].Thickness == 0)
//                        {
//                            windowCount++;
//                        }
//                        // check down
//                        if (windowCount < 2 && j < 8 && Board.Tiles[i, j + 1].Thickness == 0)
//                        {
//                            windowCount++;
//                        }
//                        if (windowCount == 2)
//                        {
//                            HoleCount_2window++;
//                        }
//                        else if (windowCount == 1)
//                        {
//                            HoleCount_1window++;
//                        }
//                        else
//                        {
//                            HoleCount_0window++;
//                        }
//                    }
//                }
//            }
//        }

//    }
//}
