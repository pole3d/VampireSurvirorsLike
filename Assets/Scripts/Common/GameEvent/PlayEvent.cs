using System;
using System.Collections;
using System.Collections.Generic;
using UnityCommon;
using UnityEngine;

[Serializable]
public class PlayEvent : GameFeedback
{
    public string Name;

    public override bool Execute(GameEventInstance gameEvent)
    {
        GlobalEvent.Play(Name);

        return true;
    }

    public override string ToString()
    {
        return $"Play event {Name}";
    }
}
