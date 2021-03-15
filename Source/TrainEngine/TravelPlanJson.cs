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

        public Train Train { get; set; }

        public TravelPlanJson(List<Station> stations, Train train)
        {
            Stations = stations;

            Train = train;
        }
    }
}
