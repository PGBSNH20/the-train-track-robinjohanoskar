using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TrainEngine.DataTypes
{
    public class Schedule
    {
        public int TrainId { get; set; }
        public List<TimetableStop> Stops { get; set; } = new List<TimetableStop>();
        public bool DirectionForward;

        public Schedule(int trainId, List<TimetableStop> stops)
        {
            Stops = stops;
        }
    }

    public class ScheduleData
    {
        public List<TimetableStop> Stops { get; } = new List<TimetableStop>();

        public ScheduleData(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] columns = lines[i].Split(',').Select(a => a.Trim()).ToArray();

                // Parse the first two columns to ints.
                int trainId = int.Parse(columns[0]);
                int stationId = int.Parse(columns[1]);

                // Parse the 3rd column to a FakeTime object.
                FakeTime departureTime = null;

                if (columns[2] != "null")
                {
                    try
                    {
                        int[] tempDepartureTime = columns[2].Split(':').Select(a => int.Parse(a)).ToArray();
                        departureTime = new FakeTime(tempDepartureTime[0], tempDepartureTime[1]);
                    }
                    catch (System.Exception)
                    {
                        // todo: maybe throw
                    }
                }
            
                // Parse the 4th column to a FakeTime object.
                FakeTime arrivalTime = null;

                if (columns[3] != "null")
                {
                    try
                    {
                        int[] tempArrivalTime = columns[2].Split(':').Select(a => int.Parse(a)).ToArray();
                        departureTime = new FakeTime(tempArrivalTime[0], tempArrivalTime[1]);
                    }
                    catch (System.Exception)
                    {
                        // todo: maybe throw
                    }
                }
               
                Stops.Add(new TimetableStop(trainId, stationId, departureTime, arrivalTime));
            }
        }
    }
}
