namespace Mini_project.Classes;

public class PlayerQuest
{
	public Quest TheQuest;
	public bool IsCompleted;

	public PlayerQuest(Quest quest)
	{
		TheQuest = quest;
		IsCompleted = false;
	}
}