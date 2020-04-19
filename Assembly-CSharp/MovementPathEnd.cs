﻿using System;
using UnityEngine;

public class MovementPathEnd : MonoBehaviour
{
	public GameObject m_TopIndicatorPiece;

	public GameObject m_IndicatorParent;

	public GameObject m_diamondContainer;

	public GameObject m_movementLineParent;

	public GameObject m_chasingParent;

	public Animator m_animationController;

	public void SetupColor(Color newColor)
	{
		MeshRenderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<MeshRenderer>(true);
		foreach (MeshRenderer meshRenderer in componentsInChildren)
		{
			if (meshRenderer.materials.Length > 0)
			{
				for (;;)
				{
					switch (6)
					{
					case 0:
						continue;
					}
					break;
				}
				if (!true)
				{
					RuntimeMethodHandle runtimeMethodHandle = methodof(MovementPathEnd.SetupColor(Color)).MethodHandle;
				}
				if (meshRenderer.materials[0] != null)
				{
					meshRenderer.materials[0].SetColor("_TintColor", newColor);
				}
			}
		}
	}

	public void Setup(ActorData actor, bool isChasing)
	{
		if (!isChasing)
		{
			for (;;)
			{
				switch (7)
				{
				case 0:
					continue;
				}
				break;
			}
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(MovementPathEnd.Setup(ActorData, bool)).MethodHandle;
			}
			this.m_IndicatorParent.SetActive(true);
			this.m_animationController.Play("Initial");
		}
		else
		{
			this.m_IndicatorParent.SetActive(false);
		}
		this.m_movementLineParent.SetActive(!isChasing);
		this.m_chasingParent.SetActive(isChasing);
	}

	public void Update()
	{
	}
}
