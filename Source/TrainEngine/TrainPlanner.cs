using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace TrainEngine
{
	public interface ITrainPlanner
	{
		ITrainPlanner StartTrainAt(string startTrain, bool DirectionForward);
		ITrainPlanner StopTrainAt(Station station, string stopTrain);
		ITravelPlan GeneratePlan();
		ITrainPlanner ReadSchedule();
    }
	
	public class FileData
    {
		public string FileName;
		public List string[] FileLines;

		public FileData(string filePath, char separator)
        {
			string[] lines = File.ReadAllLines(filePath);
			foreach (string line in lines)
			{
				string[] columns = line.Split(separator);
				FileData
			}
    }

	public class Schedule
    {
		public int StationID;
		public string StationName;
		public int ArrivalTime;
		public int DepartureTime;
		public bool DirectionForward;
    }

	public class TrainPlanner : ITrainPlanner
    {
		public ITravelPlan ReadSchedule(Schedule)
        {
			schedule = new Schedule();
			File.ReadLines();

        }

		private List<Stop> _stops = new List<Stop>();

        public bool DirectionForward = true;

		public TrainPlanner(Train train, Station station)
        {
            _stops.Add(new Stop(station));
        }

		public ITrainPlanner StartTrainAt(string startTrain, bool directionForward)
        {
            DirectionForward = directionForward;

			int[] time = startTrain.Split(':').Select(a => int.Parse(a)).ToArray();
			int startTime = time[0] * 60 + time[1];

			_stops[0].Time = startTime;

            return this;
        }

		public ITrainPlanner StopTrainAt(Station station, string stopTrain)
        {
			int[] time = stopTrain.Split(':').Select(a => int.Parse(a)).ToArray();
			int stopTime = time[0] * 60 + time[1];

			_stops.Add(new Stop(station, stopTime));

			return this;
		}

        public ITravelPlan GeneratePlan()
        {
			return new TravelPlan(_stops);
        }

		
		
    }

	public interface ITravelPlan
    {
        public List<Stop> Stops { get; set; }

	}

	public class TravelPlan : ITravelPlan
    {
        public List<Stop> Stops { get; set; }

		public void Start()
        {

        }

		public TravelPlan(List<Stop> stops)
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

	public class Train
	{
		private int _id;
		private string _name;
		private int _maxSpeed;
		private bool _operated;

		public Train(string name)
        {
			_name = name;
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
