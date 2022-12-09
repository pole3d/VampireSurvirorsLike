using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : Unit
{
    public GameObject PrefabBullet;
    public float Speed = 5;
    public LifeBar LifeBar;

    public float CoolDown = 2;

    private float _timerCoolDown;

    public Action OnDeath;

    bool _isDead;
    Rigidbody2D _rb;
    Vector2 _inputs; 

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        LifeMax = 50;
        Life = LifeMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead )
            return;

        ReadInputs();
        Shoot();
    }

    void ReadInputs()
    {
        if (MainGameplay.Instance.State != MainGameplay.GameState.Gameplay)
        {
            _inputs = new Vector2();
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _inputs = new Vector2(horizontal, vertical);
    }


    void FixedUpdate()
    {
        Move();
    }

    private void Shoot()
    {
        if (MainGameplay.Instance.State != MainGameplay.GameState.Gameplay)
            return;
        
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
      
        if (_inputs.sqrMagnitude > 0)
        {
            _inputs.Normalize();
            _rb.velocity = _inputs * Speed;
        }
        else
        {
            _rb.velocity = new Vector2();
        }
    }

    public override void Hit(float damage)
    {
        if (_isDead)
            return;

        Life -= damage;

        LifeBar.SetValue(Life, LifeMax);

        if (Life <= 0)
        {
            _isDead = true;
            OnDeath?.Invoke();
        }
    }

    void OnDestroy()
    {
        OnDeath = null;
    }
}