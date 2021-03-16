using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TrainEngine.DataTypes
{
    public class Train
    {
        public int Id;
        public string Name;
        // Speed: speed in km/h
        public int Speed;
        [JsonIgnore]
        public bool Operated;
        [JsonIgnore]
        public bool IsRunning;
        public ConsoleColor Color;

        public Train(int id, string name, int speed, bool operated, ConsoleColor color)
        {
            Id = id;
            Name = name;
            Speed = speed;
            Operated = operated;
            IsRunning = false;
            Color = color;
        }
    }

    public class TrainORM
    {
        public List<Train> Trains { get; set; } = new List<Train>();
        public TrainORM(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] columns = lines[i].Split(',').Select(a => a.Trim()).ToArray();

                try
                {
                    int id = int.Parse(columns[0]);
                    int speed = int.Parse(columns[2]);
                    bool operated = bool.Parse(columns[3]);
                    // todo: use TryParse instead?
                    ConsoleColor color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), columns[4].Trim('"'), true);

                    Train newTrain = new Train(id, columns[1], speed, operated, color);

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