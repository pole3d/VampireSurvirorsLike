using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMap : MonoBehaviour
{
    public void OnPlayLevel(int level)
    {
        ScenesManagement.Instance.SetValue("Level", level);

        SceneManager.LoadScene("GamePlayCommon");
    }
}
