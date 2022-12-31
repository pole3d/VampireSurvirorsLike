using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class AddWeaponUpgrade : BaseUpgrade
{
    [SerializeField] WeaponData _data;

    public override void Execute( PlayerController player )
    {
        for (int i = player.Weapons.Count - 1; i >= 0; i--)
        {
            if (player.Weapons[i].Slot == _data.SlotIndex)
            {
                player.Weapons.RemoveAt(i);
            }
        }
        
        player.AddWeapon(_data , _data.SlotIndex);
    }

}

