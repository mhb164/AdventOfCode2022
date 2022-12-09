using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Puzzles;

[PuzzleDayNumber(09)]
public class Day09Puzzle : Puzzle
{
    public override int DayNumber => 09;
    public override string Title => "Rope Bridge";

    public override string[] SampleInput =>
        "R 4\r\nU 4\r\nL 3\r\nD 1\r\nR 4\r\nD 1\r\nL 5\r\nR 2"
        .ToLines();

    //public override string[] SampleInput =>
    //    "R 5\r\nU 8\r\nL 8\r\nD 3\r\nR 17\r\nD 10\r\nL 25\r\nU 20"
    //    .ToLines();

    public override string PartOneSampleAnswer => "13";
    public override string PartTwoSampleAnswer => "1";
    //public override string PartTwoSampleAnswer => "36";

    public override string PartOneActualAnswer => "5710";
    public override string PartTwoActualAnswer => "2259";
}
