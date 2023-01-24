using UnityEngine;

namespace Gameplay.Weapons
{

    /// <summary>
    /// Represents a weapon that shot one hit on a random enemy
    /// </summary>
    public class WeaponThunder : WeaponBase
    {
        public override bool DoRotate => false;

        [SerializeField] GameObject _prefab;
        [SerializeField] float _range = 10;

        public WeaponThunder()
        { 
        }



        internal override void GlobalAttack(IShooter shooter)
        {
            EnemyController enemy = MainGameplay.Instance.GetRandomEnemyInRange(shooter.Position, _range);
            if (enemy == null)
                return;

            bool shoot = true;
            foreach (var item in _modifiers)
            {
                if (item.CancelAttack)
                    shoot = false;
            }
            if (shoot)
            {
                SimpleAttack(shooter, enemy.transform.position);
            }

            foreach (var item in _modifiers)
            {
                item.OnShoot(enemy.transform.position);
            }

        }

        internal override void SimpleAttack(IShooter shooter, Vector3? target, ModifierType type = ModifierType.None, params float[] values)
        {
            float angle = 0;

            if (type == ModifierType.AddProjectile)
            {
                target += (Vector3)Random.insideUnitCircle;
            }


            if (type == ModifierType.Split)
            {
                angle += values[0];

                Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * 1f;

                GameObject go = GameObject.Instantiate(_prefab, target.Value + offset, Quaternion.identity);
                go.GetComponent<Bullet>().Initialize(shooter, this, shooter.Team, new Vector3(), GetDamage(), 0);

            }
            else
            {
                GameObject go = GameObject.Instantiate(_prefab, target.Value, Quaternion.identity);
                go.GetComponent<Bullet>().Initialize(shooter, this, shooter.Team, new Vector3(), GetDamage(), 0);
            }



        }
    }
}