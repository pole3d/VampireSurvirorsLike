using System;
using UnityEngine;

[Serializable]
[GameFeedback("King/ShootWeapon")]
public class ShootWeapon : GameFeedback
{
    public WeaponData WeaponData;

    WeaponData _data;

    public ShootWeapon() : base()
    {


    }

    public override bool Execute(GameEventInstance gameEvent)
    {
        IShooter shooter = gameEvent.GameObject.GetComponent<IShooter>();
        if (WeaponData != null && _data == null)
        {
            _data = GameObject.Instantiate(WeaponData);

            foreach (var item in _data.Modifiers)
            {
                _data.Weapon.AddModifier(item,shooter);
            }
        }

        _data.Weapon.Initialize(shooter, 0);
        _data.Weapon.GlobalAttack(shooter);

        return base.Execute(gameEvent);
    }

}