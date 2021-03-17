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
        private Thread _travelPlanThread;
        private FakeTime _fakeTime;
        private int _minutesSinceLastDeparture;
        private TimetableStop _nextStop;
        private Station _nextStation;
        private bool _hasPassedCrossing;

        public Train Train { get; set; }
        public List<TimetableStop> Timetable { get; set; }
        public List<Station> Stations { get; set; }

        public TravelPlan() { }
        public TravelPlan(Train train, List<TimetableStop> timetable, List<Station> stations)
        {
            Train = train;
            Timetable = timetable;
            Stations = stations;
        }

        public void Simulate(FakeTime fakeTime)
        {
            _fakeTime = fakeTime;
            _travelPlanThread = new Thread(Tick);
            _travelPlanThread.Start();
        }

        public void Tick()
        {
            // If the FakeTime background thread is not running (i.e. when the time is "24:xx"), stop this (TravelPlan) thread.
            if (!_fakeTime.TimeThread.IsAlive)
            {
                return;
            }

            Thread.Sleep(_fakeTime.TickInterval / 2);

            foreach (TimetableStop stop in Timetable)
            {
                string stationName = Stations.Find(station => station.ID == stop.StationId).Name;

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
                        _nextStation = Stations.Find(station => station.ID == _nextStop.StationId);
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
            TravelPlanJson jsonTravelplan = new TravelPlanJson(Stations, Timetable, this.Train);

            string json = JsonConvert.SerializeObject(jsonTravelplan);

            var outputFolder = Path.Combine(Path.GetTempPath(), "the-train-track-robinjohanoskar");
            if (!Directory.Exists(outputFolder)) {
                Directory.CreateDirectory(outputFolder);
            }

            File.WriteAllText(Path.Combine(outputFolder, $"travelplan-train{Train.Id}.json"), json);
        }

        // Load a travel plan (.json file) from disk.
        public void LoadPlan(string fileName)
        {
            var outputFolder = Path.Combine(Path.GetTempPath(), "the-train-track-robinjohanoskar");

            using (StreamReader file = File.OpenText(Path.Combine(outputFolder, fileName)))
            {
                JsonSerializer serializer = new JsonSerializer();
                TravelPlanJson travelPlan = (TravelPlanJson)serializer.Deserialize(file, typeof(TravelPlanJson));

                Stations = travelPlan.Stations.ToList();
                Timetable = travelPlan.Timetable.ToList();
                Train = travelPlan.Train;
            }
        }
    }
}
