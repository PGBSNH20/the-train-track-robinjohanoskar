using System;
using System.Collections.Generic;
using System.Linq;
using TrainConsole;
using TrainEngine;
using TrainEngine.DataTypes;

namespace TrainConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ScheduleData scheduleFile = new ScheduleData("Data/timetable.txt");
            TrainData trainFile = new TrainData("Data/trains.txt");
            //FileData passengerFile = new FileData("Data/passengers.txt", ';');
            //FileData stationFile = new FileData("Data/stations.txt", '|');
            Track newTrack = new Track("Data/traintrack2.txt");

            newTrack.ReadTrack();

            foreach (string s in newTrack.TrackOrder)
            {
                Console.WriteLine(s);
            }

            List<ITravelPlan> travelPlans = new List<ITravelPlan>();

            foreach (Train train in trainFile.Trains)
            {
                // Create the schedule for the train "newTrain".
                List<TimetableStop> scheduleStops = scheduleFile.Stops.Where(stop => stop.TrainId == train.Id).ToList();
                Schedule newSchedule = new Schedule(train.Id, scheduleStops);

                // Create the travel plan for the train "newTrain".
                ITravelPlan travelPlan = new TrainPlanner(train)
                    .ReadSchedule(newSchedule)
                    //.LevelCrossing()
                    //.CloseAt("10:23")
                    //.OpenAt("10:25")
                    //.SetSwitch(switch1, SwitchDirection.Left)
                    //.SetSwitch(switch2, SwitchDirection.Right)
                    .GeneratePlan();

                // Save "travelPlan" to a list:
                travelPlans.Add(travelPlan);
            }

            FakeTime fakeTime = new FakeTime(10, 0);

            foreach (var travelPlan in travelPlans)
            {
                travelPlan.Simulate(fakeTime);
            }

            fakeTime.StartTime();

        /* --- Old code --------------------------------------------- */

            // Old Stuff:
            //Console.WriteLine("Train track!");
            // Step 1:
            // Parse the traintrack (Data/traintrack.txt) using ORM (see suggested code)
            // Parse the trains (Data/trains.txt)

            // Step 2:
            // Make the trains run in treads

            //ITrainPlanner newPlanner = new TrainPlanner().StartTrainAt("10:23").StopTrainAt("15:09").ToPlan();



            //Train train1 = new Train(1, "Flying Scotsman", 100, false);
            //Station station1 = new Station("Gothenburg");
            //Station station2 = new Station("Stockholm");

            //ITravelPlan travelPlan = new TrainPlanner(train1, station1)
            //          //.HeadTowards(station2)
            //          //.ReadSchedule()
            //          .StartTrainAt("10:23", true)
            //          .StopTrainAt(station2, "14:53")
            //          .GeneratePlan();

            //foreach (Stop s in travelPlan.Stops)
            //{
            //    Console.WriteLine($"{s.Station.Name} {s.Time}");
            //}

            //Console.WriteLine(scheduleFile.FileLines[0][2]);

            //Console.ReadLine();

            ////travelPlan.Start();
        }
    }
}
