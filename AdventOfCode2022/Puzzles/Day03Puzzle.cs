using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Puzzles;

[PuzzleDayNumber(03)]
public class Day03Puzzle : Puzzle
{
    public override int DayNumber => 03;
    public override string Title => "Rucksack Reorganization";

    public override string[] SampleInput =>
        "vJrwpWtwJgWrhcsFMMfFFhFp\r\njqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL\r\nPmmdzqPrVvPwwTWBwg\r\nwMqvLMZHhHMvwLHjbvcjnnSBnvTQFn\r\nttgJtRGJQctTZtZT\r\nCrZsJsPPZsGzwwsLwLmpwMDw"
        .ToLines();

    public override string PartOneSampleAnswer => "157";
    public override string PartTwoSampleAnswer => "70";

    public override string PartOneActualAnswer => "8233";
    public override string PartTwoActualAnswer => "2821";
}
