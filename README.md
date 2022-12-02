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

|           Method |     Mean |    Error |   StdDev |   Gen0 | Allocated |
|----------------- |---------:|---------:|---------:|-------:|----------:|
|  FirstTryPartOne | 49.73 us | 0.440 us | 0.367 us | 0.9766 |   4.23 KB |
| SecondTryPartOne | 45.96 us | 0.731 us | 0.648 us | 2.3804 |   9.96 KB |
|  FirstTryPartTwo | 50.75 us | 1.007 us | 1.445 us | 0.9766 |    4.2 KB |
| SecondTryPartTwo | 45.66 us | 0.639 us | 0.567 us | 3.6011 |   14.8 KB |
