using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[GameFeedback("Wait/Timer", 0, 255, 0)]
public class WaitFeedback : GameFeedback
{
    public float Timer;

    public override bool Execute(GameEventInstance gameEvent)
    {
        gameEvent.AddValue("Time", Time.deltaTime);

        if ( gameEvent.GetValue("Time") >= Timer )
        {
            gameEvent.SetValue("Time", 0);
            return true;
        }

        return false;
    }

    public override string ToString()
    {
        return $"Wait {Timer}s";
    }
}
