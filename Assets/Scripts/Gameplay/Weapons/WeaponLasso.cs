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

        public override void Update(IShooter shooter)
        {
            _timerCoolDown += Time.deltaTime;

            if (_timerCoolDown < _coolDown)
                return;

            _timerCoolDown -= _coolDown;


            Vector2 position = (Vector2)shooter.Position + Vector2.right * shooter.DirectionX * 2;

            GameObject go = GameObject.Instantiate(_prefab, position, Quaternion.identity, shooter.Transform);

            go.GetComponent<Bullet>().Initialize(this, shooter.Team, new Vector3(),GetDamage(),0);

        }
    }
}