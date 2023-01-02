using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

/// <summary>
/// Represents the base class for entities that can be hit and have life
/// </summary>
public class Unit : MonoBehaviour
{
    public int Team => _team;
    public float Life => _life;
    public float LifeMax => _lifeMax;

    protected int _team;
    protected float _life;
    protected float _lifeMax;

    protected Vector3? _push;
    protected float _pushTimer;


    public virtual void Hit(float damage)
    {
   
    }

    internal void Push(float pushForce, Vector3 direction, float pushTimer)
    {
        _push = direction * pushForce;
        _pushTimer = pushTimer;
    }
}
