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
        public List<Marker> Markers { get; set; }

        public override string Title => "";

        public override bool Ignore => false;

        public override string Part1(List<string> input, bool isTestRun)
        {
            Markers = new List<Marker>();
            foreach (string line in input)
            {
                string[] commaParts = line.Split(',');
                int x = int.Parse(commaParts[0].Split('<')[1]);
                int y = int.Parse(commaParts[1].Split('>')[0]);
                int vX = int.Parse(commaParts[1].Split('<')[1]);
                int vY = int.Parse(commaParts[2].Split('>')[0]);
                Markers.Add(new Marker() { X = x, Y = y, VelocityX = vX, VelocityY = vY });
            }

            int i = 0;
            WriteToDisk(IterateAndGetShortest(ref i), $"Part01-Test-X-{isTestRun}{i}");
            return "Check generated files...";
        }

        public override string Part2(List<string> input, bool isTestRun)
        {
            return "check generated files...";
        }

        private List<Marker> MoveTime(int time)
        {
            List<Marker> markersWithTimeShift = new List<Marker>();
            foreach (Marker marker in Markers)
            {
                Marker m = marker.SetNextPosition(time);
                markersWithTimeShift.Add(m);
            }
            return markersWithTimeShift;
        }

        private List<Marker> IterateAndGetShortest(ref int i)
        {
            List<Marker> result = Markers;
            List<Marker> currentList = Markers;
            int lastdiff;
            int newDiff = GetDiff(Markers);
            do
            {
                result = currentList;
                lastdiff = newDiff;
                i++;
                currentList = MoveTime(i);
                newDiff = GetDiff(currentList);
            } while (newDiff < lastdiff);
            i--;
            return result;
        }

        private static int GetDiff(List<Marker> markers)
        {
            return markers.Max(m => m.X) - markers.Min(m => m.X);
        }

        private static void WriteToDisk(List<Marker> markers, string filename)
        {
            int shiftX = markers.Min(ma => ma.X);
            int shiftY = markers.Min(ma => ma.Y);
            int maxX = markers.Max(m => m.X) - shiftX;
            int maxY = markers.Max(m => m.Y) - shiftY;
            #region with -> write file directly

            string text = string.Empty;
            for (int y = 0; y < maxY+ 1; y++)
            {
                List<Marker> markersInThisLine = markers.Where(m => y.Equals(m.Y - shiftY)).ToList();
                int maxPos = markersInThisLine.Max(m => m.X);
                for (int x = 0; x <= maxPos + 1; x++)
                {
                    if (markers.Any(m => y.Equals(m.Y - shiftY) && x.Equals(m.X - shiftX)))
                    {
                        text += "#";
                    }
                    else
                    {
                        text += " ";
                    }

                }
                text += Environment.NewLine;
            }

            File.WriteAllText($"{filename}.html", $"<pre>{text}</pre>");
            #endregion

            #region with Drawing

            Bitmap bitmap = new Bitmap(maxX+1, maxY+1);
            foreach (Marker marker in markers)
            {
                bitmap.SetPixel(marker.X - shiftX, marker.Y - shiftY, Color.Black);
            }
            bitmap.Save($"{filename}.png");

            #endregion
        }

    }
}