﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AbilityContextNamespace;
using UnityEngine;
using UnityEngine.Networking;

public class Fireborg_SyncComponent : NetworkBehaviour
{
	[Separator("Ignited Effect", true)]
	public StandardActorEffectData m_ignitedEffectData;

	public int m_ignitedTriggerDamage = 5;

	public StandardEffectInfo m_ignitedTriggerEffect;

	public int m_ignitedTriggerEnergyOnCaster;

	[Separator("Ground Fire Effect", true)]
	public int m_groundFireDamageNormal = 6;

	public int m_groundFireDamageSuperheated = 8;

	public StandardEffectInfo m_groundFireEffect;

	public bool m_groundFireAddsIgniteIfSuperheated = true;

	[Separator("Sequences", true)]
	public GameObject m_groundFirePerSquareSeqPrefab;

	public GameObject m_groundFireOnHitSeqPrefab;

	[Header("-- Superheated versions")]
	public GameObject m_superheatedGroundFireSquareSeqPrefab;

	[SyncVar]
	internal int m_superheatLastCastTurn;

	internal SyncListUInt m_actorsInGroundFireOnTurnStart = new SyncListUInt();

	private AbilityData m_abilityData;

	private FireborgSuperheat m_superheatAbility;

	private AbilityData.ActionType m_superheatActionType = AbilityData.ActionType.INVALID_ACTION;

	public static ContextNameKeyPair s_cvarSuperheated = new ContextNameKeyPair("Superheated");

	private HashSet<ActorData> m_ignitedActorsThisTurn = new HashSet<ActorData>();

	private static int kListm_actorsInGroundFireOnTurnStart = 0x55100CF7;

	static Fireborg_SyncComponent()
	{
		NetworkBehaviour.RegisterSyncListDelegate(typeof(Fireborg_SyncComponent), Fireborg_SyncComponent.kListm_actorsInGroundFireOnTurnStart, new NetworkBehaviour.CmdDelegate(Fireborg_SyncComponent.InvokeSyncListm_actorsInGroundFireOnTurnStart));
		NetworkCRC.RegisterBehaviour("Fireborg_SyncComponent", 0);
	}

	public void ResetIgnitedActorsTrackingThisTurn()
	{
		this.m_ignitedActorsThisTurn.Clear();
	}

	private void Start()
	{
		this.m_abilityData = base.GetComponent<AbilityData>();
		this.m_superheatAbility = this.m_abilityData.GetAbilityOfType<FireborgSuperheat>();
		if (this.m_superheatAbility != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(Fireborg_SyncComponent.Start()).MethodHandle;
			}
			this.m_superheatActionType = this.m_abilityData.GetActionTypeOfAbility(this.m_superheatAbility);
		}
	}

	public static string GetSuperheatedCvarUsage()
	{
		return ContextVars.\u0015(Fireborg_SyncComponent.s_cvarSuperheated.\u0012(), "1 if caster is in Superheated mode, 0 otherwise", false);
	}

	public bool InSuperheatMode()
	{
		if (this.m_superheatAbility != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(Fireborg_SyncComponent.InSuperheatMode()).MethodHandle;
			}
			bool flag = false;
			int currentTurn = GameFlowData.Get().CurrentTurn;
			int superheatDuration = this.m_superheatAbility.GetSuperheatDuration();
			if (this.m_superheatLastCastTurn > 0)
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
				flag = (currentTurn < this.m_superheatLastCastTurn + superheatDuration);
			}
			bool result;
			if (!flag)
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
				result = this.m_abilityData.HasQueuedAction(this.m_superheatActionType);
			}
			else
			{
				result = true;
			}
			return result;
		}
		return false;
	}

	public void SetSuperheatedContextVar(ContextVars abilityContext)
	{
		bool flag = this.InSuperheatMode();
		abilityContext.\u0016(Fireborg_SyncComponent.s_cvarSuperheated.\u0012(), (!flag) ? 0 : 1);
	}

	public void AddGroundFireTargetingNumber(ActorData target, ActorData caster, TargetingNumberUpdateScratch results)
	{
		if (target.\u000E() != caster.\u000E())
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(Fireborg_SyncComponent.AddGroundFireTargetingNumber(ActorData, ActorData, TargetingNumberUpdateScratch)).MethodHandle;
			}
			int num;
			if (this.InSuperheatMode())
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
				num = this.m_groundFireDamageSuperheated;
			}
			else
			{
				num = this.m_groundFireDamageNormal;
			}
			int num2 = num;
			if (num2 > 0)
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
				results.m_damage += num2;
			}
		}
	}

	public string GetTargetPreviewAccessoryString(AbilityTooltipSymbol symbolType, Ability ability, ActorData targetActor, ActorData caster)
	{
		if (symbolType == AbilityTooltipSymbol.Damage)
		{
			int num;
			if (this.InSuperheatMode())
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
					RuntimeMethodHandle runtimeMethodHandle = methodof(Fireborg_SyncComponent.GetTargetPreviewAccessoryString(AbilityTooltipSymbol, Ability, ActorData, ActorData)).MethodHandle;
				}
				num = this.m_groundFireDamageSuperheated;
			}
			else
			{
				num = this.m_groundFireDamageNormal;
			}
			int num2 = num;
			if (num2 > 0)
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
				return "\n+ " + AbilityUtils.CalculateDamageForTargeter(caster, targetActor, ability, num2, false).ToString();
			}
		}
		return null;
	}

	private void UNetVersion()
	{
	}

	public int Networkm_superheatLastCastTurn
	{
		get
		{
			return this.m_superheatLastCastTurn;
		}
		[param: In]
		set
		{
			base.SetSyncVar<int>(value, ref this.m_superheatLastCastTurn, 1U);
		}
	}

	protected static void InvokeSyncListm_actorsInGroundFireOnTurnStart(NetworkBehaviour obj, NetworkReader reader)
	{
		if (!NetworkClient.active)
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
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(Fireborg_SyncComponent.InvokeSyncListm_actorsInGroundFireOnTurnStart(NetworkBehaviour, NetworkReader)).MethodHandle;
			}
			Debug.LogError("SyncList m_actorsInGroundFireOnTurnStart called on server.");
			return;
		}
		((Fireborg_SyncComponent)obj).m_actorsInGroundFireOnTurnStart.HandleMsg(reader);
	}

	private void Awake()
	{
		this.m_actorsInGroundFireOnTurnStart.InitializeBehaviour(this, Fireborg_SyncComponent.kListm_actorsInGroundFireOnTurnStart);
	}

	public override bool OnSerialize(NetworkWriter writer, bool forceAll)
	{
		if (forceAll)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(Fireborg_SyncComponent.OnSerialize(NetworkWriter, bool)).MethodHandle;
			}
			writer.WritePackedUInt32((uint)this.m_superheatLastCastTurn);
			SyncListUInt.WriteInstance(writer, this.m_actorsInGroundFireOnTurnStart);
			return true;
		}
		bool flag = false;
		if ((base.syncVarDirtyBits & 1U) != 0U)
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
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
				flag = true;
			}
			writer.WritePackedUInt32((uint)this.m_superheatLastCastTurn);
		}
		if ((base.syncVarDirtyBits & 2U) != 0U)
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
			if (!flag)
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
				writer.WritePackedUInt32(base.syncVarDirtyBits);
				flag = true;
			}
			SyncListUInt.WriteInstance(writer, this.m_actorsInGroundFireOnTurnStart);
		}
		if (!flag)
		{
			writer.WritePackedUInt32(base.syncVarDirtyBits);
		}
		return flag;
	}

	public override void OnDeserialize(NetworkReader reader, bool initialState)
	{
		if (initialState)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(Fireborg_SyncComponent.OnDeserialize(NetworkReader, bool)).MethodHandle;
			}
			this.m_superheatLastCastTurn = (int)reader.ReadPackedUInt32();
			SyncListUInt.ReadReference(reader, this.m_actorsInGroundFireOnTurnStart);
			return;
		}
		int num = (int)reader.ReadPackedUInt32();
		if ((num & 1) != 0)
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
			this.m_superheatLastCastTurn = (int)reader.ReadPackedUInt32();
		}
		if ((num & 2) != 0)
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
			SyncListUInt.ReadReference(reader, this.m_actorsInGroundFireOnTurnStart);
		}
	}
}
