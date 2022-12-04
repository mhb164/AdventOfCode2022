# AdventOfCode2022
This repository is a platform for solving [Advent Of Code 2022](https://adventofcode.com/2022/) puzzles and contains: 
1. *Core:* A library helps to manage and perform solutions (solving, testing and adding extra solutions)
   - **Puzzle** class: Base class for puzzles (Properties: DayNumber, SampleInput and each part answers)
   - **PuzzleSolution** class: Base class for solutions (contains 'SolvePartOne' and 'SolvePartTwo' method)
   - **SolutionPerformer** class: A generic class for performing tests and solutions
   - **Puzzles** folder 
   - **Solutions** folder 
   - **ActualInputs** folder
2. *Console:* Simple console application for running tests and solutions.
3. *Benchmark:* To benchmark several solutions (My favorite part :star_struck:).

**First try**: I just make it ***Work***

**Other tries**: I make ***Right*** and ***Fast***

## [Day 01](https://adventofcode.com/2022/day/1) (Benchmark of [2 solutions](https://github.com/mhb164/AdventOfCode2022/tree/main/AdventOfCode2022/Solutions/Day01))

|           Method |     Mean |   Gen0 | Allocated |
|----------------- |---------:|-------:|----------:|  
| Part1 first try  | 49.73 us | 0.9766 |   4.23 KB |
| Part1 second try | 45.96 us | 2.3804 |   9.96 KB | 
| Part2 first try  | 50.75 us | 0.9766 |    4.2 KB |
| Part2 second try | 45.66 us | 3.6011 |   14.8 KB |

## [Day 02](https://adventofcode.com/2022/day/2) ([2 solutions](https://github.com/mhb164/AdventOfCode2022/tree/main/AdventOfCode2022/Solutions/Day02))
* I used a tricky way in the part2, by a method to determine what shape may lead to the specific round outcome.
* In the second try, the domain language improved by using [OOP](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/oop), [Enum](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum), [switch expression](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression) and using [in parameter modifier](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/in-parameter-modifier) to increase performance 
  
|           Method |     Mean | Allocated |
|----------------- |---------:|----------:|
|  Part1 first try | 44.62 us |      32 B |
| Part1 second try | 42.65 us |      56 B |
|  Part2 first try | 48.76 us |      32 B |
| Part2 second try | 55.34 us |      56 B |

## [Day 03](https://adventofcode.com/2022/day/3) ([2 solutions](https://github.com/mhb164/AdventOfCode2022/tree/main/AdventOfCode2022/Solutions/Day03))
* There is a huge allocation in the first try, so [Span](https://learn.microsoft.com/en-us/dotnet/api/system.span-1) came and magic happened.
* Some of developers offered [Intersect](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.intersect), but in this case a simple loop has better pereformance.
* The full atomic benchmarks will be found in [My Benchmarks Repo #7](https://github.com/mhb164/Benchmarks)

|           Method |     Mean |   Gen0 | Allocated |
|----------------- |---------:|-------:|----------:|
|  Part1 first try | 34.46 us | 8.1787 |   34448 B |
| Part1 second try | 25.28 us |      - |      32 B |
|  Part2 first try | 24.39 us |      - |      32 B |
| Part2 second try | 25.17 us |      - |      32 B |