using System;
using System.Collections;
using System.Collections.Generic;
using Common.Tools;
using UnityEngine;

[Serializable]
public abstract class BaseConditionFeedback : GameFeedback
{
    public override bool Execute(GameEventInstance gameEvent)
    {

        return true;
    }

    public override string ToString()
    {
        return "Condition ";
    }
}
