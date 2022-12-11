using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    Normal,
    
}

[Serializable]
public class WaveData
{
    public int TimeToStart;
    
    
    public int NumberOfOccurence = 1;
    public float RepeatTimer = 0;
    public int EnemyCount = 20;
    public EnemyData Enemy;
    public MoveType MoveType;
    public float SpawnDistance = 15;
}
