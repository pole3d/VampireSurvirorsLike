using UnityEngine;

namespace Gameplay.Weapons
{

    /// <summary>
    /// Represents a lasso with a large AOE
    /// </summary>
    public class WeaponLasso : WeaponBase
    {

        [SerializeField] GameObject _prefab;

        public WeaponLasso()
        {
        }



        internal override void SimpleAttack(IShooter shooter, Vector3? target,  ModifierType type = ModifierType.None, params float[] values)
        {
            Vector3 direction = Vector2.right * shooter.DirectionX;

            float angle = Mathf.Atan2(direction.y, direction.x);

            if (type == ModifierType.Split)
            {
                angle += values[0];
            }

            direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));


            Vector2 position = (Vector2)shooter.Position + (Vector2)direction * 2;

            GameObject go = GameObject.Instantiate(_prefab, position, Quaternion.identity, shooter.Transform);

            go.GetComponent<Bullet>().Initialize (shooter, this, shooter.Team, direction, GetDamage(), 0);
        }
    }
}