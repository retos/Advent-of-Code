using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Day20
{
    internal class Directions
    {
        private string directionString;
        private string after;

        public Directions(string directionString)
        {
            int i = 0;

            if (!directionString.Contains('(') && !directionString.Contains('|') )
            {
                Steps = directionString;
            }
            else
            {
                NextDirections = Directions.ReadDirections(directionString.Substring(i), null);
            }
        }

        public Directions(string directionString, string after)
        {
            int i = 0;
            Steps = GetSteps(directionString, ref i);
            string bracketPart = GetBracketContent(directionString, ref i);
            string restAfterBracket = string.Empty;
            if (i<=directionString.Length)
            {
                restAfterBracket = directionString.Substring(i);
            }
            if (string.IsNullOrEmpty(after))
            {
                NextDirections = ReadDirections(bracketPart, restAfterBracket);
            }
            else
            {
                NextDirections = ReadDirections(bracketPart, $"{restAfterBracket}{after}");
            }

        }

        private string GetBracketContent(string directionString, ref int i)
        {
            string bracketContent = string.Empty;
            int positionOfClosingBracket = i;
            if (Steps.Length < directionString.Length)
            {
                int openBracketCount = 0;
                for (int j = ++i; j < directionString.Length; j++)
                {
                    if (directionString[j] == '(')
                    {
                        openBracketCount++;
                    }
                    else if (directionString[j] == ')')
                    {
                        openBracketCount--;
                    }
                    if (openBracketCount == 0)
                    {
                        positionOfClosingBracket = j;
                        break;
                    }
                }
            }
            int startPos = i;
            i = positionOfClosingBracket + 1;
            if (startPos >= directionString.Length)
            {
                return string.Empty;
            }
            return directionString.Substring(startPos + 1, positionOfClosingBracket - startPos - 1);
        }

        public string Steps { get; set; }
        public List<Directions> NextDirections{ get; set; }
        public static List<Directions> ReadDirections(string input, string after)
        {
            if (string.IsNullOrEmpty(input))
            {
                input = after;
                after = string.Empty;
            }
            List<Directions> compiledDirections = new List<Directions>();
            /*
             ESSWWN(E|NNENN(EESS(WNSE|)SSS|WWWSSSSE(SW|NNNE)))

            -split by root |, but respect brackets ( and ), create parallel node(s)
             -> if more than one: create parallel nodes
             -> for each read steps, if opening bracket gets reached, take content everithing after and start over with sub node(s)

             ESSWWN         
             - E
             - NNENN
               - ESS
                 - WNSE
                   - SSS
                 - ""
                   - SSS
               - WWWSSSSE
                 - SW
                 - NNNE
             */
            List<string> stepGroups = new List<string>();
            int lastStart = 0;
            int openBracketCount = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    openBracketCount++;
                }
                else if (input[i] == ')')
                {
                    openBracketCount--;
                }
                else if (input[i] == '|' && openBracketCount == 0)
                {
                    stepGroups.Add(input.Substring(lastStart, i - lastStart));
                    lastStart = i+1;
                }
                if (i >= input.Length - 1)
                {
                    if (stepGroups.Count == 0)
                    {//No parallel steps ad only this
                        compiledDirections.Add(new Directions(input, after));
                    }
                    else
                    {//Add the remainder
                        stepGroups.Add(input.Substring(lastStart, i - lastStart+1));
                    }
                }
            }


            foreach (string s in stepGroups)
            {
                compiledDirections.AddRange(ReadDirections(s, after));
            }
            
           


            //    //NextDirections = new List<Directions>();
            //    for (int i = 0; i < input.Length; i++)
            //{
            //    if (input[i] == '(')
            //    {
            //        //find matching closing bracket, and create sub-directions
            //        string directionsInBrackets = GetsubdirectionString(i, input); ;
            //        //NextDirections = GetNextDirections(directionsInBrackets);
            //        stepGroups.Add(directionsInBrackets);
            //        break;
            //    }
            //    else
            //    {
            //        stepGroups.Add(GetSteps(input, ref i));
            //    }
            //}
            //foreach (string directionString in stepGroups)
            //{
            //    compiledDirections.Add(new Directions(directionString));
            //}
            return compiledDirections;
        }

        private static string GetSteps(string input ,ref int i)
        {
            for (int j = i; j < input.Length; j++)
            {
                if (input[j] == '(')
                {
                    string answer = input.Substring(i, j);
                    i = j - 1;
                    return answer;
                }
            }

            //No brackets found return all
            i = input.Length;
            return input;            
        }

        private List<Directions> GetNextDirections(string input)
        {
            List<string> nextDirectionStrings = new List<string>();
            int openingBracketCount = 0;
            string currentDirection = string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '|' && openingBracketCount == 0)
                {
                    nextDirectionStrings.Add(currentDirection);
                    currentDirection = string.Empty;
                }
                else if (input[i] == '(')
                {
                    openingBracketCount++;
                    currentDirection += input[i];
                }
                else if (input[i] == ')')
                {
                    currentDirection += input[i];
                    openingBracketCount--;
                }
                else
                {
                    currentDirection += input[i];
                }
            }
            nextDirectionStrings.Add(currentDirection);
            return nextDirectionStrings.Select(s => new Directions(s)).ToList();
        }

        private static string GetsubdirectionString(int i, string input)
        {
            int openinbracketCount = 0;
            for (int j = i; j < input.Length; j++)
            {
                if (input[j] == '(')
                {
                    openinbracketCount++;
                }
                else if (input[j] == ')')
                {
                    openinbracketCount--;
                }
                if (openinbracketCount == 0)
                {
                    return input.Substring(i+1, j-i-1);
                }
            }
            throw new ArgumentException("given input couldn't be parsed.");
        }
    }
}