using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Puzzles;

[PuzzleDayNumber(05)]
public class Day05Puzzle : Puzzle
{
    public override int DayNumber => 05;
    public override string Title => "Supply Stacks";

    public override string[] SampleInput =>
        "    [D]    \r\n[N] [C]    \r\n[Z] [M] [P]\r\n 1   2   3 \r\n\r\nmove 1 from 2 to 1\r\nmove 3 from 1 to 3\r\nmove 2 from 2 to 1\r\nmove 1 from 1 to 2"
        .ToLines();

    public override string PartOneSampleAnswer => "CMZ";
    public override string PartTwoSampleAnswer => "MCD";

    public override string PartOneActualAnswer => "QMBMJDFTD";
    public override string PartTwoActualAnswer => "NBTVTJNFJ";
}
