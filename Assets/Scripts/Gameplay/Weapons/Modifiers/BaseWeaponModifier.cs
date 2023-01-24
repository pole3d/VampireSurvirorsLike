using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BaseWeaponModifier : ICloneable
{
    public virtual  bool CancelAttack => false;

    protected WeaponBase _weapon;
    protected IShooter _shooter;

    public void Initialize(IShooter player , WeaponBase weapon)
    {
        _shooter = player;
        _weapon = weapon;

        InitializeInternal();   
    }

    protected virtual void InitializeInternal()
    {

    }

    public virtual void OnCoolDownStarted()
    {

    }

    public virtual void Update()
    {

    }

    internal virtual void OnShoot(Vector3? target)
    {
    }

    public virtual void OnHit(Unit enemy, Bullet bullet)
    {

    }

    public virtual object Clone()
    {
        return null;
    }
}
