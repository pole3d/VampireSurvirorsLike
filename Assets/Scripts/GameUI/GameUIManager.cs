using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages all the UI of the main Gameplay
/// </summary>
public class GameUIManager : MonoBehaviour , IContainer
{
    [SerializeField] GameObject _panelGameOver;
    [SerializeField] GameObject _panelVictory;

    [SerializeField] TMP_Text _textTimer;
    [SerializeField] XPBar _xpBar;
    [SerializeField] FillBar _bossBar;
    [SerializeField] GameObject _panelUpgradesParent;
    [SerializeField] UpgradeUI[] _panelUpgrades;
    [SerializeField] PanelWeapons _panelWeapons;
    [SerializeField] Image _background;

    public GameObject Content { get => _boss.gameObject; set => _boss = value.GetComponent<EnemyController>(); }

    PlayerController _player;
    EnemyController _boss;


    public void Initialize(PlayerController player)
    {
        _player = player;
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

    public void DisplayWeapons(UpgradeData data)
    {
        _background.gameObject.SetActive(true);

        _panelWeapons.gameObject.SetActive(true);
        _panelWeapons.Initialize(_player , data);
    }

    internal void CloseWeapons()
    {
        _background.gameObject.SetActive(false);

        _panelWeapons.gameObject.SetActive(false);
    }

    public void DisplayUpgrades(UpgradeData[] upgrades)
    {
        _background.gameObject.SetActive(true);

        _panelUpgradesParent.SetActive(true);

        for (int i = 0; i < _panelUpgrades.Length; i++)
        {
            _panelUpgrades[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < upgrades.Length; i++)
        {
            _panelUpgrades[i].gameObject.SetActive(true);
            _panelUpgrades[i].Initialize(upgrades[i]);
        }
    }

    internal void ClosePanelUpgrade()
    {
        _background.gameObject.SetActive(false);

        _panelUpgradesParent.SetActive(false);
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

    private void Update()
    {
        if (_boss == null)
            return;

        _bossBar.SetText($"{_boss.Life:0.} / {_boss.LifeMax:0.}");
        _bossBar.SetValue(_boss.Life , 0 , _boss.LifeMax);
    }

}
