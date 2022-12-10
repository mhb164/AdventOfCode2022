using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022;

public abstract class PuzzleSolution
{
    public abstract int DayNumber { get; }
    public abstract object SolvePartOne(string[] lines);
    public abstract object SolvePartTwo(string[] lines);
}
