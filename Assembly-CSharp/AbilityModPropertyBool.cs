﻿using System;

[Serializable]
public class AbilityModPropertyBool
{
	public bool value;

	public AbilityModPropertyBool.ModOp operation;

	public bool GetModifiedValue(bool input)
	{
		if (this.operation == AbilityModPropertyBool.ModOp.Override)
		{
			for (;;)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(AbilityModPropertyBool.GetModifiedValue(bool)).MethodHandle;
			}
			return this.value;
		}
		return input;
	}

	public void CopyValuesFrom(AbilityModPropertyBool other)
	{
		this.value = other.value;
		this.operation = other.operation;
	}

	public enum ModOp
	{
		Ignore,
		Override
	}
}
