using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Represents a bullet moving in a given direction
/// A bullet can be fired by the player or by an enemy
/// call Initialize to set the direction
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed = 10;
    [SerializeField] int _team;
    [SerializeField] float _damage = 5;
    [SerializeField] float _timeToLive = 10.0f;

    Vector3 _direction;

    public void Initialize(Vector3 direction)
    {
        _direction = direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, _timeToLive);
    }

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
            GameObject.Destroy(gameObject);

            other.Hit(_damage);
        }
    }
}