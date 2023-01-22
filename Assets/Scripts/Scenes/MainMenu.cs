using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents the main menu of the game
/// </summary>
public class MainMenu : MonoBehaviour
{
    public List<GameEventCaller> MainMenuButtons = new List<GameEventCaller>();
    public List<GameEventCaller> AdventureButtons = new List<GameEventCaller>();

    public Transform ParentAdventures;

    public void OnClickPlay()
    {
        foreach (var item in MainMenuButtons)
        {
            item.PlayEvent("Hide");
        }

        ParentAdventures.gameObject.SetActive(true);

        foreach (var item in AdventureButtons)
        {
            item.PlayEvent("Show");
        }
    }

    public void OnClickAdventure()
    {
        SceneManager.LoadScene("GameMap");
    }

    public void OnClickBack()
    {
        foreach (var item in MainMenuButtons)
        {
            item.PlayEvent("Show");
        }

        foreach (var item in AdventureButtons)
        {
            item.PlayEvent("Hide");
        }
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
    
}
