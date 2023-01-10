using System;
using System.Collections;
using System.Collections.Generic;
using UnityCommon;
using UnityEngine;

[Serializable]
public class StopGameObject : GameFeedback
{
    public bool IsStopped;

    public override bool Execute(GameEventInstance gameEvent)
    {
        IStoppable stoppable = gameEvent.GameObject.GetComponent<IStoppable>();
        
        if ( stoppable != null )
            stoppable.IsStopped = IsStopped;

        return true;
    }

    public override string ToString()
    {
        if ( IsStopped)
            return $"STOP";
        else
        {
            return "UNSTOP";
        }
    }
}
