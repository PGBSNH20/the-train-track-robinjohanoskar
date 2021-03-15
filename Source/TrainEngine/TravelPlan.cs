using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TrainEngine.DataTypes;

namespace TrainEngine
{
    public interface ITravelPlan
    {
        Train Train { get; }
        List<TimetableStop> Stops { get; set; }
        void Simulate(FakeTime fakeTime);
    }

    public class TravelPlan : ITravelPlan
    {
        public Train Train { get; }
        public List<TimetableStop> Stops { get; set; }
        // pseudo-code: 
        // public List<Event> CompletedEvents { get; set; }
        // public List<Event> Events { get; set; } // departure, arrival, closeCrossing, openCrossing
        private Thread _travelPlanThread;
        private FakeTime _fakeTime;
        private int _minutesSinceLastDeparture;
        private TimetableStop _nextStop;
        private Station _nextStation;

        public TravelPlan(Train train, List<TimetableStop> stops)
        {
            Train = train;
            Stops = stops;
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

                if (stop.HasDeparted)
                {
                    // 120 km * 
                    int distance = Train.Speed * (FakeTime.MinutesSinceStart - _minutesSinceLastDeparture);
                    //Console.WriteLine($"The train {Train.Name} has gone {distance} km.");

                    if (distance >= _nextStation.Distance) {
                        Console.ForegroundColor = Train.Color;
                        Console.WriteLine($"{_fakeTime.GetFormattedTimeString()} - {Train.Name} has arrived at {_nextStation.Name}");
                        stop.HasDeparted = false;
                        stop.HasArrived = true;
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
    }
}
