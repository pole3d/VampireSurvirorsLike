using System;
using UnityEngine;


[Serializable]
public class MoveSpeedUpgrade : BaseUpgrade
{
    [Tooltip("move multiplier")]
    [SerializeField] private float _multiplier = 1.1f;
    
    public override void Execute(PlayerController player , WeaponBase weapon)
    {
        player.IncreaseMoveSpeed(_multiplier);
    }
}

