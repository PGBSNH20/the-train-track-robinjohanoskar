using System.Collections.Generic;
using TrainEngine.DataTypes;

namespace TrainEngine
{
    class TravelPlanJson
    {
        public IList<Station> Stations { get; set; }
        public IList<TimetableStop> Timetable { get; set; }
        public Train Train { get; set; }

        public TravelPlanJson(List<Station> stations, List<TimetableStop> timetable, Train train)
        {
            Stations = stations;
            Timetable = timetable;
            Train = train;
        }
    }
}