using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Puzzles;

[PuzzleDayNumber(04)]
public class Day04Puzzle : Puzzle
{
    public override int DayNumber => 04;
    public override string Title => "Camp Cleanup";

    public override string[] SampleInput =>
        "2-4,6-8\r\n2-3,4-5\r\n5-7,7-9\r\n2-8,3-7\r\n6-6,4-6\r\n2-6,4-8"
        .ToLines();

    public override string PartOneSampleAnswer => "2";
    public override string PartTwoSampleAnswer => "4";

    public override string PartOneActualAnswer => "595";
    public override string PartTwoActualAnswer => "952";
}
