using Common.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeData : ScriptableObject
{
    [SerializeField] [TextArea] string _description;
    [SerializeField] Sprite _sprite;
    [SerializeField] [SerializeReference] [Instantiable(type: typeof(BaseUpgrade))] BaseUpgrade _upgrade;
    [SerializeField] UpgradeData[] _nextUpgrades;
}
