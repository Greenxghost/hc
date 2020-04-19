﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class ClericRangedHeal : Ability
{
	[Separator("On Hit Heal/Effect", true)]
	public int m_healAmount = 0x1E;

	public int m_selfHealIfTargetingAlly = 0xF;

	public StandardEffectInfo m_targetHitEffect;

	[Separator("Extra Heal Based on Enemy Hits", true)]
	public ClericRangedHeal.ExtraHealApplyTiming m_extraHealApplyTiming;

	public int m_extraHealOnEnemyHit;

	public int m_extraHealOnSubseqEnemyHit;

	[Separator("Extra Heal Based on Current Health", true)]
	public float m_healPerPercentHealthLost;

	[Separator("On Self", true)]
	public StandardEffectInfo m_effectOnSelf;

	[Separator("Effect in Radius", true)]
	public float m_enemyDebuffRadiusAroundTarget;

	public float m_enemyDebuffRadiusAroundCaster;

	public bool m_enemyDebuffRadiusIgnoreLoS;

	public StandardEffectInfo m_enemyDebuffInRadiusEffect;

	[Separator("Reactions", true)]
	public StandardEffectInfo m_reactionEffectForHealTarget;

	public StandardEffectInfo m_reactionEffectForCaster;

	[Separator("Sequences", true)]
	public GameObject m_castSequencePrefab;

	public GameObject m_reactionProjectileSequencePrefab;

	[Header("-- For Extra Heal Effect, if Extra Heal On Enemy Hit is used")]
	public GameObject m_extraHealPersistentSeqPrefab;

	public GameObject m_extraHealTriggerSeqPrefab;

	private AbilityMod_ClericRangedHeal m_abilityMod;

	private ClericAreaBuff m_buffAbility;

	private StandardEffectInfo m_cachedTargetHitEffect;

	private StandardEffectInfo m_cachedEffectOnSelf;

	private StandardEffectInfo m_cachedReactionEffectForHealTarget;

	private StandardEffectInfo m_cachedReactionEffectForCaster;

	private StandardEffectInfo m_cachedEnemyDebuffInRadiusEffect;

	private void Start()
	{
		if (this.m_abilityName == "Base Ability")
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.Start()).MethodHandle;
			}
			this.m_abilityName = "Cleric Ranged Heal";
		}
		this.SetupTargeter();
	}

	private void SetupTargeter()
	{
		this.SetCachedFields();
		this.m_buffAbility = (base.GetAbilityOfType(typeof(ClericAreaBuff)) as ClericAreaBuff);
		if (this.GetEnemyDebuffRadiusAroundCaster() > 0f)
		{
			base.Targeter = new AbilityUtil_Targeter_AoE_AroundActor(this, this.GetEnemyDebuffRadiusAroundCaster(), this.m_enemyDebuffRadiusIgnoreLoS, true, false, -1, false, false, true);
			base.Targeter.SetAffectedGroups(true, false, true);
			base.Targeter.SetShowArcToShape(false);
		}
		else
		{
			base.Targeter = new AbilityUtil_Targeter_AoE_AroundActor(this, this.GetEnemyDebuffRadiusAroundTarget(), this.m_enemyDebuffRadiusIgnoreLoS, true, false, -1, false, true, true);
			base.Targeter.SetAffectedGroups(true, false, true);
			base.Targeter.SetShowArcToShape(true);
		}
	}

	public override bool CustomCanCastValidation(ActorData caster)
	{
		return base.HasTargetableActorsInDecision(caster, false, true, true, Ability.ValidateCheckPath.Ignore, this.m_targetData[0].m_checkLineOfSight, false, false) && base.CustomCanCastValidation(caster);
	}

	public override bool CustomTargetValidation(ActorData caster, AbilityTarget target, int targetIndex, List<AbilityTarget> currentTargets)
	{
		BoardSquare boardSquare = Board.\u000E().\u000E(target.GridPos);
		if (boardSquare != null && boardSquare.OccupantActor != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.CustomTargetValidation(ActorData, AbilityTarget, int, List<AbilityTarget>)).MethodHandle;
			}
			if (boardSquare.OccupantActor.\u000E() == caster.\u000E() && !boardSquare.OccupantActor.IgnoreForAbilityHits)
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
				return true;
			}
		}
		return false;
	}

	protected override void OnApplyAbilityMod(AbilityMod abilityMod)
	{
		if (abilityMod.GetType() == typeof(AbilityMod_ClericRangedHeal))
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.OnApplyAbilityMod(AbilityMod)).MethodHandle;
			}
			this.m_abilityMod = (abilityMod as AbilityMod_ClericRangedHeal);
			this.SetupTargeter();
		}
	}

	protected override void OnRemoveAbilityMod()
	{
		this.m_abilityMod = null;
		this.SetupTargeter();
	}

	private void SetCachedFields()
	{
		StandardEffectInfo cachedTargetHitEffect;
		if (this.m_abilityMod)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.SetCachedFields()).MethodHandle;
			}
			cachedTargetHitEffect = this.m_abilityMod.m_targetHitEffectMod.GetModifiedValue(this.m_targetHitEffect);
		}
		else
		{
			cachedTargetHitEffect = this.m_targetHitEffect;
		}
		this.m_cachedTargetHitEffect = cachedTargetHitEffect;
		StandardEffectInfo cachedEffectOnSelf;
		if (this.m_abilityMod)
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
			cachedEffectOnSelf = this.m_abilityMod.m_effectOnSelfMod.GetModifiedValue(this.m_effectOnSelf);
		}
		else
		{
			cachedEffectOnSelf = this.m_effectOnSelf;
		}
		this.m_cachedEffectOnSelf = cachedEffectOnSelf;
		StandardEffectInfo cachedReactionEffectForHealTarget;
		if (this.m_abilityMod)
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
			cachedReactionEffectForHealTarget = this.m_abilityMod.m_reactionEffectForHealTargetMod.GetModifiedValue(this.m_reactionEffectForHealTarget);
		}
		else
		{
			cachedReactionEffectForHealTarget = this.m_reactionEffectForHealTarget;
		}
		this.m_cachedReactionEffectForHealTarget = cachedReactionEffectForHealTarget;
		StandardEffectInfo cachedReactionEffectForCaster;
		if (this.m_abilityMod)
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
			cachedReactionEffectForCaster = this.m_abilityMod.m_reactionEffectForCasterMod.GetModifiedValue(this.m_reactionEffectForCaster);
		}
		else
		{
			cachedReactionEffectForCaster = this.m_reactionEffectForCaster;
		}
		this.m_cachedReactionEffectForCaster = cachedReactionEffectForCaster;
		StandardEffectInfo cachedEnemyDebuffInRadiusEffect;
		if (this.m_abilityMod)
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
			cachedEnemyDebuffInRadiusEffect = this.m_abilityMod.m_enemyDebuffInRadiusEffectMod.GetModifiedValue(this.m_enemyDebuffInRadiusEffect);
		}
		else
		{
			cachedEnemyDebuffInRadiusEffect = this.m_enemyDebuffInRadiusEffect;
		}
		this.m_cachedEnemyDebuffInRadiusEffect = cachedEnemyDebuffInRadiusEffect;
	}

	public int GetHealAmount()
	{
		int result;
		if (this.m_abilityMod)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.GetHealAmount()).MethodHandle;
			}
			result = this.m_abilityMod.m_healAmountMod.GetModifiedValue(this.m_healAmount);
		}
		else
		{
			result = this.m_healAmount;
		}
		return result;
	}

	public int GetSelfHealIfTargetingAlly()
	{
		int result;
		if (this.m_abilityMod)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.GetSelfHealIfTargetingAlly()).MethodHandle;
			}
			result = this.m_abilityMod.m_selfHealIfTargetingAllyMod.GetModifiedValue(this.m_selfHealIfTargetingAlly);
		}
		else
		{
			result = this.m_selfHealIfTargetingAlly;
		}
		return result;
	}

	public StandardEffectInfo GetTargetHitEffect()
	{
		StandardEffectInfo result;
		if (this.m_cachedTargetHitEffect != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.GetTargetHitEffect()).MethodHandle;
			}
			result = this.m_cachedTargetHitEffect;
		}
		else
		{
			result = this.m_targetHitEffect;
		}
		return result;
	}

	public int GetExtraHealOnEnemyHit()
	{
		int result;
		if (this.m_abilityMod)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.GetExtraHealOnEnemyHit()).MethodHandle;
			}
			result = this.m_abilityMod.m_extraHealOnEnemyHitMod.GetModifiedValue(this.m_extraHealOnEnemyHit);
		}
		else
		{
			result = this.m_extraHealOnEnemyHit;
		}
		return result;
	}

	public int GetExtraHealOnSubseqEnemyHit()
	{
		int result;
		if (this.m_abilityMod)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.GetExtraHealOnSubseqEnemyHit()).MethodHandle;
			}
			result = this.m_abilityMod.m_extraHealOnSubseqEnemyHitMod.GetModifiedValue(this.m_extraHealOnSubseqEnemyHit);
		}
		else
		{
			result = this.m_extraHealOnSubseqEnemyHit;
		}
		return result;
	}

	public int GetExtraHealPerTargetDistance()
	{
		int result;
		if (this.m_abilityMod)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.GetExtraHealPerTargetDistance()).MethodHandle;
			}
			result = this.m_abilityMod.m_extraHealPerTargetDistanceMod.GetModifiedValue(0);
		}
		else
		{
			result = 0;
		}
		return result;
	}

	public int GetSelfHealAdjustIfTargetingSelf()
	{
		int result;
		if (this.m_abilityMod)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.GetSelfHealAdjustIfTargetingSelf()).MethodHandle;
			}
			result = this.m_abilityMod.m_selfHealAdjustIfTargetingSelfMod.GetModifiedValue(0);
		}
		else
		{
			result = 0;
		}
		return result;
	}

	public float GetHealPerPercentHealthLost()
	{
		return (!this.m_abilityMod) ? this.m_healPerPercentHealthLost : this.m_abilityMod.m_healPerPercentHealthLostMod.GetModifiedValue(this.m_healPerPercentHealthLost);
	}

	public StandardEffectInfo GetEffectOnSelf()
	{
		return (this.m_cachedEffectOnSelf == null) ? this.m_effectOnSelf : this.m_cachedEffectOnSelf;
	}

	public StandardEffectInfo GetReactionEffectForHealTarget()
	{
		StandardEffectInfo result;
		if (this.m_cachedReactionEffectForHealTarget != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.GetReactionEffectForHealTarget()).MethodHandle;
			}
			result = this.m_cachedReactionEffectForHealTarget;
		}
		else
		{
			result = this.m_reactionEffectForHealTarget;
		}
		return result;
	}

	public StandardEffectInfo GetReactionEffectForCaster()
	{
		StandardEffectInfo result;
		if (this.m_cachedReactionEffectForCaster != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.GetReactionEffectForCaster()).MethodHandle;
			}
			result = this.m_cachedReactionEffectForCaster;
		}
		else
		{
			result = this.m_reactionEffectForCaster;
		}
		return result;
	}

	public float GetEnemyDebuffRadiusAroundTarget()
	{
		float result;
		if (this.m_abilityMod)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.GetEnemyDebuffRadiusAroundTarget()).MethodHandle;
			}
			result = this.m_abilityMod.m_enemyDebuffRadiusAroundTargetMod.GetModifiedValue(this.m_enemyDebuffRadiusAroundTarget);
		}
		else
		{
			result = this.m_enemyDebuffRadiusAroundTarget;
		}
		return result;
	}

	public float GetEnemyDebuffRadiusAroundCaster()
	{
		float result;
		if (this.m_abilityMod)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.GetEnemyDebuffRadiusAroundCaster()).MethodHandle;
			}
			result = this.m_abilityMod.m_enemyDebuffRadiusAroundCasterMod.GetModifiedValue(this.m_enemyDebuffRadiusAroundCaster);
		}
		else
		{
			result = this.m_enemyDebuffRadiusAroundCaster;
		}
		return result;
	}

	public StandardEffectInfo GetEnemyDebuffInRadiusEffect()
	{
		StandardEffectInfo result;
		if (this.m_cachedEnemyDebuffInRadiusEffect != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.GetEnemyDebuffInRadiusEffect()).MethodHandle;
			}
			result = this.m_cachedEnemyDebuffInRadiusEffect;
		}
		else
		{
			result = this.m_enemyDebuffInRadiusEffect;
		}
		return result;
	}

	public int GetTechPointGainPerIncomingHit()
	{
		return (!this.m_abilityMod) ? 0 : this.m_abilityMod.m_techPointGainPerIncomingHitThisTurn.GetModifiedValue(0);
	}

	public int CalcExtraHealFromMissingHealth(ActorData healTarget)
	{
		int result = 0;
		if (this.GetHealPerPercentHealthLost() > 0f)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.CalcExtraHealFromMissingHealth(ActorData)).MethodHandle;
			}
			if (healTarget.HitPoints < healTarget.\u0012())
			{
				int num = Mathf.CeilToInt((1f - healTarget.\u0012()) * 100f);
				result = Mathf.RoundToInt(this.GetHealPerPercentHealthLost() * (float)num);
			}
		}
		return result;
	}

	public int CalcFinalHealOnActor(ActorData forActor, ActorData caster, ActorData actorOnTargetedSquare)
	{
		bool flag = caster == actorOnTargetedSquare;
		int num = this.GetHealAmount();
		int num2 = this.m_healAmount;
		if (forActor == caster)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.CalcFinalHealOnActor(ActorData, ActorData, ActorData)).MethodHandle;
			}
			if (!flag)
			{
				num = this.GetSelfHealIfTargetingAlly();
				num2 = this.m_selfHealIfTargetingAlly;
			}
		}
		if (num2 > num)
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
			num2 = num;
		}
		int num3 = 0;
		if (this.GetExtraHealPerTargetDistance() != 0)
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
			if (!flag)
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
				float num4 = actorOnTargetedSquare.\u0012().HorizontalDistanceInSquaresTo(caster.\u0012());
				if (num4 > 0f)
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
					num4 -= 1f;
				}
				num3 += Mathf.RoundToInt((float)this.GetExtraHealPerTargetDistance() * num4);
			}
		}
		int num5 = Mathf.Max(num2, num + num3);
		if (flag)
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
			num5 = Mathf.Max(0, num5 + this.GetSelfHealAdjustIfTargetingSelf());
		}
		num5 += this.CalcExtraHealFromMissingHealth(forActor);
		if (this.m_buffAbility != null)
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
			if (this.m_buffAbility.GetExtraHealForPurifyOnBuffedAllies() != 0)
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
				if (this.m_buffAbility.IsActorInBuffShape(forActor))
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
					num5 += this.m_buffAbility.GetExtraHealForPurifyOnBuffedAllies();
				}
			}
		}
		return num5;
	}

	protected override void AddSpecificTooltipTokens(List<TooltipTokenEntry> tokens, AbilityMod modAsBase)
	{
		base.AddTokenInt(tokens, "HealAmount", string.Empty, this.m_healAmount, false);
		base.AddTokenInt(tokens, "SelfHealIfTargetingAlly", string.Empty, this.m_selfHealIfTargetingAlly, false);
		AbilityMod.AddToken_EffectInfo(tokens, this.m_targetHitEffect, "TargetHitEffect", this.m_targetHitEffect, true);
		base.AddTokenInt(tokens, "ExtraHealOnEnemyHit", string.Empty, this.m_extraHealOnEnemyHit, false);
		base.AddTokenInt(tokens, "ExtraHealOnSubseqEnemyHit", string.Empty, this.m_extraHealOnSubseqEnemyHit, false);
		AbilityMod.AddToken_EffectInfo(tokens, this.m_effectOnSelf, "EffectOnSelf", this.m_effectOnSelf, true);
		AbilityMod.AddToken_EffectInfo(tokens, this.m_reactionEffectForHealTarget, "ReactionEffectForHealTarget", this.m_reactionEffectForHealTarget, true);
		AbilityMod.AddToken_EffectInfo(tokens, this.m_reactionEffectForCaster, "ReactionEffectForCaster", this.m_reactionEffectForCaster, true);
		AbilityMod.AddToken_EffectInfo(tokens, this.m_enemyDebuffInRadiusEffect, "EnemyDebuffInRadiusEffect", this.m_enemyDebuffInRadiusEffect, true);
	}

	protected override List<AbilityTooltipNumber> CalculateNameplateTargetingNumbers()
	{
		return new List<AbilityTooltipNumber>
		{
			new AbilityTooltipNumber(AbilityTooltipSymbol.Healing, AbilityTooltipSubject.Ally, this.m_healAmount),
			new AbilityTooltipNumber(AbilityTooltipSymbol.Healing, AbilityTooltipSubject.Self, this.m_selfHealIfTargetingAlly)
		};
	}

	public override bool GetCustomTargeterNumbers(ActorData targetActor, int currentTargeterIndex, TargetingNumberUpdateScratch results)
	{
		ActorData actorData = base.ActorData;
		AbilityUtil_Targeter_AoE_AroundActor abilityUtil_Targeter_AoE_AroundActor = base.Targeter as AbilityUtil_Targeter_AoE_AroundActor;
		if (abilityUtil_Targeter_AoE_AroundActor != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ClericRangedHeal.GetCustomTargeterNumbers(ActorData, int, TargetingNumberUpdateScratch)).MethodHandle;
			}
			if (actorData.\u000E() == targetActor.\u000E())
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
				if (abilityUtil_Targeter_AoE_AroundActor.m_lastCenterActor != null)
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
					int healing = this.CalcFinalHealOnActor(targetActor, actorData, abilityUtil_Targeter_AoE_AroundActor.m_lastCenterActor);
					results.m_healing = healing;
					return true;
				}
			}
		}
		return false;
	}

	public enum ExtraHealApplyTiming
	{
		CombatEndOfInitialTurn,
		PrepPhaseOfNextTurn
	}
}
