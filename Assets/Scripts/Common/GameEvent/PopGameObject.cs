namespace UnityCommon.GameEvent
{
    [GameFeedback("GameObject/PopGameObject", 255, 0, 0)]
    public class PopGameObject : GameFeedback
    {
        
        public override bool Execute(GameEventInstance gameEvent)
        {
            gameEvent.PopGameObject();
            return true;
        }

        public override string ToString()
        {
            return $"Pop GameObject";
        }
    }
}