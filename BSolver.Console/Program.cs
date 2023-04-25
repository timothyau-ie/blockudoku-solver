using BSolver.GameMech.Boards;
using BSolver.GameMech.Games;
using BSolver.GameMech.GeneticAlgo;
using BSolver.GameMech.Think;
using BSolver.GameMech.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSolver.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.Write("Run mode (F=Fitness,N=NextGen)? ");
            string modeStr = System.Console.ReadLine();

            FlatBoard.Init();

            GeneticAlgoV1 algo = new GeneticAlgoV1();
            if (modeStr == "F")
            {
                algo.GetGenerationFitness();
            }
            else if (modeStr == "N")
            {
                algo.NextGeneration();
            }
            //else
            //{
            //    algo.TempClean();
            //}


            //TestSingleFitness
        }


        private static void TestSingleFitness()
        {
            GeneticAlgoV1 algo = new GeneticAlgoV1();
            BrainV1 brain = new BrainV1(10, -1, -2, -5);
            int fitness = algo.GetSingleFitness(brain, true);
            System.Console.WriteLine(fitness.ToString());
            System.Console.ReadLine();
        }
    }
}
