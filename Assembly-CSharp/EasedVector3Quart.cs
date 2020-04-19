﻿using System;
using UnityEngine;

internal class EasedVector3Quart : Eased<Vector3>
{
	internal EasedVector3Quart(Vector3 startValue) : base(startValue)
	{
	}

	protected override Vector3 CalcValue()
	{
		float t = Time.time - this.m_startTime;
		return EasedVector3Quart.CalcValue(this.m_startValue, this.m_endValue, t, this.m_duration);
	}

	internal static Vector3 CalcValue(Vector3 start, Vector3 end, float t, float duration)
	{
		return new Vector3(Easing.QuartEaseInOut(t, start.x, end.x - start.x, duration), Easing.QuartEaseInOut(t, start.y, end.y - start.y, duration), Easing.QuartEaseInOut(t, start.z, end.z - start.z, duration));
	}
}
