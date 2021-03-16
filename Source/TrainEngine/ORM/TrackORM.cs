using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace TrainEngine.DataTypes
{
    public class TrackORM
    {
        private int distance = 0;

        public static Crossing newCrossing { get; set; }
        public string[] FileLines { get; set; }
        public StationORM Station { get; }

        public TrackORM(string trackPath, List<Station> stations)
        {
            FileLines = File.ReadAllLines(trackPath);

            int firstStationY = -1;
            int firstStationX = -1;

            // Find the first station, and one one cannot be found return.
            for (int yLine = 0; yLine < FileLines.Length; yLine++)
            {
                for (int xChar = 0; xChar < FileLines[yLine].Length; xChar++)
                {
                    if (FileLines[yLine][xChar] == '1' && stations.Any(station => station.ID == 1)) {
                        firstStationY = yLine;
                        firstStationX = xChar;
                    }
                }
            }

            if (firstStationY == -1 || firstStationX == -1)
            {
                return;
            }

            // The first station was found.
            for (firstStationX += 1; firstStationX < FileLines[firstStationY].Length; firstStationX++)
            {
                char nextChar = FileLines[firstStationY][firstStationX];

                if (Char.IsDigit(nextChar))
                {
                    stations.Find(a => a.ID == int.Parse(nextChar.ToString())).Distance = distance;
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
