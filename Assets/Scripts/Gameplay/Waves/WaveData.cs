using Common.Tools;
using System;
using UnityEngine;
using UnityEngine.Serialization;


/// <summary>
/// Represents a wave a spawning enemies
/// </summary>
[Serializable]
public class WaveData
{
    /// <summary>
    /// Enemies can have different movement patterns
    /// </summary>
    public enum MoveType
    {
        Normal,
        Circle
    }

    public int TimeToStart => _timeToStart;
    public int TimesToRepeat => _timesToRepeat;
    public float RepeatTimer => _repeatTimer;
    public int EnemyCount => _enemyCount;
    public EnemyData Enemy => _enemy;
    public BaseMovement Movement => _movement;
    public float SpawnDistance => _spawnDistance;
    public float OverrideLife => _overrideLife;

    [SerializeField] int _timeToStart;
    [SerializeField] int _timesToRepeat = 1;
    [SerializeField] float _repeatTimer = 0;
    [SerializeField] int _enemyCount = 20;
    [SerializeField] EnemyData _enemy;
    [SerializeField] float _spawnDistance = 15;
    [SerializeField] float _overrideLife = -1;
    [SerializeReference][Instantiable(typeof(BaseMovement))] BaseMovement _movement;
}