namespace AdventOfCode2022.Day03;

internal class Day03 : DayBase
{
    public override string Title => "--- Day 3: Rucksack Reorganization ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        long priority = 0;
        foreach (string line in input)
        {
            int linelength = line.Length;
            string left = line.Substring(0, linelength/2);
            string right = line.Substring(linelength/2);
            bool matchfound = false;

            string prio = "0abcdefghijklmnopqrstuvwqxzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            foreach (char cl in left)
            {
                foreach (char cr in right)
                {
                    if (cl==cr)
                    {
                        //match
                        //Lowercase item types a through z have priorities 1 through 26.
                        //Uppercase item types A through Z have priorities 27 through 52.
                        priority += prio.IndexOf(cl);
                        matchfound= true;
                        break;
                    }
                }
                if (matchfound)
                {
                    break;
                }
            }
        }
        return priority.ToString();
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        List<List<string>> groupInput = new List<List<string>>();
        int count = 1;
        List<string> currentGroup = new List<string>();
        groupInput.Add(currentGroup);

        foreach (string line in input)
        {
            if (count > 3)
            {
                count = 1;
                currentGroup = new List<string>();
                groupInput.Add(currentGroup);
            }
            currentGroup.Add(line);
            count ++;
        }

        long priority = 0;
        foreach (List<string> g in groupInput)
        {
            bool matchfound = false;

            string prio = "0abcdefghijklmnopqrstuvwqxzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            foreach (char cl in g[0])
            {
                foreach (char cr in g[1])
                {
                    foreach  (char cm in g[2])
                    {
                        if (cl == cr && cr == cm)
                        {
                            //match
                            //Lowercase item types a through z have priorities 1 through 26.
                            //Uppercase item types A through Z have priorities 27 through 52.
                            priority += prio.IndexOf(cl);
                            matchfound = true;
                            break;
                        }
                    }
                    if (matchfound)
                    {
                        break;
                    }
                }
                if (matchfound)
                {
                    break;
                }
            }
        }
        return priority.ToString();
    }
}

