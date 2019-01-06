using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018.Day20
{
    internal class Directions
    {
        private string directionString;
        private string after;

        public Directions(string directionString, ref List<Directions> after)
        {
            int i = 0;
            Steps = GetSteps(directionString, ref i);
            string bracketPart = GetBracketContent(directionString, ref i);
            string restAfterBracket = string.Empty;
            if (i<=directionString.Length)
            {
                restAfterBracket = directionString.Substring(i);
                List<Directions> rest = ReadDirections(restAfterBracket, ref after);
                NextDirections = ReadDirections(bracketPart, ref rest);
            }
            else
            {
                NextDirections = ReadDirections(bracketPart, ref after);
            }
        }

        internal void WriteToMap(ref Hashtable map, int x, int y)
        {
            Room r;
            if (!map.ContainsKey($"{x},{y}"))
            {
                r = new Room(ref map, x, y);
            }
            else
            {
                r = ((Room)map[$"{x},{y}"]);
            }

            WriteToMap(ref map, r);
        }

        private void WriteToMap(ref Hashtable map, Room r)
        {
            foreach (char c in Steps)
            {
                r = r.AddRoom(c);
            }

            foreach (Directions directions in NextDirections)
            {
                directions.WriteToMap(ref map, r);
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

        public static List<Directions> ReadDirections(string input, ref List<Directions> after)
        {
            if (string.IsNullOrEmpty(input))
            {
                return after;
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
                        compiledDirections.Add(new Directions(input, ref after));
                    }
                    else
                    {//Add the remainder
                        stepGroups.Add(input.Substring(lastStart, i - lastStart+1));
                    }
                }
            }

            foreach (string s in stepGroups)
            {
                compiledDirections.AddRange(ReadDirections(s, ref after));
            }            

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

    }
}