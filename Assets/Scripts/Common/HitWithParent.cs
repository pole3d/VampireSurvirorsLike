using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitWithParent : MonoBehaviour
{

    public static GameObject GetGameObject(Collider2D collision)
    {
        var other = collision.gameObject;
        var hitWithParent = collision.GetComponent<HitWithParent>();
        
        if (hitWithParent != null)
            other = hitWithParent.transform.parent.gameObject;

        return other;
    }
}
