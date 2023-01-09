using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[GameFeedback("Tween/Arrival")]
public class DoArrival : GameFeedback
{
    public Vector3 StartOffset = Vector3.up * 0.2f;
    public float Duration = 0.2f;
    public bool Local;

    public override bool Execute(GameEventInstance gameEvent)
    {
        if (Local)
        {
            Vector3 position = gameEvent.GameObject.transform.localPosition;
            gameEvent.GameObject.transform.localPosition += StartOffset;

            gameEvent.GameObject.transform.DOLocalMove(position, Duration).SetEase(Ease.Linear);
        }
        else
        {
            Vector3 position = gameEvent.GameObject.transform.position;
            gameEvent.GameObject.transform.position += StartOffset;

            gameEvent.GameObject.transform.DOMove(position, Duration).SetEase(Ease.Linear);
        }

        return true;
    }

    public override string ToString()
    {
        return $"DoArrival From {ShowVector3(StartOffset)} in {Duration}s ; local={Local}";
    }
}
