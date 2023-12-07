using System.Collections.Generic;

namespace AdventOfCode2023.Day02;

internal class Day02 : DayBase
{
    public override string Title => "--- Day 2: Cube Conundrum ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {   
            List<Game> games = new List<Game>();

        foreach(string line in input)
        {
            string[] sp = line.Split(';');
            Game g = new Game();
            games.Add(g);
            g.Id = long.Parse(sp[0].Split(' ')[1].Replace(":", ""));
            sp[0] = sp[0].Replace($"{line.Split(':')[0]}:", "");
            
            foreach(string reveal in sp)
            {
                Reveal r = new Reveal();
                g.Reveals.Add(r);
                string[] colors = reveal.Split(",");
                foreach(string color in colors)
                {
                    string[] parts = color.Trim().Split(' ');
                    switch (parts[1])
                    {
                        case "red":
                            r.Red = long.Parse(parts[0]);
                            break;
                        case "blue":
                            r.Blue = long.Parse(parts[0]);
                            break;
                        case "green":
                            r.Green = long.Parse(parts[0]);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        long limitRed = 12;
        long limitGreen = 13;
        long limitBlue = 14;

        List<Game> filteredGames = games.Where(g => g.Reveals.All(r => r.Red <= limitRed && r.Green <= limitGreen && r.Blue <= limitBlue)).ToList();

        long sum = filteredGames.Sum(g => g.Id);

        return $"{sum}";
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        List<Game> games = new List<Game>();

        foreach (string line in input)
        {
            string[] sp = line.Split(';');
            Game g = new Game();
            games.Add(g);
            g.Id = long.Parse(sp[0].Split(' ')[1].Replace(":", ""));
            sp[0] = sp[0].Replace($"{line.Split(':')[0]}:", "");

            foreach (string reveal in sp)
            {
                Reveal r = new Reveal();
                g.Reveals.Add(r);
                string[] colors = reveal.Split(",");
                foreach (string color in colors)
                {
                    string[] parts = color.Trim().Split(' ');
                    switch (parts[1])
                    {
                        case "red":
                            r.Red = long.Parse(parts[0]);
                            break;
                        case "blue":
                            r.Blue = long.Parse(parts[0]);
                            break;
                        case "green":
                            r.Green = long.Parse(parts[0]);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        long sum = games.Sum(g => g.PowerOfGame);
        return $"{sum}";
    }
}
