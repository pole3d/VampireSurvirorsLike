using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text _textName;
    [SerializeField] Image _imageIcon;
    [SerializeField] Button[] _slots;

    WeaponData _data;
    PanelWeapons _panel;
    PlayerController _player;
    UpgradeData _upgradeData;
    int _weaponIndex;

    public void Initialize( PanelWeapons panel, int weaponIndex,  UpgradeData upgrade, PlayerController player, WeaponData data)
    {
        _weaponIndex = weaponIndex;
        _panel = panel;
        _data = data;
        _upgradeData = upgrade;
        _player = player;

        _textName.text = data.name;
        _imageIcon.sprite = data.Sprite;
    }

    public void OnClickSlot(int index)
    {
        _player.UnlockUpgrade(_upgradeData ,_player.Weapons[_weaponIndex]);

        _panel.Close();
    }

}
