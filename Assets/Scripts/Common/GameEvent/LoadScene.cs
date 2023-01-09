using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityCommon;
using UnityEngine;

[Serializable]
public class LoadScene : GameFeedback
{
    public string Name;
    
    public override bool Execute(GameEventInstance gameEvent)
    {
        ScenesManagement.Instance.LoadScene(Name);

        return true;
    }

    public override string ToString()
    {
        return $"Load scene {Name}";
    }
}
