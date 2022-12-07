using System.Drawing;

namespace AdventOfCode2022.Solutions;

[PuzzleDayNumber(07)]
public class Day07FirstTrySolution : PuzzleSolution
{
    public override int DayNumber => 07;

    class directory
    {
        public directory Parent { get; set; }
        public string Name { get; set; }
        public List<directory> directories { get; set; } = new List<directory>();
        public List<file> files { get; set; } = new List<file>();
        public override string ToString() => $"{Name} {GetSize()}";

        public string GetFullname()
        {
            if (Parent != null) return $"{Parent.GetFullname()}\\{Name}";
            else return Name;
        }

        public int GetSize()
        {
            var size = 0;
            if (directories.Count > 0)
                size = directories.Sum(x => x.GetSize());
            if (files.Count > 0)
                size += files.Sum(x => x.Size);
            return size;
        }
    }

    class file
    {
        public directory Parent { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }

        public override string ToString() => $"{Name} {Size}";

    }

    public override string SolvePartOne(in string[] inputLines)
    {
        var rootDirectory = new directory() { Parent = null, Name = "root" };
        var allDirectories = new Dictionary<string, directory>();
        allDirectories.Add(rootDirectory.GetFullname(), rootDirectory);
        var currecntDirectory = rootDirectory;
        foreach (var inputLine in inputLines)
        //for (int i = 0; i < inputLines.Length; i++)
        {
            if (inputLine.StartsWith("$"))
            {
                if (inputLine.StartsWith("$ cd /"))
                {
                    currecntDirectory = rootDirectory;
                }
                else if (inputLine.StartsWith("$ ls"))
                {
                    continue;
                }
                else if (inputLine.StartsWith("$ cd .."))
                {
                    currecntDirectory = currecntDirectory?.Parent ?? rootDirectory;
                }
                else if (inputLine.StartsWith("$ cd"))
                {
                    var dirName = inputLine.Replace("$ cd", "").Trim();
                    var dir = currecntDirectory?.directories.FirstOrDefault(d => d.Name == dirName);
                    if (dir != null) { currecntDirectory = dir; }
                    else
                    {

                    }
                }
            }
            else if (inputLine.StartsWith("dir"))
            {
                if (currecntDirectory != null)
                {
                    var dirName = inputLine.Replace("dir", "").Trim();
                    var existed = currecntDirectory.directories.FirstOrDefault(x => x.Name == dirName);
                    if (existed == null)
                    {
                        var newdirectory = new directory() { Parent = currecntDirectory, Name = dirName };
                        currecntDirectory.directories.Add(newdirectory);
                        allDirectories.Add(newdirectory.GetFullname(), newdirectory);
                    }
                }
            }
            else
            {
                var splitted = inputLine.Split(" ");
                if (currecntDirectory != null && int.TryParse(splitted[0], out var size))
                {
                    var filename = splitted[1];
                    var existedfile = currecntDirectory.files.FirstOrDefault(f => f.Name == filename);
                    if (existedfile == null)
                    {
                        currecntDirectory.files.Add(new file()
                        {
                            Parent = currecntDirectory,
                            Name = filename,
                            Size = size
                        });
                    }
                }
            }
        }

        var totals = new Dictionary<HashSet<string>, int>();
        var all = allDirectories.Values.ToArray();
        var max = 100000;
        for (int i = 0; i < all.Length; i++)
        {
            var main = all[i];
            if (main.GetSize() > max) continue;
            var totalkey = new HashSet<string>();
            totalkey.Add(main.GetFullname());
            var totalSize = main.GetSize();

            for (int j = 0; j < all.Length; j++)
            {
                if (i == j) continue;
                var ccc = all[j];
                if (totalSize + ccc.GetSize() > max) continue;

                totalkey.Add(ccc.GetFullname());

                //if (main.GetFullname().Contains(ccc.GetFullname())) continue;
                //if (ccc.GetFullname().Contains(main.GetFullname())) continue;

                totalSize += ccc.GetSize();
            }

            bool totalkeyExisted = false;
            foreach (var item in totals.Keys)
            {
                if (item.Count != totalkey.Count) break;
                var matchnameCount = 0;
                foreach (var fullname in item)
                {
                    if (totalkey.Contains(fullname))
                        matchnameCount++;
                }

                if (matchnameCount == totalkey.Count)
                {
                    totalkeyExisted = true;
                    break;
                }
            }
            if (!totalkeyExisted)
                totals.Add(totalkey, totalSize);
        }

        var temp = new Dictionary<string, directory>();
        foreach (var total in totals)
            foreach (var item in total.Key)
            {
                if (temp.ContainsKey(item)) continue;
                temp.Add(item, allDirectories[item]);
            }
        var maxTotal = temp.Values.Sum(x => x.GetSize());

        return maxTotal.ToString();
    }

    public override string SolvePartTwo(in string[] inputLines)
    {
        var rootDirectory = new directory() { Parent = null, Name = "root" };
        var allDirectories = new Dictionary<string, directory>();
        allDirectories.Add(rootDirectory.GetFullname(), rootDirectory);
        var currecntDirectory = rootDirectory;
        foreach (var inputLine in inputLines)
        //for (int i = 0; i < inputLines.Length; i++)
        {
            if (inputLine.StartsWith("$"))
            {
                if (inputLine.StartsWith("$ cd /"))
                {
                    currecntDirectory = rootDirectory;
                }
                else if (inputLine.StartsWith("$ ls"))
                {
                    continue;
                }
                else if (inputLine.StartsWith("$ cd .."))
                {
                    currecntDirectory = currecntDirectory?.Parent ?? rootDirectory;
                }
                else if (inputLine.StartsWith("$ cd"))
                {
                    var dirName = inputLine.Replace("$ cd", "").Trim();
                    var dir = currecntDirectory?.directories.FirstOrDefault(d => d.Name == dirName);
                    if (dir != null) { currecntDirectory = dir; }
                    else
                    {

                    }
                }
            }
            else if (inputLine.StartsWith("dir"))
            {
                if (currecntDirectory != null)
                {
                    var dirName = inputLine.Replace("dir", "").Trim();
                    var existed = currecntDirectory.directories.FirstOrDefault(x => x.Name == dirName);
                    if (existed == null)
                    {
                        var newdirectory = new directory() { Parent = currecntDirectory, Name = dirName };
                        currecntDirectory.directories.Add(newdirectory);
                        allDirectories.Add(newdirectory.GetFullname(), newdirectory);
                    }
                }
            }
            else
            {
                var splitted = inputLine.Split(" ");
                if (currecntDirectory != null && int.TryParse(splitted[0], out var size))
                {
                    var filename = splitted[1];
                    var existedfile = currecntDirectory.files.FirstOrDefault(f => f.Name == filename);
                    if (existedfile == null)
                    {
                        currecntDirectory.files.Add(new file()
                        {
                            Parent = currecntDirectory,
                            Name = filename,
                            Size = size
                        });
                    }
                }
            }
        }

        var freeSpace = 70000000 - rootDirectory.GetSize();
        var needSpace = 30000000 - freeSpace;
        //var targetdd = string.Join("\r\n", allDirectories.Values.OrderBy(x => x.GetSize()).Select(x => $"{x.GetFullname()} {x.GetSize()}"));
        var target = allDirectories.Values.Where(x => x.GetSize() >= needSpace).ToList(); ;

        return target.Min(x => x.GetSize()).ToString();
    }

}
