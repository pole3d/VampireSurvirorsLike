using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[GameFeedback("Tween/Fade")]
public class DoFade : GameFeedback
{
    public float To;
    public float Duration = 0.2f;

    public override bool Execute(GameEventInstance gameEvent)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(gameEvent);

        if ( To <= float.Epsilon && Duration <= float.Epsilon)
        {
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                spriteRenderer.color = new Color(color.r, color.g, color.b, 0);
            }
            else
            {
                CanvasGroup group = GetComponent<CanvasGroup>(gameEvent);
                if (group != null)
                {
                    group.alpha = 0;
                }
                else
                {
                    Graphic img = GetComponent<Graphic>(gameEvent);

                    if (img != null)
                    {
                        Color color = img.color;
                        img.color = new Color(color.r, color.g, color.b, 0);
                    }
                }
            }

            return true;
        }

        if (spriteRenderer != null)
        {
            Tween tween = spriteRenderer.DOFade(To, Duration);
            tween.id = gameEvent.GameObject.transform;
        }
        else
        {
            CanvasGroup group = GetComponent<CanvasGroup>(gameEvent);
            if (group != null)
            {
                Tween tween = group.DOFade(To, Duration);
                tween.id = gameEvent.GameObject.transform;

            }
            else
            {
                Tween tween = GetComponent<Graphic>(gameEvent)?.DOFade(To, Duration);
                if (tween != null) tween.id = gameEvent.GameObject.transform;
            }
        }

        return true;
    }

    public override string ToString()
    {
        return $"DoFade To {To} in {Duration}s";
    }
}
