using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Unit
{
    public GameObject PrefabBullet;
    public float Speed = 5;
    public LifeBar LifeBar;

    public float CoolDown = 2;

    private float _timerCoolDown;


    // Start is called before the first frame update
    void Start()
    {
        LifeMax = 50;
        Life = LifeMax;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();

    }

    private void Shoot()
    {
        _timerCoolDown += Time.deltaTime;

        if (_timerCoolDown < CoolDown)
            return;

        _timerCoolDown -= CoolDown;

        EnemyController enemy = MainGameplay.Instance.GetClosestEnemy(transform.position);
        if (enemy == null)
            return;
        
        GameObject go = GameObject.Instantiate(PrefabBullet, transform.position, Quaternion.identity);
        Vector3 direction = enemy.transform.position - transform.position;
        if (direction.sqrMagnitude > 0)
        {
            direction.Normalize();

            go.GetComponent<Bullet>().Initialize(direction);
        }

    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector2(horizontal, vertical);

        if (direction.sqrMagnitude > 0)
        {
            direction.Normalize();
            transform.position += direction * Speed * Time.deltaTime;
        }
    }

    public override void Hit(float damage)
    {
        Life -= damage;

        LifeBar.SetValue(Life, LifeMax);
    }
}
