using Newtonsoft.Json;

namespace TrainEngine.DataTypes
{
    public class Crossing
    {
        [JsonIgnore]
        public bool BarClosed { get; set; }
        public int Distance { get; set; }
    }
}
