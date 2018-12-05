using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2017
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Day 01 - Wertvergleich an versetzter Stelle in der Zeile
            //StreamReader reader = new StreamReader("input01.txt");            
            //Day01.Calculate(reader.ReadLine());

            //reader = new StreamReader("input01.txt");
            //Day01.CalculateSecond(reader.ReadLine());
            #endregion

            #region Day 02 - Differenz von Zahlen in einer Zeile
            //Day02.Calculate();
            //Day02.CalculateSecond();
            #endregion

            #region Day 03 - Spiral Memory
            //Day03.CalculatePart1(325489);
            //Day03.CalculatePart2(325489);
            #endregion

            #region Day 04 - Passphrases
            //Day04.CalculatePart1();
            //Day04.CalculatePart2();
            #endregion

            #region Day 05 - List processing
            //Day05.CalculatePart1();
            //Day05.CalculatePart2();
            #endregion

            #region Day 06 - Memory Reallocation
            //Day06.CalculatePart1();
            //Day06.CalculatePart2();
            #endregion

            #region Day 07 - Recursive Circus aka Programs in balance
            //Day07.CalculatePart1();
            //Day07.CalculatePart2();
            #endregion


            #region Day 08 - Registers
            //Day08.CalculatePart1();
            //Day08.CalculatePart2();
            #endregion

            #region Day 09 - Streams
            //Day09.CalculatePart1();
            //Day09.CalculatePart2();
            #endregion

            List<IDay> instances = System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                .Where(d => d.GetInterfaces().Contains(typeof(IDay))
                    && d.GetConstructor(Type.EmptyTypes) != null
                    && !d.IsAbstract)
                    .Select(d => (IDay)Activator.CreateInstance(d))
                    .ToList();

            foreach (IDay day in instances.OrderBy(d => d.GetType().Name))
            {
                day.Calculate();
            }

            Console.WriteLine("Calculation ended, hit any key to close");
            Console.ReadKey();

            Console.ReadKey();            
        }
    }
}
