using System;
using System.Collections.Generic;
using UnityEngine;

public class NekoBendingDisc : Ability
{
	[Header("Targeting")]
	public float m_laserWidth = 1f;

	public float m_minRangeBeforeBend = 1f;

	public float m_maxRangeBeforeBend = 5.5f;

	public float m_maxTotalRange = 7.5f;

	public float m_maxBendAngle = 45f;

	public int m_maxTargets;

	public bool m_startTargeterFadeAtActorRadius = true;

	[Header("Damage stuff")]
	public int m_directDamage = 25;

	public int m_returnTripDamage = 10;

	public bool m_returnTripIgnoreCover = true;

	[Header("Sequences")]
	public GameObject m_castSequencePrefab;

	public GameObject m_returnTripSequencePrefab;

	public GameObject m_persistentDiscSequencePrefab;

	private Neko_SyncComponent m_syncComp;

	private void Start()
	{
		if (m_abilityName == "Base Ability")
		{
			m_abilityName = "Bending Boomerang Disc";
		}
		m_syncComp = GetComponent<Neko_SyncComponent>();
		SetupTargeter();
	}

	private void SetupTargeter()
	{
		base.Targeters.Clear();
		for (int i = 0; i < GetExpectedNumberOfTargeters(); i++)
		{
			AbilityUtil_Targeter_BendingLaser abilityUtil_Targeter_BendingLaser = new AbilityUtil_Targeter_BendingLaser(this, GetLaserWidth(), GetMinRangeBeforeBend(), GetMaxRangeBeforeBend(), GetMaxTotalRange(), GetMaxBendAngle(), false, GetMaxTargets());
			abilityUtil_Targeter_BendingLaser.SetUseMultiTargetUpdate(true);
			abilityUtil_Targeter_BendingLaser.m_startFadeAtActorRadius = m_startTargeterFadeAtActorRadius;
			base.Targeters.Add(abilityUtil_Targeter_BendingLaser);
		}
	}

	public override int GetExpectedNumberOfTargeters()
	{
		if (!base.Targeters.IsNullOrEmpty())
		{
			AbilityUtil_Targeter_BendingLaser abilityUtil_Targeter_BendingLaser = base.Targeters[0] as AbilityUtil_Targeter_BendingLaser;
			if (abilityUtil_Targeter_BendingLaser.DidStopShort())
			{
				while (true)
				{
					switch (2)
					{
					case 0:
						break;
					default:
						return 1;
					}
				}
			}
		}
		return 2;
	}

	public override bool ShouldAutoConfirmIfTargetingOnEndTurn()
	{
		return true;
	}

	private Vector3 GetTargeterClampedAimDirection(Vector3 aimDir, List<AbilityTarget> targets)
	{
		aimDir.y = 0f;
		aimDir.Normalize();
		float maxBendAngle = GetMaxBendAngle();
		Vector3 aimDirection = targets[0].AimDirection;
		if (maxBendAngle > 0f)
		{
			if (maxBendAngle < 360f)
			{
				aimDir = Vector3.RotateTowards(aimDirection, aimDir, (float)Math.PI / 180f * maxBendAngle, 0f);
			}
		}
		return aimDir;
	}

	private float GetClampedRangeInSquares(ActorData targetingActor, AbilityTarget currentTarget)
	{
		Vector3 travelBoardSquareWorldPositionForLos = targetingActor.GetLoSCheckPos();
		float magnitude = (currentTarget.FreePos - travelBoardSquareWorldPositionForLos).magnitude;
		if (magnitude < GetMinRangeBeforeBend() * Board.Get().squareSize)
		{
			return GetMinRangeBeforeBend();
		}
		if (magnitude > GetMaxRangeBeforeBend() * Board.Get().squareSize)
		{
			while (true)
			{
				switch (6)
				{
				case 0:
					break;
				default:
					return GetMaxRangeBeforeBend();
				}
			}
		}
		return magnitude / Board.Get().squareSize;
	}

	private float GetDistanceRemaining(ActorData targetingActor, AbilityTarget previousTarget, out Vector3 bendPos)
	{
		Vector3 travelBoardSquareWorldPositionForLos = targetingActor.GetLoSCheckPos();
		float clampedRangeInSquares = GetClampedRangeInSquares(targetingActor, previousTarget);
		bendPos = travelBoardSquareWorldPositionForLos + previousTarget.AimDirection * clampedRangeInSquares * Board.Get().squareSize;
		return GetMaxTotalRange() - clampedRangeInSquares;
	}

	public float GetMinRangeBeforeBend()
	{
		return m_minRangeBeforeBend;
	}

	public float GetMaxRangeBeforeBend()
	{
		return m_maxRangeBeforeBend;
	}

	public float GetMaxTotalRange()
	{
		return m_maxTotalRange;
	}

	public float GetMaxBendAngle()
	{
		return m_maxBendAngle;
	}

	public float GetLaserWidth()
	{
		return m_laserWidth;
	}

	public int GetMaxTargets()
	{
		return m_maxTargets;
	}

	public int GetDirectDamage()
	{
		return m_directDamage;
	}

	public int GetReturnTripDamage()
	{
		return m_returnTripDamage;
	}

	public bool GetReturnTripIgnoresCover()
	{
		return m_returnTripIgnoreCover;
	}

	protected override void AddSpecificTooltipTokens(List<TooltipTokenEntry> tokens, AbilityMod modAsBase)
	{
		AddTokenInt(tokens, "MaxTargets", string.Empty, m_maxTargets);
		AddTokenInt(tokens, "DirectDamage", string.Empty, m_directDamage);
		AddTokenInt(tokens, "ReturnTripDamage", string.Empty, m_returnTripDamage);
	}

	protected override List<AbilityTooltipNumber> CalculateAbilityTooltipNumbers()
	{
		List<AbilityTooltipNumber> list = new List<AbilityTooltipNumber>();
		list.Add(new AbilityTooltipNumber(AbilityTooltipSymbol.Damage, AbilityTooltipSubject.Primary, m_directDamage));
		list.Add(new AbilityTooltipNumber(AbilityTooltipSymbol.Damage, AbilityTooltipSubject.Secondary, m_returnTripDamage));
		return list;
	}
}
