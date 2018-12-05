using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace AdventOfCode2018
{
    internal abstract class DayBase : IDay
    {
        public List<string> TestInput
        {
            get
            {
                return ReadInput($"TestInput.txt");
            }
        }
        public List<string> Input
        {
            get
            {
                return ReadInput($"Input.txt");
            }
        }

        public abstract string Title { get; }
        public abstract bool Ignore { get; }

        private List<string> ReadInput(string filename)
        {
            StreamReader reader = new StreamReader(System.IO.Path.Combine(GetType().Name, filename));

            List<string> inputList = new List<string>();
            while (!reader.EndOfStream)
            {
                inputList.Add(reader.ReadLine());
            }

            return inputList;
        }

        public void Calculate()
        {
            string className = GetType().Name;

            if (Title.Equals(string.Empty))
            {
                Console.WriteLine(className);
            }
            else
            {
                Console.WriteLine(Title);
            }

            if (!Ignore)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Console.WriteLine($"{className}, part 1 TEST: {Part1(TestInput)} [{stopwatch.ElapsedMilliseconds}ms]");

                stopwatch.Restart();                
                Console.WriteLine($"{className}, part 1     : {Part1(Input)} [{stopwatch.ElapsedMilliseconds}ms]");
                stopwatch.Restart();
                Console.WriteLine($"{className}, part 2 TEST: {Part2(TestInput)} [{stopwatch.ElapsedMilliseconds}ms]");
                stopwatch.Restart();
                Console.WriteLine($"{className}, part 2     : {Part2(Input)} [{stopwatch.ElapsedMilliseconds}ms]");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Ignored");
            }

            Console.WriteLine();

        }
        public abstract string Part1(List<string> input);
        public abstract string Part2(List<string> input);
    }
}