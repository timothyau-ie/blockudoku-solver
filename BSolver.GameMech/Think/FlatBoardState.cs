using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSolver.GameMech.Boards;

namespace BSolver.GameMech.Think
{
    public class FlatBoardState
    {
        public FlatBoard Board;
        public ThinkResult ThinkResult;
        public int Erased;
        public int HoleCount_2window;
        public int HoleCount_1window;
        public int HoleCount_0window;

        public double Heuristic;
        


        public int SweepPlaces;
        public bool SweepErase;

        const int SINGLE_BIT = 0b_0000_0001_0000_0000;

        public FlatBoardState(FlatBoard board, ThinkResult result)
        {
            Board = board;
            ThinkResult = result;
            Erased = 0;
            HoleCount_2window = 0;
            HoleCount_1window = 0;
            HoleCount_0window = 0;
            SweepPlaces = 0;
            SweepErase = false;
        }

        public FlatBoardState(FlatBoardState state)
        {
            Board = new FlatBoard(state.Board);
            ThinkResult = new ThinkResult(state.ThinkResult);
            Erased = state.Erased;
            SweepPlaces = state.SweepPlaces;
            SweepErase = state.SweepErase;
            HoleCount_2window = state.HoleCount_2window;
            HoleCount_1window = state.HoleCount_1window;
            HoleCount_0window = state.HoleCount_0window;
        }

        public override string ToString()
        {
            return Board.ToString();
        }

        public void CalcHoles()
        {
            HoleCount_2window = 0;
            HoleCount_1window = 0;
            HoleCount_0window = 0;
            for (int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if ((Board.RowBits[j] & (SINGLE_BIT >> i)) == 0)
                    {
                        int windowCount = 0;
                        // check left
                        if (i > 0 && ((Board.RowBits[j] & (SINGLE_BIT >> (i - 1))) == 0))
                        {
                            windowCount++;
                        }
                        // check right
                        if (i < 8 && ((Board.RowBits[j] & (SINGLE_BIT >> (i + 1))) == 0))
                        {
                            windowCount++;
                        }
                        // check up
                        if (windowCount < 2 && j > 0 && ((Board.RowBits[j - 1] & (SINGLE_BIT >> i)) == 0))
                        {
                            windowCount++;
                        }
                        // check down
                        if (windowCount < 2 && j < 8 && ((Board.RowBits[j + 1] & (SINGLE_BIT >> i)) == 0))
                        {
                            windowCount++;
                        }
                        if (windowCount == 2)
                        {
                            HoleCount_2window++;
                        }
                        else if (windowCount == 1)
                        {
                            HoleCount_1window++;
                        }
                        else
                        {
                            HoleCount_0window++;
                        }
                    }
                }
            }
        }

    }
}
