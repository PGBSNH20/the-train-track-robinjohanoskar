namespace TrainEngine.DataTypes
{
    public class TimetableStop
    {
        public int TrainId { get; set; }
        public int StationId { get; set; }
        // public int? DepartureTime { get; set; } // time in seconds
        // public int? ArrivalTime { get; set; } // time in seconds
        public FakeTime DepartureTime { get; set; } // time in seconds
        public FakeTime ArrivalTime { get; set; } // time in seconds
        // public bool Completed { get; set; } = false;
        public bool HasArrived { get; set; } = false;
        public bool HasDeparted { get; set; } = false;

        // public TimetableStop(int trainId, int stationId, int? departureTime, int? arrivalTime)
        public TimetableStop(int trainId, int stationId, FakeTime departureTime, FakeTime arrivalTime)
        {
            TrainId = trainId;
            StationId = stationId;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
        }
    }
}
