using System;
using System.Collections.Generic;
using System.Text;

namespace TrainEngine
{
	interface ITrainPlanner
	{
		ITrainPlanner StartTrainAt(string startTrain);
		ITrainPlanner StopTrainAt(string stopTrain);
		TravelPlan ToPlan();
	}

	class TrainPlanner
    {
		ITrainPlanner StartTrainAt(string startTrain);
		ITrainPlanner StopTrainAt(string stopTrain);
		TravelPlan ToPlan();
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
