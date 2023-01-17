using System;
using UnityEngine;


[Serializable]
public class DamageUpgrade : BaseUpgrade
{
    [Tooltip("Damage multiplier")]
    [SerializeField] private float _multiplier = 1.1f;
    
    public override void Execute(PlayerController player, WeaponBase weapon)
    {
        player.AddDamageMultiplier(_multiplier);
    }
}

