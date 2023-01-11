using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatModifier : BaseWeaponModifier
{
	[SerializeField] int _count = 1;
	[SerializeField] float _timeToWait = 1;

	bool _isAttackDone = false;
	float _timer;
	int _currentCount = 0;

	public override void OnCoolDownStarted()
	{
		_isAttackDone = true;
        _timer = 0;
        _currentCount = 0;
    }

	public override void Update()
	{
		if (_isAttackDone == false)
			return;

		_timer += Time.deltaTime;

		if ( _timer >= _timeToWait)
		{
			_timer = 0;
			_currentCount++;

			if (_currentCount >= _count)
				_isAttackDone = false;

			_weapon.GlobalAttack(_shooter);
		}

	}

}
