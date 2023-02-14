namespace Mini_project;

public class PlayerQuest
{
    public Quest TheQuest;
    public bool IsCompleted;

    public PlayerQuest(Quest quest)
    {
        this.TheQuest = quest;
        this.IsCompleted = false;
    }
}