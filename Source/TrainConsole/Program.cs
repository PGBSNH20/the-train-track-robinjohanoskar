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

            FileData scheduleFile = new FileData("Data/timetable.txt", ',');
            //FileData passengerFile = new FileData("passengers.txt", ';');
            //FileData stationFile = new FileData("stations.txt", '|');
            //FileData trainFile = new FileData("trains.txt", ',');

            // New stuff:
            Train train1 = new Train("Name of train");
            Station station1 = new Station("Gothenburg");
            Station station2 = new Station("Stockholm");
            Schedule schedule1 = new Schedule
            {

            };

            ITravelPlan travelPlan = new TrainPlanner(train1, station1)
                      //.HeadTowards(station2)
                      //.ReadSchedule()
                      .StartTrainAt("10:23", true)
                      .StopTrainAt(station2, "14:53")
                      .GeneratePlan();

            foreach (Stop s in travelPlan.Stops)
            {
                Console.WriteLine($"{s.Station.Name} {s.Time}");
            }

            Console.WriteLine(scheduleFile.FileLines[0][2]);

            Console.ReadLine();

            //travelPlan.Start();
        }
    }
}
