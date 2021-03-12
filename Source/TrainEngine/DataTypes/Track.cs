using System;
using System.Collections.Generic;
using System.IO;

namespace TrainEngine.DataTypes
{
    public class Track
    {
        public string[] FileLines { get; set; }

        //public List<string> TrackOrder { get; set; } = new List<string>();

        public StationORM Station { get; }

        public Track(string trackPath, StationORM station)
        {
            FileLines = File.ReadAllLines(trackPath);
            Station = station;
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
                        foreach(Station s in Station.Stations)
                        {
                            if(s.ID == 1)
                            {

                                TrackOrder.Add("Station1");
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
