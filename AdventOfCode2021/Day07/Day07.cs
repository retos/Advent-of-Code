namespace AdventOfCode2021.Day07;

internal class Day07 : DayBase
{
    public override string Title => "--- Day 7: The Treachery of Whales ---";
    public override bool Ignore => true;
    private Dictionary<int, int> costDictionary = new Dictionary<int, int>();

    public override string Part1(List<string> input, bool isTestRun)
    {
        List<int> crabs = input[0].Split(',').Select(int.Parse).ToList();
        Dictionary<int, int> crabCounts = new Dictionary<int, int>();

        for (int i = 0; i <= crabs.Max(); i++)
        {
            int count = crabs.Count(x => x == i);
            if (count > 0) crabCounts[i] = count;
        }

        int bestCost = int.MaxValue;
        int bestPosition = int.MaxValue;

        //brute force
        for (int i = 0; i < crabCounts.Count(); i++)
        {
            int nextCost = CalculateCost(crabCounts, i, false);
            if (nextCost < bestCost)
            {
                bestCost = nextCost;
                bestPosition = i;
            }
        }
        
        return $"best Position {bestPosition}, with cost {bestCost}";
    }


    public override string Part2(List<string> input, bool isTestRun)
    {
        List<int> crabs = input[0].Split(',').Select(int.Parse).ToList();
        Dictionary<int, int> crabCounts = new Dictionary<int, int>();
        for (int i = 0; i <= crabs.Max(); i++)
        {
            crabCounts[i] = crabs.Count(x => x == i);
        }

        long bestCost = long.MaxValue;
        long bestPosition = long.MaxValue;

        //brute force
        for (int i = 0; i < crabCounts.Count(); i++)
        {
            long nextCost = CalculateCost(crabCounts, i, true);
            if (nextCost < bestCost)
            {
                bestCost = nextCost;
                bestPosition = i;
            }
        }
        return $"best Position {bestPosition}, with cost {bestCost}";
    }

    private int CalculateCost(Dictionary<int, int> crabCounts, int target, bool parttwo)
    {
        int cost = 0;

        foreach (var entry in crabCounts)
        {
            int currentCost = Math.Abs(entry.Key - target);
            if (parttwo) currentCost = GetCost(currentCost);

            cost += currentCost * entry.Value;
        }

        return cost;
    }

    private int GetCost(int targetPos)
    {
        if (costDictionary.ContainsKey(targetPos))
        {
            return costDictionary[targetPos];
        }
        else
        {
            int currentCost = 0;
            int lastCost = 0;
            
            //if we started at the last position and only calculated from there, it is not much faster. So lets calculate all the way to targetpos
            for (int i = 0; i <= targetPos; i++)
            {
                currentCost = lastCost + i;
                lastCost = currentCost;
            }
            costDictionary[targetPos] = currentCost;
            return currentCost;
        }
    }
}