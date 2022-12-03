using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Solutions;

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

    public override string SolvePartOne(in string[] inputLines)
    {
        var Elves = CreateElves(in inputLines);

        return Elves.OrderByDescending(x => x.Calories)
                    .First().Calories
                    .ToString();
    }
    public override string SolvePartTwo(in string[] inputLines)
    {
        var Elves = CreateElves(in inputLines);


        return Elves.OrderByDescending(x => x.Calories)
                    .Take(3).
                    Sum(x => x.Calories)
                    .ToString();
    }

    private static List<Elf> CreateElves(in string[] inputLines)
    {
        var Elves = new List<Elf>() { new Elf() };
        for (int i = 0; i < inputLines.Length; i++)
        {
            if (inputLines[i].Length == 0)
            {
                Elves.Add(new Elf());
            }
            else
            {
                Elves[Elves.Count - 1].AddCallories(long.Parse(inputLines[i]));
            }
        }
        return Elves;
    }

}
