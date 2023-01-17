using Common.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    [SerializeReference] [Instantiable(  type: typeof(WeaponBase))] WeaponBase _weapon;
    [SerializeField] private int _slotIndex;
    [SerializeReference][Instantiable(type: typeof(BaseWeaponModifier))] List<BaseWeaponModifier> _modifiers;
    [SerializeReference] Sprite _sprite;


    public WeaponBase Weapon => _weapon;
    public int SlotIndex => _slotIndex;
    public List<BaseWeaponModifier> Modifiers => _modifiers;
    public Sprite Sprite => _sprite;

}
