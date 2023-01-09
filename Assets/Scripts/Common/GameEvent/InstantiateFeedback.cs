using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
[GameFeedback("GameObject/Instantiate", 255, 255, 0)]
public class InstantiateFeedback : GameFeedback
{
    public GameObject Prefab;
    public bool SetParent;

    public override bool Execute(GameEventInstance gameEvent)
    {
        GameObject go = Object.Instantiate(Prefab, gameEvent.GameObject.transform.position, Quaternion.identity);
        go.name = Prefab.name;
        
        if ( SetParent)
            go.transform.SetParent(gameEvent.GameObject.transform);

        gameEvent.PushGameObject(go);
        
        return true;
    }

    public override string ToString()
    {
        if ( Prefab == null )
            return $"Instantiate null";

        return $"Instantiate {Prefab.name}";
    }
}
