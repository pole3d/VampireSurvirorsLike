using UnityEngine;

namespace Gameplay.Weapons
{

    /// <summary>
    /// Represents a weapon that shot one bullet at a time to the closest enemy
    /// </summary>
    public class WeaponBullet : WeaponBase
    {


        [SerializeField] GameObject _prefab;
        [SerializeField] float _speed;
        [SerializeField] bool _needTarget;

        public WeaponBullet()
        {
        }



        internal override void SimpleAttack(IShooter shooter, Vector3? targetPosition, ModifierType type = ModifierType.None, params float[] values)
        {
            GameObject target = shooter.GetTarget();

            if (_needTarget && target == null)
            {
                return;
            }

            Vector3 direction = Vector3.right;
            var shooterPosition = shooter.Position;

            if ( type == ModifierType.AddProjectile)
            {
                shooterPosition += (Vector3)Random.insideUnitCircle;
            }

            if (target != null)
                direction = target.transform.position - shooterPosition;

            GameObject go = GameObject.Instantiate(_prefab, shooterPosition, Quaternion.identity);

            if (direction.sqrMagnitude > 0)
            {
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x);

                if (type == ModifierType.Split)
                {
                    angle += values[0];
                }

                direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));

                go.GetComponent<Bullet>().Initialize(shooter, this, shooter.Team, direction, GetDamage(), _speed);
            }
        }

    }
}