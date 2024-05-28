namespace DungeonsSample.Quests
{
    public class InteractQuest : Quest
    {
        public void OnInteracted() => IsComplete = true;
    }
}
