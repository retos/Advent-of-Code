using System;
using System.Collections.Generic;

namespace AdventOfCode2017
{
    internal class StreamGroup
    {
        private string originalStream;
        private string cleanedStream;
        private string childrenStream;
        public List<StreamGroup> Children { get; set; }
        public int Score { get; internal set; }
        public int NoneCanceledCharacterCount { get; internal set; }

        public StreamGroup(string line)
        {
            originalStream = line;
            cleanedStream = originalStream;

            //Remove escaped stuff
            while (cleanedStream.Contains("!"))
            {
                int start = cleanedStream.IndexOf("!");
                cleanedStream = cleanedStream.Remove(start, 2);
            }

            //find and remove contained garbage
            while (cleanedStream.Contains("<"))
            {
                int start = cleanedStream.IndexOf("<");
                int end = cleanedStream.IndexOf(">");
                cleanedStream = cleanedStream.Remove(start, end - start + 1);
                NoneCanceledCharacterCount += end - start - 1;
            } 

            childrenStream = cleanedStream.Replace(",","");

            Score = CalculateScore(childrenStream, 1);
        }

        private int CalculateScore(string substream, int level)
        {
            int subScore = 0;
            while (substream.Contains("{"))
            {
                //Check for children, Count the Score
                //Find starting {
                int start = substream.IndexOf("{");
                int stop = start;
                int brackedBalance = 0;
                for (int i = start+1; i < substream.Length; i++)
                {
                    switch (substream[i])
                    {
                        case '}':
                            if (brackedBalance == 0)
                            {
                                stop = i;
                            }
                            else
                            {
                                brackedBalance--;
                            }
                            break;
                        case '{':
                            brackedBalance++;
                            break;
                    }
                    if (stop != start)
                    {
                        break;
                    }
                }

                //found a match!
                subScore = subScore + level;
                //calculate groups within
                if (stop - start > 1)
                {
                    subScore += CalculateScore(substream.Substring(start+1, stop-1), level + 1);
                }
                substream = substream.Remove(start, stop - start+1);
            }
           
            return subScore;


        }
    }
}