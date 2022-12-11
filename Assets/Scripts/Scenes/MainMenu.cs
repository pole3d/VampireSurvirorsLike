using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void OnClickPlay()
    {
        SceneManager.LoadScene("MainGameplay");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
    
}
