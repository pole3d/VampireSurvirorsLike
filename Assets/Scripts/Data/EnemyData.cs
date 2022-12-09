using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public float Life;
    public float MoveSpeed;
    public GameObject Prefab;
    public Sprite SpriteOverride;
}