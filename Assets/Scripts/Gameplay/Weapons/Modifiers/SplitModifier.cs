using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitModifier : BaseWeaponModifier
{
	public override bool CancelAttack => true;

	[SerializeField] int _count = 2;
	[SerializeField] float _angle = Mathf.PI / 4.0f;
	[SerializeField] float _damageMultiplier = 0.5f;


	public override object Clone()
	{
		SplitModifier modifier = new SplitModifier();
		modifier._count = _count;
		modifier._angle = _angle;
		modifier._damageMultiplier = _damageMultiplier;
		return modifier;
	}

	protected override void InitializeInternal()
	{
		_weapon.ModifyDamage(_damageMultiplier);
	}


	internal override void OnShoot(Vector3? target)
	{
        float totalAngle = (_count - 1) * _angle;

		if (_count == 1)
			totalAngle = _angle * 2;

        for (int i = 0; i < _count; i++)
        {
            float angle = -totalAngle / 2.0f + i * _angle;
            _weapon.SimpleAttack(_shooter, target, ModifierType.Split, angle);
        }
    }


	public override void OnCoolDownStarted()
	{

    }

	public override void Update()
	{


	}

}
