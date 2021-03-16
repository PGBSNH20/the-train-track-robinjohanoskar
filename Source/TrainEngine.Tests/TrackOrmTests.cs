using System;
using Xunit;
using TrainEngine.DataTypes;
using System.Collections.Generic;

namespace TrainEngine.Tests
{
    public class TrackOrmTests
    {
        [Fact]
        //public void When_OnlyAStationIsProvided_Expect_TheResultOnlyToContainAStationWithId1()
        public void When_ScheduleORMLoadsFile_Validate_OneSTopForEachTrain()
        {
            ScheduleData scheduleFile = new ScheduleData("Data/timetable.txt");
            // Act

             //Assert
            Assert.IsType<ScheduleData>(scheduleFile);
            Assert.Equal(2, scheduleFile.Stops[0].TrainId);
            Assert.Equal(1, scheduleFile.Stops[0].StationId);
            Assert.Equal("10:20", scheduleFile.Stops[0].DepartureTime.GetFormattedTimeString());
            Assert.Equal(3, scheduleFile.Stops[3].TrainId);
            Assert.Equal(1, scheduleFile.Stops[3].StationId);
            Assert.Equal("10:23", scheduleFile.Stops[3].DepartureTime.GetFormattedTimeString());
        }

        //[Fact]
        //public void TwoStations()
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
