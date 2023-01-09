using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[GameFeedback( "GameObject/Activate", 255,0,0)]
public class ActivateFeedback : GameFeedback
{
    public bool Activate;

    public override bool Execute(GameEventInstance gameEvent)
    {
        if (Activate == false)
            gameEvent.AddParameter(gameEvent.GameObject); 
        
        gameEvent.GameObject.SetActive(Activate);

        return true;
    }

    public override string ToString()
    {
        return Activate?"Activate":"Deactivate";
    }
}
