namespace DungeonsSample.Quests
{
    public class InteractWithCanvasQuest : Quest
    {
        public void OnInteracted() => IsComplete = true;
    }
}
