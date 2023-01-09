using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

[GameFeedback("Tween/Hit")]
class DoHit : GameFeedback
{
    public Color Color = Color.white;
    public string Property = "_Black";
    public float Duration = 0.1f;

    public override bool Execute(GameEventInstance gameEvent)
    {
        Renderer renderer = GetComponent<Renderer>(gameEvent);

        if ( renderer.material.HasProperty(Property))
            renderer.material.DOBlendableColor(Color, Property, Duration).SetLoops(2, LoopType.Yoyo);

        return true;
    }
}

