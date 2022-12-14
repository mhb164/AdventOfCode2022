using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Puzzles;

[PuzzleDayNumber(14)]
public class Day14Puzzle : Puzzle
{
    public override int DayNumber => 14;
    public override string Title => "Regolith Reservoir";

    public override string[] SampleInput =>
        "498,4 -> 498,6 -> 496,6\r\n503,4 -> 502,4 -> 502,9 -> 494,9"
        .ToLines();

    public override string PartOneSampleAnswer => "24";
    public override string PartTwoSampleAnswer => "93";

    public override string PartOneActualAnswer => "1016";
    public override string PartTwoActualAnswer => "25402";
}
