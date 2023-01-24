using System;
using System.Collections;
using System.Collections.Generic;
using UnityCommon;
using UnityEngine;

[Serializable]
public class SetContainer : GameFeedback
{
    public string Container;

    public override bool Execute(GameEventInstance gameEvent)
    {
        GameEventsManager.TryGetParameter(Container, out var container);

        container.GetComponent<IContainer>().Content = gameEvent.GameObject;

        return true;
    }

    public override string ToString()
    {
        return $"Set container for {Container}";
    }
}
