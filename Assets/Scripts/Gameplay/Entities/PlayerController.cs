using System;
using System.Collections.Generic;
using Gameplay.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Represents the player
/// manages the controller, the weapons, the in game lifebar and the level up
/// </summary>
public class PlayerController : Unit
{
    [SerializeField] PlayerData _playerData;
    [SerializeField] LevelUpData _levelUpData;

    [SerializeField] LifeBar _lifeBar;

    [SerializeField] GameObject _prefabBullet;

    public Action OnDeath { get; set; }
    public Action<int, int, int> OnXP { get; set; }
    public Action<int> OnLevelUp { get; set; }

    public GameObject PrefabBullet => _prefabBullet;

    int _level = 1;
    int _xp = 0;

    bool _isDead;
    Rigidbody2D _rb;
    Vector2 _inputs;
    List<WeaponBase> _weapons = new();

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _lifeMax = _playerData.Life;
        _life = LifeMax;
        
        _weapons.Add(new WeaponBullet());
    }

    void Update()
    {
        if (_isDead)
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

        foreach (var weapon in _weapons)
        {
            weapon.Update(this);
        }
    }

    private void Move()
    {
        if (_inputs.sqrMagnitude > 0)
        {
            _inputs.Normalize();
            _rb.velocity = _inputs * _playerData.MoveSpeed;
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

        _life -= damage;

        _lifeBar.SetValue(Life, LifeMax);

        if (Life <= 0)
        {
            _isDead = true;
            OnDeath?.Invoke();
        }
    }

    public void CollectXP(int value)
    {
        if (_levelUpData.IsLevelMax(_level))
            return;

        _xp += value;

        int nextLevel = _level + 1;
        int currentLevelMaxXP = _levelUpData.GetXpForLevel(nextLevel);
        if (_xp >= currentLevelMaxXP)
        {
            _level++;
            OnLevelUp?.Invoke(_level);
            currentLevelMaxXP = _levelUpData.GetXpForLevel(nextLevel);
        }

        int currentLevelMinXP = _levelUpData.GetXpForLevel(_level);

        if (_levelUpData.IsLevelMax(_level))
        {
            OnXP?.Invoke(currentLevelMaxXP + 1, currentLevelMinXP, currentLevelMaxXP + 1);
        }
        else
        {
            OnXP?.Invoke(_xp, currentLevelMinXP, currentLevelMaxXP);
        }
    }

    void OnDestroy()
    {
        OnDeath = null;
        OnXP = null;
        OnLevelUp = null;
    }
}