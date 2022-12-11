using UnityEngine;

/// <summary>
/// Represents the data of the player
/// </summary>
[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public float Life => _life;
    public float MoveSpeed => _moveSpeed;

    [SerializeField] float _life;
    [SerializeField] float _moveSpeed;
}
