using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TrainEngine.DataTypes
{
    public class ScheduleORM
    {
        public List<TimetableStop> Timetable { get; } = new List<TimetableStop>();

        public ScheduleORM(string filePath)
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
                    catch (System.Exception) {}
                }
            
                // Parse the 4th column to a FakeTime object.
                FakeTime arrivalTime = null;
                if (columns[3] != "null")
                {
                    try
                    {
                        int[] tempArrivalTime = columns[3].Split(':').Select(a => int.Parse(a)).ToArray();
                        arrivalTime = new FakeTime(tempArrivalTime[0], tempArrivalTime[1]);
                    }
                    catch (System.Exception) {}
                }
               
                Timetable.Add(new TimetableStop(trainId, stationId, departureTime, arrivalTime));
            }
        }
    }
}
