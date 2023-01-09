using System;
using System.Collections;
using System.Collections.Generic;
using UnityCommon;
using UnityEngine;

[Serializable]
[GameFeedback(255,0,255)]
public class EnableInput : GameFeedback
{
    public bool Enable;

    public override bool Execute(GameEventInstance gameEvent)
    {
        if (Enable)
        {
           // InputsManager.Unlock();
        }
        else
        {
            //InputsManager.Lock();
        }

        return true;
    }

    public override string ToString()
    {
        return Enable?"Input enabled":"Input disable";
    }
}
