using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityCommon;
using UnityEngine;

[Serializable]
public class ExecuteGameEvent : GameFeedback
{
    public GameEvent Event;

    public override bool Execute(GameEventInstance gameEvent)
    {
        GameEventInstance instance = GameEventsManager.PlayEvent(Event, gameEvent.GameObject,gameEvent._gameObjectParameters.Values.ToArray());

        return true;
    }

    public override string ToString()
    {
        if ( Event != null )
            return $"Execute {Event.name}";

        return "Execute nothing";
    }
}
