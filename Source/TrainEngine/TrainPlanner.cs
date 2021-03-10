using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using TrainEngine.DataTypes;

namespace TrainEngine
{
    public interface ITrainPlanner
    {
        //ITrainPlanner StartTrainAt(string startTrain, bool DirectionForward);
        //ITrainPlanner StopTrainAt(Station station, string stopTrain);
        ITravelPlan GeneratePlan();
        ITrainPlanner ReadSchedule(Schedule schedule);
    }

    // Note: split into a separate class for each data file type? ("train.txt", "passangers.txt" ...etc).
    public class FileData
    {
        public string FileName;
        public List<string[]> FileLines { get; } = new List<string[]>();

        public FileData(string filePath, char separator)
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] columns = line.Split(separator).Select(a => a.Trim()).ToArray();
                FileLines.Add(columns);
            }
        }
    }



    public class TrainPlanner : ITrainPlanner
    {

        //private List<Stop> _stops = new List<Stop>();
        private List<TimetableStop> _stops = new List<TimetableStop>();
        private Train _train;
        public bool DirectionForward = true;

        //public TrainPlanner(Train train, Station station)
        //{
        //    _stops.Add(new Stop(station));
        //}

        public TrainPlanner(Train train)
        {
            _train = train;
        }

        public ITrainPlanner ReadSchedule(Schedule schedule)
        {
            _stops = schedule.Stops;
            return this;
        }

        //public ITrainPlanner StartTrainAt(string startTrain, bool directionForward)
        //{
        //    DirectionForward = directionForward;

        //    int[] time = startTrain.Split(':').Select(a => int.Parse(a)).ToArray();
        //    int startTime = time[0] * 60 + time[1];

        //    _stops[0].Time = startTime;

        //    return this;
        //}

        //public ITrainPlanner StopTrainAt(Station station, string stopTrain)
        //{
        //    int[] time = stopTrain.Split(':').Select(a => int.Parse(a)).ToArray();
        //    int stopTime = time[0] * 60 + time[1];

        //    _stops.Add(new Stop(station, stopTime));

        //    return this;
        //}

        public ITravelPlan GeneratePlan()
        {
            return new TravelPlan(_stops);
        }
    }

    public interface ITravelPlan
    {
        public List<TimetableStop> Stops { get; set; }

    }

    public class TravelPlan : ITravelPlan
    {
        public List<TimetableStop> Stops { get; set; }

        public void Start()
        {

        }

        public TravelPlan(List<TimetableStop> stops)
        {
            Stops = stops;
        }
    }

    public class Stop
    {
        public Station Station { get; set; }
        public int Time { get; set; }

        public Stop(Station station)
        {
            Station = station;
        }

        public Stop(Station station, int time)
        {
            Station = station;
            Time = time;
        }
    }

    public class TimetableStop
    {
        public int TrainId { get; set; }
        public int StationId { get; set; }
        public int? DepartureTime { get; set; } // time in seconds
        public int? ArrivalTime { get; set; } // time in seconds

        public TimetableStop(int trainId, int stationId, int? departureTime, int? arrivalTime)
        {
            TrainId = trainId;
            StationId = stationId;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
        }
    }

    public class Station
    {
        // private int _id;
        public string Name { get; }
        // private bool _endStation;

        public Station(string name)
        {
            Name = name;
        }
    }
}
