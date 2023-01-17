using System.Collections.Generic;
using UnityCommon.Graphics.Actors;
using UnityEngine;

/// <summary>
/// Manages the waves : stores the data, updates and plays them 
/// </summary>
public class WavesManager : MonoBehaviour
{
    [SerializeField] WavesLevelData _wavesLevel;
    [SerializeField] Transform _topRight;
    [SerializeField] Transform _bottomLeft;
    [SerializeField] LayerMask _noSpawn;

    readonly List<WaveInstance> _wavesToPlay = new List<WaveInstance>();
    float _timer;

    void Awake()
    {
        foreach (var data in _wavesLevel.Waves)
        {
            WaveInstance instance = new WaveInstance(data);
            _wavesToPlay.Add(instance);
        }
    }

    void Update()
    {
        if (MainGameplay.Instance.State != MainGameplay.GameState.Gameplay)
            return;

        _timer += Time.deltaTime;

        for (int i = _wavesToPlay.Count - 1; i >= 0; i--)
        {
            _wavesToPlay[i].Update(this, _timer);

            if (_wavesToPlay[i].IsDone)
                _wavesToPlay.RemoveAt(i);
        }
    }

    public void DebugTimer(int timer)
    {
        _timer = timer;

        for (int i = _wavesToPlay.Count - 1; i >= 0; i--)
        {
            if (_wavesToPlay[i].TimeToStart < timer)
                _wavesToPlay.RemoveAt(i);
        }
    }

    public void Spawn(WaveData data)
    {
        if (data.Movement is CircleMovement)
        {
            SpawnCircle(data);
            return;
        }

        for (int i = 0; i < data.EnemyCount; i++)
        {

            bool success = false;
            Vector3 spawnPosition = Random.insideUnitCircle;

            for (int tryIndex = 0; tryIndex < 8; tryIndex++)
            {
                spawnPosition = Random.insideUnitCircle;
                spawnPosition.z = 0;
                spawnPosition.Normalize();

                Vector3 tempPosition = MainGameplay.Instance.Player.transform.position + spawnPosition * data.SpawnDistance;
                if (tempPosition.x > _topRight.transform.position.x ||
                    tempPosition.x < _bottomLeft.transform.position.x)
                {
                    spawnPosition.x = -spawnPosition.x;
                }
                if (tempPosition.y > _topRight.transform.position.y ||
                    tempPosition.y < _bottomLeft.transform.position.y)
                {
                    spawnPosition.y = -spawnPosition.y;
                }

                tempPosition = MainGameplay.Instance.Player.transform.position + spawnPosition * data.SpawnDistance;

                RaycastHit2D hit = Physics2D.CircleCast(tempPosition, 0.4f, Vector2.zero, 0.0f, _noSpawn);
                Debug.DrawLine(tempPosition, tempPosition + Vector3.right * 0.4f, Color.red, 5);
                if (hit.collider == null)
                {
                    success = true;
                    break;
                }
            }

            if (success)
            {
                GameObject go = GameObject.Instantiate(data.Enemy.Prefab);
                go.transform.position = MainGameplay.Instance.Player.transform.position + spawnPosition * data.SpawnDistance;

                EnemyData enemyData = Instantiate(data.Enemy);

                if (data.OverrideLife != -1)
                    enemyData.OverrideLife(data.OverrideLife);

                var enemy = go.GetComponent<EnemyController>();
                enemy.Initialize(MainGameplay.Instance.Player.gameObject, enemyData, new DefaultMovement());
                MainGameplay.Instance.Enemies.Add(enemy);

                ActorView view = enemy.GetComponent<ActorView>();
                view.PlayActionIfExists("spawn");
            }
        }
    }

    private void SpawnCircle(WaveData data)
    {
        float angle = Mathf.PI * 2.0f / (float)data.EnemyCount;
        CircleMovement circleDefault = data.Movement as CircleMovement;


        for (int i = 0; i < data.EnemyCount; i++)
        {

            bool success = false;
            Vector3 spawnPosition = new Vector2(Mathf.Cos(angle * i), Mathf.Sin(angle * i));


            Vector3 tempPosition = MainGameplay.Instance.Player.transform.position + spawnPosition * data.SpawnDistance;
            if (tempPosition.x > _topRight.transform.position.x ||
                tempPosition.x < _bottomLeft.transform.position.x)
            {
                continue;
            }
            if (tempPosition.y > _topRight.transform.position.y ||
                tempPosition.y < _bottomLeft.transform.position.y)
            {
                continue;
            }

            RaycastHit2D hit = Physics2D.CircleCast(tempPosition, 0.4f, Vector2.zero, 0.0f, _noSpawn);
            if (hit.collider == null)
            {
                success = true;
            }

            if (success)
            {
                GameObject go = GameObject.Instantiate(data.Enemy.Prefab);
                go.transform.position = MainGameplay.Instance.Player.transform.position + spawnPosition * data.SpawnDistance;

                EnemyData enemyData = Instantiate(data.Enemy);

                if (data.OverrideLife != -1)
                    enemyData.OverrideLife(data.OverrideLife);

                CircleMovement movement = new CircleMovement();

                var enemy = go.GetComponent<EnemyController>();
                movement.Initialize( enemy,  go.transform.position, MainGameplay.Instance.Player.transform.position);
                enemy.Initialize(MainGameplay.Instance.Player.gameObject, enemyData ,movement );
                MainGameplay.Instance.Enemies.Add(enemy);
            }
        }
    }
}