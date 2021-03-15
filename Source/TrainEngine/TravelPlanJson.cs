using System;
using System.Collections.Generic;
using System.Text;
using TrainEngine.DataTypes;
using Newtonsoft.Json;
using System.IO;

namespace TrainEngine
{
    class TravelPlanJson
    {
        public IList<Station> Stations { get; set; }
        public IList<TimetableStop> Stops { get; set; }
        public Train Train { get; set; }

        public TravelPlanJson(List<Station> stations, List<TimetableStop> stops, Train train)
        {
            Stations = stations;
            Stops = stops;
            Train = train;
        }
    }
}