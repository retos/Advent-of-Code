namespace AdventOfCode2021.Day21;

internal class Day21 : DayBase
{
    public override string Title => "--- Day 21 ---";
    public override bool Ignore => false;

    public override string Part1(List<string> input, bool isTestRun)
    {
        int thorwsPerTurn = 3;
        int diceMax = 100;
        int targetScore = 1000;

        List<Player> playerList = new List<Player>();
        if (isTestRun)
        {
            playerList.Add(new("Player 1", 4, 0));
            playerList.Add(new("Player 2", 8, 0));
        }
        else
        {
            playerList.Add(new("Player 1", 4, 0));
            playerList.Add(new("Player 2", 5, 0));
        }


        long rollCount = 0;
        do
        {
            foreach (var player in playerList)
            {
                int roll1 = (int)(rollCount + 1) % (diceMax);
                if (roll1 == 0) roll1 = 100;
                int roll2 = (int)(rollCount + 2) % (diceMax);
                if (roll2 == 0) roll2 = 100;
                int roll3 = (int)(rollCount + 3) % (diceMax);
                if (roll3 == 0) roll3 = 100;
                
                int steps = roll1 + roll2 + roll3;
                int newPosition = (player.Position + steps) % 10;
                if (newPosition == 0) newPosition = 10;

                string info = $"{player.Name} rolls {roll1}+{roll2}+{roll3} and moves to space {newPosition} for a total score of {player.Score + newPosition}";

                player.Position = newPosition;
                player.Score += newPosition;
                rollCount += thorwsPerTurn;

                if (player.Score >= targetScore)
                {
                    break;
                }
            }
            if (playerList.Any(p => p.Score >= targetScore))
            {
                long result = playerList.OrderBy(p => p.Score).First().Score * rollCount;
                return $"{result}";
            }
        } while (true);

        return $"no solution found";
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        //let me talk to doctor strange and get back to you.

        return $"no solution found";
    }
}