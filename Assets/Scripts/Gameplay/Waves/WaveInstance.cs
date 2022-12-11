using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveInstance
{
    public bool Done => _currentOccurence >= _data.NumberOfOccurence;
    
    private WaveData _data;
    private float _timer;
    private int _currentOccurence;

    public WaveInstance(WaveData data)
    {
        _data = data;
    }

    public void Update( WavesManager manager, float currentTimer)
    {
        if (_data.TimeToStart > currentTimer  )
            return;

        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            _currentOccurence++;
            _timer = _data.RepeatTimer;

            manager.Spawn(_data);
        }

    }

  
}
