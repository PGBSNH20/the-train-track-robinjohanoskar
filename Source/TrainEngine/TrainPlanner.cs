using System;
using System.Collections.Generic;
using System.Text;
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

    public class TrainPlanner : ITrainPlanner
    {

        private Train _train;
        //private List<Stop> _stops = new List<Stop>();
        private List<TimetableStop> _stops = new List<TimetableStop>();
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

        public ITravelPlan GeneratePlan()
        {
            return new TravelPlan(_train, _stops);
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
    }
}
