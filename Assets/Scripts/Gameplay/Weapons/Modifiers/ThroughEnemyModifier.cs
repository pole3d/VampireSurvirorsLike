using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughEnemyModifier : BaseWeaponModifier
{

	[SerializeField] int _count = 1;

	protected override void InitializeInternal()
	{
		_weapon.SetThroughEnemy(_count);
	}

}
