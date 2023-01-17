using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the base class of all the player's weapons
/// </summary>
public abstract class WeaponBase
{
    [SerializeField] protected float _damageMin;
    [SerializeField] protected float _damageMax;
    [SerializeField] protected float _coolDown;
    [SerializeField] protected int _throughEnemyCount = 1;

    public int Slot { get; private set; }
    public virtual bool DoRotate => true;
    public int ThroughEnemyCount => _throughEnemyCount;
    public WeaponData Data => _data;

    protected float _timerCoolDown;

    [System.NonSerialized] 
    protected List<BaseWeaponModifier> _modifiers = new List<BaseWeaponModifier>();
    
    IShooter _shooter;
    WeaponData _data;

    public void Initialize(IShooter shooter, WeaponData data, int slot)
    {
        _shooter = shooter;
        _data = data;
        Slot = slot;
    }

    public void AddModifier(BaseWeaponModifier modifier, IShooter shooter)
    {
        modifier.Initialize(shooter, this);
        _modifiers.Add(modifier);
    }

    protected float GetDamage()
    {
        return Random.Range(_damageMin, _damageMax) * _shooter.DamageMultiplier;
    }
    
    public abstract void Update(IShooter shooter);

    internal virtual void SimpleAttack(IShooter shooter, ModifierType type = ModifierType.None, params float[] values)
    {
        
    }

    internal virtual void GlobalAttack(IShooter shooter)
    {

    }

    internal void ModifyDamage(float multiplier)
    {
        _damageMin *= multiplier;
        _damageMax *= multiplier;
    }

    internal void ModifyCooldown(float multiplier)
    {
        _coolDown *= multiplier;    
    }

    internal void SetThroughEnemy(int count)
    {
        _throughEnemyCount = count;
    }

    internal void OnHit(Unit other , Bullet bullet)
    {
        foreach (var item in _modifiers)
        {
            item.OnHit(other, bullet);
        }
    }


}
