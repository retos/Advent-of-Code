using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Program
    {
        static void Main(string[] args)
        {
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
        }
    }
}
