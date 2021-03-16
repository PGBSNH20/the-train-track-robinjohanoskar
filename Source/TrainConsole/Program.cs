﻿using System;
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
            TrainORM trainFile = new TrainORM("Data/trains.txt");
            //FileData passengerFile = new FileData("Data/passengers.txt", ';');
            //StationORM stationFile = new StationORM("Data/stations.txt");
            new StationORM("Data/stations.txt");
            Track newTrack = new Track("Data/traintrack2.txt");
            newTrack.ReadTrack();

            List<ITravelPlan> travelPlans = new List<ITravelPlan>();

            foreach (Train train in trainFile.Trains)
            {
                // Create the travel plan for the train "newTrain".
                ITravelPlan travelPlan = new TrainPlanner(train)
                    .ReadSchedule(scheduleFile.Stops)
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

            //Console.WriteLine($"TrainId; StationId; ArrivalTime; DepartureTime");
            foreach (var travelPlan in travelPlans)
            {
                //Console.WriteLine($"Travel plan for train: ({travelPlan.Train.Id}) {travelPlan.Train.Name}");
                foreach (var stop in travelPlan.Stops)
                {
                    //Console.WriteLine($"{stop.TrainId}; {stop.StationId}; {stop.ArrivalTime.GetFormattedTimeString()}; {stop.DepartureTime != null ? stop.DepartureTime.GetFormattedTimeString()}");
                    //Console.WriteLine();
                    //Console.WriteLine();
                }
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
