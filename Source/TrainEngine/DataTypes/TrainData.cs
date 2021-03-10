using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TrainEngine.DataTypes
{
    public class Train
    {
        public int Id;
        public string Name;
        public int MaxSpeed;
        public bool Operated;

        public Train(int id, string name, int maxSpeed, bool operated)
        {
            Id = id;
            Name = name;
            MaxSpeed = maxSpeed;
            Operated = operated;
        }
    }

    public class TrainData
    {
        public List<Train> Trains { get; set; } = new List<Train>();
        public TrainData(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] columns = lines[i].Split(',').Select(a => a.Trim()).ToArray();

                try
                {
                    int id = int.Parse(columns[0]);
                    int maxSpeed = int.Parse(columns[2]);
                    bool operated = bool.Parse(columns[3]);

                    Train newTrain = new Train(id, columns[1], maxSpeed, operated);

                    Trains.Add(newTrain);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}