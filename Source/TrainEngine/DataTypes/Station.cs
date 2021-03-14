using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TrainEngine.DataTypes
{
    public class Station
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int Distance { get; set; }

        public bool EndStation { get; set; }

        public Station(int id, string name, bool endStation)
        {
            ID = id;
            Name = name;
            EndStation = endStation;
        }

        public Station()
        {

        }
    }
    public class StationORM
    {
        public string FileName;

        public static List<Station> Stations { get; } = new List<Station>();

        public StationORM(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath).Skip(1).ToArray();
            foreach (string line in lines)
            {
                string[] columns = line.Split('|').Select(a => a.Trim()).ToArray();
                Stations.Add(new Station
                {
                    ID = int.Parse(columns[0]),
                    Name = columns[1],
                    EndStation = bool.Parse(columns[2])
                });
            }
        }
    }
}
