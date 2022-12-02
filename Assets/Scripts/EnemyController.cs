using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Unit
{
    public float Speed = 4;

    private GameObject _player;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    public void Initialize( GameObject player , EnemyData data)
    {
        _player = player;
        Team = 1;
        Speed = data.MoveSpeed;
        Life = data.Life;
    }

    void FixedUpdate()
    {
        MoveToPlayer();

    }

    private void MoveToPlayer()
    {
        Vector3 direction = _player.transform.position - transform.position;
        direction.z = 0;

        float moveStep = Speed * Time.deltaTime;
        if (moveStep >= direction.magnitude)
        {
            _rb.velocity = Vector2.zero;
            transform.position = _player.transform.position;

        }
        else
        {
            direction.Normalize();
            _rb.velocity = direction * Speed;
        }

    }

    public override void Hit(float damage)
    {
        Life -= damage;

        if (Life <= 0)
        {
            MainGameplay.Instance.Enemies.Remove(this);
            GameObject.Destroy(gameObject);
        }
    }
}
