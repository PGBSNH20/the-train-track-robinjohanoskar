using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace TrainEngine.DataTypes
{
    public class Track
    {
        public string[] TrackMap { get; set; }

        public List<string> TrackOrder { get; set; } = new List<string>();

        public Track(string filePath)
        {
            TrackMap = File.ReadAllLines(filePath);
        }

        public void ReadTrack()
        {
            for (int yLine = 0; yLine < TrackMap.Length; yLine++)
            {
                for (int xChar = 0; xChar < TrackMap[yLine].Length; xChar++)
                {
                    if (TrackMap[yLine][xChar] == '1')
                    {
                        TrackOrder.Add("Station1");
                        SearchNext(yLine, xChar);
                    }

                }
            }
        }

        public void SearchNext(int y, int x)
        {
            //char c = TrackMap[y][x];

            for (x += 1; x < TrackMap[y].Length; x++)
            {
                char nextChar = TrackMap[y][x];

                if (Char.IsDigit(nextChar))
                {
                    TrackOrder.Add($"Station {nextChar}");
                }
                else if (nextChar == '=')
                {
                    TrackOrder.Add("Crossing");
                }
            }
        }
    }
}
