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

        public WeaponBullet()
        {
        }
        
        public override void Update(IShooter shooter)
        {
            foreach (var item in _modifiers)
            {
                item.Update();
            }

            _timerCoolDown += Time.deltaTime;

            if (_timerCoolDown < _coolDown)
                return;

            foreach (var item in _modifiers)
            {
                item.OnCoolDownStarted();
            }

            _timerCoolDown -= _coolDown;

            GlobalAttack(shooter); 
        }

        internal override void GlobalAttack(IShooter shooter)
        {
            bool shoot = true;
            foreach (var item in _modifiers)
            {
                if (item.CancelAttack)
                    shoot = false;
            }
            if (shoot)
            {
                SimpleAttack(shooter);
            }

            foreach (var item in _modifiers)
            {
                item.OnShoot();
            }
        }

        internal override void SimpleAttack(IShooter shooter, ModifierType type = ModifierType.None, params float[] values)
        {
            GameObject target = shooter.GetTarget();
            if (target == null)
                return;

            var playerPosition = shooter.Position;
            GameObject go = GameObject.Instantiate(_prefab, playerPosition, Quaternion.identity);
            Vector3 direction = target.transform.position - playerPosition;

            if (direction.sqrMagnitude > 0)
            {
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x);

                if ( type == ModifierType.Split)
                {
                    angle += values[0];
                }

                direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));

                go.GetComponent<Bullet>().Initialize(this, shooter.Team, direction, GetDamage(), _speed);
            }
        }

    }
}