using Newtonsoft.Json;

namespace TrainEngine.DataTypes
{
    public class TimetableStop
    {
        public int TrainId { get; set; }
        public int StationId { get; set; }
        public FakeTime DepartureTime { get; set; }
        public FakeTime ArrivalTime { get; set; }
        
        [JsonIgnore]
        public bool HasArrived { get; set; } = false;
        public bool HasDeparted { get; set; } = false;

        public TimetableStop(int trainId, int stationId, FakeTime departureTime, FakeTime arrivalTime)
        {
            TrainId = trainId;
            StationId = stationId;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
        }
    }
}
