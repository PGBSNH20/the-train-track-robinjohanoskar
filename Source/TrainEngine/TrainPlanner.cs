using System.Collections.Generic;
using System.Linq;
using TrainEngine.DataTypes;

namespace TrainEngine
{
    public interface ITrainPlanner
    {
        ITravelPlan GeneratePlan();
        ITrainPlanner ReadSchedule(List<TimetableStop> timetable);
    }

    public class TrainPlanner : ITrainPlanner
    {
        private Train _train;
        private List<TimetableStop> _timetable = new List<TimetableStop>();
        public bool DirectionForward = true;

        public TrainPlanner(Train train)
        {
            _train = train;
        }

        public ITrainPlanner ReadSchedule(List<TimetableStop> timetable)
        {
            _timetable = timetable.Where(stop => stop.TrainId == _train.Id).ToList();
            return this;
        }

        public ITravelPlan GeneratePlan()
        {
            return new TravelPlan(_train, _timetable);
        }
    }
}
