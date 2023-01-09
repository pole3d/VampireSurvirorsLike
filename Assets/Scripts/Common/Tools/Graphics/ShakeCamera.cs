using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
	float _duration = 1.0f;
	float _magnitude = 0.1f;

	private IEnumerator currentCoroutine;

	public void Shake(float duration, float magnitude)
	{
		_duration = duration;
		_magnitude = magnitude;
		
		if (currentCoroutine != null)
		{
			StopCoroutine(currentCoroutine);
		}
		currentCoroutine = ShakeCoroutine();
		StartCoroutine(currentCoroutine);
	}

	private IEnumerator ShakeCoroutine()
	{
		Vector3 originalPosition = transform.localPosition;
		float elapsed = 0.0f;

		while (elapsed < _duration)
		{
			float x = Random.Range(-1f, 1f) * _magnitude;
			float y = Random.Range(-1f, 1f) * _magnitude;

			transform.localPosition = new Vector3(x, y, originalPosition.z);

			elapsed += Time.deltaTime;

			yield return null;
		}

		transform.localPosition = originalPosition;
		currentCoroutine = null;
	}
}