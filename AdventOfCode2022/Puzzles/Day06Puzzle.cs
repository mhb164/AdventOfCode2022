using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Puzzles;

[PuzzleDayNumber(06)]
public class Day06Puzzle : Puzzle
{
    public override int DayNumber => 06;
    public override string Title => "Tuning Trouble";

    public override string[] SampleInput =>
        "mjqjpqmgbljsphdztnvjfqwrcgsmlb"
        .ToLines();

    public override string PartOneSampleAnswer => "7";
    public override string PartTwoSampleAnswer => "19";

    public override string PartOneActualAnswer => "1912";
    public override string PartTwoActualAnswer => "2122";
}
