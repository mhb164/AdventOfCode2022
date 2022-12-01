using AdventOfCode2022.Puzzles;
using AdventOfCode2022.Solutions;
using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022;

[MemoryDiagnoser]
public class Day01Benchmark
{
    [GlobalSetup]
    public void GlobalSetup()
    {
        Original = new SolutionPerformer<Day01Puzzle, Day01FirstTrySolution>(null);
        SecondTry = new SolutionPerformer<Day01Puzzle, Day01SecondTrySolution>(null);
    }

    public ISolutionPerformer Original;
    public ISolutionPerformer SecondTry;

    [Benchmark]
    public string OriginalPartOne() => Original.Solution.SolvePartOne(Original.ActualInput);

    [Benchmark]
    public string SecondTryPartOne() => SecondTry.Solution.SolvePartOne(SecondTry.ActualInput);


    [Benchmark]
    public void OriginalPartTwo() => Original.Solution.SolvePartTwo(Original.ActualInput);

    [Benchmark]
    public void SecondTryPartTwo() => SecondTry.Solution.SolvePartTwo(SecondTry.ActualInput);
}
