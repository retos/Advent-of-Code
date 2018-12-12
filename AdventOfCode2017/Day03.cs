using System;
using System.Collections.Generic;

namespace AdventOfCode2017
{
    internal class Day03
    {
        internal static void CalculatePart1(int n)
        {
            if (n == 1)
            {
                Console.WriteLine(0);
                return;
            };

            var x = 0;
            var y = 0;

            var stepCount = 1; // Initial step amount.
            var direction = 0; // right, up, left, down

            // Get the x,y coordinate for each step of i. 
            for (int i = 1; ;)
            {
                for (int j = 0; j < stepCount; j += 1)
                {
                    // Take a step
                    switch (direction)
                    {
                        case 0:
                            // right
                            x += 1;
                            break;
                        case 1:
                            // up
                            y += 1;
                            break;

                        case 2:
                            // left
                            x -= 1;
                            break;

                        case 3:
                            // down
                            y -= 1;
                            break;
                        default:
                            break;
                    }

                    // Needs to be incremented here after we take a step.
                    // Then we check the outer loop condition here, and so then jump out if needed.
                    // The ghost of Djikstra will probably haunt me for a bit now...~
                    i += 1;

                    if (i == n) {
                        int l1distance = Math.Abs(x) + Math.Abs(y);
                        Console.WriteLine(l1distance);
                        return;
                    };
                }
                if (i == n) break;

                direction = (direction + 1) % 4;
                //Ist die neue Richtung nach links oder rechts muss die Schrittzahl erhöht werden
                if (direction == 0 || direction == 2)
                {
                    stepCount += 1;
                }

            }
            return;
        }
        internal static void CalculatePart2(int input)
        {
            if (input == 1)
            {
                Console.WriteLine(0);
                return;
            };

            var x = 0;
            var y = 0;

            var stepCount = 1; // Initial step amount.
            var direction = 0; // right, up, left, down
            Dictionary<string, int> spiralMemory = new Dictionary<string, int>();
            spiralMemory["0,0"] = 1;

            // Get the x,y coordinate for each step of i. 
            for (; ;)
            {
                for (int j = 0; j < stepCount; j += 1)
                {
                    // Take a step
                    switch (direction)
                    {
                        case 0:
                            // right
                            x += 1;
                            break;
                        case 1:
                            // up
                            y += 1;
                            break;

                        case 2:
                            // left
                            x -= 1;
                            break;

                        case 3:
                            // down
                            y -= 1;
                            break;
                        default:
                            break;
                    }

                    // Determine sum of neighbours for value of current location.
                    int sum = 0;
                    int val = 0;

                    if (spiralMemory.TryGetValue(string.Format("{0},{1}", x + 1, y), out val)) sum += val;
                    if (spiralMemory.TryGetValue(string.Format("{0},{1}", x + 1, y + 1), out val)) sum += val;
                    if (spiralMemory.TryGetValue(string.Format("{0},{1}", x, y + 1), out val)) sum += val;
                    if (spiralMemory.TryGetValue(string.Format("{0},{1}", x - 1, y + 1), out val)) sum += val;
                    if (spiralMemory.TryGetValue(string.Format("{0},{1}", x - 1, y), out val)) sum += val;
                    if (spiralMemory.TryGetValue(string.Format("{0},{1}", x - 1, y - 1), out val)) sum += val;
                    if (spiralMemory.TryGetValue(string.Format("{0},{1}", x, y - 1), out val)) sum += val;
                    if (spiralMemory.TryGetValue(string.Format("{0},{1}", x + 1, y - 1), out val)) sum += val;

                    // Check here to see if the sum exceeds our input. Otherwise, store the sum computed and continue.
                    if (sum > input)
                    {
                        Console.WriteLine(sum);
                        return;
                    }
                    spiralMemory[string.Format("{0},{1}", x, y)] = sum;
                }

                direction = (direction + 1) % 4;
                //Ist die neue Richtung nach links oder rechts muss die Schrittzahl erhöht werden
                if (direction == 0 || direction == 2)
                {
                    stepCount += 1;
                }

            }
        }

    }
}