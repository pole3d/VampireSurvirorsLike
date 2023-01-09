using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[GameFeedback("Tween/Scale")]
public class DoScale : GameFeedback
{
    public Vector3 Intensity = Vector3.up * 0.2f;
    public float Duration = 0.4f;


    public override bool Execute(GameEventInstance gameEvent)
    {
        gameEvent.GameObject.transform.DOComplete();
        gameEvent.GameObject.transform.DOScale(Intensity, Duration);

        return true;
    }

    public override string ToString()
    {
        return $"DoScale {ShowVector3(Intensity)} in {Duration}s";
    }
}
