using System.Collections.Generic;
using System.ComponentModel;

namespace AdventOfCode2022.Day13;

internal class Day13 : DayBase
{
    public override string Title => "--- Day 13: Distress Signal ---";
    public override bool Ignore => true;
    private bool debugModePart1 = false;
    private bool debugModePart2 = false;


    public override string Part1(List<string> input, bool isTestRun)
    {
        //Read packages
        List<PackagePair> Pairs = PackagePair.ReadInput(input);
        List<int> indexesOfCorrectPairs = new List<int>();

        for (int i = 0; i < Pairs.Count; i++)
        {
            if (debugModePart1) { Console.WriteLine($"== Pair {i + 1} =="); }
            CompareResult compareResult = Compare(Pairs[i].Left.RawData, Pairs[i].Right.RawData, string.Empty);
            if (compareResult == CompareResult.LeftIsSmaller || 
                compareResult == CompareResult.LeftRanOutOfItems ||
                compareResult == CompareResult.StillChecking ||
                compareResult == CompareResult.TheyAreTheSame)
            {
                indexesOfCorrectPairs.Add(i+1);
            }
            if (debugModePart1) { Console.WriteLine(""); }
        }

        if (debugModePart1)
        {
            return $"indeces are ({string.Join(", ", indexesOfCorrectPairs)}) the sum is {indexesOfCorrectPairs.Sum()}";
        }
        return $"The sum is {indexesOfCorrectPairs.Sum()}";
    }

    private CompareResult Compare(string left, string right, string intendation)
    {
        if (debugModePart1)
        {
            Console.WriteLine($"{intendation}- Compare {left} vs {right}");
        }

        //Try int comparision, also fix mixed types
        CompareResult compareresult = LeftIsSmaller(left, right, intendation);
        if (compareresult != CompareResult.StillChecking)
        {
            return compareresult;
        }
        FixMixedData(ref left, ref right, ref intendation);

        //we do not have int values to compare yet, but we are looking at a list
        string leftCompareData;
        string rightCompareData;
        RemoveOuterbracketsIfNeeded(left, right, out leftCompareData, out rightCompareData);
        List<string> leftParts = BreakIntoParts(ref leftCompareData);
        List<string> rightParts = BreakIntoParts(ref rightCompareData);

        if (leftParts.Count == 1 && leftParts[0].Equals(string.Empty))
        {
            if (debugModePart1)
            {
                Console.WriteLine($"{intendation}  - Left side ran out of items, so inputs are in the right order");
            }
            return CompareResult.LeftRanOutOfItems;
        }

        //go through all parts to compare
        int maxNumber = (leftParts.Count > rightParts.Count) ? leftParts.Count : rightParts.Count;
        for (int i = 0; i < maxNumber; i++)
        {
            if (rightParts.Count <= i)
            {
                if (debugModePart1)
                {
                    Console.WriteLine($"{intendation}  - Right side ran out of items, so inputs are not in the right order");
                }
                return CompareResult.RightRanOutOfItems;
            }
            else if (leftParts.Count <= i)
            {
                if (debugModePart1)
                {
                    Console.WriteLine($"{intendation}  - Left side ran out of items, so inputs are in the right order");
                }
                return CompareResult.LeftRanOutOfItems;
            }
            compareresult = Compare(leftParts[i], rightParts[i], intendation + "  ");
            if (compareresult == CompareResult.LeftIsSmaller ||
                compareresult == CompareResult.LeftRanOutOfItems ||
                compareresult == CompareResult.RightIsSmaller ||
                compareresult == CompareResult.RightRanOutOfItems)
            {
                return compareresult;
            }
        }

        //checked everything no postion found where left side is smaller
        return CompareResult.StillChecking;

    }

    private void RemoveOuterbracketsIfNeeded(string left,string right, out string leftInnerData, out string rightInnerData)
    {
        leftInnerData = left;
        rightInnerData = right;

        if (left.StartsWith("[") && right.StartsWith("["))
        {
            //so far so good, we have a list, BUT is the whole thing a list?
            //Compare sub-items extract stuff between current '[' and closing '['
            int closingpositionLeft = 0;
            string leftSubstring = ExtractListContent(left, ref closingpositionLeft);
            int closingpositionRight = 0;
            string rightSubstring = ExtractListContent(right, ref closingpositionRight);

            if(closingpositionLeft+1 == left.Length && closingpositionRight+1 == right.Length)
            {
                //both are a list, break them down
                //remove brackets
                leftInnerData = leftSubstring;
                rightInnerData = rightSubstring;
            }
        }
    }

    private CompareResult LeftIsSmaller(string left, string right, string intendation)
    {
        int leftValue;
        int rightValue;

        bool leftIsNumber = int.TryParse(left, out leftValue);
        bool rightIsNumber = int.TryParse(right, out rightValue);

        if (leftIsNumber && rightIsNumber)
        {
            if (leftValue == rightValue)
            {
                return CompareResult.TheyAreTheSame;
            }
            else if (leftValue < rightValue)
            {
                if (debugModePart1)
                {
                    Console.WriteLine($"{intendation}  - Left side is smaller, so inputs are in the right order");
                }
                return CompareResult.LeftIsSmaller;
            }
            else
            {
                if (debugModePart1)
                {
                    Console.WriteLine($"{intendation}  - Right side is smaller, so inputs are not in the right order");
                }
                return CompareResult.RightIsSmaller;
            }
        }       
        //we are looking at something else, just continue
        return CompareResult.StillChecking;
    }
    private void FixMixedData(ref string left, ref string right, ref string intendation)
    {
        int leftValue;
        int rightValue;

        bool leftIsNumber = int.TryParse(left, out leftValue);
        bool rightIsNumber = int.TryParse(right, out rightValue);
                
        if (leftIsNumber || rightIsNumber)
        {
            //at least one number, conversion is needed, and continue
            if (leftIsNumber)
            {
                left = $"[{left}]";
                if (debugModePart1)
                {
                    Console.WriteLine($"{intendation}  - Mixed types; convert left to {left} and retry comparison");
                }

            }
            if (rightIsNumber)
            {
                right = $"[{right}]";
                if (debugModePart1)
                {
                    Console.WriteLine($"{intendation}  - Mixed types; convert right to {right} and retry comparison");
                }
            }
        }
    }

    private string ExtractFirstData(ref string dataString)
    {
        string fieldData = dataString.Split(',')[0];
        if (dataString.Contains(','))
        {
            dataString = dataString.Substring(fieldData.Length + 1);
        }
        else
        {
            dataString = string.Empty;
        }

        return fieldData;
    }

    private List<string> BreakIntoParts(ref string dataString)
    {
        List<string> parts = new List<string>(); ;
        int openCount = 0;
        int closeCount = 0;
        string value = string.Empty;

        for (int i = 0; i < dataString.Length; i++)
        {
            switch (dataString[i])
            {
                case '[':
                    openCount++;
                    value += dataString[i];
                    break;
                case ']':
                    closeCount++;
                    value += dataString[i];
                    break;
                case ',':
                    if (openCount == closeCount)
                    {
                        parts.Add(value);
                        value = string.Empty;
                    }
                    else
                    {
                        value += dataString[i];
                    }
                    break;
                default:
                    value += dataString[i];
                    break;
            }
        }
        if (!string.IsNullOrEmpty(value))
        {
            parts.Add(value);
        }
        return parts;
    }

    private string ExtractListContent(string dataString, ref int closingPosition)
    {
        
        //opening bracket find closing match
        int openingPosition = 0;
        int openCount = 0;
        int closingCount = 0;

        for (int i = 0; i < dataString.Length; i++)
        {
            switch (dataString[i])
            {
                case '[':
                    openCount++;
                    break;
                case ']':
                    closingCount++;
                    if (openCount <= closingCount)
                    {
                        //match!
                        closingPosition = i;
                        string listContent = dataString.Substring(openingPosition + 1, closingPosition - 1);
                        return listContent;

                    }
                    break;
                default:
                    //ignore
                    break;
            }
        }

        return string.Empty;
        
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        //Read packages
        List<string> packages = new List<string>();
        foreach (string line in input)
        {
            if (!string.IsNullOrEmpty(line))
            {
                packages.Add(line);
            }
        }
        packages.Add("[[2]]");
        packages.Add("[[6]]");


        bool switched = false;

        do
        {
            switched = false;
            for (int i = 0; i < packages.Count; i++)
            {
                
                if (debugModePart2) { Console.WriteLine($"Comparing item {packages[i]} on position {i} "); }
                if (debugModePart2) { PrintList(packages, i); }


                for (int j = 0; j < packages.Count; j++)
                {
                    if (j > i)
                    {
                        CompareResult compareResult = Compare(packages[j - 1], packages[j], string.Empty);
                        if (compareResult == CompareResult.LeftIsSmaller ||
                            compareResult == CompareResult.LeftRanOutOfItems ||
                            compareResult == CompareResult.StillChecking ||
                            compareResult == CompareResult.TheyAreTheSame)
                        {
                            //left is smaller
                            if (debugModePart2) { Console.WriteLine($"  Left side {packages[j - 1]} is smaller than {packages[j]}, do nothing"); }
                            
                            //abort this i
                            break;
                        }
                        else
                        {
                            //right is smaller --> switch places
                            if (debugModePart2) { Console.WriteLine($"Right side {packages[j]} is smaller than {packages[j - 1]}. Places need to be switched"); }
                            
                            switched = true;
                            string tmp = packages[j];
                            packages[j] = packages[j - 1];
                            packages[j - 1] = tmp;

                            if (debugModePart2) { PrintList(packages, j); }
                        }
                    }
                }



            }
        } while (switched);

        int indexOne = packages.IndexOf("[[2]]")+1;
        int indexTwo = packages.IndexOf("[[6]]")+1;


        if (debugModePart2) { PrintList(packages, -1); }
        return $"Decoder key is {indexOne*indexTwo}";
    }

    private void PrintList(List<string> packages, int higlightIndex)
    {
        //int leftCursorPos = Console.GetCursorPosition().Left;
        //int topCursorPos = Console.GetCursorPosition().Top;

        ConsoleColor foreground = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;

        for (int i = 0; i < packages.Count; i++)
        {
            if (i == higlightIndex) { Console.ForegroundColor = ConsoleColor.Magenta; }
            Console.WriteLine(packages[i]);
            if (i == higlightIndex) { Console.ForegroundColor = ConsoleColor.Green; }
        }



        Console.ForegroundColor = foreground;
        //Console.SetCursorPosition(leftCursorPos, topCursorPos);
    }
}