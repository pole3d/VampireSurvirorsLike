using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text TextName;
    [SerializeField] TMPro.TMP_Text TextDescription;
    [SerializeField] Image ImageIcon;

    UpgradeData _data;

    public void Initialize(UpgradeData data)
    {
        _data = data;

        TextName.text = data.Name;
        TextDescription.text = data.Description;
        ImageIcon.sprite = data.Sprite;
    }


    public void OnClick()
    {
        MainGameplay.Instance.GameUIManager.ClosePanelUpgrade();


        if (_data.TargetWeapon == false)
        {
            MainGameplay.Instance.Player.UnlockUpgrade(_data , null);
            MainGameplay.Instance.UnPause();
        }
        else
        {
            MainGameplay.Instance.GameUIManager.DisplayWeapons(_data);
        }

    }
}
