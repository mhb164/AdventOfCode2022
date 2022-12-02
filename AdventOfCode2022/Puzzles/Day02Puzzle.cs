using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Puzzles;

public class Day02Puzzle : Puzzle
{
    public override int DayNumber => 02;
    public override string[] SampleInput =>
        "A Y\r\nB X\r\nC Z"
        .ToLines();

    public override string PartOneSampleAnswer => "15";
    public override string PartTwoSampleAnswer => "12";

    public override string PartOneActualAnswer => "13809";
    public override string PartTwoActualAnswer => "12316";
}
