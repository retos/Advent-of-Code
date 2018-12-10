using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Drawing.Imaging;

namespace AdventOfCode2018.Day10
{
    internal class Day10 : DayBase
    {
        public override string Title => "";

        public override bool Ignore => false;

        public override string Part1(List<string> input, bool isTestRun)
        {
            MarkerManager manager = new MarkerManager(input);
            return manager.Part1(isTestRun);
        }


        private List<Marker> IterateAndGetShortestY(List<Marker> markers, ref int i)
        {


            return markers;
        }

        

        private static char[][] CreateJaggedArray(int x, int y)
        {
            // Declare the array of two elements:
            char[][] arr = new char[x][];

            for (int i = 0; i < x; i++)
            {
                arr[i] = new char[y];
            }
            // Initialize the elements:
            return arr;
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            return input.Count.ToString();
        }


    }
}