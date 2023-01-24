using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMap : MonoBehaviour
{
    [SerializeField] Button[] _slots;
    [SerializeField] GameObject _arrow;

    private void Start()
    {
        int levelDone = Mathf.Max(0, ScenesManagement.Instance.GetIntValue("LevelDone"));

        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].interactable = false;
        }

        _slots[levelDone].interactable = true;
        _arrow.transform.position = _slots[levelDone].transform.position;
    }

    public void OnPlayLevel(int level)
    {
        ScenesManagement.Instance.SetValue("Level", level);

        SceneManager.LoadScene("GamePlayCommon");
    }
}
