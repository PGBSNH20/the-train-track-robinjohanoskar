namespace TrainEngine.DataTypes
{
    public class Stop
    {
        public Station Station { get; set; }
        public int Time { get; set; }

        public Stop(Station station)
        {
            Station = station;
        }

        public Stop(Station station, int time)
        {
            Station = station;
            Time = time;
        }
    }
}
