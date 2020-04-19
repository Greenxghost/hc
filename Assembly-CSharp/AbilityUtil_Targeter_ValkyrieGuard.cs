﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUtil_Targeter_ValkyrieGuard : AbilityUtil_Targeter_Barrier
{
	public bool m_addCasterToActorsInRange;

	public float m_coverAngleLineLength = 1.5f;

	public bool m_useCone;

	private float m_coneWidthAngle;

	private float m_coneRadiusInSquares;

	private bool m_coneIgnoreLos;

	private OperationOnSquare_TurnOnHiddenSquareIndicator m_indicatorHandler;

	public AbilityUtil_Targeter_ValkyrieGuard(Ability ability, float width, bool snapToBorder = false, bool allowAimAtDiagonals = false, bool hideIfMovingFast = true) : base(ability, width, snapToBorder, allowAimAtDiagonals, hideIfMovingFast)
	{
		this.m_indicatorHandler = new OperationOnSquare_TurnOnHiddenSquareIndicator(this);
	}

	public void SetConeParams(bool useCone, float coneWidthAngle, float coneRadiusInSquares, bool ignoreLos)
	{
		this.m_useCone = useCone;
		this.m_coneWidthAngle = coneWidthAngle;
		this.m_coneRadiusInSquares = coneRadiusInSquares;
		this.m_coneIgnoreLos = ignoreLos;
	}

	public override void UpdateTargetingMultiTargets(AbilityTarget currentTarget, ActorData targetingActor, int currentTargetIndex, List<AbilityTarget> targets)
	{
		base.ClearActorsInRange();
		base.UpdateTargetingMultiTargets(currentTarget, targetingActor, currentTargetIndex, targets);
		int num;
		if (this.m_snapToBorder)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(AbilityUtil_Targeter_ValkyrieGuard.UpdateTargetingMultiTargets(AbilityTarget, ActorData, int, List<AbilityTarget>)).MethodHandle;
			}
			num = 2;
		}
		else
		{
			num = 1;
		}
		int num2 = num;
		int num3;
		if (this.m_useCone)
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
			num3 = 1;
		}
		else
		{
			num3 = 0;
		}
		int num4 = num3;
		if (this.m_highlights.Count <= num2 + 1 + num4)
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
			this.m_highlights.Add(HighlightUtils.Get().CreateBoundaryLine(this.m_coverAngleLineLength, false, true));
			this.m_highlights.Add(HighlightUtils.Get().CreateBoundaryLine(this.m_coverAngleLineLength, false, false));
			if (this.m_useCone)
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
				this.m_highlights.Add(HighlightUtils.Get().CreateConeCursor(this.m_coneRadiusInSquares * Board.\u000E().squareSize, this.m_coneWidthAngle));
			}
		}
		Vector3 barrierCenterPos = this.m_barrierCenterPos;
		float num5 = 0.5f * GameplayData.Get().m_coverProtectionAngle;
		float num6 = VectorUtils.HorizontalAngle_Deg(this.m_barrierOutwardFacing);
		float d = 0.5f * this.m_width * Board.\u000E().squareSize;
		Vector3 normalized = Vector3.Cross(this.m_barrierOutwardFacing, Vector3.up).normalized;
		this.m_highlights[num2].transform.position = barrierCenterPos - normalized * d;
		this.m_highlights[num2].transform.rotation = Quaternion.LookRotation(VectorUtils.AngleDegreesToVector(num6 + num5));
		this.m_highlights[num2 + 1].transform.position = barrierCenterPos + normalized * d;
		this.m_highlights[num2 + 1].transform.rotation = Quaternion.LookRotation(VectorUtils.AngleDegreesToVector(num6 - num5));
		if (this.m_useCone)
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
			this.m_highlights[num2].SetActive(false);
			this.m_highlights[num2 + 1].SetActive(false);
			Vector3 vector = targetingActor.\u0015();
			if (currentTargetIndex > 0)
			{
				BoardSquare boardSquare = Board.\u000E().\u000E(targets[0].GridPos);
				if (boardSquare != null)
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
					vector = boardSquare.ToVector3();
				}
			}
			Vector3 vector2 = -1f * this.m_barrierOutwardFacing;
			List<ActorData> actorsInCone = AreaEffectUtils.GetActorsInCone(vector, VectorUtils.HorizontalAngle_Deg(vector2), this.m_coneWidthAngle, this.m_coneRadiusInSquares, 0f, this.m_coneIgnoreLos, targetingActor, TargeterUtils.GetRelevantTeams(targetingActor, this.m_affectsAllies, this.m_affectsEnemies), null, false, default(Vector3));
			TargeterUtils.RemoveActorsInvisibleToActor(ref actorsInCone, targetingActor);
			actorsInCone.Remove(targetingActor);
			for (int i = 0; i < actorsInCone.Count; i++)
			{
				base.AddActorInRange(actorsInCone[i], vector, targetingActor, AbilityTooltipSubject.Primary, false);
			}
			for (;;)
			{
				switch (4)
				{
				case 0:
					continue;
				}
				break;
			}
			if (this.m_affectsTargetingActor)
			{
				base.AddActorInRange(targetingActor, vector, targetingActor, AbilityTooltipSubject.Primary, false);
			}
			GameObject gameObject = this.m_highlights[num2 + 2];
			Vector3 position = vector;
			position.y = HighlightUtils.GetHighlightHeight();
			gameObject.transform.position = position;
			gameObject.transform.rotation = Quaternion.LookRotation(vector2);
			this.DrawInvalidSquareIndicators(currentTarget, targetingActor, vector, vector2);
		}
		if (this.m_addCasterToActorsInRange)
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
			base.AddActorInRange(targetingActor, targetingActor.\u0015(), targetingActor, AbilityTooltipSubject.Self, false);
		}
	}

	private void DrawInvalidSquareIndicators(AbilityTarget currentTarget, ActorData targetingActor, Vector3 coneStartPos, Vector3 forwardDirection)
	{
		if (targetingActor == GameFlowData.Get().activeOwnedActorData)
		{
			base.ResetSquareIndicatorIndexToUse();
			float coneCenterAngleDegrees = VectorUtils.HorizontalAngle_Deg(forwardDirection);
			AreaEffectUtils.OperateOnSquaresInCone(this.m_indicatorHandler, coneStartPos, coneCenterAngleDegrees, this.m_coneWidthAngle, this.m_coneRadiusInSquares, 0f, targetingActor, this.m_coneIgnoreLos, null);
			base.HideUnusedSquareIndicators();
		}
	}
}
