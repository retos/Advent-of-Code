using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AdventOfCode2018.Day10
{
    internal class MarkerManager
    {
        public List<Marker> Markers { get; set; }
        public MarkerManager(List<string> input)
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
        }

        internal string Part1(bool isTestrun)
        {
            int i = IterateAndGetShortest();
            WriteToDisk(MoveTime(i), $"Part01-Test-X-{isTestrun}{i}");
            return "Check generated files...";
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

        private int IterateAndGetShortest()
        {
            int i = 0;
            int lastdiff;
            int newDiff = GetDiff(Markers);
            do
            {
                lastdiff = newDiff;
                i++;
                newDiff = GetDiff(MoveTime(i));
            } while (newDiff < lastdiff);

            return i-1;
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
            for (int y = 0; y < maxY - shiftY + 1; y++)
            {
                List<Marker> markersInThisLine = markers.Where(m => y.Equals(m.Y - shiftY)).ToList();
                if (markersInThisLine.Any())
                {
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
                }
                text += Environment.NewLine;
            }

            File.WriteAllText($"{filename}.html", $"<pre>{text}</pre>");

            #endregion
            #region with jagged array [too large]
            /*
            char[][] output = CreateJaggedArray(maxX + shiftX + 1, maxY + shiftY + 1);

            foreach (Marker marker in markers)
            {
                output[marker.X + shiftX][ marker.Y + shiftY] = '#';
            }

            string text = string.Empty;
            for (int y = 0; y < maxY + shiftY + 1; y++)
            {
                for (int x = 0; x < maxX + shiftX + 1; x++)
                {
                    if (output[x][ y] == '\0')
                    {
                        text += ' ';
                    }
                    else
                    {
                        text += output[x][y];
                    }

                }
                text += Environment.NewLine;
            }

            File.WriteAllText($"{filename}.html", $"<pre> {text} </pre>");
            */
            #endregion
            #region with Drawing [too large]

            Bitmap bitmap = new Bitmap(maxX + shiftX + 1, maxY + shiftY + 1);
            foreach (Marker marker in markers)
            {
                bitmap.SetPixel(marker.X - shiftX, marker.Y - shiftY, Color.Black);
            }
            bitmap.Save($"{filename}.png");

            #endregion
        }
    }
}