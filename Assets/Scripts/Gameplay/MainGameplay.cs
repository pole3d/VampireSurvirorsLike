using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainGameplay : MonoBehaviour
{
    public static MainGameplay Instance;

    public enum GameState
    {
        Gameplay,
        GameOver
    }

    public PlayerController Player;
    public List<EnemyController> Enemies;
    public GameObject PanelGameOver;
    public GameObject PanelVictory;

    public GameState State;
    public TMP_Text TextTimer;
    public Image ImageXP;
    
    public GameplayData Data;
    public GameObject PrefabXp;

    public XPBar XPBar;
    
    
    

    float _timerIncrement;
    int _timerSeconds;
    
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        UpdateTimer();
        Player.OnDeath += OnPlayerDeath;
        Player.OnXP += OnXP;
        Player.OnLevelUp += OnLevelUp;
    }

    void OnLevelUp(int level)
    {
        XPBar.SetLevel(level);
    }

    void OnXP(int currentXP , int levelXPMin , int levelXPMax)
    {
        XPBar.SetValue( currentXP , levelXPMin , levelXPMax );
    }


    void OnPlayerDeath()
    {
        State = GameState.GameOver;
        PanelGameOver.SetActive(true);
    }

    void SetVictory()
    {
        State = GameState.GameOver;
        PanelVictory.SetActive(true);
    }
    
    // Update is called once per frame
    void Update()
    {
        _timerIncrement += Time.deltaTime;

        if (_timerIncrement >= 1)
        {
            _timerIncrement -= 1;
            _timerSeconds++;
            UpdateTimer();

            if (_timerSeconds >= Data.TimerToWin)
            {
                SetVictory();
            }
        }
        
        
    }

    void UpdateTimer()
    {
        int seconds = _timerSeconds % 60;
        int minutes = _timerSeconds / 60;
        TextTimer.text = $"{minutes:00}:{seconds:00}";
    }

    public EnemyController GetClosestEnemy( Vector3 position  )
    {
        float bestDistance = float.MaxValue;
        EnemyController bestEnemy = null;

        foreach (var enemy in Enemies)
        {
            Vector3 direction = enemy.transform.position - position;

            float distance = direction.sqrMagnitude;

            if ( distance < bestDistance)
            {
                bestDistance = distance;
                bestEnemy = enemy;
            }
        }

        return bestEnemy;
    }

    public void OnClickQuit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
