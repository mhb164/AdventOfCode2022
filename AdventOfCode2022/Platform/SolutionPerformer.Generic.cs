using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public sealed class SolutionPerformer<TPuzzle, TPuzzleSolution> : SolutionPerformer
          where TPuzzle : Puzzle
          where TPuzzleSolution : PuzzleSolution
    {
        public SolutionPerformer(ILogger logger)
            : base(logger,
                  Activator.CreateInstance(typeof(TPuzzle)) as Puzzle,
                  Activator.CreateInstance(typeof(TPuzzleSolution)) as PuzzleSolution)
        {
        }

    }
}
