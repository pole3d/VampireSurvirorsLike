using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughEnemyModifier : BaseWeaponModifier
{

	[SerializeField] int _count = 1;


	public override object Clone()
	{
		ThroughEnemyModifier modifier = new ThroughEnemyModifier();
		modifier._count = _count;
		return modifier;
	}

	protected override void InitializeInternal()
	{
		_weapon.SetThroughEnemy(_count);
	}

}
