using UnityEngine;

namespace Gameplay.Weapons
{
    
    /// <summary>
    /// Represents a weapon that shot one bullet at a time to the closest enemy
    /// </summary>
    public class WeaponBullet : WeaponBase
    {

        public WeaponBullet()
        {
            _coolDown = 1.1f;
        }
        
        public override void Update( PlayerController player )
        {
            _timerCoolDown += Time.deltaTime;

            if (_timerCoolDown < _coolDown)
                return;

            _timerCoolDown -= _coolDown;

            EnemyController enemy = MainGameplay.Instance.GetClosestEnemy(player.transform.position);
            if (enemy == null)
                return;

            var playerPosition = player.transform.position;
            GameObject go = GameObject.Instantiate(MainGameplay.Instance.Player.PrefabBullet, playerPosition, Quaternion.identity);
            Vector3 direction = enemy.transform.position - playerPosition;
            if (direction.sqrMagnitude > 0)
            {
                direction.Normalize();

                go.GetComponent<Bullet>().Initialize(direction);
            }
        }
    }
}