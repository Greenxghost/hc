﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class ClientPositionHitResults
{
	private List<ClientEffectStartData> m_effectsToStart;

	private List<ClientBarrierStartData> m_barriersToStart;

	private List<int> m_effectsToRemove;

	private List<int> m_barriersToRemove;

	private List<ServerClientUtils.SequenceEndData> m_sequencesToEnd;

	private List<ClientMovementResults> m_reactionsOnPosHit;

	public ClientPositionHitResults(ref IBitStream stream)
	{
		this.m_effectsToStart = AbilityResultsUtils.DeSerializeEffectsToStartFromStream(ref stream);
		this.m_barriersToStart = AbilityResultsUtils.DeSerializeBarriersToStartFromStream(ref stream);
		this.m_effectsToRemove = AbilityResultsUtils.DeSerializeEffectsForRemovalFromStream(ref stream);
		this.m_barriersToRemove = AbilityResultsUtils.DeSerializeBarriersForRemovalFromStream(ref stream);
		this.m_sequencesToEnd = AbilityResultsUtils.DeSerializeSequenceEndDataListFromStream(ref stream);
		this.m_reactionsOnPosHit = AbilityResultsUtils.DeSerializeClientMovementResultsListFromStream(ref stream);
		this.ExecutedHit = false;
	}

	public bool ExecutedHit { get; private set; }

	public void ExecutePositionHit()
	{
		if (this.ExecutedHit)
		{
			return;
		}
		if (ClientAbilityResults.\u001D)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClientPositionHitResults.ExecutePositionHit()).MethodHandle;
			}
			Debug.LogWarning(ClientAbilityResults.s_executePositionHitHeader + " Executing Position Hit");
		}
		using (List<ClientEffectStartData>.Enumerator enumerator = this.m_effectsToStart.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ClientEffectStartData effectData = enumerator.Current;
				ClientEffectBarrierManager.Get().ExecuteEffectStart(effectData);
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
		}
		foreach (ClientBarrierStartData barrierData in this.m_barriersToStart)
		{
			ClientEffectBarrierManager.Get().ExecuteBarrierStart(barrierData);
		}
		foreach (int effectGuid in this.m_effectsToRemove)
		{
			ClientEffectBarrierManager.Get().EndEffect(effectGuid);
		}
		using (List<int>.Enumerator enumerator4 = this.m_barriersToRemove.GetEnumerator())
		{
			while (enumerator4.MoveNext())
			{
				int barrierGuid = enumerator4.Current;
				ClientEffectBarrierManager.Get().EndBarrier(barrierGuid);
			}
			for (;;)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		using (List<ServerClientUtils.SequenceEndData>.Enumerator enumerator5 = this.m_sequencesToEnd.GetEnumerator())
		{
			while (enumerator5.MoveNext())
			{
				ServerClientUtils.SequenceEndData sequenceEndData = enumerator5.Current;
				sequenceEndData.EndClientSequences();
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
		}
		using (List<ClientMovementResults>.Enumerator enumerator6 = this.m_reactionsOnPosHit.GetEnumerator())
		{
			while (enumerator6.MoveNext())
			{
				ClientMovementResults clientMovementResults = enumerator6.Current;
				clientMovementResults.ReactToMovement();
			}
			for (;;)
			{
				switch (1)
				{
				case 0:
					continue;
				}
				break;
			}
		}
		this.ExecutedHit = true;
		ClientResolutionManager.Get().UpdateLastEventTime();
	}
}
