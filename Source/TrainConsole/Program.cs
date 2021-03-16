using System.Collections.Generic;
using TrainEngine;
using TrainEngine.DataTypes;

namespace TrainConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TimetableORM timetableFile = new TimetableORM("Data/timetable.txt");
            TrainORM trainFile = new TrainORM("Data/trains.txt");
            StationORM stationFile = new StationORM("Data/stations.txt");
            new TrackORM("Data/traintrack2.txt", stationFile.Stations);

            List<ITravelPlan> travelPlans = new List<ITravelPlan>();

            foreach (Train train in trainFile.Trains)
            {
                // Create the travel plan for the train "newTrain".
                ITravelPlan travelPlan = new TrainPlanner(train)
                    .AddStations(stationFile.Stations)
                    .AddTimetable(timetableFile.Timetable)
                    .GeneratePlan();
                    
                // Save the travel plan to file
                travelPlan.SavePlan();

                // Save the travel to a list
                travelPlans.Add(travelPlan);
            }

            // Create a fakeTime object which we can send into the travel plan "simulator".
            FakeTime fakeTime = new FakeTime(10, 00);

            System.Console.WriteLine("The Train Simulator");

            foreach (var travelPlan in travelPlans)
            {
                travelPlan.Simulate(fakeTime);
            }

            // Start the time after all travelplan simulations have been instantiated.
            fakeTime.StartTime();


            TravelPlan travelPlan1 = new TravelPlan();
            travelPlan1.LoadPlan(@"C:\Temp\travelplan-train2.json");
        }
    }
}
