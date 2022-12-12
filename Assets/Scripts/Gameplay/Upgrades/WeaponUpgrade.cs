using System;
using UnityEngine;

[Serializable]
public class WeaponUpgrade : BaseUpgrade
{
    [SerializeField] WeaponData _data;

    public override void Execute( PlayerController player )
    {
        player.AddWeapon(_data.Weapon);
    }

}

