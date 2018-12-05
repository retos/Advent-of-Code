using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace AdventOfCode2017
{
    internal class Day08
    {
        internal static void CalculatePart1()
        {
            StreamReader reader = new StreamReader("input08.txt");

            Dictionary<string, int> register = new Dictionary<string, int>();

            while (!reader.EndOfStream)
            {
                //Read instructions
                string line = reader.ReadLine();
                string[] lineparts = line.Split(' ');

                string registername = lineparts[0];
                string operation = (lineparts[1].Equals("inc"))? "+" : "-";
                int operationValue = int.Parse(lineparts[2]);

                string conditionRegistername = lineparts[4];
                string conditionOperation = lineparts[5];
                int conditionValue = int.Parse(lineparts[6]);

                //Prepare register
                if (!register.ContainsKey(registername))
                {
                    register.Add(registername, 0);
                }
                if (!register.ContainsKey(conditionRegistername))
                {
                    register.Add(conditionRegistername, 0);
                }

                //Excecute instruction
                DataTable dt = new DataTable();
                //splitting and putting back, could be done with string.replace...
                string condition = $"{register[conditionRegistername]} {conditionOperation.Replace("==", "=").Replace("!=", "<>")} {conditionValue}";
                if ((bool)dt.Compute(condition, ""))
                {
                    register[registername] = (int)dt.Compute($"{register[registername]} {operation} {operationValue}", "");
                }
            }

            Console.WriteLine($"Largest number in register: {register.Max(r => r.Value)}");
        }

        internal static void CalculatePart2()
        {
            int highestValue = int.MinValue;
            StreamReader reader = new StreamReader("input08.txt");

            Dictionary<string, int> register = new Dictionary<string, int>();

            while (!reader.EndOfStream)
            {
                //Read instructions
                string line = reader.ReadLine();
                string[] lineparts = line.Split(' ');

                string registername = lineparts[0];
                string operation = (lineparts[1].Equals("inc")) ? "+" : "-";
                int operationValue = int.Parse(lineparts[2]);

                string conditionRegistername = lineparts[4];
                string conditionOperation = lineparts[5];
                int conditionValue = int.Parse(lineparts[6]);

                //Prepare register
                if (!register.ContainsKey(registername))
                {
                    register.Add(registername, 0);
                }
                if (!register.ContainsKey(conditionRegistername))
                {
                    register.Add(conditionRegistername, 0);
                }

                //Excecute instruction
                DataTable dt = new DataTable();
                //splitting and putting back, could be done with string.replace...
                string condition = $"{register[conditionRegistername]} {conditionOperation.Replace("==", "=").Replace("!=", "<>")} {conditionValue}";
                if ((bool)dt.Compute(condition, ""))
                {
                    register[registername] = (int)dt.Compute($"{register[registername]} {operation} {operationValue}", "");
                    if (register[registername] > highestValue)
                    {
                        highestValue = register[registername];
                    }
                }
            }

            Console.WriteLine($"Largest number during the operations in register: {highestValue}");
        }
    }
}