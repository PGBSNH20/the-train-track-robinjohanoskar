using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using TrainEngine.DataTypes;

namespace TrainEngine
{
    public interface ITravelPlan
    {
        Train Train { get; }
        List<TimetableStop> Timetable { get; set; }
        void Simulate(FakeTime fakeTime);
        void SavePlan();
        void LoadPlan(string jsonPath);
    }

    public class TravelPlan : ITravelPlan
    {
        public Train Train { get; set; }
        public List<TimetableStop> Timetable { get; set; }
        private Thread _travelPlanThread;
        private FakeTime _fakeTime;
        private int _minutesSinceLastDeparture;
        private TimetableStop _nextStop;
        private Station _nextStation;
        private bool _hasPassedCrossing;

        public TravelPlan() { }
        public TravelPlan(Train train, List<TimetableStop> timetable)
        {
            Train = train;
            Timetable = timetable;
        }

        public void Simulate(FakeTime fakeTime)
        {
            _fakeTime = fakeTime;
            _travelPlanThread = new Thread(Tick);
            _travelPlanThread.Start();
        }

        public void Tick()
        {
            Thread.Sleep(_fakeTime.TickInterval / 2);

            foreach (TimetableStop stop in Timetable)
            {
                // Get the station name from the static List "StationORM.Stations".
                string stationName = StationORM.Stations.Find(station => station.ID == stop.StationId).Name;

                // todo: operator overloading?
                if (!stop.HasDeparted && stop.DepartureTime != null && stop.DepartureTime.Hours == _fakeTime.Hours && stop.DepartureTime.Minutes == _fakeTime.Minutes) {
                    stop.HasDeparted = true;
                    _minutesSinceLastDeparture = FakeTime.MinutesSinceStart;

                    Console.ForegroundColor = Train.Color;
                    Console.WriteLine($"{_fakeTime.ToString()} - {Train.Name} has departed {stationName}");
                    Console.ForegroundColor = ConsoleColor.White;

                    int i = Timetable.IndexOf(stop);
                    if (i != -1 && i + 1 < Timetable.Count)
                    {
                        _nextStop = Timetable[i + 1];
                        _nextStation = StationORM.Stations.Find(station => station.ID == _nextStop.StationId);
                    }
                }

                if (stop.HasDeparted && _nextStop.HasArrived == false)
                {
                    double distance = Train.Speed / 60d * (FakeTime.MinutesSinceStart - _minutesSinceLastDeparture);

                    // Check if the train has reached or passed a crossing and close/open the crossing accordingly.
                    if (distance >= TrackORM.newCrossing.Distance - 5 && distance < TrackORM.newCrossing.Distance + 5 && TrackORM.newCrossing.BarClosed == false && _hasPassedCrossing == false)
                    {
                        TrackORM.newCrossing.BarClosed = true;
                        Console.WriteLine($"Closing crossingbars {Train.Name} is passing");
                    }
                    else if (distance >= TrackORM.newCrossing.Distance + 5 && TrackORM.newCrossing.BarClosed == true && _hasPassedCrossing == false)
                    {
                        _hasPassedCrossing = true;
                        TrackORM.newCrossing.BarClosed = false;
                        Console.WriteLine("Open crossingbars");
                    }

                    // Check if a train has reached a station.
                    if (distance >= _nextStation.Distance)
                    {
                        _nextStop.HasArrived = true;
                        _minutesSinceLastDeparture = 0;

                        Console.ForegroundColor = Train.Color;
                        Console.WriteLine($"{_fakeTime.ToString()} - {Train.Name} has arrived at {_nextStation.Name} ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }

            Tick();
        }

        // Save a travel plan (.json file) to disk.
        public void SavePlan()
        {
            TravelPlanJson jsonTravelplan = new TravelPlanJson(StationORM.Stations, Timetable, this.Train);

            string json = JsonConvert.SerializeObject(jsonTravelplan);

            File.WriteAllText(@$"C:\Temp\travelplan-train{Train.Id}.json", json);
        }

        // Load a travel plan (.json file) from disk.
        public void LoadPlan(string jsonPath)
        {
            using (StreamReader file = File.OpenText(jsonPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                TravelPlanJson travelPlan = (TravelPlanJson)serializer.Deserialize(file, typeof(TravelPlanJson));

                StationORM.Stations = travelPlan.Stations.ToList();
                Timetable = travelPlan.Timetable.ToList();
                Train = travelPlan.Train;
            }
        }
    }
}
