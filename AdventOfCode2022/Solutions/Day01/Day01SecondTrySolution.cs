using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(01)]
public class Day01SecondTrySolution : PuzzleSolution
{
    public override int DayNumber => 01;

    class Elf
    {
        public Elf()
        {
            Calories = 0;
        }

        public long Calories { get; private set; } = 0;
        public void AddCallories(long callories)
        {
            Calories += callories;
        }
    }

    public override object SolvePartOne(string[] lines)
    {
        var Elves = CreateElves(lines);

        return Elves.OrderByDescending(x => x.Calories)
                    .First().Calories;
    }
    public override object SolvePartTwo(string[] lines)
    {
        var Elves = CreateElves(lines);


        return Elves.OrderByDescending(x => x.Calories)
                    .Take(3).
                    Sum(x => x.Calories);
    }

    private static List<Elf> CreateElves(string[] lines)
    {
        var Elves = new List<Elf>() { new Elf() };
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Length == 0)
            {
                Elves.Add(new Elf());
            }
            else
            {
                Elves[Elves.Count - 1].AddCallories(long.Parse(lines[i]));
            }
        }
        return Elves;
    }

}
