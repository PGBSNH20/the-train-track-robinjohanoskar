using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TrainEngine
{
	public interface ITrainPlanner
	{
		ITrainPlanner StartTrainAt(string startTrain);
		ITrainPlanner StopTrainAt(Station station, string stopTrain);
		ITravelPlan GeneratePlan();
    }

	public class TrainPlanner : ITrainPlanner
	{
		private Stop _startsAt;
		private List<Stop> _stops;

		public TrainPlanner(Train train, Station station)
        {
			_startsAt = new Stop(station);
		}

		public ITrainPlanner StartTrainAt(string startTrain)
		{
			int[] time = startTrain.Split(':').Select(a => int.Parse(a)).ToArray();
			int startTime = time[0] * 60 + time[1];

			_startsAt.Time = startTime;

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

            Console.WriteLine(_startsAt.Time + " " + _stops[0].Time);

			return new TravelPlan(_startsAt, _stops);
        }
    }

	public interface ITravelPlan
	{

	}

	public class TravelPlan : ITravelPlan
	{
		public void Start()
        {

        }

		public TravelPlan(Stop start, List<Stop> stops)
        {

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
		private string _name;
		// private bool _endStation;

		public Station(string name)
        {
			_name = name;
        }
	}
}
