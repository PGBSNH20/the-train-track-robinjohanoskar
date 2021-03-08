using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TrainEngine
{
	public interface ITrainPlanner
	{
		ITrainPlanner StartTrainAt(string startTrain);
		ITrainPlanner StopTrainAt(string stopTrain);
        ITrainPlanner ToPlan();
    }

	public class TrainPlanner : ITrainPlanner
	{
		public int StartTime;
		public int StopTime;

		public ITrainPlanner StartTrainAt(string startTrain)
		{
			int[] time = startTrain.Split(':').Select(a => int.Parse(a)).ToArray();

			StartTime = time[0] * 60 + time[1];

            return this;
        }

		public ITrainPlanner StopTrainAt(string stopTrain)
        {
			int[] time = stopTrain.Split(':').Select(a => int.Parse(a)).ToArray();

			StopTime = time[0] * 60 + time[1];

			return this;
		}

        public ITrainPlanner ToPlan()
        {

            Console.WriteLine(StartTime + " " + StopTime);

			return this;
        }
    }

	class TravelPlan
	{

	}

	class Train
	{
		int Id;
		string Name;
		int MaxSpeed;
		bool Operated;
	}
}
