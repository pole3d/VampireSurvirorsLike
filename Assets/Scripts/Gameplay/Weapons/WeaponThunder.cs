using UnityEngine;

namespace Gameplay.Weapons
{

    /// <summary>
    /// Represents a weapon that shot one hit on a random enemy
    /// </summary>
    public class WeaponThunder : WeaponBase
    {

        public WeaponThunder()
        {
            _coolDown = 3.0f;
        }

        public override void Update(PlayerController player)
        {
            _timerCoolDown += Time.deltaTime;

            if (_timerCoolDown < _coolDown)
                return;

            _timerCoolDown -= _coolDown;

            EnemyController enemy = MainGameplay.Instance.GetRandomEnemyOnScreen();
            if (enemy == null)
                return;

            GameObject go = GameObject.Instantiate(MainGameplay.Instance.Player.PrefabThunder, enemy.transform.position, Quaternion.identity);

            go.GetComponent<Bullet>().Initialize(new Vector3());

        }
    }
}