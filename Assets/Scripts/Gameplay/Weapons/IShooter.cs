using UnityEngine;

public  interface IShooter
{
    float DamageMultiplier { get; }
    int DirectionX{ get; }
    Vector3 Position { get; }
    Transform Transform { get; }
    GameObject GetTarget();
    int Team { get; }
}