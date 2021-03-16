using System;
using Xunit;
using TrainEngine.DataTypes;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.IO;

namespace TrainEngine.Tests
{
    public class TrackOrmTests
    {
        [Fact]
        public void When_ScheduleORMLoadsFile_Validate_OneSTopForEachTrain()
        {
            //Arrange
            ScheduleData scheduleFile = new ScheduleData("Data/timetable.txt");

             //Assert
            Assert.IsType<ScheduleData>(scheduleFile);
            Assert.Equal(2, scheduleFile.Stops[0].TrainId);
            Assert.Equal(1, scheduleFile.Stops[0].StationId);
            Assert.Equal("10:20", scheduleFile.Stops[0].DepartureTime.GetFormattedTimeString());
            Assert.Equal(3, scheduleFile.Stops[3].TrainId);
            Assert.Equal(1, scheduleFile.Stops[3].StationId);
            Assert.Equal("10:23", scheduleFile.Stops[3].DepartureTime.GetFormattedTimeString());
        }

        [Fact]
        public void TestingAllElementsOfOneStationReadFromStationTxtFile_Expect_SameDataAsTextFile()
        {
            //Arrange
            StationORM stationFile = new StationORM("Data/stations.txt");

            //Assert
            Assert.IsType<StationORM>(stationFile);
            Assert.Equal(1, StationORM.Stations[0].ID);
            Assert.Equal("Stonecro", StationORM.Stations[0].Name);
            Assert.True(StationORM.Stations[0].EndStation);
        }

        [Fact]
        public void When_TrackORMLoadsFile_Verfify_TheDistanceBetweenStationsAndCrossing()
        {
            //Arrange
            StationORM stationFile = new StationORM("Data/stations.txt");
            Track track = new Track("Data/traintrack2.txt");
            track.ReadTrack();

            //Assert
            Assert.Equal(120, StationORM.Stations[1].Distance);
            Assert.Equal(130, StationORM.Stations[2].Distance);
            Assert.Equal(40, Track.newCrossing.Distance);
        }

        [Fact]
        public void Test_Save_TravelPlan()
        {
            ScheduleData scheduleFile = new ScheduleData("Data/timetable.txt");
            TrainORM trainFile = new TrainORM("Data/trains.txt");
            new StationORM("Data/stations.txt");
            Track newTrack = new Track("Data/traintrack2.txt");
            newTrack.ReadTrack();


            Train train = trainFile.Trains[1];


            // Create the schedule for the train "newTrain".
            List<TimetableStop> scheduleStops = scheduleFile.Stops.Where(stop => stop.TrainId == train.Id).ToList();
            Schedule newSchedule = new Schedule(train.Id, scheduleStops);

            // Create the travel plan for the train "newTrain".
            new TrainPlanner(train)
                .ReadSchedule(newSchedule)
                .GeneratePlan()
                .SavePlan();

            string expectedFileContent = "{\"Stations\":[{\"ID\":1,\"Name\":\"Stonecro\",\"Distance\":0,\"EndStation\":true},{\"ID\":2,\"Name\":\"Mount Juanceo\",\"Distance\":120,\"EndStation\":false},{\"ID\":3,\"Name\":\"Grand Retro\",\"Distance\":130,\"EndStation\":true}],\"Stops\":[{\"TrainId\":2,\"StationId\":1,\"DepartureTime\":{\"TickInterval\":100,\"Hours\":10,\"Minutes\":20,\"TimeThread\":null},\"ArrivalTime\":null,\"HasDeparted\":false},{\"TrainId\":2,\"StationId\":2,\"DepartureTime\":{\"TickInterval\":100,\"Hours\":12,\"Minutes\":0,\"TimeThread\":null},\"ArrivalTime\":null,\"HasDeparted\":false},{\"TrainId\":2,\"StationId\":3,\"DepartureTime\":null,\"ArrivalTime\":null,\"HasDeparted\":false}],\"Train\":{\"Id\":2,\"Name\":\"Golden Arrow\",\"Speed\":120,\"Operated\":true,\"IsRunning\":false,\"Color\":6}}";

            string actualOutputFile = File.ReadAllText(@"C:\Temp\travelplan-train2.json");

            //Assert
            Assert.Equal(expectedFileContent, actualOutputFile);
        }







        [Fact]
        public void Test_LoadTravelPlan_And_Train_Arrival()
        {
            //Arrange
            FakeTime fakeTime = new FakeTime(10, 00);
            fakeTime.TickInterval = 1;

            TravelPlan travelPlan2 = new TravelPlan();
            travelPlan2.LoadPlan("Data/travelplan-train2.json");

            travelPlan2.Simulate(fakeTime);
            fakeTime.StartTime();

            //Thread.Sleep(100);

            //Assert

            Assert.Collection(travelPlan2.Stops, a => Assert.True(a.HasArrived && a.HasDeparted));
            //Assert.Equal(1, StationORM.Stations[0].ID);
            //Assert.Equal("Stonecro", StationORM.Stations[0].Name);
            //Assert.True(StationORM.Stations[0].EndStation);
        }

        //[Fact]
        //public void TestingTick()
        //{
        //    Train train1 = new Train(2, "Golden Arrow", 120, true, ConsoleColor.DarkYellow);
        //    List<TimetableStop> scheduleStops = scheduleFile.Stops.Where(stop => stop.TrainId == train.Id).ToList();
        //    Schedule newSchedule = new Schedule(train1.Id, scheduleStops);


        //    ITravelPlan travelPlan = new TrainPlanner(train1)
        //        //.HeadTowards(station2)
        //        .ReadSchedule()
        //        .GeneratePlan();

        //    // Act


        //    // Assert
        //    Assert.IsType<TravelPlan>(travelPlan);
        //    Assert.Equal("Gothenburg", travelPlan.Stops[0].Station.Name);
        //    Assert.Equal(623, travelPlan.Stops[0].Time);
        //    Assert.Equal("Stockholm", travelPlan.Stops[1].Station.Name);
        //    Assert.Equal(893, travelPlan.Stops[1].Time);
        //}





        //[Fact]
        //public void When_OnlyAStationIsProvided_Expect_TheResultOnlyToContainAStationWithId1()
        //{
        //    // Arrange
        //    string track = "[1]";
        //    TrackOrm trackOrm = new TrackOrm();

        //    // Act
        //    var result = trackOrm.ParseTrackDescription(track);

        //    // Assert
        //    //Assert.IsType<Station>(result.TackPart[0]);
        //    //Station s = (Station)result.TackPart[0];
        //    //Assert.Equal(1, s.Id);
        //}

        //[Fact]
        //public void When_ProvidingTwoStationsWithOneTrackBetween_Expect_TheTrackToConcistOf3Parts()
        //{
        //    // Arrange
        //    string track = "[1]-[2]";
        //    TrackOrm trackOrm = new TrackOrm();

        //    // Act
        //    var result = trackOrm.ParseTrackDescription(track);

        //    // Assert
        //    Assert.Equal(3, result.NumberOfTrackParts);
        //}
    }
}
