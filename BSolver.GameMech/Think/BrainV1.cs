using BSolver.GameMech.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSolver.GameMech.Think
{
    public class BrainV1
    {
        BaseGame _game;
        int _stateLimit;
        double _coeff_erased;
        double _coeff_hole2;
        double _coeff_hole1;
        double _coeff_hole0;

        public BrainV1(double coeff_erased, double coeff_hole2, double coeff_hole1, double coeff_hole0)
        {
            //_game = game;
            _stateLimit = 100000;
            _coeff_erased = coeff_erased;
            _coeff_hole2 = coeff_hole2;
            _coeff_hole1 = coeff_hole1;
            _coeff_hole0 = coeff_hole0;
        }

        public void SetGame(BaseGame game)
        {
            _game = game;
        }

        public ThinkResult Think()
        {
            List<FlatBoardState> currentStates = new List<FlatBoardState>();
            currentStates.Add(new FlatBoardState((Boards.FlatBoard)_game.Board, new ThinkResult()));
            int pendingShapeCount = _game.Board.SelectedShapes.Count;
            while (pendingShapeCount > 0)
            {
                List<FlatBoardState> nextStates = new List<FlatBoardState>();
                for(int i = 0; i < currentStates.Count; i++)
                {
                    List<FlatBoardState> newStates = new List<FlatBoardState>();
                    Get1BlockOutcomeBoardStates(currentStates[i], newStates);
                    nextStates.AddRange(newStates);
                }
                currentStates = nextStates.ToList();
                if (currentStates.Any(s => s.Erased > 0))
                {
                    currentStates = currentStates.Where(s => s.Erased > 0).ToList();
                }
                for (int i = 0; i < currentStates.Count; i++)
                {
                    currentStates[i].CalcHoles();
                    currentStates[i].Heuristic = _coeff_erased * currentStates[i].Erased
                        + _coeff_hole0 * currentStates[i].HoleCount_0window
                        + _coeff_hole1 * currentStates[i].HoleCount_1window
                        + _coeff_hole2 * currentStates[i].HoleCount_2window;
                }
 
                currentStates = currentStates.OrderByDescending(s => s.Heuristic).Take(_stateLimit).ToList();
                //currentStates = currentStates.Take(stateLimit).ToList();
                //if (pendingShapeCount == 3)
                //{
                //    var test = currentStates.Where(s => s.ThinkResult.Steps[0].Shape.Name == "t5c" 
                //    && s.ThinkResult.Steps[0].Position.Item1 == 0 &&
                //    s.ThinkResult.Steps[0].Position.Item2 == 0 
                //    ).ToList();
                //}
                //if (pendingShapeCount == 2)
                //{
                //    var test = currentStates.Where(s => s.ThinkResult.Steps[0].Shape.Name == "t5c" &&
                //    s.ThinkResult.Steps[1].Shape.Name == "l5d"
                //    && s.ThinkResult.Steps[0].Position.Item1 == 0 &&
                //    s.ThinkResult.Steps[0].Position.Item2 == 0 &&
                //    s.ThinkResult.Steps[1].Position.Item1 == 3 &&
                //    s.ThinkResult.Steps[1].Position.Item2 == 0
                //    ).ToList();
                //}
                pendingShapeCount--;
            }

            return currentStates.Count == 0? null : currentStates[0].ThinkResult;
            //GetOutcomeBoardStates(new BoardState(Board, new ThinkResult()), outcomeStates);

            ////if any have erased, filter out those without
            //if (outcomeStates.Any(o => o.Erased > 0))
            //{
            //    outcomeStates = outcomeStates.Where(o => o.Erased > 0).ToList();
            //}

            //if (cleverness == 0)
            //{
            //    return outcomeStates.OrderByDescending(o => o.Erased).FirstOrDefault().ThinkResult;
            //}

            //if (outcomeStates.Count == 1)
            //{
            //    return outcomeStates[0].ThinkResult;
            //}

            //FilterBySweepResults(outcomeStates, "o");
            //if (outcomeStates.Count == 1)
            //{
            //    return outcomeStates[0].ThinkResult;
            //}

            //if (cleverness == 1)
            //{
            //    return outcomeStates.OrderByDescending(o => o.SweepPlaces).FirstOrDefault().ThinkResult;
            //}

            //FilterBySweepResults(outcomeStates, "l5a");
            //if (outcomeStates.Count == 1)
            //{
            //    return outcomeStates[0].ThinkResult;
            //}
            //if (cleverness == 2)
            //{
            //    return outcomeStates.OrderByDescending(o => o.SweepPlaces).FirstOrDefault().ThinkResult;
            //}


            //FilterBySweepResults(outcomeStates, "i5b");
            //if (outcomeStates.Count == 1)
            //{
            //    return outcomeStates[0].ThinkResult;
            //}



            //return outcomeStates.OrderByDescending(o => o.SweepPlaces).FirstOrDefault().ThinkResult;
        }

        private void FilterBySweepResults(List<FlatBoardState> outcomeStates, string shapePattern)
        {
            Shape shape = _game.Shapes.Where(s => s.Name == "o").FirstOrDefault();
            for(int i = 0; i < outcomeStates.Count; i++)
            {

                outcomeStates[i].SweepPlaces = 0;
                outcomeStates[i].SweepErase = false;
                for (int x = 0; x < 10 - shape.Size.Item1; x++)
                {
                    for (int y = 0; y < 10 - shape.Size.Item2; y++)
                    {
                        FlatBoardState nextState = new FlatBoardState(outcomeStates[i]);
                        int result = nextState.Board.PlaceShape(x, y, shape);
                        if (result != -1)
                        {
                            nextState.SweepPlaces++;
                            if (result > 0)
                            {
                                nextState.SweepErase = true;
                            }
                        }
                    }
                }
            }
            int keepCount = (int)Math.Ceiling(outcomeStates.Count / 30.0);
            outcomeStates = outcomeStates.OrderByDescending(o => o.SweepPlaces + (o.SweepErase ? 100 : 0)).Take(keepCount).ToList();

        }

        private void Get1BlockOutcomeBoardStates(FlatBoardState currentState, List<FlatBoardState> outcomeStates)
        {
            //if (currentState.ThinkResult.Steps.Count == 2
            //                && currentState.ThinkResult.Steps[0].Shape.Name == "t5c" &&
            //        currentState.ThinkResult.Steps[1].Shape.Name == "l5d"
            //        && currentState.ThinkResult.Steps[0].Position.Item1 == 0 &&
            //        currentState.ThinkResult.Steps[0].Position.Item2 == 0 &&
            //        currentState.ThinkResult.Steps[1].Position.Item1 == 3 &&
            //        currentState.ThinkResult.Steps[1].Position.Item2 == 0
            //                )
            //{
            //    int a = 0;
            //}
            for (int i = 0; i < currentState.Board.SelectedShapes.Count; i++)
            {
                for (int x = 0; x < 10 - currentState.Board.SelectedShapes[i].Size.Item1; x++)
                {
                    for (int y = 0; y < 10 - currentState.Board.SelectedShapes[i].Size.Item2; y++)
                    {
                    //    if (currentState.ThinkResult.Steps.Count == 2
                    ////        && currentState.ThinkResult.Steps[0].Shape.Name == "t5c" &&
                    ////currentState.ThinkResult.Steps[1].Shape.Name == "l5d"
                    //&& currentState.ThinkResult.Steps[0].Position.Item1 == 0 &&
                    //currentState.ThinkResult.Steps[0].Position.Item2 == 0 &&
                    //currentState.ThinkResult.Steps[1].Position.Item1 == 0 &&
                    //currentState.ThinkResult.Steps[1].Position.Item2 == 1
                    //        && x == 0 && y == 2)
                    //    {
                    //        int a = 0;
                    //    }

                        //                        if (currentState.ThinkResult.Steps.Count == 1 && currentState.Board.SelectedShapes[i].Name == "l5d"
                        //    && currentState.ThinkResult.Steps[0].Shape.Name == "t5c" 
                        //&& currentState.ThinkResult.Steps[0].Position.Item1 == 0 &&
                        //currentState.ThinkResult.Steps[0].Position.Item2 == 0 
                        //    && x == 3 && y == 0)
                        //                        {
                        //                            int a = 0;
                        //                        }



                        FlatBoardState nextState = new FlatBoardState(currentState);
                        //int result = nextState.Board.PlaceShape(x, y, i);
                        int result = nextState.Board.PlaceShape(x, y, currentState.Board.SelectedShapes[i]);
                        if (result != -1)
                        {
                            nextState.Board.SelectedShapes.RemoveAt(i);
                            nextState.Erased += result;
                            nextState.ThinkResult.Steps.Add(new Step(currentState.Board.SelectedShapes[i], new Tuple<int, int>(x, y)));
                            outcomeStates.Add(nextState);
                        }
                    }
                }
            }
        }

        //private void GetOutcomeBoardStates(BoardState currentState, List<BoardState> outcomeStates)
        //{
        //    if (currentState.Board.SelectedShapes.Count == 0)
        //    {
        //        outcomeStates.Add(currentState);
        //        return;
        //    }
        //    if (outcomeStates.Count > 100000)
        //    {
        //        // i've had enough!
        //        return;
        //    }
        //    for (int i = 0; i < currentState.Board.SelectedShapes.Count; i++)
        //    {
        //        for (int x = 0; x < 10 - currentState.Board.SelectedShapes[i].Size.Item1; x++)
        //        {
        //            for (int y = 0; y < 10 - currentState.Board.SelectedShapes[i].Size.Item2; y++)
        //            {
        //                BoardState nextState = new BoardState(currentState);
        //                int result = nextState.Board.PlaceShape(x, y, i);
        //                if (result != -1)
        //                {
        //                    nextState.Erased += result;
        //                    nextState.ThinkResult.Steps.Add(new Step(currentState.Board.SelectedShapes[i], new Tuple<int, int>(x, y)));
        //                    GetOutcomeBoardStates(nextState, outcomeStates);
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
