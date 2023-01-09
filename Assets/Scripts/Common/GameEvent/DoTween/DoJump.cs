using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[GameFeedback("Tween/Jump")]
public class DoJump : GameFeedback
{
    public enum DoMoveType
    {
        Absolute,
        Offset
    }

    public Vector3 Position;
    public DoMoveType Type;
    public float Duration = 0.2f;
    public float JumpPower;
    public bool Local;

    public override bool Execute(GameEventInstance gameEvent)
    {

        if (Type == DoMoveType.Absolute)
        {
            if ( Local)
            {
                gameEvent.GameObject.transform.DOLocalJump(Position, JumpPower ,1,Duration).SetEase(Ease.Linear);
            }
            else
                gameEvent.GameObject.transform.DOJump(Position,JumpPower,1, Duration).SetEase(Ease.Linear);
        }
        else if (Type == DoMoveType.Offset)
        {
            if (Local)
            {
                gameEvent.GameObject.transform.DOLocalJump(gameEvent.GameObject.transform.localPosition + Position,JumpPower,1, Duration).SetEase(Ease.Linear);
            }
            else
            {
                gameEvent.GameObject.transform.DOJump(gameEvent.GameObject.transform.position + Position,JumpPower,1,  Duration).SetEase(Ease.Linear);
            }
        }


        return true;
    }

    public override string ToString()
    {
        return $"DoJump {Type} To {ShowVector3(Position)} in {Duration}s";
    }
}
