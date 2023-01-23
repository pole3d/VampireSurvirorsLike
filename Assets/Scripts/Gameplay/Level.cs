using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] WavesLevelData _waveData;

    private void Start()
    {
        MainGameplay.Instance.WavesManager.Initialize(_waveData);
    }
}
