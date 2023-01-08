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

    protected float _timerCoolDown;

    [System.NonSerialized] 
    protected List<BaseWeaponModifier> _modifiers = new List<BaseWeaponModifier>();
    
    PlayerController _player;

    public void Initialize(PlayerController player, int slot)
    {
        _player = player;
        Slot = slot;
    }

    public void AddModifier(PlayerController player, BaseWeaponModifier modifier)
    {
        modifier.Initialize(player, this);
        _modifiers.Add(modifier);
    }

    protected float GetDamage()
    {
        return Random.Range(_damageMin, _damageMax) * _player.DamageMultiplier;
    }
    
    public abstract void Update(PlayerController player);

    internal virtual void SimpleAttack(PlayerController player, ModifierType type = ModifierType.None, params float[] values)
    {
        
    }

    internal virtual void GlobalAttack(PlayerController player)
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
