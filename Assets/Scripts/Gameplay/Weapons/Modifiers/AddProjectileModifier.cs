using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddProjectileModifier : BaseWeaponModifier
{
    float? _shootTimer;
    Vector3? _target;

    public override void Update()
    {
        if (_shootTimer == null)
            return;

        _shootTimer -= Time.deltaTime;

        if ( _shootTimer <= 0)
        {
            _shootTimer = null;
            _weapon.SimpleAttack(_shooter, _target , ModifierType.AddProjectile);
        }
    }


    internal override void OnShoot(Vector3? target)
    {
        _shootTimer = Random.Range(0, 0.3f);
        _target = target;
    }

    public override object Clone()
    {
        return new AddProjectileModifier();
    }

}
