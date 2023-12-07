namespace AdventOfCode2023.Day01;

internal class Day01 : DayBase
{
    public override string Title => "--- Day 1: Trebuchet?! ---";
    public override bool Ignore => true;

    public override string Part1(List<string> input, bool isTestRun)
    {
        if (isTestRun) { return ""; }
        List<long> digits = new List<long>();
        foreach(string line in input)
        {
            Char a = line.First(c => Char.IsNumber(c));
            Char b = line.Last(c => Char.IsNumber(c));
            digits.Add(long.Parse($"{a}{b}"));
        }
        return $"{digits.Sum()}";
    }

    public override string Part2(List<string> input, bool isTestRun)
    {
        List<long> digits = new List<long>();
        foreach (string line in input)
        {
            //finding indexes of "letter digits"
            string[] letterDigits = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            List<int> firstIndexesLetters = new List<int>();
            List<int> lastIndexesLetters = new List<int>();
            for (int i = 0; i < letterDigits.Length; i++)
            {
                firstIndexesLetters.Add(line.IndexOf(letterDigits[i]));
                lastIndexesLetters.Add(line.LastIndexOf(letterDigits[i]));
            }


            //Finding indexes of digits
            int first = 0;
            int second = 0;

            int indexFirstDigit = -1;
            int indexLastDigit = -1;

            if (line.Any(c => Char.IsNumber(c)))
            {
                Char a = line.First(c => Char.IsNumber(c));
                first = int.Parse($"{a}");
                indexFirstDigit = line.IndexOf(a);

                Char b = line.Last(c => Char.IsNumber(c));
                second = int.Parse($"{b}");
                indexLastDigit = line.LastIndexOf(b);
            }


            //let's see if the letter digit was better than the digit
            if (firstIndexesLetters.Any(i => i >= 0))//was there a letter digit?
            {
                int lowestIndex = firstIndexesLetters.Where(i => i >= 0).Min();
                //there is a digit && the digit is better than the letterIndex? || there is no digit
                if ((indexFirstDigit >= 0 && indexFirstDigit > lowestIndex) || indexFirstDigit < 0)
                {
                    int tmp = firstIndexesLetters.IndexOf(lowestIndex);
                    first = tmp + 1;
                }

                int highestIndex = lastIndexesLetters.Where(i => i >= 0).Max();
                //there is a digit && the digit is better than the letterIndex? || there is no digit
                if ((indexLastDigit >= 0 && indexLastDigit < highestIndex) || indexLastDigit < 0)
                {
                    int tmp = lastIndexesLetters.IndexOf(highestIndex);
                    second = tmp + 1;
                }
            }

            //Console.WriteLine($"{line}  {first}{second}");
            digits.Add(long.Parse($"{first}{second}"));
        }

        return $"{digits.Sum()}";
    }
}

