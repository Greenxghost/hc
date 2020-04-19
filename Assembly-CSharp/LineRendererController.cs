﻿using System;
using UnityEngine;

[ExecuteInEditMode]
public class LineRendererController : MonoBehaviour
{
	private LineRenderer m_lineRenderer;

	private ParticleSystem m_parentParticleSystem;

	public Vector3[] m_positions;

	public float m_startWidth;

	public float m_endWidth;

	public Color m_startColor;

	public Color m_endColor;

	public FloatTimeEntry[] m_scaleEntries;

	public FloatTimeEntry[] m_alphaEntries;

	public float m_scale;

	public float GetTotalDuration()
	{
		float num = 0f;
		if (this.m_scaleEntries != null)
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
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(LineRendererController.GetTotalDuration()).MethodHandle;
			}
			if (this.m_scaleEntries.Length > 0)
			{
				num = this.m_scaleEntries[this.m_scaleEntries.Length - 1].m_time;
			}
		}
		if (this.m_alphaEntries != null)
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
			if (this.m_alphaEntries.Length > 0)
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
				num = Mathf.Max(num, this.m_alphaEntries[this.m_alphaEntries.Length - 1].m_time);
			}
		}
		return num;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (this.m_parentParticleSystem)
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
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(LineRendererController.Update()).MethodHandle;
			}
			if (this.m_lineRenderer)
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
				this.m_lineRenderer.positionCount = this.m_positions.Length;
				for (int i = 0; i < this.m_positions.Length; i++)
				{
					this.m_lineRenderer.SetPosition(i, this.m_positions[i]);
				}
				float num = this.GetValue(this.m_parentParticleSystem.time, this.m_scaleEntries) * this.m_scale;
				float value = this.GetValue(this.m_parentParticleSystem.time, this.m_alphaEntries);
				this.m_lineRenderer.startWidth = this.m_startWidth * num;
				this.m_lineRenderer.endWidth = this.m_endWidth * num;
				Color startColor = this.m_startColor;
				startColor.a *= value;
				Color endColor = this.m_endColor;
				endColor.a *= value;
				this.m_lineRenderer.startColor = startColor;
				this.m_lineRenderer.endColor = endColor;
				return;
			}
		}
		this.m_lineRenderer = base.GetComponent<LineRenderer>();
		if (base.transform.parent)
		{
			for (;;)
			{
				switch (2)
				{
				case 0:
					continue;
				}
				break;
			}
			this.m_parentParticleSystem = base.transform.parent.GetComponent<ParticleSystem>();
		}
	}

	private float GetValue(float curTime, FloatTimeEntry[] entries)
	{
		float result = 1f;
		if (entries != null)
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
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(LineRendererController.GetValue(float, FloatTimeEntry[])).MethodHandle;
			}
			if (entries.Length > 0)
			{
				int num = entries.Length;
				int i = 0;
				while (i < entries.Length)
				{
					if (entries[i].m_time > curTime)
					{
						num = i;
						IL_53:
						if (num == 0)
						{
							return entries[num].m_value;
						}
						if (num == entries.Length)
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
							return entries[entries.Length - 1].m_value;
						}
						float time = entries[num].m_time;
						float time2 = entries[num - 1].m_time;
						float t = (curTime - time2) / (time - time2);
						return Mathf.Lerp(entries[num - 1].m_value, entries[num].m_value, t);
					}
					else
					{
						i++;
					}
				}
				for (;;)
				{
					switch (5)
					{
					case 0:
						continue;
					}
					goto IL_53;
				}
			}
		}
		return result;
	}
}
