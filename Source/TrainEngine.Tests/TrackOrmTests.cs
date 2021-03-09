using System;
using Xunit;

namespace TrainEngine.Tests
{
    public class TrackOrmTests
    {
        [Fact]
        public void TwoStations()
        {
            // Arrange
            Train train1 = new Train("Name of train");
            Station station1 = new Station("Gothenburg");
            Station station2 = new Station("Stockholm");

            ITravelPlan travelPlan = new TrainPlanner(train1, station1)
                //.HeadTowards(station2)
                .StartTrainAt("10:23")
                .StopTrainAt(station2, "14:53")
                .GeneratePlan();

            // Act


            // Assert
            Assert.IsType<TravelPlan>(travelPlan);
            Assert.Equal("Gothenburg", travelPlan.Stops[0].Station.Name);
            Assert.Equal(623, travelPlan.Stops[0].Time);
            Assert.Equal("Stockholm", travelPlan.Stops[1].Station.Name);
            Assert.Equal(893, travelPlan.Stops[1].Time);
        }



        [Fact]
        public void When_OnlyAStationIsProvided_Expect_TheResultOnlyToContainAStationWithId1()
        {
            // Arrange
            string track = "[1]";
            TrackOrm trackOrm = new TrackOrm();

            // Act
            var result = trackOrm.ParseTrackDescription(track);

            // Assert
            //Assert.IsType<Station>(result.TackPart[0]);
            //Station s = (Station)result.TackPart[0];
            //Assert.Equal(1, s.Id);
        }

        [Fact]
        public void When_ProvidingTwoStationsWithOneTrackBetween_Expect_TheTrackToConcistOf3Parts()
        {
            // Arrange
            string track = "[1]-[2]";
            TrackOrm trackOrm = new TrackOrm();
            
            // Act
            var result = trackOrm.ParseTrackDescription(track);

            // Assert
            Assert.Equal(3, result.NumberOfTrackParts);
        }
    }
}
