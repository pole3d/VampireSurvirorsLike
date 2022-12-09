using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    public List<WaveData> Waves;

    private List<WaveInstance> _wavesToPlay = new List<WaveInstance>();

    private float _timer;

    void Awake()
    {
        foreach (var data in Waves)
        {
            WaveInstance instance = new WaveInstance(data);
            _wavesToPlay.Add(instance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MainGameplay.Instance.State != MainGameplay.GameState.Gameplay)
            return;
        
        _timer += Time.deltaTime;

        for (int i = _wavesToPlay.Count - 1; i >= 0; i--)
        {
            _wavesToPlay[i].Update(this,_timer);
            
            if ( _wavesToPlay[i].Done)
                _wavesToPlay.RemoveAt(i);
        }
    }

    public void Spawn(WaveData data)
    {
        for (int i = 0; i < data.EnemyCount; i++)
        {
            GameObject go = GameObject.Instantiate(data.Enemy.Prefab);

            Vector3 spawnPosition = Random.insideUnitCircle;
            spawnPosition.z = 0;
            spawnPosition.Normalize();
            go.transform.position = spawnPosition * data.SpawnDistance;
            
            var enemy = go.GetComponent<EnemyController>();
            enemy.Initialize(MainGameplay.Instance.Player.gameObject , data.Enemy);
            MainGameplay.Instance.Enemies.Add(enemy);
        }
    }
}