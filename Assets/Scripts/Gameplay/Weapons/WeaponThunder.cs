﻿using UnityEngine;

namespace Gameplay.Weapons
{

    /// <summary>
    /// Represents a weapon that shot one hit on a random enemy
    /// </summary>
    public class WeaponThunder : WeaponBase
    {
        public override bool DoRotate => false;

        [SerializeField] GameObject _prefab;

        public WeaponThunder()
        { 
        }

        public override void Update(IShooter shooter)
        {
            _timerCoolDown += Time.deltaTime;

            if (_timerCoolDown < _coolDown)
                return;

            _timerCoolDown -= _coolDown;

            EnemyController enemy = MainGameplay.Instance.GetRandomEnemyOnScreen();
            if (enemy == null)
                return;

            GameObject go = GameObject.Instantiate(_prefab, enemy.transform.position, Quaternion.identity);

            go.GetComponent<Bullet>().Initialize( this, shooter.Team, new Vector3(),GetDamage(),0);

        }
    }
}