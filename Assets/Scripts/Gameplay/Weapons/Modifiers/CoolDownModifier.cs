using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class CoolDownModifier : BaseWeaponModifier
{

    [SerializeField] float _multiplier = 1;

    protected override void InitializeInternal()
    {
        _weapon.ModifyCooldown(_multiplier);
    }


    public override object Clone()
    {
        CoolDownModifier modifier = new CoolDownModifier();
        modifier._multiplier = _multiplier;
        return modifier;
    }
}

