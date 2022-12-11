using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    [SerializeField] TMP_Text _textLevel;
    [SerializeField] Image _imageXP;


    void Start()
    {
        _imageXP.fillAmount = 0;
        _textLevel.text = 1.ToString();
    }

    public void SetValue(float current , float minValue , float maxValue)
    {
        Debug.Log($"current{current} / minvalue{minValue} / maxValue{maxValue}");
        
        
        float value = (current - minValue) / (maxValue-minValue);
        _imageXP.fillAmount = Mathf.Clamp01(value);;
    }

    public void SetLevel(int level)
    {
        _textLevel.text = level.ToString();
    }
}
