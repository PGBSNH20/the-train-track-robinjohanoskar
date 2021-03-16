using System;
using System.IO;

namespace TrainEngine.DataTypes
{
    public class TrackORM
    {
        public static Crossing newCrossing { get; set; }
        public string[] FileLines { get; set; }
        public StationORM Station { get; }

        public TrackORM(string trackPath)
        {
            FileLines = File.ReadAllLines(trackPath);
        }

        int distance = 0;

        public void ReadTrack()
        {
            // Find the first station
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
            for (x += 1; x < FileLines[y].Length; x++)
            {
                char nextChar = FileLines[y][x];

                if (Char.IsDigit(nextChar))
                {
                    StationORM.Stations.Find(a => a.ID == int.Parse(nextChar.ToString())).Distance = distance;
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
                    newCrossing.Distance = distance;

                }
            }
        }
    }
}
