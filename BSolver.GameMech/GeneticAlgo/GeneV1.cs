using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSolver.GameMech.GeneticAlgo
{
    public class GeneV1
    {
        public double[] Coeffs;
        public double Fitness;

        public double CoeffErased
        {
            get
            {
                return Coeffs[0];
            }
        }

        public double CoeffHole2
        {
            get
            {
                return Coeffs[1];
            }
        }
        public double CoeffHole1
        {
            get
            {
                return Coeffs[2];
            }
        }
        public double CoeffHole0
        {
            get
            {
                return Coeffs[3];
            }
        }
        public GeneV1()
        {
            Coeffs = new double[4];
            Fitness = 0;
        }
        public GeneV1(double[] coeffs)
        {
            Coeffs = new double[4];
            Coeffs = coeffs;
            Fitness = 0;
        }
        public GeneV1(string pattern)
        {
            Coeffs = new double[4];
            string[] str = pattern.Split(new char[] { ',', ':' });
            for(int i = 0; i < 4; i++)
            {
                Coeffs[i] = double.Parse(str[i]);
            }
            Fitness = double.Parse(str[4]);
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}:{4}", Coeffs[0], Coeffs[1], Coeffs[2], Coeffs[3], Fitness);
        }
    }
}
