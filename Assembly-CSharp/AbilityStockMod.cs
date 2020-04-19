﻿using System;
using UnityEngine;

[Serializable]
public class AbilityStockMod
{
	public AbilityData.ActionType abilitySlot = AbilityData.ActionType.INVALID_ACTION;

	public AbilityModPropertyInt availableStockModAmount;

	public AbilityModPropertyInt refreshTimeRemainingModAmount;

	public void ModifyStockCountAndRefreshTime(AbilityData abilityData)
	{
		if (abilityData != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(AbilityStockMod.ModifyStockCountAndRefreshTime(AbilityData)).MethodHandle;
			}
			if (this.abilitySlot != AbilityData.ActionType.INVALID_ACTION)
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
				int maxStocksCount = abilityData.GetMaxStocksCount(this.abilitySlot);
				int num = Mathf.Max(0, maxStocksCount - abilityData.GetConsumedStocksCount(this.abilitySlot));
				int num2 = Mathf.Max(0, this.availableStockModAmount.GetModifiedValue(num));
				if (num != num2)
				{
					abilityData.OverrideStockRemaining(this.abilitySlot, num2);
				}
				int stockRefreshCountdown = abilityData.GetStockRefreshCountdown(this.abilitySlot);
				int num3 = Mathf.Max(0, this.refreshTimeRemainingModAmount.GetModifiedValue(stockRefreshCountdown));
				if (stockRefreshCountdown != num3)
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
					abilityData.OverrideStockRefreshCountdown(this.abilitySlot, num3);
				}
			}
		}
	}
}
