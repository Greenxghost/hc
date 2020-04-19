﻿using System;
using UnityEngine;

public class ActorMultiHitContext
{
	public int m_numHits;

	public int m_numHitsFromCover;

	public Vector3 m_hitOrigin = Vector3.zero;

	public static int CalcDamageFromNumHits(int numHits, int numFromCover, int baseDamage, int subseqDamage)
	{
		int num = Mathf.Max(0, baseDamage);
		int num2 = Mathf.Max(0, subseqDamage);
		float coverProtectionDmgMultiplier = GameplayData.Get().m_coverProtectionDmgMultiplier;
		int b;
		if (numHits == numFromCover)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorMultiHitContext.CalcDamageFromNumHits(int, int, int, int)).MethodHandle;
			}
			b = Mathf.RoundToInt(coverProtectionDmgMultiplier * (float)(num + (numHits - 1) * num2));
		}
		else if (numFromCover == 0)
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
			b = num + (numHits - 1) * num2;
		}
		else
		{
			b = num + Mathf.RoundToInt(coverProtectionDmgMultiplier * (float)(numHits - 1) * (float)num2);
		}
		return Mathf.Max(0, b);
	}
}
