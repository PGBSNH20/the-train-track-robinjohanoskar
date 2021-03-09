using System;
using System.Collections.Generic;
using System.Linq;
using TrainConsole;
using TrainEngine;

namespace TrainConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ScheduleDataFile scheduleFile = new ScheduleDataFile("Data/timetable.txt");
            FileData passengerFile = new FileData("Data/passengers.txt", ';');
            FileData stationFile = new FileData("Data/stations.txt", '|');
            FileData trainFile = new FileData("Data/trains.txt", ',');

            if (trainFile.FileLines.Count > 1)
            {
                for (int y = 1; y < trainFile.FileLines.Count; y++)
                {
                    string[] trainData = trainFile.FileLines[y];
                    int id;
                    int maxSpeed;
                    bool operated;

                    // Note: Should we move the code below which parses the train rows, to its own class (i.e. "TrainsDataFile")? Like we do with the "Data/timetable.txt" file in the "ScheduleDataFile"-class?
                    try
                    {
                        id = int.Parse(trainData[0]);
                        maxSpeed = int.Parse(trainData[2]);
                        operated = bool.Parse(trainData[3]);

                        Train newTrain = new Train(id, trainData[1], maxSpeed, operated);

                        // Create the schedule for the train "newTrain".
                        List<TimetableStop> scheduleStops = scheduleFile.Stops.Where(stop => stop.TrainId == newTrain.Id).ToList();
                        Schedule newSchedule = new Schedule(newTrain.Id, scheduleStops);

                        // Create the travel plan for the train "newTrain".
                        ITravelPlan travelPlan3 = new TrainPlanner(newTrain)
                                .ReadSchedule(newSchedule)
                                //.LevelCrossing()
                                //.CloseAt("10:23")
                                //.OpenAt("10:25")
                                //.SetSwitch(switch1, SwitchDirection.Left)
                                //.SetSwitch(switch2, SwitchDirection.Right)
                                .GeneratePlan();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }


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
