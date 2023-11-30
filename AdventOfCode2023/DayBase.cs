namespace AdventOfCode2023;

internal abstract class DayBase : IDay
{
    public List<string> TestInput
    {
        get
        {
            return ReadInput($"TestInput.txt");
        }
    }
    public List<string> Input
    {
        get
        {
            return ReadInput($"Input.txt");
        }
    }

    public abstract string Title { get; }
    public abstract bool Ignore { get; }

    public int SortOrder => int.Parse(GetType().Name.Substring(3, 2));

    private List<string> ReadInput(string filename)
    {
        StreamReader reader = new StreamReader(System.IO.Path.Combine(GetType().Name, filename));

        List<string> inputList = new List<string>();
        while (!reader.EndOfStream)
        {
            inputList.Add(reader.ReadLine());
        }

        return inputList;
    }

    public void Calculate()
    {
        string className = GetType().Name;

        if (Ignore) { Console.ForegroundColor = ConsoleColor.DarkCyan; }
        Console.WriteLine((string.IsNullOrEmpty(Title)) ? className : Title);
        if (Ignore) { Console.ForegroundColor = ConsoleColor.Gray; }

        if (!Ignore)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine($"{className}, part 1 TEST: {Part1(TestInput, true)} [{stopwatch.ElapsedMilliseconds}ms]        ");
            stopwatch.Restart();
            Console.WriteLine($"{className}, part 1     : {Part1(Input, false)} [{stopwatch.ElapsedMilliseconds}ms]        ");
            stopwatch.Restart();
            Console.WriteLine($"{className}, part 2 TEST: {Part2(TestInput, true)} [{stopwatch.ElapsedMilliseconds}ms]        ");
            stopwatch.Restart();
            Console.WriteLine($"{className}, part 2     : {Part2(Input, false)} [{stopwatch.ElapsedMilliseconds}ms]        ");
            Console.WriteLine();
        }

    }
    public abstract string Part1(List<string> input, bool isTestRun);
    public abstract string Part2(List<string> input, bool isTestRun);

    /// <summary>
    /// Returns an int[x,y] array of the input.
    /// </summary>
    /// <param name="input">the desired input, that needs to be processed</param>
    /// <returns>int[x,y] array</returns>
    protected int[,] GetIntMap(List<string> input)
    {
        int[,] map = new int[input[0].Length, input.Count];
        for (int y = 0; y < input.Count; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                map[x, y] = int.Parse(input[y][x].ToString());
            }
        }
        return map;
    }
}

