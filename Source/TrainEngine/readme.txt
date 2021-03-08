# Our train project
What we have done can be explained by this mindmap.
![Mindmap of train track](mindmap.jpg)
Bla bla bla bla

# 2021-03-08 14:00
![Level crossing with bars](crossing.png)

var travelPlan1 = new TrainPlaner(train1).FollowSchedule(scheduleTrain1).LevelCrossing().CloseAt("10:23").OpenAt("10:25").SetSwitch(switch1, SwitchDirection.Left).SetSwitch(switch2, SwitchDirection.Right).ToPlan();

var travelPlan2 = new TrainPlaner(train2).StartTrainAt("10:23").StopTrainAt("10:53").ToPlan();

*[1]------------[2]

Interface
{
	StartTrainAt()
	StopTrainAt()
	ToPlan();
}

TrainPlaner klass
{
	StartTrainAt(string tid)
	{
	}

	StopTrainAt("10:53")

	ToPlan();
}

Tåg klass
{
	int	Id
	string	Name
	int	MaxSpeed
	bool	Operated
}