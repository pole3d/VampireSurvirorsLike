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

        internal override void GlobalAttack(IShooter shooter)
        {
            SimpleAttack(shooter, null);

        }


        internal override void SimpleAttack(IShooter shooter, Vector3? target,  ModifierType type = ModifierType.None, params float[] values)
        {
            Vector2 position = (Vector2)shooter.Position + Vector2.right * shooter.DirectionX * 2;

            GameObject go = GameObject.Instantiate(_prefab, position, Quaternion.identity, shooter.Transform);

            go.GetComponent<Bullet>().Initialize(this, shooter.Team, new Vector3(), GetDamage(), 0);
        }
    }
}