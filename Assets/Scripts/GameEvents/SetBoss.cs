using System;
using UnityEngine;

[Serializable]
[GameFeedback("King/SetBoss")]
public class SetBoss : GameFeedback
{

    public SetBoss() : base()
    {


    }

    public override bool Execute(GameEventInstance gameEvent)
    {
        MainGameplay.Instance.SetBoss(gameEvent.GameObject.GetComponent<EnemyController>());


        return true;
    }

}