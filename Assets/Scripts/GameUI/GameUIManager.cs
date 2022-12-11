using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages all the UI of the main Gameplay
/// </summary>
public class GameUIManager : MonoBehaviour
{
    [SerializeField] GameObject _panelGameOver;
    [SerializeField] GameObject _panelVictory;

    [SerializeField] TMP_Text _textTimer;
    [SerializeField] XPBar _xpBar;

    public void Initialize(PlayerController player)
    {
        player.OnXP += OnXP;
        player.OnLevelUp += OnLevelUp;
    }
    
    void OnLevelUp(int level)
    {
        _xpBar.SetLevel(level);
    }

    void OnXP(int currentXP, int levelXPMin, int levelXPMax)
    {
        _xpBar.SetValue(currentXP, levelXPMin, levelXPMax);
    }

    public void DisplayGameOver()
    {
        _panelGameOver.SetActive(true);
    }

    public void DisplayVictory()
    {
        _panelVictory.SetActive(true);
    }

    public void RefreshTimer( int timer)
    {
        int seconds = timer % 60;
        int minutes = timer / 60;
        _textTimer.text = $"{minutes:00}:{seconds:00}";
    }
}
