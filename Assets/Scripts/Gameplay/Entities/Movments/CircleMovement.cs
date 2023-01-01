using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class CircleMovement : BaseMovement
{
    [SerializeField] float _speed = 1;
    [SerializeField] float _timeYoyo = 5;
    [SerializeField] float _overrideMass = 5;


    float _timer;
    Vector3 _direction;



    public void Initialize(EnemyController enemy, Vector3 position, Vector3 center)
    {
        enemy.GetComponent<Rigidbody2D>().mass = _overrideMass;
        _direction = center - position;
        _direction.Normalize();
    }

    public override void Update(EnemyController enemy, Rigidbody2D rb)
    {
        _timer += Time.deltaTime;

        float sign = Mathf.Sign(Mathf.Sin((_timer / _timeYoyo) * Mathf.PI));

        rb.velocity = sign * _direction * enemy.Data.MoveSpeed;


    }
}

