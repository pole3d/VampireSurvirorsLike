using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class UpgradeDoneInfo
{
    public string UpgradeName { get; private set; }
    public int WeaponIndex { get; private set; }

    public UpgradeDoneInfo(string name, int weaponIndex)
    {
        UpgradeName =   name ;
        WeaponIndex = weaponIndex;
    }
}
