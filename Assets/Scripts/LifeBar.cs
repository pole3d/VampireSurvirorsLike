using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;

    public void SetValue(float value)
    {
        value = Mathf.Clamp01(value);
        _spriteRenderer.transform.localScale = new Vector3(value, _spriteRenderer.transform.localScale.y, _spriteRenderer.transform.localScale.z);
    }

    public void SetValue(float current , float maxValue)
    {
        float value = current / maxValue;
        value = Mathf.Clamp01(value);
        _spriteRenderer.transform.localScale = new Vector3(value, _spriteRenderer.transform.localScale.y, _spriteRenderer.transform.localScale.z);
    }

}
