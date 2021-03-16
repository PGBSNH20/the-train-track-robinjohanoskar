using System.Collections.Generic;
using TrainEngine;
using TrainEngine.DataTypes;

namespace TrainConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ScheduleORM scheduleFile = new ScheduleORM("Data/timetable.txt");
            TrainORM trainFile = new TrainORM("Data/trains.txt");
            StationORM stationFile = new StationORM("Data/stations.txt");
            TrackORM newTrack = new TrackORM("Data/traintrack2.txt");
            newTrack.ReadTrack(stationFile.Stations);

            List<ITravelPlan> travelPlans = new List<ITravelPlan>();

            foreach (Train train in trainFile.Trains)
            {
                // Create the travel plan for the train "newTrain".
                ITravelPlan travelPlan = new TrainPlanner(train)
                    .AddStations(stationFile.Stations)
                    .ReadSchedule(scheduleFile.Timetable)
                    .GeneratePlan();
                    
                // Save the travel plan to file
                travelPlan.SavePlan();

                // Save the travel to a list
                travelPlans.Add(travelPlan);
            }

            // todo fix bug with the file not being found
            TravelPlan travelPlan1 = new TravelPlan();
            travelPlan1.LoadPlan(@"C:\Temp\travelplan-train2.json");

            // Create a fakeTime object which we can send into the travel plan "simulator".
            FakeTime fakeTime = new FakeTime(10, 00);

            foreach (var travelPlan in travelPlans)
            {
                travelPlan.Simulate(fakeTime);
            }

            // Start the time after all travelplan simulations have been instantiated.
            fakeTime.StartTime();
        }
    }
}
