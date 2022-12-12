using Common.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    [SerializeField] [SerializeReference] [Instantiable(  type: typeof(WeaponBase))] WeaponBase _weapon;

    public WeaponBase Weapon => _weapon;
}
