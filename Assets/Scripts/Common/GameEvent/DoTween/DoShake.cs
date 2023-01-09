using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[GameFeedback("Tween/Shake")]
public class DoShake : GameFeedback
{
    public float Duration;
    public float Strength = 1;
    public int Vibrato = 10;


    public override bool Execute(GameEventInstance gameEvent)
    {
        gameEvent.GameObject.transform.DOComplete();
        gameEvent.GameObject.transform.DOShakePosition(Duration,Strength,Vibrato);

        return true;
    }

    public override string ToString()
    {
        return $"Shake {Strength} in {Duration}s";
    }
}
