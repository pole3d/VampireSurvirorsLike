using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StopBubble : MonoBehaviour
{
    [SerializeField] float _speed = 8;
    [SerializeField] int _team = 0;
    [SerializeField] int _damage = 9;
    [SerializeField] GameObject _prefabHit;
    [SerializeField] GameObject _prefabTextHit;

    Vector3 _direction;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3);

        _direction = MainGameplay.Instance.Player.transform.position - transform.position;
        _direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var other = HitWithParent.GetComponent<Unit>(col);

        if (other == null)
        {
            GameObject.Destroy(gameObject);
        }
        else if (other.Team != _team)
        {
            Vector3 hitPosition = col.ClosestPoint(transform.position);

            if (_prefabHit != null)
                GameObject.Instantiate(_prefabHit, hitPosition, Quaternion.identity);
            if (_prefabTextHit != null)
            {
                GameObject go = GameObject.Instantiate(_prefabTextHit, hitPosition, Quaternion.identity);
                go.transform.DOMoveY(hitPosition.y + 1, 1).SetEase(Ease.OutCubic);
                go.GetComponent<TMP_Text>().text = $"-{(int)_damage}";

                go.transform.DOShakeScale(0.4f, 0.5f);
                go.GetComponent<TMP_Text>().DOFade(0, 0.2f).SetDelay(0.6f);
            }


            other.Hit(_damage);

            GameObject.Destroy(gameObject);

        }
    }
}