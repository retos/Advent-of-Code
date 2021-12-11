namespace AdventOfCode2021.Day10;

internal class Day10 : DayBase
{
    public override string Title => "--- Day 10: Syntax Scoring ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        //map[][]  (+modulo)                                        Day 03 2020 https://adventofcode.com/2020/day/3
        //map[,]   (+output to console, write to textfile)          Day 05 2021
        //switch case string & int parsing                          Day 02 2021
        //convert binaryint to decimal  Convert.ToInt32(string, 2)  Day 03 2021
        //visualisation txt->jpg with irfanview, jpg frames->mp4 with avidemux Day 05 2021
        //visualisation png                                         Day 10 2018
        //dictionary                                                Day 06 2021
        return "";
        int sum = 0;

        foreach (string line in input)
        {
            sum += CheckForChunks(line, 0);
        }


        return $"{sum}";
    }


    public override string Part2(List<string> input, bool isTestRun)
    {
        List<ulong> scores = new List<ulong>();

        foreach (string line in input)
        {
            ulong score = CheckForChunksPart2(line, 0);
            if (score>0)
            {
                scores.Add(score);
            }
            
        }

        scores.Sort();

        return $"{scores[scores.Count() / 2]}";
    }
    private int CheckForChunks(string line, int startPos)
    {
        Stack<char> stack = new Stack<char>();
        for (int i = startPos; i < line.Length; i++)
        {
            if (stack.Count() > 0 && stack.Peek() == line[i])
            {
                stack.Pop();
            }
            else if (new char[] {'[', '{', '(', '<' }.Contains(line[i]))
            {
                
                switch (line[i])
                {
                    case '(':
                        stack.Push(')');
                        break;
                    case '[':
                        stack.Push(']');
                        break;
                    case '{':
                        stack.Push('}');
                        break;
                    case '<':
                        stack.Push('>');
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //error
                switch (line[i])
                {
                    case ')':
                        return 3;
                        break;
                    case ']':
                        return 57;
                        break;
                    case '}':
                        return 1197;
                        break;
                    case '>':
                        return 25137;
                        break;
                    default:
                        break;
                }
            }

        }

        return 0;//all good
    }
    private ulong CheckForChunksPart2(string line, int startPos)
    {
        Stack<char> stack = new Stack<char>();
        for (int i = startPos; i < line.Length; i++)
        {
            if (stack.Count() > 0 && stack.Peek() == line[i])
            {
                stack.Pop();
            }
            else if (new char[] { '[', '{', '(', '<' }.Contains(line[i]))
            {

                switch (line[i])
                {
                    case '(':
                        stack.Push(')');
                        break;
                    case '[':
                        stack.Push(']');
                        break;
                    case '{':
                        stack.Push('}');
                        break;
                    case '<':
                        stack.Push('>');
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //error, ignore
                return 0;
            }

        }
        if (stack.Count>0)
        {
            ulong points = 0;
            foreach (char c in stack)
            {
                switch (c)
                {
                    case ')':
                        points = (points * 5) + 1;
                        break;
                    case ']':
                        points = (points * 5) + 2;
                        break;
                    case '}':
                        points = (points * 5) + 3;
                        break;
                    case '>':
                        points = (points * 5) + 4;
                        break;
                    default:
                        break;
                }
            }
            return points;
        }
        return 0;//all good, ignore
    }
}