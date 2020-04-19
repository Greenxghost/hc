﻿using System;
using TMPro;
using UnityEngine;

public class UIDropdownItem : MonoBehaviour
{
	public TextMeshProUGUI[] m_textLabels;

	public _ButtonSwapSprite m_hitbox;

	public _DropdownMenuList m_parent;

	[HideInInspector]
	public int Value;

	public void SetText(string newText)
	{
		foreach (TextMeshProUGUI textMeshProUGUI in this.m_textLabels)
		{
			textMeshProUGUI.text = newText;
		}
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
			RuntimeMethodHandle runtimeMethodHandle = methodof(UIDropdownItem.SetText(string)).MethodHandle;
		}
	}
}
