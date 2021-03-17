using System.IO;
using System.Linq;
using TrainEngine.DataTypes;
using Xunit;

namespace TrainEngine.Tests
{
    public class TrackOrmTests
    {
        [Fact]
        public void When_ScheduleORMLoadsFile_Validate_OneSTopForEachTrain()
        {
            //Arrange
            TimetableORM timetableFile = new TimetableORM("Data/timetable.txt");

             //Assert
            Assert.IsType<TimetableORM>(timetableFile);
            Assert.Equal(2, timetableFile.Timetable[0].TrainId);
            Assert.Equal(1, timetableFile.Timetable[0].StationId);
            Assert.Equal("10:20", timetableFile.Timetable[0].DepartureTime.ToString());
            Assert.Equal(3, timetableFile.Timetable[3].TrainId);
            Assert.Equal(1, timetableFile.Timetable[3].StationId);
            Assert.Equal("10:23", timetableFile.Timetable[3].DepartureTime.ToString());
        }

        [Fact]
        public void TestingAllElementsOfOneStationReadFromStationTxtFile_Expect_SameDataAsTextFile()
        {
            //Arrange
            StationORM stationFile = new StationORM("Data/stations.txt");

            //Assert
            Assert.IsType<StationORM>(stationFile);
            Assert.Equal(1, stationFile.Stations[0].ID);
            Assert.Equal("Stonecro", stationFile.Stations[0].Name);
            Assert.True(stationFile.Stations[0].EndStation);
        }

        [Fact]
        public void When_TrackORMLoadsFile_Verfify_TheDistanceBetweenStationsAndCrossing()
        {
            //Arrange
            StationORM stationFile = new StationORM("Data/stations.txt");
            TrackORM track = new TrackORM("Data/traintrack2.txt", stationFile.Stations);

            //Assert
            Assert.Equal(120, stationFile.Stations[1].Distance);
            Assert.Equal(130, stationFile.Stations[2].Distance);
            Assert.Equal(40, TrackORM.newCrossing.Distance);
        }

        [Fact]
        public void Test_Save_TravelPlan()
        {
            //Arrange
            StationORM stationFile = new StationORM("Data/stations.txt");
            TimetableORM timetableFile = new TimetableORM("Data/timetable.txt");
            TrainORM trainFile = new TrainORM("Data/trains.txt");
            new StationORM("Data/stations.txt");
            TrackORM newTrack = new TrackORM("Data/traintrack2.txt", stationFile.Stations);
            Train train = trainFile.Trains[1];

            // Create the travel plan and save it to file.
            new TrainPlanner(train)
                .AddStations(stationFile.Stations)
                .AddTimetable(timetableFile.Timetable.Where(stop => stop.TrainId == train.Id).ToList())
                .GeneratePlan()
                .SavePlan();

            string expectedFileContent = "{\"Stations\":[{\"ID\":1,\"Name\":\"Stonecro\",\"Distance\":0,\"EndStation\":true},{\"ID\":2,\"Name\":\"Mount Juanceo\",\"Distance\":120,\"EndStation\":false},{\"ID\":3,\"Name\":\"Grand Retro\",\"Distance\":130,\"EndStation\":true}],\"Timetable\":[{\"TrainId\":2,\"StationId\":1,\"DepartureTime\":{\"TickInterval\":100,\"Hours\":10,\"Minutes\":20,\"TimeThread\":null},\"ArrivalTime\":null,\"HasDeparted\":false},{\"TrainId\":2,\"StationId\":2,\"DepartureTime\":{\"TickInterval\":100,\"Hours\":12,\"Minutes\":0,\"TimeThread\":null},\"ArrivalTime\":null,\"HasDeparted\":false},{\"TrainId\":2,\"StationId\":3,\"DepartureTime\":null,\"ArrivalTime\":null,\"HasDeparted\":false}],\"Train\":{\"Id\":2,\"Name\":\"Golden Arrow\",\"Speed\":120,\"Color\":6}}";

            string actualOutputFile = File.ReadAllText(@"C:\Temp\travelplan-train2.json");

            //Assert
            Assert.Equal(expectedFileContent, actualOutputFile);
        }

        // todo: try to fix this test
        //[Fact]
        //public void Test_LoadTravelPlan_And_Train_Arrival()
        //{
        //    //Arrange
        //    FakeTime fakeTime = new FakeTime(10, 00);
        //    fakeTime.TickInterval = 1;

        //    TravelPlan travelPlan2 = new TravelPlan();
        //    travelPlan2.LoadPlan("Data/travelplan-train2.json");

        //    travelPlan2.Simulate(fakeTime);
        //    fakeTime.StartTime();

        //    // note: Doesn't because of thread.sleep(0)
        //    //Thread.Sleep(100);

        //    //Assert
        //    Assert.Collection(travelPlan2.Timetable, a => Assert.True(a.HasArrived && a.HasDeparted));
        //    //Assert.Equal(1, StationORM.Stations[0].ID);
        //    //Assert.Equal("Stonecro", StationORM.Stations[0].Name);
        //    //Assert.True(StationORM.Stations[0].EndStation);
        //}
    }
}
