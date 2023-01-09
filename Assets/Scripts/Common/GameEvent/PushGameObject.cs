namespace UnityCommon.GameEvent
{
    [GameFeedback("GameObject/PushGameObject", 255, 255, 0)]
    public class PushGameObject : GameFeedback
    {
        public string Name;
        
        public override bool Execute(GameEventInstance gameEvent)
        {
            gameEvent.PushGameObject(Name);
            return true;
        }

        public override string ToString()
        {
            return $"Push {Name}";
        }
    }
}