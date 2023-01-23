using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using Gameplay.Weapons;
using TMPro;
using UnityCommon.Graphics.Actors;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

/// <summary>
/// Represents the player
/// manages the controller, the weapons, the in game lifebar and the level up
/// </summary>
public class PlayerController : Unit , IShooter
{
    [SerializeField] PlayerData _playerData;
    [SerializeField] LevelUpData _levelUpData;

    [SerializeField] LifeBar _lifeBar;

    public Action OnDeath { get; set; }
    public Action<int, int, int> OnXP { get; set; }
    public Action<int> OnLevelUp { get; set; }
    public List<UpgradeData> UpgradesAvailable { get; private set; }

    public float DamageMultiplier { get; set; } = 1.0f;
    public Vector2 Direction => _lastDirection;
    public int DirectionX => _lastDirectionX;
    public int Level => _level;
    public PlayerData PlayerData => _playerData;

    public List<WeaponBase> Weapons => _weapons;
    public List<UpgradeDoneInfo> UpgradesDone => _upgradesDone;

    public Vector3 Position => transform.position;


    public Transform Transform => transform;

    int _level = 1;
    int _xp = 0;


    ActorView _actorView;
    bool _isDead;
    Rigidbody2D _rb;
    Vector2 _inputs;
    Vector2 _lastDirection = Vector2.right;
    int _lastDirectionX = 1;
    List<WeaponBase> _weapons = new List<WeaponBase>();
    List<UpgradeDoneInfo> _upgradesDone = new();


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _actorView = GetComponent<ActorView>();

        UpgradesAvailable = new List<UpgradeData>();

        foreach (var item in _playerData.Upgrades)
        {
            UpgradesAvailable.Add(Instantiate(item));
        }
    }

    void Start()
    {
        _lifeMax = _playerData.Life;
        _life = LifeMax;



    }

    public void Initialize()
    {
        foreach (var weapon in _playerData.Weapons)
        {
            AddWeapon(weapon, weapon.SlotIndex);
        }


    }

    void Update()
    {
        if (_isDead)
            return;

        ReadInputs();
        Shoot();

        if (Input.GetKeyDown(KeyCode.F5))
        {
            CollectXP(3);
        }
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
        if (_isDead)
            return;

        Move();
    }

    private void Shoot()
    {
        if (MainGameplay.Instance.State != MainGameplay.GameState.Gameplay)
            return;

        foreach (var weapon in Weapons)
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

            _lastDirection = _inputs;

            if (Mathf.Abs(_inputs.x) > 0.1f)
                _lastDirectionX = (int)Mathf.Sign(_inputs.x);

            _actorView.SetState("walk");
        }
        else
        {
            _rb.velocity = new Vector2();
            _actorView.SetState("idle");

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
            _rb.velocity = new Vector2();

            _inputs = new Vector2();
            _isDead = true;
            OnDeath?.Invoke();
        }
    }

    internal void Heal(int value)
    {
        _life += value;
        if (_life > _lifeMax)
            _life = _lifeMax;

        _lifeBar.SetValue(Life, LifeMax);

    }

    internal void UnlockUpgrade(UpgradeData data , WeaponBase weapon )
    {
        var info = new UpgradeDoneInfo(data.name,GetWeaponIndex(weapon));
        _upgradesDone.Add(info);

        data.Upgrade.Execute(this, weapon);
        data.Use();

        if (data.TimesAllowed <= 0)
            UpgradesAvailable.Remove(data);

        UpgradesAvailable.AddRange(data.NextUpgrades);
    }

    int GetWeaponIndex(WeaponBase weapon)
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            if (_weapons[i] == weapon) return i;
        }

        return -1;
    }


    internal void AddWeapon(WeaponData weaponData, int slot)
    {
        var data = Instantiate(weaponData);

        data.Weapon.Initialize(this,data, slot);
        Weapons.Add(data.Weapon);
    }


    public void CollectXP(int value)
    {
        if (_levelUpData.IsLevelMax(_level))
            return;

        _xp += value;

        CheckLevelUp();
    }

    public void CheckLevelUp()
    {
        if (_levelUpData.IsLevelMax(_level))
            return;

        int nextLevel = _level + 1;
        int currentLevelMaxXP = _levelUpData.GetXpForLevel(nextLevel);
        int currentLevelMinXP = _levelUpData.GetXpForLevel(_level);

        if (_xp >= currentLevelMaxXP)
        {
            _level++;
            OnLevelUp?.Invoke(_level);
            currentLevelMaxXP = _levelUpData.GetXpForLevel(nextLevel);
        }


        if (_levelUpData.IsLevelMax(_level))
        {
            OnXP?.Invoke(currentLevelMaxXP + 1, currentLevelMinXP, currentLevelMaxXP + 1);
        }
        else
        {
            OnXP?.Invoke(_xp, currentLevelMinXP, currentLevelMaxXP);
        }
    }

    internal void DebugUpgrade(int debugXP)
    {
        int currentLevelMaxXP = 0;
        int currentLevelMinXP = 0;
        _xp += debugXP;


        do
        {
            int nextLevel = _level + 1;
            currentLevelMinXP = _levelUpData.GetXpForLevel(_level);
            currentLevelMaxXP = _levelUpData.GetXpForLevel(nextLevel);

            int rnd = UnityEngine.Random.Range(0, UpgradesAvailable.Count);
            var upgrade = UpgradesAvailable[rnd];

            UnlockUpgrade(upgrade, _weapons[UnityEngine.Random.Range(0, _weapons.Count)]);

            if (_xp >= currentLevelMaxXP)
            {
                _level++;
            }

            if (_levelUpData.IsLevelMax(_level))
                return;


        } while (_xp >= currentLevelMaxXP);

        OnXP?.Invoke(_xp, currentLevelMinXP, currentLevelMaxXP);
    }


    void OnDestroy()
    {
        OnDeath = null;
        OnXP = null;
        OnLevelUp = null;
    }

    public void AddDamageMultiplier(float multiplier)
    {
        DamageMultiplier *= multiplier;
    }

    public void MultiplyCoolDown(float multiplier)
    {
        DamageMultiplier *= multiplier;
    }


    public void IncreaseLifeMax(float multiplier)
    {
        float valueToAdd = _lifeMax * (multiplier - 1.0f);

        _life += valueToAdd;
        _lifeMax += valueToAdd;
    }

    public GameObject GetTarget()
    {
        EnemyController enemy = MainGameplay.Instance.GetClosestEnemy(transform.position);
        if (enemy == null)
            return null;

        return enemy.gameObject;
    }


}