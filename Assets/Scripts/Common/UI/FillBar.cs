using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the XP bar in the UI
/// </summary>
public class FillBar : MonoBehaviour
{
	[SerializeField] TMP_Text _text;
	[SerializeField] Image _imageFill;

	void Start()
	{
		_imageFill.fillAmount = 0;
	}

	public void SetValue(float current , float minValue , float maxValue)
	{
		float value = (current - minValue) / (maxValue-minValue);
		_imageFill.fillAmount = Mathf.Clamp01(value);;
	}

	public void SetText(string text)
	{
		_text.text = text;
	}
}
