using BSolver.GameMech.Games;
using BSolver.GameMech.Think;
using BSolver.GameMech.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSolver.GameMech.GeneticAlgo
{
    public class GeneticAlgoV1
    {
        Random random = new Random();
        List<GeneV1> population = new List<GeneV1>();
        Logger logger = new Logger(@"C:\Working\Blockudoku\BSolver\geneticResult.txt");
        const int POPULATION_COUNT_1_PERCENT = 10; // whole population is 1000
        const int MAX_FITNESS = 10000;
        //const int MAX_PARALLELISM = 2;
        PopulationFile populationFile = new PopulationFile(@"C:\Working\Blockudoku\BSolver\populationFile.txt");
        //int _parallelism = 1;

        public GeneticAlgoV1()
        {
            // try get last progress / initial seed
            List<GeneV1> initialPopulation = populationFile.ReadAllGenes();
            if (initialPopulation != null && initialPopulation.Count > 0)
            {
                population.AddRange(initialPopulation);
            }


            // the rest of the population is randomized
            while (population.Count < POPULATION_COUNT_1_PERCENT * 100)
            {
                GeneV1 gene = new GeneV1(new double[4] {
                    random.Next(0, 100),
                    -random.Next(0, 100),
                    -random.Next(0, 100),
                    -random.Next(0, 100)
                });
                NormalizeCoeffs(gene);
                population.Add(gene);
            }
            //PrintPopulation();
        }

        public void TempClean()
        {
            population.ForEach(gene => gene.Fitness = 0);

            populationFile.WriteSeparator();
            populationFile.WriteSeparator();
            populationFile.WriteSeparator();
            populationFile.WriteAllGenes(population);
        }

        //public void Run()//int parallelism)
        //{
        //    //_parallelism = parallelism;

        //    //just loop infinitely, dev to check results and terminate if needed.
        //    while (true)
        //    {
        //        populationFile.WriteSeparator();
        //        NextGeneration();
        //    }
        //}

        public void GetGenerationFitness()
        {
            populationFile.WriteSeparator();
            for (int i = 0; i < population.Count; i++)
            {
                if (population[i].Fitness == 0)
                {
                    CalcAverageFitness(population[i]);
                }
                else
                {
                    populationFile.WriteGene(population[i]);
                }
            }
        }



        public void NextGeneration()
        {
            
            //PrintPopulation();

            List<GeneV1> newOffsprings = new List<GeneV1>();

            // tournament selection - produce offsprints (30% of whole population)
            while (newOffsprings.Count < POPULATION_COUNT_1_PERCENT * 30)
            {
                // get random 10% of the population
                List<GeneV1> pool = new List<GeneV1>(population);
                List<GeneV1> selectedPopulation = new List<GeneV1>();
                for (int i = 0; i < POPULATION_COUNT_1_PERCENT * 10; i++)
                {
                    int index = random.Next(pool.Count);
                    selectedPopulation.Add(pool[index]);
                    pool.RemoveAt(index);
                }

                // get fittest two
                List<GeneV1> fittestTwo = selectedPopulation.OrderByDescending(gene => gene.Fitness).Take(2).ToList();
                GeneV1 newOffspring = GetOffspring(fittestTwo);

                // mutate, 5% chance
                if (random.NextDouble() <= 0.05)
                {
                    Mutate(newOffspring);
                }
                newOffsprings.Add(newOffspring);
            }

            // tournament selection - remove 30% unfit ones of the population
            population = population.OrderByDescending(gene => gene.Fitness).Take((int)(POPULATION_COUNT_1_PERCENT * 70)).ToList();

            // tournament selection - replacement with new population
            population.AddRange(newOffsprings);
            population.ForEach(gene => gene.Fitness = 0);

            populationFile.WriteSeparator();
            populationFile.WriteSeparator();
            populationFile.WriteSeparator();
            populationFile.WriteAllGenes(population);
        }

        private void Mutate(GeneV1 gene)
        {
            logger.WriteLog(string.Format("Before mutate: {0}", gene));

            // for one random coeff, change it by +/- 0.2
            gene.Coeffs[random.Next(4)] += (random.Next(400) / 1000.0 - 0.2);

            logger.WriteLog(string.Format("After mutate: {0}", gene));
            NormalizeCoeffs(gene);
        }

        private void NormalizeCoeffs(GeneV1 gene)
        {
            // "normalize" - the first coeff is always 1
            for (int i = 1; i < 4; i++)
            {
                gene.Coeffs[i] = gene.Coeffs[i] / gene.Coeffs[0];
            }
            gene.Coeffs[0] = 1;
        }

        private void CalcAverageFitness(GeneV1 gene)
        {
            BrainV1 brain = new BrainV1(gene.CoeffErased, gene.CoeffHole2, gene.CoeffHole1, gene.CoeffHole0);

            int totalFitness = 0;

            // simulate 100 games
            int gameCount = 100;
            logger.WriteLog(string.Format("Consider this gene for fitness: {0}", gene));

            //if (_parallelism > 1)
            //{
            //    // use parallelism to utilize all CPU cores to speed up
            //    Parallel.For(0, gameCount, new ParallelOptions { MaxDegreeOfParallelism = _parallelism }, k =>
            //    {
            //        int singleFitness = GetSingleFitness(brain);
            //        totalFitness += singleFitness;
            //        logger.WriteLog(string.Format("Got single fitness after game-{0}: {1}", k, singleFitness));
            //    });
            //}
            //else
            //{
                for (int k = 0; k < gameCount; k++)
                {
                    int singleFitness = GetSingleFitness(brain);
                    totalFitness += singleFitness;
                    logger.WriteLog(string.Format("Got single fitness after game-{0}: {1}", k, singleFitness));
                }
            //}

            // average fitness
            gene.Fitness = totalFitness * 1.0 / gameCount;
            logger.WriteLog(string.Format("Calculated average fitness: {0}", gene));
            populationFile.WriteGene(gene);
        }

        public int GetSingleFitness(BrainV1 brain, bool logging = false)
        {
            int fitness = 0;
            ClassicGame game = new ClassicGame();
            while (fitness <= MAX_FITNESS)
            {
                game.PickRandomSelectedShapes(random);
                if (logging)
                {
                    logger.WriteLog(string.Format("selected these 3: {0} {1} {2}", game.Board.SelectedShapes[0], game.Board.SelectedShapes[1], game.Board.SelectedShapes[2]));
                }
                brain.SetGame(game);
                ThinkResult result = brain.Think();
                if (result == null)
                {
                    break;
                }
                for (int i = 0; i < 3; i++)
                {
                    if (logging)
                    {
                        game.Board.PlaceShapeUiMode(result.Steps[i].Position.Item1, result.Steps[i].Position.Item2, result.Steps[i].Shape);
                        logger.WriteLog(game.Board.ToString());
                        game.Board.ClearColor();
                        int erased = game.Board.CheckForEraser();
                        if (erased > 0)
                        {
                            logger.WriteLog(game.Board.ToString());
                        }
                    }
                    else
                    {
                        game.Board.PlaceShape(result.Steps[i].Position.Item1, result.Steps[i].Position.Item2, result.Steps[i].Shape);
                    }
                }
                game.Board.SelectedShapes.Clear();
                fitness++;
            }
            return fitness;
        }

        private GeneV1 GetOffspring(List<GeneV1> parents)
        {
            double[] coeffs = new double[4];
            coeffs[0] = 1;
            for (int i = 1; i < 4; i++)
            {
                // weighted crossover
                coeffs[i] = (parents[0].Coeffs[i] * parents[0].Fitness + parents[1].Coeffs[i] * parents[1].Fitness) 
                    / (parents[0].Fitness + parents[1].Fitness);
            }
            GeneV1 offspring = new GeneV1(coeffs);
            logger.WriteLog(string.Format("[{0}] is offspring of [{1}][{2}]", offspring, parents[0], parents[1]));
            return offspring;
        }

        //private void PrintPopulation()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("population: ");
        //    foreach(GeneV1 gene in population)
        //    {
        //        sb.AppendLine(gene.ToString());
        //    }
        //    logger.WriteLog(sb.ToString());
        //}
    }
}
