using BenchmarkDotNet.Running;

namespace AdventOfCode2022;

internal class Program
{
    static void Main(string[] args)
    {
        //BenchmarkRunner.Run<Day01Benchmark>();
        //BenchmarkRunner.Run<Day02Benchmark>();
        BenchmarkRunner.Run<Day03Benchmark>();

    }
}