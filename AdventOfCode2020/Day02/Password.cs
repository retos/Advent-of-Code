using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day02
{
    internal class PasswordEntry
    {
        public int MinNumber { get; set; }
        public int MaxNumber { get; set; }
        public char Character { get; set; }
        public string Password { get; set; }
        public bool IsValid 
        { 
            get 
            {
                int occurences = Password.Count(c => c == Character);
                return occurences >= MinNumber && occurences <= MaxNumber;
            } 
        }

        public bool IsValidPart2 
        {
            get
            {
                bool firstPosValid = Password[MinNumber - 1] == Character;
                bool secondPosValid = Password[MaxNumber - 1] == Character;
                return firstPosValid ^ secondPosValid;
            }
        }

        internal static List<PasswordEntry> Convert(List<string> input)
        {
            List<PasswordEntry> pwList = new List<PasswordEntry>();

            foreach (string line in input)
            {
                string[] spaceSplit = line.Split(' ');
                string[] edgeNumbers = spaceSplit[0].Split('-');

                int minNumber = int.Parse(edgeNumbers[0]); 
                int maxNumber = int.Parse(edgeNumbers[1]);
                Char character = spaceSplit[1][0];
                string password = spaceSplit[2];
                pwList.Add(new PasswordEntry() { MinNumber = minNumber, MaxNumber = maxNumber, Character = character, Password = password });
            }

            return pwList;
        }
    }
}