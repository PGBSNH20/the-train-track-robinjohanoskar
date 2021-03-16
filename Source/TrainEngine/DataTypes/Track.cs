using System;
using System.Collections.Generic;
using System.IO;

namespace TrainEngine.DataTypes
{
    public class Track
    {
        public static Crossing newCrossing { get; set; }

        public string[] FileLines { get; set; }

        public StationORM Station { get; }

        public Track(string trackPath)
        {
            FileLines = File.ReadAllLines(trackPath);
        }

        int distance = 0;

        public void ReadTrack()
        {
            for (int yLine = 0; yLine < FileLines.Length; yLine++)
            {
                for (int xChar = 0; xChar < FileLines[yLine].Length; xChar++)
                {
                    if (FileLines[yLine][xChar] == '1')
                    {
                        foreach(Station s in StationORM.Stations)
                        {
                            if(s.ID == 1)
                            {
                                SearchNext(yLine, xChar);
                            }
                        }
                    }
                }
            }
        }
        
        public void SearchNext(int y, int x)
        {
            //char c = TrackMap[y][x];

            for (x += 1; x < FileLines[y].Length; x++)
            {
                char nextChar = FileLines[y][x];

                if (Char.IsDigit(nextChar))
                {
                    StationORM.Stations.Find(a => a.ID == int.Parse(nextChar.ToString())).Distance = distance;
                    //TrackOrder.Add($"Station {nextChar}");
                    distance = 0;
                }
                else if (nextChar == '-')
                {
                    distance += 10;
                }
                else if (nextChar == '=')
                {
                    distance += 10;
                    newCrossing = new Crossing();
                    newCrossing.distance = distance;

                }
            }
        }
    }
}
