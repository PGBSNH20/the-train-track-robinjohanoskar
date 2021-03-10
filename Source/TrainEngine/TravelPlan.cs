using System.Collections.Generic;
using TrainEngine.DataTypes;

namespace TrainEngine
{
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
}
