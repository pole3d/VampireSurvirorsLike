using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PanelWeapons : MonoBehaviour
{
    [SerializeField] Image _imageUpgrade;
    [SerializeField] WeaponUI[] _weaponsUI;

    public void Initialize(PlayerController controller, UpgradeData data)
    {
        _imageUpgrade.sprite = data.Sprite;

        foreach (var item in _weaponsUI)
        {
            item.gameObject.SetActive(false);
        }

        for (int i = 0; i < controller.Weapons.Count; i++)
        {
            _weaponsUI[i].gameObject.SetActive(true);
            _weaponsUI[i].Initialize( this,i, data,controller,  controller.Weapons[i].Data);
        }

    }

    public void OnClick()
    {
        Close();
    }

    public void Close()
    {
        MainGameplay.Instance.GameUIManager.CloseWeapons();

        MainGameplay.Instance.UnPause();
    }

}

