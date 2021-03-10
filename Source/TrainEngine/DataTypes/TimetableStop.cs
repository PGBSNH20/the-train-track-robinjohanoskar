namespace TrainEngine.DataTypes
{
    public class TimetableStop
    {
        public int TrainId { get; set; }
        public int StationId { get; set; }
        public int? DepartureTime { get; set; } // time in seconds
        public int? ArrivalTime { get; set; } // time in seconds

        public TimetableStop(int trainId, int stationId, int? departureTime, int? arrivalTime)
        {
            TrainId = trainId;
            StationId = stationId;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
        }
    }
}
