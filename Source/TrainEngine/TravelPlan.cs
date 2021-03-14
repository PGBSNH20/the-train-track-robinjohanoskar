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
                // todo: operator overloading?
                if (!stop.HasDeparted && stop.DepartureTime != null && stop.DepartureTime.Hours == _fakeTime.Hours && stop.DepartureTime.Minutes == _fakeTime.Minutes) {
                    stop.HasDeparted = true;
                    Console.WriteLine($"{Train.Name} has departed {StationORM.Stations.Find(a => a.ID == stop.StationId).Name} at {_fakeTime.Hours.ToString().PadLeft(2, '0')}:{_fakeTime.Minutes.ToString().PadLeft(2, '0')}");
                    Console.WriteLine(FakeTime.MinutesSinceStart + " minutes since start"); // testing
                }
                //if (int distance = Train.Speed * )

                // todo: operator overloading?
                if (!stop.HasArrived && stop.ArrivalTime != null && stop.ArrivalTime.Hours == _fakeTime.Hours && stop.ArrivalTime.Minutes == _fakeTime.Minutes) {
                    stop.HasArrived = true;
                    Console.WriteLine($"{Train.Name} has arrived at {stop.StationId} at {_fakeTime.Hours.ToString().PadLeft(2, '0')}:{_fakeTime.Minutes.ToString().PadLeft(2, '0')}");
                }
            }

            Tick();
        }
    }
}
