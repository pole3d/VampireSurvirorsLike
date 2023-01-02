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
        
        public override void Update( PlayerController player )
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

            GlobalAttack(player); 
        }

        internal override void GlobalAttack(PlayerController player)
        {
            bool shoot = true;
            foreach (var item in _modifiers)
            {
                if (item.CancelAttack)
                    shoot = false;
            }
            if (shoot)
            {
                SimpleAttack(player);
            }

            foreach (var item in _modifiers)
            {
                item.OnShoot();
            }
        }

        internal override void SimpleAttack(PlayerController player , ModifierType type =  ModifierType.None , params float[] values )
        {
            EnemyController enemy = MainGameplay.Instance.GetClosestEnemy(player.transform.position);
            if (enemy == null)
                return;

            var playerPosition = player.transform.position;
            GameObject go = GameObject.Instantiate(_prefab, playerPosition, Quaternion.identity);
            Vector3 direction = enemy.transform.position - playerPosition;

            if (direction.sqrMagnitude > 0)
            {
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x);

                if ( type == ModifierType.Split)
                {
                    angle += values[0];
                }

                direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));

                go.GetComponent<Bullet>().Initialize(this,direction, GetDamage(), _speed);
            }
        }

    }
}