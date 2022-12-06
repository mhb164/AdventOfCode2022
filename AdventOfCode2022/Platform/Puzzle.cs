using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public abstract class Puzzle
    {
        public abstract int DayNumber { get; }
        public abstract string Title { get; }

        public abstract string[] SampleInput { get; }
        public abstract string PartOneSampleAnswer { get; }
        public abstract string PartTwoSampleAnswer { get; }

        public abstract string PartOneActualAnswer { get; }
        public abstract string PartTwoActualAnswer { get; }

    }

}
