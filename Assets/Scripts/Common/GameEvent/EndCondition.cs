using System;
using System.Collections;
using System.Collections.Generic;
using Common.Tools;
using UnityEngine;

[Serializable]
[GameFeedback("Condition/EndCondition", 255, 255, 0)]
public class EndCondition : GameFeedback
{
    public override bool Execute(GameEventInstance gameEvent)
    {

        return true;
    }

    public override string ToString()
    {
        return "End IF ";
    }
}
