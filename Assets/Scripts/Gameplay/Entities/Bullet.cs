using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Represents a bullet moving in a given direction
/// A bullet can be fired by the player or by an enemy
/// call Initialize to set the direction
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] int _team;
    [SerializeField] float _timeToLive = 10.0f;
    [SerializeField] GameObject _prefabHit;
    [SerializeField] GameObject _prefabTextHit;

    float _speed = 10;
    float _damage = 5;
    Vector3 _direction;
    bool _canMove = true;
    WeaponBase _weapon;
    int _troughEnemyCount;

    public void Initialize(WeaponBase weapon,  Vector3 direction , float damage , float speed )
    {
        _weapon = weapon;
        _troughEnemyCount = _weapon.ThroughEnemyCount;

        if (Mathf.Abs(direction.x) > 0.001f || Mathf.Abs(direction.y) > 0.001f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) - Mathf.PI / 2.0f;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle);
            _direction = direction;
        }
        else
            _canMove = false;

        _speed = speed;
        _damage = damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, _timeToLive);
    }

    void Update()
    {
        if (_canMove == false)
            return;

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
            _troughEnemyCount--;

            if ( _prefabHit != null)
                GameObject.Instantiate(_prefabHit, transform.position,Quaternion.identity);
            if (_prefabTextHit != null)
            {
                GameObject go =   GameObject.Instantiate(_prefabTextHit, transform.position, Quaternion.identity);
                go.transform.DOLocalMoveY(transform.localPosition.y + 1,1).SetEase( Ease.OutCubic);
                go.GetComponent<TMP_Text>().text = $"-{(int)_damage}";

                go.transform.DOShakeScale(0.4f, 0.5f);
                go.GetComponent<TMP_Text>().DOFade(0, 0.2f).SetDelay(0.6f);
            }


            _weapon.OnHit(other ,this);
            other.Hit(_damage);

            if (_troughEnemyCount <= 0)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}