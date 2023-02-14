namespace Mini_project;

public class PlayerQuest
{
    public Quest TheQuest;
    public bool IsCompleted;

    public PlayerQuest()
    {
        this.TheQuest = new Quest();
        this.IsCompleted = false;
    }
}