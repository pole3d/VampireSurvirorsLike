using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[GameFeedback("Tween/PunchScale")]
public class DoPunchScale : GameFeedback
{
    public Vector3 Intensity = Vector3.up * 0.2f;
    public float Duration = 0.4f;
    public int Vibrato = 10;
    public float Elasticity = 1;

    public override bool Execute(GameEventInstance gameEvent)
    {
        var gameEventGameObject = gameEvent.GameObject;
        if (gameEventGameObject == null)
            return true;
        
        gameEventGameObject.transform.DOComplete();
        gameEventGameObject.transform.DOPunchScale(Intensity, Duration,Vibrato,Elasticity);

        return true;
    }

    public override string ToString()
    {
        return $"DoPunchScale {ShowVector3(Intensity)} in {Duration}s";
    }
}
