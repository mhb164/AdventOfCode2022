using AdventOfCode2022.Puzzles;
using AdventOfCode2022.Solutions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022;

[MemoryDiagnoser(true), Config(typeof(Config))]
public class Day04Benchmark
{
    private class Config : ManualConfig
    {
        public Config()
        {
            HideColumns("Error", "StdDev", "Median");
        }
    }

    [GlobalSetup]
    public void GlobalSetup()
    {
        var firstTryPerformer = new SolutionPerformer<Day04Puzzle, Day04FirstTrySolution>(null);
        First = firstTryPerformer.Solution;
        ActualInput = firstTryPerformer.ActualInput;
        Second = new SolutionPerformer<Day04Puzzle, Day04SecondTrySolution>(null).Solution;
    }

    public PuzzleSolution First;
    public PuzzleSolution Second;
    public string[] ActualInput;

    [Benchmark(Description = "Day04 Part1 First")]
    public void PartOneFirstTry()
    {
        _ = First.SolvePartOne(ActualInput);
    }

    [Benchmark(Description = "Day04 Part1 Second")]
    public void PartOneSecondTry()
    {
        _ = Second.SolvePartOne(ActualInput);
    }

    [Benchmark(Description = "Day04 Part2 First")]
    public void PartTwoFirstTry()
    {
        _ = First.SolvePartTwo(ActualInput);
    }

    [Benchmark(Description = "Day04 Part2 Second")]
    public void PartTwoSecondTry()
    {
        _ = Second.SolvePartTwo(ActualInput);
    }
}
