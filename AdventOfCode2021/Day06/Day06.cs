namespace AdventOfCode2021.Day06;

internal class Day06 : DayBase
{
    public override string Title => "--- Day 6: Lanternfish ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        List<Lanternfish> fishes = ReadInput(input);

        for (int i = 1; i <= 80; i++)
        {
            List<Lanternfish> children = new List<Lanternfish>();
            //todo single line foreach
            foreach (Lanternfish lanternfish in fishes)
            {
                Lanternfish child = lanternfish.AgeOneDay();
                if (child != null)
                {
                    children.Add(child);
                }
            }
            fishes.AddRange(children);
            //Console.WriteLine($"Aged {i} | {string.Join(',', fishes.Select(f => f.Age))}");
        }


        return $"{fishes.Count()}";
    }


    public override string Part2(List<string> input, bool isTestRun)
    {
        //read data to usable format
        List<int> fishies = input[0].Split(',').Select(int.Parse).ToList();
        Dictionary<int, long> fishCounts = new Dictionary<int, long>();
        for (int i = 0; i <= 8; i++)
        {
            fishCounts[i] = fishies.Count(x => x == i);
        }

        //age them
        for (int i = 0; i < 256; i++)
        {
            long numberOfChildren = fishCounts[0];

            //shift numbers in dictionary to age them
            for (int j = 0; j < 8; j++)
            { 
                fishCounts[j] = fishCounts[j + 1]; 
            }

            fishCounts[6] += numberOfChildren;  //add parents to existing fishes of same age
            fishCounts[8] = numberOfChildren;   //add children
        }

        return $"{fishCounts.Values.Sum()}";
    }
    private List<Lanternfish> ReadInput(List<string> input)
    {
        int[] inputnumbers = Array.ConvertAll(input[0].Split(','), s => int.Parse(s));
        List<Lanternfish> fishes = new List<Lanternfish>();

        foreach (int i in inputnumbers)
        {
            fishes.Add(new Lanternfish(i));
        }
        return fishes;
    }
}