using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class PushModifier : BaseWeaponModifier
{

    [SerializeField] float _pushForce = 2;
    [SerializeField] float _pushTimer = 0.2f;
    [SerializeField] float _massmodifier = 10;

    protected override void InitializeInternal()
    {
        
    }

    public override void OnHit(Unit enemy , Bullet bullet)
    {
        Vector3 direction = enemy.transform.position - bullet.Shooter.Position;
        direction.Normalize();

        enemy.Push(_pushForce, direction, _pushTimer , _massmodifier);
    }
}

