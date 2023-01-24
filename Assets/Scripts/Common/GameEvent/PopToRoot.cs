namespace UnityCommon.GameEvent
{
    [GameFeedback("GameObject/PopToRoot", 255, 0, 0)]
    public class PopToRoot : GameFeedback
    {
        
        public override bool Execute(GameEventInstance gameEvent)
        {
            gameEvent.PopToRoot();
            return true;
        }

        public override string ToString()
        {
            return $"PopToRoot GameObject";
        }
    }
}