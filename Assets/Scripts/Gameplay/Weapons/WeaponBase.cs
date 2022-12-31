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
    
    public int Slot { get; private set; }
    public virtual bool DoRotate => true;

    protected float _timerCoolDown;

    [System.NonSerialized] 
    protected List<BaseWeaponModifier> _modifiers = new List<BaseWeaponModifier>();
    

    public void Initialize(int slot)
    { 
        Slot = slot;
    }

    public void AddModifier(PlayerController player, BaseWeaponModifier modifier)
    {
        modifier.Initialize(player, this);
        _modifiers.Add(modifier);
    }

    protected float GetDamage()
    {
        return Random.Range(_damageMin, _damageMax);
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
}
