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

            Execute(player);
          
        }

        internal override void Execute(PlayerController player , ModifierType type =  ModifierType.None , params float[] values )
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

                go.GetComponent<Bullet>().Initialize(direction, GetDamage(), _speed);
            }
        }

    }
}