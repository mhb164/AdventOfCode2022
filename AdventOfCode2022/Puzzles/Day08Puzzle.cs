using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Puzzles;

[PuzzleDayNumber(08)]
public class Day08Puzzle : Puzzle
{
    public override int DayNumber => 08;
    public override string Title => "";

    public override string[] SampleInput =>
        "30373\r\n25512\r\n65332\r\n33549\r\n35390"
        .ToLines();

    public override string PartOneSampleAnswer => "21";
    public override string PartTwoSampleAnswer => "8";

    public override string PartOneActualAnswer => "1798";
    public override string PartTwoActualAnswer => "259308";
}
