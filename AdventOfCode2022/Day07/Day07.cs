namespace AdventOfCode2022.Day07;

internal class Day07 : DayBase
{
    public override string Title => "--- Day 7: No Space Left On Device ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        List<Entry> allEntries = new List<Entry>();
        Entry root = new Entry(allEntries) { IsDirectory= true, Path = "/" };
        Entry currentDirectory = root;
        foreach (string line in input)
        {
            if (line.StartsWith("$"))
            {
                //command
                switch (line.Substring(0, 4))
                {
                    case "$ cd":
                        if (line.Substring(5) == "/")
                        {
                            currentDirectory = root;
                        }
                        else if (line.Substring(5) == "..")
                        {
                            currentDirectory = currentDirectory.GetParent();
                        }
                        else
                        {
                            string subpath = line.Substring(5);
                            currentDirectory = currentDirectory.Children.Where(c => c.Path == subpath).Single();
                        }
                        break;
                    case "$ ls":
                        //next items will be files in the current directory
                        break;

                    default:
                        throw new Exception($"bad argument");
                        break;
                }

            }
            else
            {
                if (line.StartsWith("dir"))
                {
                    Entry newDir = new Entry(allEntries)
                    {
                        IsDirectory = true,
                        //Path = $"/{line.Substring(4)}"
                        Path = line.Substring(4)
                    };
                    currentDirectory.Children.Add(newDir);
                }
                else
                {
                    string[] parts = line.Split(' ');
                    Entry file = new Entry(allEntries) { IsDirectory = false, Size = long.Parse(parts[0]), Path = parts[1] };
                    currentDirectory.Children.Add(file);
                }
            }
        }

        long treeSize = root.TotalSize;

        List<Entry> largeDirectories = allEntries.Where(d => d.IsDirectory && d.TotalSize <= 100000).ToList();

        return largeDirectories.Sum(d => d.TotalSize).ToString();
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        List<Entry> allEntries = new List<Entry>();
        Entry root = new Entry(allEntries) { IsDirectory = true, Path = "/" };
        Entry currentDirectory = root;
        foreach (string line in input)
        {
            if (line.StartsWith("$"))
            {
                //command
                switch (line.Substring(0, 4))
                {
                    case "$ cd":
                        if (line.Substring(5) == "/")
                        {
                            currentDirectory = root;
                        }
                        else if (line.Substring(5) == "..")
                        {
                            currentDirectory = currentDirectory.GetParent();
                        }
                        else
                        {
                            string subpath = line.Substring(5);
                            currentDirectory = currentDirectory.Children.Where(c => c.Path == subpath).Single();
                        }
                        //add entry
                        //currentDirectory = newDir;
                        break;
                    case "$ ls":
                        //next items will be files in the current directory
                        break;

                    default:
                        throw new Exception($"bad argument");
                        break;
                }

            }
            else
            {
                if (line.StartsWith("dir"))
                {
                    Entry newDir = new Entry(allEntries)
                    {
                        IsDirectory = true,
                        //Path = $"/{line.Substring(4)}"
                        Path = line.Substring(4)
                    };
                    currentDirectory.Children.Add(newDir);
                }
                else
                {
                    string[] parts = line.Split(' ');
                    Entry file = new Entry(allEntries) { IsDirectory = false, Size = long.Parse(parts[0]), Path = parts[1] };
                    currentDirectory.Children.Add(file);
                }
            }
        }

        long treeSize = root.TotalSize;

        long totalDiskSpace = 70000000;
        long desiredUnusedSpace = 30000000;
        long currentyUnused = totalDiskSpace - root.TotalSize;
        List<Entry> directories = allEntries.Where(d => d.IsDirectory).ToList();

        return directories.Where(d => currentyUnused + d.TotalSize >= desiredUnusedSpace)
            .OrderBy(d => d.TotalSize)
            .ToList()
            .First()
            .TotalSize
            .ToString();
    }
}

