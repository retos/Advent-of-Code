using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode2018.Day09
{
    internal class Day09 : DayBase
    {
        public override string Title => "--- Day 9: Marble Mania ---";

        public override bool Ignore => true;

        public override string Part1(List<string> input, bool isTestRun)
        {            
            string answer = "";
            foreach (string s in input)
            {
                answer += CalculateWinnerScore(s, false) + Environment.NewLine;
            }
            return answer;
        }

        private string CalculateWinnerScore(string s, bool part2)
        {
            List<string> pieces = s.Split(' ').ToList();
            int numberOfPlayers = int.Parse(pieces[0]);
            List<ulong> playerScores = new List<ulong>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                playerScores.Add(0);
            }
            int highestMarbleValue = int.Parse(pieces[6]);
            if (part2)
            {
                highestMarbleValue = highestMarbleValue * 100;
            }
            List<int> marbles = new List<int>();
            int currentPositionInList = 0;
            marbles.Add(0);


            for (int i = 1; i <= highestMarbleValue; i++)
            {
                //einfügen zwischen 1 & 2 nach rechts (im Uhrzeigersinn) dies ist dann die aktive kugel
                int targetIndex = (currentPositionInList + 2) % marbles.Count;//endposition berechnen indem man über die loop-grenze hinaus kann
                if (i % 23 == 0)//falls die kugel aber ein vielfaches von 23 ist wird sie behalten (+score)
                {
                    playerScores[i % numberOfPlayers] += (ulong)i;
                    // & die kugel 7 nach links (gegenuhrzeigersinn) wird entfernt (+ score)
                    if (currentPositionInList >= 7)
                    {
                        targetIndex = (currentPositionInList - 7) % marbles.Count;
                    }
                    else
                    {
                        targetIndex = marbles.Count + currentPositionInList - 7;
                    }

                    playerScores[i % numberOfPlayers] += (ulong)marbles[targetIndex];
                    marbles.RemoveAt(targetIndex);
                    // -> die kugel nach rechts ist die neue aktive kugel im kreis
                }
                else
                {
                    marbles.Insert(targetIndex, i);
                }
                currentPositionInList = targetIndex;

                Console.SetCursorPosition(0, 0);
                Console.Write($"{i} / {highestMarbleValue}");

            }

            return $"players {numberOfPlayers}, max-marble {highestMarbleValue}, winning-score {playerScores.Max()}       ";
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            string answer = "";
            foreach (string s in input)
            {
                answer += CalculateWinnerScore(s, true);
            }
            return answer;
        }
    }
}