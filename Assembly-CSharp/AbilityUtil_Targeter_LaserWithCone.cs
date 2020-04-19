﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUtil_Targeter_LaserWithCone : AbilityUtil_Targeter
{
	public float m_width = 1f;

	public float m_distance = 15f;

	public float m_coneBackwardOffsetInSquares;

	public bool m_penetrateLoS;

	private float m_coneWidthAngle = 60f;

	protected float m_coneLengthRadiusInSquares = 4f;

	private bool m_explodeOnPathEnd;

	private bool m_explodeOnEnvironmentHit;

	private bool m_clampToCursorPos;

	private bool m_snapToTargetSquareWhenClampRange;

	public float m_minRangeIfClampToCursor;

	private bool m_laserIgnoreCover;

	private bool m_explosionIgnoreCover;

	private bool m_explosionPenetrateLos;

	private bool m_addLaserHitActorAsPrimary = true;

	protected int m_maxLaserTargets = 1;

	private OperationOnSquare_TurnOnHiddenSquareIndicator m_indicatorHandler;

	private List<ISquareInsideChecker> m_squarePosCheckerList = new List<ISquareInsideChecker>();

	private List<ISquareInsideChecker> m_coneOnlyCheckerList = new List<ISquareInsideChecker>();

	private SquareInsideChecker_Box m_laserChecker;

	private SquareInsideChecker_Cone m_coneChecker;

	public Vector3 m_lastLaserEndPos;

	public AbilityUtil_Targeter_LaserWithCone(Ability ability, float width, float distance, bool penetrateLoS, bool affectsAllies, float coneWidthAngle, float coneLengthRadiusInSquares, float coneBackwardOffsetInSquares) : base(ability)
	{
		this.m_width = width;
		this.m_distance = distance;
		this.m_coneBackwardOffsetInSquares = coneBackwardOffsetInSquares;
		this.m_penetrateLoS = penetrateLoS;
		this.m_affectsAllies = affectsAllies;
		this.m_coneWidthAngle = coneWidthAngle;
		this.m_coneLengthRadiusInSquares = coneLengthRadiusInSquares;
		this.m_shouldShowActorRadius = GameWideData.Get().UseActorRadiusForLaser();
		this.m_indicatorHandler = new OperationOnSquare_TurnOnHiddenSquareIndicator(this);
		this.m_laserChecker = new SquareInsideChecker_Box(this.m_width);
		this.m_coneChecker = new SquareInsideChecker_Cone();
		this.m_squarePosCheckerList.Add(this.m_laserChecker);
		this.m_squarePosCheckerList.Add(this.m_coneChecker);
		this.m_coneOnlyCheckerList.Add(this.m_coneChecker);
	}

	public void SetClampToCursorPos(bool value)
	{
		this.m_clampToCursorPos = value;
	}

	public void SetSnapToTargetSquareWhenClampRange(bool value)
	{
		this.m_snapToTargetSquareWhenClampRange = value;
	}

	public void SetExplodeOnEnvironmentHit(bool value)
	{
		this.m_explodeOnEnvironmentHit = value;
	}

	public void SetExplodeOnPathEnd(bool value)
	{
		this.m_explodeOnPathEnd = value;
	}

	public void SetCoverAndLosConfig(bool laserIgnoreCover, bool explosionIgnoreCover, bool explosionPenetrateLos)
	{
		this.m_laserIgnoreCover = laserIgnoreCover;
		this.m_explosionIgnoreCover = explosionIgnoreCover;
		this.m_explosionPenetrateLos = explosionPenetrateLos;
	}

	public void SetMaxLaserTargets(int maxLaserTargets)
	{
		this.m_maxLaserTargets = maxLaserTargets;
	}

	public void SetAddDirectHitActorAsPrimary(bool value)
	{
		this.m_addLaserHitActorAsPrimary = value;
	}

	private bool SnapToTargetSquare()
	{
		bool result;
		if (this.m_clampToCursorPos)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(AbilityUtil_Targeter_LaserWithCone.SnapToTargetSquare()).MethodHandle;
			}
			result = this.m_snapToTargetSquareWhenClampRange;
		}
		else
		{
			result = false;
		}
		return result;
	}

	public virtual float GetWidth()
	{
		return this.m_width;
	}

	public virtual float GetDistance()
	{
		return this.m_distance;
	}

	public virtual bool GetPenetrateLoS()
	{
		return this.m_penetrateLoS;
	}

	public virtual int GetLaserMaxTargets()
	{
		return this.m_maxLaserTargets;
	}

	public virtual float GetConeRadius()
	{
		return this.m_coneLengthRadiusInSquares;
	}

	public virtual float GetConeWidthAngle()
	{
		return this.m_coneWidthAngle;
	}

	public virtual bool GetConeAffectsTarget(ActorData potentialTarget, ActorData targetingActor)
	{
		return base.GetAffectsTarget(potentialTarget, targetingActor);
	}

	public virtual void AddTargetedActor(ActorData actor, Vector3 damageOrigin, ActorData targetingActor, AbilityTooltipSubject subjectType = AbilityTooltipSubject.Primary)
	{
		base.AddActorInRange(actor, damageOrigin, targetingActor, subjectType, false);
	}

	private void DisableConeHighlights()
	{
		if (this.m_highlights != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(AbilityUtil_Targeter_LaserWithCone.DisableConeHighlights()).MethodHandle;
			}
			for (int i = 1; i < this.m_highlights.Count; i++)
			{
				this.m_highlights[i].SetActive(false);
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
		}
	}

	public override void UpdateTargeting(AbilityTarget currentTarget, ActorData targetingActor)
	{
		base.ClearActorsInRange();
		Vector3 vector;
		if (currentTarget == null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(AbilityUtil_Targeter_LaserWithCone.UpdateTargeting(AbilityTarget, ActorData)).MethodHandle;
			}
			vector = targetingActor.transform.forward;
		}
		else
		{
			vector = currentTarget.AimDirection;
		}
		Vector3 vector2 = vector;
		Vector3 b = currentTarget.FreePos;
		BoardSquare boardSquare = Board.\u000E().\u000E(currentTarget.GridPos);
		if (this.SnapToTargetSquare() && boardSquare != null)
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
			if (boardSquare != targetingActor.\u0012())
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
				vector2 = boardSquare.ToVector3() - targetingActor.\u0016();
				vector2.y = 0f;
				vector2.Normalize();
				b = boardSquare.ToVector3();
			}
		}
		Vector3 vector3 = targetingActor.\u0015();
		float num = this.GetDistance();
		if (this.m_clampToCursorPos)
		{
			float num2 = VectorUtils.HorizontalPlaneDistInSquares(targetingActor.\u0016(), b);
			if (this.m_minRangeIfClampToCursor > 0f)
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
				if (num2 < this.m_minRangeIfClampToCursor)
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
					num2 = this.m_minRangeIfClampToCursor;
				}
			}
			num = Mathf.Min(num2, num);
		}
		VectorUtils.LaserCoords adjustedCoords;
		adjustedCoords.start = targetingActor.\u0015();
		List<ActorData> actorsInLaser = AreaEffectUtils.GetActorsInLaser(adjustedCoords.start, vector2, num, this.GetWidth(), targetingActor, base.GetAffectedTeams(), this.GetPenetrateLoS(), this.GetLaserMaxTargets(), false, false, out adjustedCoords.end, null, null, false, true);
		bool flag = AreaEffectUtils.LaserHitWorldGeo(num, adjustedCoords, this.GetPenetrateLoS(), actorsInLaser);
		float widthInWorld = this.GetWidth() * Board.\u000E().squareSize;
		float y = 0.1f - BoardSquare.s_LoSHeightOffset;
		Vector3 start = adjustedCoords.start;
		Vector3 end = adjustedCoords.end;
		float magnitude = (end - vector3).magnitude;
		if (base.Highlight == null)
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
			base.Highlight = HighlightUtils.Get().CreateRectangularCursor(widthInWorld, magnitude, null);
		}
		else
		{
			HighlightUtils.Get().ResizeRectangularCursor(widthInWorld, magnitude, base.Highlight);
		}
		base.Highlight.transform.position = start + new Vector3(0f, y, 0f);
		base.Highlight.transform.rotation = Quaternion.LookRotation(vector2);
		if (this.m_addLaserHitActorAsPrimary)
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
			foreach (ActorData actorData in actorsInLaser)
			{
				Vector3 vector4;
				if (this.m_laserIgnoreCover)
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
					vector4 = actorData.\u0016();
				}
				else
				{
					vector4 = vector3;
				}
				Vector3 damageOrigin = vector4;
				this.AddTargetedActor(actorData, damageOrigin, targetingActor, AbilityTooltipSubject.Primary);
			}
		}
		this.m_lastLaserEndPos = end;
		Vector3 vector5 = end;
		Vector3 vector6 = vector5;
		float num3 = VectorUtils.HorizontalAngle_Deg(vector2);
		bool flag2;
		if (!this.m_explodeOnPathEnd)
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
			if (flag)
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
				if (this.m_explodeOnEnvironmentHit)
				{
					goto IL_336;
				}
				for (;;)
				{
					switch (2)
					{
					case 0:
						continue;
					}
					break;
				}
			}
			flag2 = (actorsInLaser.Count > 0);
			goto IL_337;
		}
		IL_336:
		flag2 = true;
		IL_337:
		bool flag3 = flag2;
		if (flag3)
		{
			this.CreateConeHighlights(vector5, num3);
			if (!this.m_explosionPenetrateLos)
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
				vector6 = AbilityCommon_LaserWithCone.GetConeLosCheckPos(adjustedCoords.start, vector5);
			}
			List<ActorData> actorsInCone = AreaEffectUtils.GetActorsInCone(vector5, num3, this.GetConeWidthAngle(), this.GetConeRadius(), this.m_coneBackwardOffsetInSquares, this.m_explosionPenetrateLos, targetingActor, null, null, true, vector6);
			TargeterUtils.RemoveActorsInvisibleToClient(ref actorsInCone);
			using (List<ActorData>.Enumerator enumerator2 = actorsInCone.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					ActorData actorData2 = enumerator2.Current;
					if (actorData2 != null && this.GetConeAffectsTarget(actorData2, targetingActor))
					{
						Vector3 vector7;
						if (this.m_explosionIgnoreCover)
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
							vector7 = actorData2.\u0016();
						}
						else
						{
							vector7 = vector5;
						}
						Vector3 damageOrigin2 = vector7;
						this.AddTargetedActor(actorData2, damageOrigin2, targetingActor, AbilityTooltipSubject.Secondary);
					}
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
		}
		else
		{
			this.DisableConeHighlights();
		}
		if (targetingActor == GameFlowData.Get().activeOwnedActorData)
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
			this.m_laserChecker.UpdateBoxProperties(adjustedCoords.start, adjustedCoords.end, targetingActor);
			this.m_coneChecker.UpdateConeProperties(vector5, this.GetConeWidthAngle(), this.GetConeRadius(), this.m_coneBackwardOffsetInSquares, num3, targetingActor);
			if (!this.GetPenetrateLoS())
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
				this.m_coneChecker.SetLosPosOverride(true, vector6, true);
			}
			base.ResetSquareIndicatorIndexToUse();
			bool flag4 = this.GetWidth() > 0f;
			if (flag4)
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
				IOperationOnSquare indicatorHandler = this.m_indicatorHandler;
				Vector3 start2 = adjustedCoords.start;
				Vector3 end2 = adjustedCoords.end;
				float width = this.GetWidth();
				bool penetrateLoS = this.GetPenetrateLoS();
				List<Vector3> additionalLosSources = null;
				List<ISquareInsideChecker> losCheckOverrides;
				if (flag3)
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
					losCheckOverrides = this.m_squarePosCheckerList;
				}
				else
				{
					losCheckOverrides = null;
				}
				AreaEffectUtils.OperateOnSquaresInBoxByActorRadius(indicatorHandler, start2, end2, width, targetingActor, penetrateLoS, additionalLosSources, losCheckOverrides, true);
			}
			if (flag3)
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
				IOperationOnSquare indicatorHandler2 = this.m_indicatorHandler;
				Vector3 coneStart = vector5;
				float coneCenterAngleDegrees = num3;
				float coneWidthAngle = this.GetConeWidthAngle();
				float coneRadius = this.GetConeRadius();
				float coneBackwardOffsetInSquares = this.m_coneBackwardOffsetInSquares;
				bool penetrateLoS2 = this.GetPenetrateLoS();
				List<ISquareInsideChecker> losCheckOverrides2;
				if (flag4)
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
					losCheckOverrides2 = this.m_squarePosCheckerList;
				}
				else
				{
					losCheckOverrides2 = this.m_coneOnlyCheckerList;
				}
				AreaEffectUtils.OperateOnSquaresInCone(indicatorHandler2, coneStart, coneCenterAngleDegrees, coneWidthAngle, coneRadius, coneBackwardOffsetInSquares, targetingActor, penetrateLoS2, losCheckOverrides2);
			}
			base.HideUnusedSquareIndicators();
		}
	}

	private void CreateConeHighlights(Vector3 coneOrigin, float aimDir_degrees)
	{
		Vector3 vector = VectorUtils.AngleDegreesToVector(aimDir_degrees);
		float d = this.m_coneBackwardOffsetInSquares * Board.\u000E().squareSize;
		float y = 0.1f - BoardSquare.s_LoSHeightOffset;
		Vector3 position = coneOrigin + new Vector3(0f, y, 0f) - vector * d;
		this.AllocateConeHighlights();
		for (int i = 1; i < this.m_highlights.Count; i++)
		{
			this.m_highlights[i].transform.position = position;
			this.m_highlights[i].transform.rotation = Quaternion.LookRotation(vector);
			this.m_highlights[i].gameObject.SetActive(true);
		}
	}

	protected virtual void AllocateConeHighlights()
	{
		if (this.m_highlights.Count == 1)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(AbilityUtil_Targeter_LaserWithCone.AllocateConeHighlights()).MethodHandle;
			}
			float radiusInWorld = (this.GetConeRadius() + this.m_coneBackwardOffsetInSquares) * Board.\u000E().squareSize;
			GameObject item = HighlightUtils.Get().CreateConeCursor(radiusInWorld, this.GetConeWidthAngle());
			this.m_highlights.Add(item);
		}
	}

	protected override Vector3 GetTargetingArcEndPosition(ActorData targetingActor)
	{
		if (this.m_highlights != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(AbilityUtil_Targeter_LaserWithCone.GetTargetingArcEndPosition(ActorData)).MethodHandle;
			}
			if (this.m_highlights.Count > 1)
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
				if (this.m_highlights[1] != null)
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
					return this.m_highlights[1].transform.position;
				}
			}
		}
		return base.GetTargetingArcEndPosition(targetingActor);
	}
}
