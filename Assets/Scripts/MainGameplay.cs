using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameState State;
    
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player.OnDeath += OnGameOver;
    }
    

    void OnGameOver()
    {
        State = GameState.GameOver;
        PanelGameOver.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
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
