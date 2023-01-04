using UnityEngine;

/// <summary>
/// Represents all the waves of a level
/// </summary>
[CreateAssetMenu]
public class WavesLevelData : ScriptableObject
{
    [SerializeField] WaveData[] _waves;
    [SerializeField] UpgradeData[] _startUpgrades;
    [SerializeField] UpgradesToAdd[] _upgradesToAdd;

    public WaveData[] Waves => _waves;
}
