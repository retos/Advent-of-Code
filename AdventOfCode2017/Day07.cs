using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    internal class Day07
    {
        internal static void CalculatePart1()
        {
            StreamReader reader = new StreamReader("input07.txt");

            //Create Programs
            List<Discprogram> discprograms = new List<Discprogram>();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                discprograms.Add(new Discprogram(line));
            }

            //Match Childreleations
            foreach (Discprogram disc in discprograms)
            {
                //Find parent
                Discprogram parent = discprograms.Where(d => d.ChildrensNames.Contains(disc.Name)).SingleOrDefault();
                if (parent != null)
                {
                    parent.AddChildren(disc);
                }                
            }

            //Output the root Program
            Discprogram root = discprograms.Where(d => d.Parent == null).Single();
            Console.WriteLine(root.Name);
        }

        internal static void CalculatePart2()
        {
            StreamReader reader = new StreamReader("input07.txt");

            //Create Programs
            List<Discprogram> discprograms = new List<Discprogram>();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                discprograms.Add(new Discprogram(line));
            }

            //Match Childreleations
            foreach (Discprogram disc in discprograms)
            {
                //Find parent
                Discprogram parent = discprograms.Where(d => d.ChildrensNames.Contains(disc.Name)).SingleOrDefault();
                if (parent != null)
                {
                    parent.AddChildren(disc);
                }
            }

            //Find the program with the wrong weight           
            Discprogram root = discprograms.Where(d => d.Parent == null).Single();
            Console.WriteLine("The first mentioned program is the one with the wrong weight since it has the lowest level:");
            root.CheckWeight();
        }
    }
}