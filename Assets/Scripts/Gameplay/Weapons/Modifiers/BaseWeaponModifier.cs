using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BaseWeaponModifier
{
    public virtual  bool CancelAttack => false;

    protected WeaponBase _weapon;
    protected PlayerController _player;

    public void Initialize(PlayerController player , WeaponBase weapon)
    {
        _player = player;
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

    internal virtual void OnShoot()
    {
    }
}
