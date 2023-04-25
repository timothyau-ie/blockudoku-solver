using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSolver.GameMech.Think
{
    public class ThinkResult
    {
        public List<Step> Steps;

        public ThinkResult()
        {
            Steps = new List<Step>();
        }

        public ThinkResult(ThinkResult result)
        {
            Steps = new List<Step>();
            for(int i = 0; i < result.Steps.Count; i++)
            {
                Steps.Add(new Step(result.Steps[i]));
            }
        }
    }
}
