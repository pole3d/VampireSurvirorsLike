using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
[GameFeedback("GameObject/Destroy", 255, 0, 0)]
public class DestroyFeedback : GameFeedback
{
    public string Name;

    public override bool Execute(GameEventInstance gameEvent)
    {
        Transform transform = gameEvent.GameObject.transform.Find(Name);

        if (transform != null)
        {
            Object.Destroy(transform.gameObject);
        }
        else
        {
            Debug.LogError($"Can't find {Name} in {gameEvent.GameObject.name}");
        }
        
        
        return true;
    }

    public override string ToString()
    {
        return $"Destroy {Name}";
    }
}
