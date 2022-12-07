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

## [Day 01: Calorie Counting](https://adventofcode.com/2022/day/1) (Benchmark of [2 solutions](https://github.com/mhb164/AdventOfCode2022/tree/main/AdventOfCode2022/Solutions/Day01))

|           Method |     Mean |   Gen0 | Allocated |
|----------------- |---------:|-------:|----------:|  
| Part1 first try  | 49.73 us | 0.9766 |   4.23 KB |
| Part1 second try | 45.96 us | 2.3804 |   9.96 KB | 
| Part2 first try  | 50.75 us | 0.9766 |    4.2 KB |
| Part2 second try | 45.66 us | 3.6011 |   14.8 KB |

## [Day 02: Rock Paper Scissors](https://adventofcode.com/2022/day/2) ([2 solutions](https://github.com/mhb164/AdventOfCode2022/tree/main/AdventOfCode2022/Solutions/Day02))
* I used a tricky way in the part2, by a method to determine what shape may lead to the specific round outcome.
* In the second try, the puzzle language improved by [OOP](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/oop), [Enum](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum), [switch expression](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression) and [in parameter modifier](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/in-parameter-modifier) to increase performance 
  
|           Method |     Mean | Allocated |
|----------------- |---------:|----------:|
|  Part1 first try | 44.62 us |      32 B |
| Part1 second try | 42.65 us |      56 B |
|  Part2 first try | 48.76 us |      32 B |
| Part2 second try | 55.34 us |      56 B |

## [Day 03: Rucksack Reorganization](https://adventofcode.com/2022/day/3) ([2 solutions](https://github.com/mhb164/AdventOfCode2022/tree/main/AdventOfCode2022/Solutions/Day03))
* There is a huge allocation in the first try, so [Span](https://learn.microsoft.com/en-us/dotnet/api/system.span-1) came and magic happened.
* Some of developers offered [Intersect](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.intersect), but in this case a simple loop has better performance.
* The full atomic benchmarks will be found in [My Benchmarks Repo #7](https://github.com/mhb164/Benchmarks/blob/main/_07_AdventOfCodeDay03AtomicBenchmark.cs)

|           Method |     Mean |   Gen0 | Allocated |
|----------------- |---------:|-------:|----------:|
|  Part1 first try | 34.46 us | 8.1787 |   34448 B |
| Part1 second try | 25.28 us |      - |      32 B |
|  Part2 first try | 24.39 us |      - |      32 B |
| Part2 second try | 25.17 us |      - |      32 B |

## [Day 04: Camp Cleanup](https://adventofcode.com/2022/day/4) ([2 solutions](https://github.com/mhb164/AdventOfCode2022/tree/main/AdventOfCode2022/Solutions/Day04))
* First try was a mess! But it's work :sweat_smile:.
* On the second try, each line works could be done separately, so I used [Parallel.ForEach](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.parallel.foreach). I should have dealt with concurrency, thus [Interlocked](https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked) came to the game.
* [Span](https://learn.microsoft.com/en-us/dotnet/api/system.span-1) still has its magic!
* Benchmark results were awesome, the second try is brilliant. 

|           Method |        Mean |     Gen0 |  Allocated |
|----------------- |------------:|---------:|-----------:|
|  Part1 first try | 2,170.16 us | 664.0625 | 2714.34 KB |
| Part1 second try |    54.59 us |   0.8545 |    3.47 KB |
|  Part2 first try | 1,827.28 us | 664.0625 | 2714.34 KB |
| Part2 second try |    61.38 us |   0.7935 |     3.4 KB |

## [Day 05: Supply Stacks](https://adventofcode.com/2022/day/5) ([3 solutions](https://github.com/mhb164/AdventOfCode2022/tree/main/AdventOfCode2022/Solutions/Day05))
* I think the challenge was reading stack from input :grin:
* On the second try, the puzzle language and performance improved.
* On the third try, the solution was redeveloped ([object-oriented](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/oop)).

|           Method |      Mean |    Gen0 | Allocated |
|----------------- |----------:|--------:|----------:|
|  Part1 first try | 200.20 us | 31.4941 | 129.52 KB |
| Part1 second try |  55.27 us |  1.0376 |   4.38 KB |
|  Part1 third try |  54.01 us |  1.0986 |   4.67 KB |
|  Part2 first try | 232.25 us | 42.4805 | 173.58 KB |
| Part2 second try |  63.44 us |  5.4932 |  22.53 KB |
|  Part2 third try |  59.39 us |  5.4932 |  22.83 KB |

## [Day 06: Tuning Trouble](https://adventofcode.com/2022/day/6) ([3 solutions](https://github.com/mhb164/AdventOfCode2022/tree/main/AdventOfCode2022/Solutions/Day06))
* [Span](https://learn.microsoft.com/en-us/dotnet/api/system.span-1) removes unnecessary allocations.
* On the first try, 'HasDuplicate' method wreckس all ***Span*** efforts.
* On the second try, 'HasDuplicate' method rewrote by nested for loop and results are pleasing. 
* On the third try, I used [HashSet](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1) to find duplicates.
* The full atomic benchmarks will be found in [My Benchmarks Repo #8](https://github.com/mhb164/Benchmarks/blob/main/_08_AdventOfCodeDay06AtomicBenchmark.cs)	

|           Method |      Mean |     Gen0 | Allocated |
|----------------- |----------:|---------:|----------:|
|  Part1 first try | 351.67 us | 164.0625 |  687480 B |
| Part1 second try |  14.52 us |        - |      32 B |
|  Part1 third try |  90.91 us |        - |     264 B |
|  Part2 first try | 792.21 us | 274.4141 | 1148672 B |
| Part2 second try |  28.85 us |        - |      32 B |
|  Part2 third try | 105.22 us |        - |     424 B |

## [Day 07: No Space Left On Device](https://adventofcode.com/2022/day/7) ([1 solutions](https://github.com/mhb164/AdventOfCode2022/tree/main/AdventOfCode2022/Solutions/Day07))


###### Day 08: comming soon...
###### Day 09: comming soon...
###### Day 10: comming soon...
###### Day 11: comming soon...
###### Day 12: comming soon...
###### Day 13: comming soon...
###### Day 14: comming soon...
###### Day 15: comming soon...
###### Day 16: comming soon...
###### Day 17: comming soon...
###### Day 18: comming soon...
###### Day 19: comming soon...
###### Day 20: comming soon...
###### Day 21: comming soon...
###### Day 22: comming soon...
###### Day 23: comming soon...
###### Day 24: comming soon...
###### Day 25: comming soon...
