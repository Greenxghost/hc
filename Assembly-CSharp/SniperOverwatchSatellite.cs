﻿using System;
using UnityEngine;

public class SniperOverwatchSatellite : TempSatellite
{
	private GameObject m_attackTarget;

	private float m_timeDespawnTriggered = -1f;

	public override void TriggerAttack(GameObject attackTarget)
	{
		this.m_modelAnimator.SetTrigger("StartAttack");
		this.m_attackTarget = attackTarget;
	}

	public override void TriggerSpawn()
	{
		this.m_modelAnimator.SetTrigger("Spawn");
	}

	public override void TriggerDespawn()
	{
		this.m_modelAnimator.SetTrigger("Despawn");
		this.m_timeDespawnTriggered = Time.time;
	}

	private void Update()
	{
		AnimatorStateInfo currentAnimatorStateInfo = this.m_modelAnimator.GetCurrentAnimatorStateInfo(0);
		if (currentAnimatorStateInfo.IsTag("Despawn"))
		{
			for (;;)
			{
				switch (3)
				{
				case 0:
					continue;
				}
				break;
			}
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(SniperOverwatchSatellite.Update()).MethodHandle;
			}
			if (currentAnimatorStateInfo.normalizedTime >= 1f)
			{
				goto IL_6C;
			}
		}
		if (this.m_timeDespawnTriggered > 0f && Time.time - this.m_timeDespawnTriggered >= 10f)
		{
			for (;;)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		else
		{
			if (!currentAnimatorStateInfo.IsTag("Attack"))
			{
				return;
			}
			for (;;)
			{
				switch (5)
				{
				case 0:
					continue;
				}
				break;
			}
			if (this.m_attackTarget != null)
			{
				for (;;)
				{
					switch (1)
					{
					case 0:
						continue;
					}
					break;
				}
				base.transform.rotation = Quaternion.LookRotation((this.m_attackTarget.transform.position - base.transform.position).normalized);
				return;
			}
			return;
		}
		IL_6C:
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
