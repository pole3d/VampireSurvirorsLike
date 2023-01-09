using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[GameFeedback("Tween/Move")]
public class DoMove : GameFeedback
{
    public enum DoMoveType
    {
        Absolute,
        Offset
    }

    public Vector3 Position;
    public DoMoveType Type;
    public float Duration = 0.2f;
    public bool Local;

    public override bool Execute(GameEventInstance gameEvent)
    {

        if (Type == DoMoveType.Absolute)
        {
            if ( Local)
            {
                gameEvent.GameObject.transform.DOLocalMove(Position, Duration).SetEase(Ease.Linear);
            }
            else
                gameEvent.GameObject.transform.DOMove(Position, Duration).SetEase(Ease.Linear);
        }
        else if (Type == DoMoveType.Offset)
        {
            if (Local)
            {
                gameEvent.GameObject.transform.DOLocalMove(gameEvent.GameObject.transform.localPosition + Position, Duration).SetEase(Ease.Linear);
            }
            else
            {
                gameEvent.GameObject.transform.DOMove(gameEvent.GameObject.transform.position + Position, Duration).SetEase(Ease.Linear);
            }
        }


        return true;
    }

    public override string ToString()
    {
        return $"DoMove {Type} To {ShowVector3(Position)} in {Duration}s";
    }
}
