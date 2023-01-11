using System;
using System.Linq;
using UnityEngine;
using Common.Tools;

[Serializable]
public class WeaponModifierUpgrade : BaseUpgrade
{
	[SerializeReference] [Instantiable(  type: typeof(BaseWeaponModifier))] BaseWeaponModifier _modifier;
	
    public override void Execute( PlayerController player )
    {
        player.Weapons[0].AddModifier(_modifier, player);
    }

}

