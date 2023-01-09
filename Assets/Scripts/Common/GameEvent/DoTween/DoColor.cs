using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[GameFeedback("Tween/Color")]
public class DoColor : GameFeedback
{
    public Color To;
    public float Duration;
    public int Loop = -1;


    public override bool Execute(GameEventInstance gameEvent)
    {

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(gameEvent);

        if (Duration <= float.Epsilon)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = To;
            }
            else
            {
                Graphic img = GetComponent<Graphic>(gameEvent);

                if (img != null)
                {
                    img.color = To;
                }
            }

            return true;
        }

        if (spriteRenderer != null)
        {
            Tween tween = spriteRenderer.DOColor(To, Duration);
            tween.id = gameEvent.GameObject.transform;

            if ( Loop > 0 )
            {
                tween.SetLoops(Loop, LoopType.Yoyo);
            }
        }
        else
        {
            Graphic graphic = GetComponent<Graphic>(gameEvent);

            if (graphic != null)
            {
                graphic.DOComplete();
                Tween tween = graphic.DOColor(To, Duration).SetLoops(Loop, LoopType.Yoyo);
                if (tween != null) tween.id = gameEvent.GameObject.transform;
            }

        }

        return true;
    }

    public override string ToString()
    {
        return $"DoColor {To} in {Duration}s ; Loop : {Loop}";
    }
}
