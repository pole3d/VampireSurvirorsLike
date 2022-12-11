using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the base class of all the player's weapons
/// </summary>
public abstract class WeaponBase
{
    [SerializeField] protected float _coolDown;

    protected float _timerCoolDown;

    public abstract void Update(PlayerController player);
}
