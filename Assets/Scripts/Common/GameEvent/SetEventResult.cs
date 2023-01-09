using System;
using System.Collections;
using System.Collections.Generic;
using UnityCommon;
using UnityEngine;

[Serializable]
public class SetEventResult : GameFeedback
{
    public bool Result;

    public override bool Execute(GameEventInstance gameEvent)
    {
        gameEvent.EventResult = Result;

        return true;
    }

    public override string ToString()
    {
        return $"Set result {Result}";
    }
}
