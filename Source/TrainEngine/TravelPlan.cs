using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using TrainEngine.DataTypes;
using Newtonsoft.Json;

namespace TrainEngine
{
    public interface ITravelPlan
    {
        Train Train { get; }
        List<TimetableStop> Stops { get; set; }
        void Simulate(FakeTime fakeTime);
        void SavePlan();
        void LoadPlan(string jsonPath);
    }

    public class TravelPlan : ITravelPlan
    {
        public Train Train { get; set; }
        public List<TimetableStop> Stops { get; set; }
        // pseudo-code: 
        // public List<Event> CompletedEvents { get; set; }
        // public List<Event> Events { get; set; } // departure, arrival, closeCrossing, openCrossing
        private Thread _travelPlanThread;
        private FakeTime _fakeTime;
        private int _minutesSinceLastDeparture;
        private TimetableStop _nextStop;
        private Station _nextStation;
        private bool _hasPassedCrossing;
        //public List<Station> _stations;

        public TravelPlan(Train train, List<TimetableStop> stops) //, List<Station> stations)
        {
            Train = train;
            Stops = stops;
            //_stations = stations;
        }
        public TravelPlan()
        {

        }
        public void Simulate(FakeTime fakeTime)
        {
            _fakeTime = fakeTime;
            _travelPlanThread = new Thread(Tick);
            _travelPlanThread.Start();
        }
        public void Tick()
        {
            Thread.Sleep(_fakeTime.TickInterval / 2); // half of FakeTime's sleep
            // pseudo-code:
            // alternative solution:
            // if (Events[0].time >= _fakeTime)
            //      print: e.g. "time: 'trainname' has arrived at 'station'"
            //      moved Events[0] from Events to CompletedEvents

            foreach (TimetableStop stop in Stops)
            {
                // Get the station name from the static List "StationORM.Stations".
                string stationName = StationORM.Stations.Find(station => station.ID == stop.StationId).Name;

                // todo: operator overloading?
                if (!stop.HasDeparted && stop.DepartureTime != null && stop.DepartureTime.Hours == _fakeTime.Hours && stop.DepartureTime.Minutes == _fakeTime.Minutes) {
                    stop.HasDeparted = true;
                    Console.ForegroundColor = Train.Color;
                    Console.WriteLine($"{_fakeTime.GetFormattedTimeString()} - {Train.Name} has departed {stationName}");
                    Console.ForegroundColor = ConsoleColor.White;
                    //Console.WriteLine(FakeTime.MinutesSinceStart + " minutes since start"); // testing
                    _minutesSinceLastDeparture = FakeTime.MinutesSinceStart;
                    //_nextStationID = stop.StationId + 1;

                    int i = Stops.IndexOf(stop);
                    if (i != -1 && i + 1 < Stops.Count)
                    {
                        _nextStop = Stops[i + 1];
                        _nextStation = StationORM.Stations.Find(station => station.ID == _nextStop.StationId);
                    }
                }

                if (stop.HasDeparted && _nextStop.HasArrived == false)
                {
                    // 120 km * 
                    double distance = Train.Speed / 60d * (FakeTime.MinutesSinceStart - _minutesSinceLastDeparture);
                    //Console.WriteLine($"The train {Train.Name} has gone {distance} km.");

                    if (distance >= Track.newCrossing.distance - 5 && distance < Track.newCrossing.distance + 5 && Track.newCrossing.barClosed == false && _hasPassedCrossing == false)
                    {
                        Track.newCrossing.barClosed = true;
                        Console.WriteLine($"Closing crossingbars {Train.Name} is passing");
                    }

                    if (distance >= Track.newCrossing.distance + 5 && Track.newCrossing.barClosed == true && _hasPassedCrossing == false)
                    {
                        _hasPassedCrossing = true;
                        Track.newCrossing.barClosed = false;
                        Console.WriteLine("Open crossingbars");
                    }

                    if (distance >= _nextStation.Distance) {
                        Console.ForegroundColor = Train.Color;
                        Console.WriteLine($"{_fakeTime.GetFormattedTimeString()} - {Train.Name} has arrived at {_nextStation.Name} ");
                        Console.ForegroundColor = ConsoleColor.White;
                        //_nextStop.HasDeparted = false;
                        _nextStop.HasArrived = true;
                        
                        _minutesSinceLastDeparture = 0;
                    }
                    else
                    {
                        //Console.WriteLine($"The train {Train.Name} has gone {distance} km.");
                    }
                }

                // todo: operator overloading?
                //if (!stop.HasArrived && stop.ArrivalTime != null && stop.ArrivalTime.Hours == _fakeTime.Hours && stop.ArrivalTime.Minutes == _fakeTime.Minutes) {
                //    stop.HasArrived = true;
                //    Console.WriteLine($"{Train.Name} has arrived at {stop.StationId} at {_fakeTime.Hours.ToString().PadLeft(2, '0')}:{_fakeTime.Minutes.ToString().PadLeft(2, '0')}");
                //}
            }

            Tick();
        }

        public void SavePlan()
        {
            TravelPlanJson jsonTravelplan = new TravelPlanJson(StationORM.Stations, Stops, this.Train);

            string json = JsonConvert.SerializeObject(jsonTravelplan);

            File.WriteAllText(@$"C:\Temp\travelplan-train{Train.Id}.json", json);
        }

        public void LoadPlan(string jsonPath)
        {
            using (StreamReader file = File.OpenText(@$"c:\temp\{jsonPath}"))
            {
                JsonSerializer serializer = new JsonSerializer();
                TravelPlanJson travelPlan = (TravelPlanJson)serializer.Deserialize(file, typeof(TravelPlanJson));

                StationORM.Stations = travelPlan.Stations.ToList();
                Stops = travelPlan.Stops.ToList();
                Train = travelPlan.Train;
            }
        }
    }
}
