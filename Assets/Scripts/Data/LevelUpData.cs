using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelUpData : ScriptableObject
{
    [SerializeField] int[] _xpForLevels;

    public bool IsLevelMax(int level) => level >= _xpForLevels.Length -1;
    
    public int GetXpForLevel(int level)
    {
        if (IsLevelMax(level))
            level = _xpForLevels.Length - 1;
        
        return _xpForLevels[level];
    }
}
