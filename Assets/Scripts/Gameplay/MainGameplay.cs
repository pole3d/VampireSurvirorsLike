﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Entry point of the main gameplay
/// Main singleton to get access to everything :
/// Player, Enemies, Data, UI etc...
/// </summary>
public class MainGameplay : MonoBehaviour
{
    #region Singleton

    public static MainGameplay Instance;

    #endregion

    /// <summary>
    /// Represents the current state of gameplay
    /// </summary>
    public enum GameState
    {
        Gameplay,
        GameOver
    }



    #region Inspector

    [SerializeField] PlayerController _player;
    [SerializeField] GameplayData _data;
    [SerializeField] GameUIManager _gameUIManager;
    [SerializeField] WavesManager _wavesManager;

    [SerializeField] GameObject _prefabXp;
    [SerializeField] int _debugTimer = -1;
    [SerializeField] int _debugXP = -1;
    [SerializeField] int _debugLevel = -1;


    #endregion

    #region Properties


    public PlayerController Player => _player;
    public GameObject PrefabXP => _prefabXp;
    public GameState State { get; private set; }
    public List<EnemyController> Enemies => _enemies;
    public GameUIManager GameUIManager => _gameUIManager;
    public WavesManager WavesManager => _wavesManager;

    [field:SerializeField]
    public GameObject PrefabLife { get; private set; }

    #endregion

    #region Fields

    readonly List<EnemyController> _enemies = new List<EnemyController>();
    float _timerIncrement;
    int _timerSeconds;
    EnemyController _boss;
    bool _hasBoss;

    #endregion

    #region Initialize

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("an instance of MainGameplay already exists");
        }

        Instance = this;


        if (ScenesManagement.Instance.HasValue("Level"))
        {
            int level = ScenesManagement.Instance.GetIntValue("Level");
            SceneManager.LoadScene($"Level{level}", LoadSceneMode.Additive);

     
        }
        else if (_debugLevel > 0)
        {
            ScenesManagement.Instance.SetValue("Level", _debugLevel);
            SceneManager.LoadScene($"Level{_debugLevel}", LoadSceneMode.Additive);
        }
        else
        {
            ScenesManagement.Instance.SetValue("Level", 1);
            SceneManager.LoadScene($"Level1", LoadSceneMode.Additive);
        }

        

    }

    void Start()
    {
#if UNITY_EDITOR
        if (_debugTimer > 0)
        {
            _timerSeconds = _debugTimer;
            _wavesManager.DebugTimer(_debugTimer);
        }
#endif

        _gameUIManager.RefreshTimer(_timerSeconds);

        _gameUIManager.Initialize(_player);
        _player.OnDeath += OnPlayerDeath;
        _player.OnLevelUp += OnLevelUp;
        _player.Initialize();

        if (ScenesManagement.Instance.HasValue("upgrades"))
        {
            int xp = ScenesManagement.Instance.GetIntValue("xp");
            int playerLevel = ScenesManagement.Instance.GetIntValue("playerLevel");

            if (xp > -1 && playerLevel > -1)
                _player.ForceXPLevel(xp, playerLevel);

            var upgrades = ScenesManagement.Instance.GetData<List<UpgradeDoneInfo>>("upgrades");

            if (upgrades != null)
            {
                _player.ForceUpgrades(upgrades);
                ScenesManagement.Instance.SetValue("upgrades", null);
            }
        }
#if UNITY_EDITOR
        else
        {
            if (_debugXP > 0)
            {
                _player.DebugUpgrade(_debugXP);
            }
        }
#endif

    }



    #endregion

    #region Update

    void Update()
    {
        UpdateTimer();
        UpdateBoss();

#if UNITY_EDITOR
        if ( Input.GetKeyDown(KeyCode.F9))
        {
            SetVictory();
        }
#endif
    }

    private void UpdateBoss()
    {
        if (_hasBoss == false)
            return;

        if (State == GameState.GameOver)
            return;

        if ( _boss == null || _boss.Life <= 0)
        {
            SetVictory();
        }
    }

    void UpdateTimer()
    {
        _timerIncrement += Time.deltaTime;

        if (_timerIncrement >= 1)
        {
            _timerIncrement -= 1;
            _timerSeconds++;
            _gameUIManager.RefreshTimer(_timerSeconds);

            if (_timerSeconds >= _data.TimerToWin)
            {
                SetVictory();
            }
        }
    }

    #endregion

    #region Game Events

    internal void UnPause()
    {
        Time.timeScale = 1;

        _player.CheckLevelUp();

    }

    internal void Pause()
    {
        Time.timeScale = 0;
    }

    private void OnLevelUp(int level)
    {
        if (_player.UpgradesAvailable.Count == 0)
            return;

        Pause();

        List<UpgradeData> upgrades = new List<UpgradeData>();
        upgrades.AddRange(_player.UpgradesAvailable);

        List<UpgradeData> randomUpgrades = new List<UpgradeData>();
        const int nbUpgrades = 3;
        for (int i = 0; i < nbUpgrades; i++)
        {
            if (upgrades.Count == 0)
                break;

            int rnd = Random.Range(0, upgrades.Count);
            UpgradeData upgrade = upgrades[rnd];
            upgrades.RemoveAt(rnd);
            randomUpgrades.Add(upgrade);
        }

        _gameUIManager.DisplayUpgrades(randomUpgrades.ToArray());
    }

    void OnPlayerDeath()
    {
        Pause();

        State = GameState.GameOver;
        _gameUIManager.DisplayGameOver();
    }

    void SetVictory()
    {
        Pause();
        State = GameState.GameOver;
        _gameUIManager.DisplayVictory();
    }

    public void OnClickNextLevel()
    {
        Time.timeScale = 1;

        int currentLevel = ScenesManagement.Instance.GetIntValue("Level");

        ScenesManagement.Instance.SetValue("LevelDone",currentLevel);

        if (ScenesManagement.Instance.GetIntValue("Level") == 3 )
        {
            SceneManager.LoadScene("Mainmenu");
        }
        else
        {
            ScenesManagement.Instance.SetValue("xp", _player.XP);
            ScenesManagement.Instance.SetValue("playerLevel", _player.Level);

            ScenesManagement.Instance.SetValue("upgrades", _player.UpgradesDone);
            SceneManager.LoadScene("GameMap");
        }


    }

    public void OnClickQuit()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("Mainmenu");
    }

    #endregion

    #region Tools

    public void SetBoss(EnemyController boss)
    {
        _boss = boss;
        _hasBoss = true;
    }

    public EnemyController GetClosestEnemy(Vector3 position)
    {
        float bestDistance = float.MaxValue;
        EnemyController bestEnemy = null;

        foreach (var enemy in _enemies)
        {
            if (enemy.IsStopped)
                continue;

            Vector3 viewport = Camera.main.WorldToViewportPoint(enemy.transform.position);
            if (viewport.x >= 0 && viewport.x <= 1 && viewport.y >= 0 && viewport.y <= 1)
            {

                Vector3 direction = enemy.transform.position - position;

                float distance = direction.sqrMagnitude;

                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestEnemy = enemy;
                }
            }
        }

        return bestEnemy;
    }

    List<EnemyController> _enemiesOnScreen = new List<EnemyController>();

    public EnemyController GetRandomEnemyOnScreen()
    {
        _enemiesOnScreen.Clear();

        foreach (var enemy in _enemies)
        {
            Vector3 viewport = Camera.main.WorldToViewportPoint(enemy.transform.position);
            if (viewport.x >= 0 && viewport.x <= 1 && viewport.y >= 0 && viewport.y <= 1)
                _enemiesOnScreen.Add(enemy);
        }

        if (_enemiesOnScreen.Count == 0)
            return null;

        int rnd = Random.Range(0, _enemiesOnScreen.Count);

        return _enemies[rnd];
    }

    public EnemyController GetRandomEnemyInRange(Vector3 position, float range)
    {
        _enemiesOnScreen.Clear();

        foreach (var enemy in _enemies)
        {
            Vector3 viewport = Camera.main.WorldToViewportPoint(enemy.transform.position);
            if (viewport.x >= 0 && viewport.x <= 1 && viewport.y >= 0 && viewport.y <= 1)
            {
                float distanceSq = Vector3.SqrMagnitude(enemy.transform.position - position);

                if (distanceSq < range * range)
                    _enemiesOnScreen.Add(enemy);
            }
        }

        if (_enemiesOnScreen.Count == 0)
            return null;

        int rnd = Random.Range(0, _enemiesOnScreen.Count);

        return _enemies[rnd];
    }

    #endregion
}