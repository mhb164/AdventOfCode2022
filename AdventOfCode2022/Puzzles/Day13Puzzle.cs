using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Puzzles;

[PuzzleDayNumber(13)]
public class Day13Puzzle : Puzzle
{
    public override int DayNumber => 13;
    public override string Title => "Distress Signal";

    public override string[] SampleInput =>
        "[1,1,3,1,1]\r\n[1,1,5,1,1]\r\n\r\n[[1],[2,3,4]]\r\n[[1],4]\r\n\r\n[9]\r\n[[8,7,6]]\r\n\r\n[[4,4],4,4]\r\n[[4,4],4,4,4]\r\n\r\n[7,7,7,7]\r\n[7,7,7]\r\n\r\n[]\r\n[3]\r\n\r\n[[[]]]\r\n[[]]\r\n\r\n[1,[2,[3,[4,[5,6,7]]]],8,9]\r\n[1,[2,[3,[4,[5,6,0]]]],8,9]"
        .ToLines();

    public override string PartOneSampleAnswer => "13";
    public override string PartTwoSampleAnswer => "140";

    public override string PartOneActualAnswer => "6428";
    public override string PartTwoActualAnswer => "22464";
}
