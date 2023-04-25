using BSolver.GameMech.GeneticAlgo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSolver.GameMech.Utils
{
    public class PopulationFile
    {
        private string _filePath;

        public PopulationFile(string filePath)
        {
            _filePath = filePath;
        }

        public List<GeneV1> ReadAllGenes()
        {
            if (File.Exists(_filePath))
            {
                string[] lines = File.ReadAllLines(_filePath);
                List<GeneV1> population = new List<GeneV1>();
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("---"))
                    {
                        population.Clear();
                    }
                    else
                    {
                        population.Add(new GeneV1(lines[i]));
                    }
                }
                return population;
            }
            else
            {
                return null;
            }
        }

        public void WriteGene(GeneV1 gene)
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "");
            }
            File.AppendAllText(_filePath, string.Format("{0}{1}", gene.ToString(), Environment.NewLine));
        }

        public void WriteAllGenes(List<GeneV1> genes)
        {
            genes.ForEach(g => WriteGene(g));
        }

        public void WriteSeparator()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "");
            }
            File.AppendAllText(_filePath, string.Format("{0}{1}", new string('-', 30), Environment.NewLine));
        }

        public void Clear()
        {
            File.WriteAllText(_filePath, "");
        }
    }
}
