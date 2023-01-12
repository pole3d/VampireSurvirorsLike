using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using static AIData;
using static UnityEngine.EventSystems.EventTrigger;

/// <summary>
/// Represents an enemy who's moving toward the player
/// and damage him on collision
/// data bout the enemy are stored in the EnemyData class
/// CAUTION : don't forget to call Initialize when you create an enemy
/// </summary>
public class EnemyController : Unit, IStoppable, IShooter
{
    public EnemyData Data => _data;
    public BaseMovement Movement => _movement;
    public bool IsStopped { get; set; }

    public float DamageMultiplier => 1;

    public Vector3 Position => transform.position;

    public int DirectionX => 1;

    public Transform Transform => transform;

    [SerializeField] SpriteRenderer _renderer;

    GameObject _player;
    Rigidbody2D _playerRb;
    Rigidbody2D _rb;
    EnemyData _data;
    BaseMovement _movement;
    AIData _ai;
    GameEventInstance _currentAI;


    private List<PlayerController> _playersInTrigger = new List<PlayerController>();

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(GameObject player, EnemyData data, BaseMovement movement)
    {
        _player = player;
        _playerRb = _player.GetComponent<Rigidbody2D>();

        _data = data;
        _life = data.Life;
        _team = 1;

        _movement = movement;
        _ai = data.AI;

    }

    private void Update()
    {
        if (IsStopped || _life <= 0)
            return;

        if ( _ai != null && ( _currentAI == null || _currentAI.IsDone))
        {
            _currentAI = GameEventsManager.PlayEvents(_ai.Sequences[0].Feedbacks, gameObject);
        }

        foreach (var player in _playersInTrigger)
        {
            player.Hit(Time.deltaTime * _data.DamagePerSeconds);
        }
    }

    void FixedUpdate()
    {
        if (IsStopped)
            return;

        if (_push != null)
        {
            _pushTimer -= Time.deltaTime;
            _rb.velocity = _push.Value;

            if (_pushTimer <= 0)
            {
                _push = null;
                _rb.mass /= _pushMassModifier;
            }
        }
        else
        {
            _movement?.Update(this, _rb);
        }
    }


    public GameObject GetTarget()
    {
        return _player;
    }

    public Vector3 GetFuturePlayerPosition()
    {
        float time = 0.2f;
        return (Vector2)_player.transform.position + _playerRb.velocity * time;
    }


    public override void Hit(float damage)
    {
        _renderer.color = Color.black;
        _renderer.DOKill();
        _renderer.DOColor(Color.grey, 0.1f).SetLoops(2, LoopType.Yoyo);

        _life -= damage;

        if (Life <= 0)
        {
            _renderer.DOKill();

            Die();
        }
    }

    void Die()
    {
        MainGameplay.Instance.Enemies.Remove(this);
        GameObject.Destroy(gameObject);

        int rnd = UnityEngine.Random.Range(0, 100);

        if (rnd <= 60)
        {
            var xp = GameObject.Instantiate(MainGameplay.Instance.PrefabXP, transform.position, Quaternion.identity);
            xp.GetComponent<CollectableXp>().Initialize(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var other = HitWithParent.GetComponent<PlayerController>(col);

        if (other != null)
        {
            if (_playersInTrigger.Contains(other) == false)
                _playersInTrigger.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        var other = HitWithParent.GetComponent<PlayerController>(col);

        if (other != null)
        {
            if (_playersInTrigger.Contains(other))
                _playersInTrigger.Remove(other);
        }
    }


}