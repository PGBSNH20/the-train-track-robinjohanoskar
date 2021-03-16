using System;
using System.Collections.Generic;
using System.Linq;
using TrainEngine.DataTypes;

namespace TrainEngine
{
    public interface ITrainPlanner
    {
        ITrainPlanner AddTimetable(List<TimetableStop> timetable);
        ITrainPlanner AddStations(List<Station> stations);
        ITravelPlan GeneratePlan();
    }
    public class TrainPlanner : ITrainPlanner
    {
        private Train _train;
        private List<TimetableStop> _timetable = new List<TimetableStop>();
        private List<Station> _stations = new List<Station>();

        public TrainPlanner(Train train)
        {
            _train = train;
        }

        public ITrainPlanner AddTimetable(List<TimetableStop> timetable)
        {
            _timetable = timetable.Where(stop => stop.TrainId == _train.Id).ToList();
            return this;
        }

        public ITrainPlanner AddStations(List<Station> stations)
        {
            _stations = stations;
            return this;
        }

        public ITravelPlan GeneratePlan()
        {
            return new TravelPlan(_train, _timetable, _stations);
        }
    }
}
