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

            SimpleAttack(shooter, enemy.transform.position);

        }

        internal override void SimpleAttack(IShooter shooter, Vector3? target, ModifierType type = ModifierType.None, params float[] values)
        {
            GameObject go = GameObject.Instantiate(_prefab, target.Value, Quaternion.identity);

            go.GetComponent<Bullet>().Initialize(this, shooter.Team, new Vector3(), GetDamage(), 0);
        }
    }
}