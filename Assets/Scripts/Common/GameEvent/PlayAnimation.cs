using System;
using System.Collections;
using System.Collections.Generic;
using UnityCommon;
using UnityEngine;

[Serializable]
public class PlayAnimation : GameFeedback
{
    public string Name;

    public override bool Execute(GameEventInstance gameEvent)
    {
        gameEvent.GameObject.GetComponent<Animation>().Play(Name);

        return true;
    }

    public override string ToString()
    {
        return $"Play animation {Name}";
    }
}
