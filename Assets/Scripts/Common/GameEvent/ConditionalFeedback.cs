using System;
using System.Collections;
using System.Collections.Generic;
using Common.Tools;
using UnityEngine;

[Serializable]
[GameFeedback(255,255,0)]
public class ConditionalFeedback : GameFeedback
{
    [SerializeReference , Instantiable(typeof(BaseConditionFeedback))]
    public BaseConditionFeedback Condition;

    public override bool Execute(GameEventInstance gameEvent)
    {
        return Condition.Execute(gameEvent);
    }

    public override string ToString()
    {
        if (Condition == null)
            return "Condition";
        
        return Condition.ToString();
    }
}
