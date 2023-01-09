
using UnityEngine;

class CameraShake : GameFeedback
{
    public float Duration = 0.2f;
    public float Magnitude = 0.2f;

    public override bool Execute(GameEventInstance gameEvent)
    {
        Camera.main.GetComponent<ShakeCamera>().Shake(Duration, Magnitude);
        
        return true;
    }


}

