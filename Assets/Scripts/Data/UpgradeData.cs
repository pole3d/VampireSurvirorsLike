using Common.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeData : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] [TextArea] string _description;
    [SerializeField] Sprite _sprite;
    [SerializeField] [SerializeReference] [Instantiable(type: typeof(BaseUpgrade))] BaseUpgrade _upgrade;
    [SerializeField] UpgradeData[] _nextUpgrades;
    [SerializeField] int _timesAllowed;
    [SerializeField] bool _targetWeapon;

    public BaseUpgrade Upgrade => _upgrade;
    public string Description => _description;
    public string Name => _name;
    public Sprite Sprite => _sprite;
    public UpgradeData[] NextUpgrades => _nextUpgrades;
    public int TimesAllowed => _timesAllowed;
    public bool TargetWeapon => _targetWeapon;

    public int Index{ get; set; }

    public void Use()
    {
        _timesAllowed--;
    }


    private void Awake()
    {

    }
}
