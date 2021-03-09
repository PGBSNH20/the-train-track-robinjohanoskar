using System;
using TrainConsole;
using TrainEngine;

namespace TrainConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Old Stuff:
            //Console.WriteLine("Train track!");
            // Step 1:
            // Parse the traintrack (Data/traintrack.txt) using ORM (see suggested code)
            // Parse the trains (Data/trains.txt)

            // Step 2:
            // Make the trains run in treads

            //ITrainPlanner newPlanner = new TrainPlanner().StartTrainAt("10:23").StopTrainAt("15:09").ToPlan();

            // New stuff:
            Train train1 = new Train("Name of train");
            Station station1 = new Station("Gothenburg");
            Station station2 = new Station("Stockholm");

            ITravelPlan travelPlan = new TrainPlanner(train1, station1)
                      //.HeadTowards(station2)
                      .StartTrainAt("10:23")
                      .StopTrainAt(station2, "14:53")
                      .GeneratePlan();

            travelPlan.Start();
        }
    }
}
