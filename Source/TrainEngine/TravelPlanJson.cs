using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TrainEngine.DataTypes;

namespace TrainEngine
{
    class TravelPlanJson
    {
        public string Stations { get; set; }

        public TravelPlanJson(List<Station> stations)
        {
            Stations = JsonSerializer.Serialize(stations, typeof(List<Station>));
        }
    }
}
