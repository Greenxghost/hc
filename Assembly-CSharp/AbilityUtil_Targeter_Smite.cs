﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUtil_Targeter_Smite : AbilityUtil_Targeter
{
	public float m_coneAngleDegrees;

	public float m_coneLengthRadiusInSquares;

	public float m_coneBackwardOffsetInSquares;

	public bool m_conePenetrateLoS;

	public NanoSmithBoltInfo m_boltInfo;

	public float m_boltAngle = 45f;

	public int m_boltCount = 3;

	public AbilityUtil_Targeter_Smite(Ability ability, float coneAngleDegrees, float coneLengthRadiusInSquares, float coneBackwardOffsetInSquares, bool conePenetrateLoS, NanoSmithBoltInfo boltInfo, float boltAngle, int boltCount) : base(ability)
	{
		this.m_coneAngleDegrees = coneAngleDegrees;
		this.m_coneLengthRadiusInSquares = coneLengthRadiusInSquares;
		this.m_coneBackwardOffsetInSquares = coneBackwardOffsetInSquares;
		this.m_conePenetrateLoS = conePenetrateLoS;
		this.m_boltInfo = boltInfo;
		this.m_boltAngle = boltAngle;
		this.m_boltCount = boltCount;
		this.m_shouldShowActorRadius = GameWideData.Get().UseActorRadiusForCone();
	}

	public override void UpdateTargeting(AbilityTarget currentTarget, ActorData targetingActor)
	{
		base.ClearActorsInRange();
		Vector3 vector = targetingActor.\u0015();
		Vector3 vector2;
		if (currentTarget == null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(AbilityUtil_Targeter_Smite.UpdateTargeting(AbilityTarget, ActorData)).MethodHandle;
			}
			vector2 = targetingActor.transform.forward;
		}
		else
		{
			vector2 = currentTarget.AimDirection;
		}
		Vector3 vec = vector2;
		float num = VectorUtils.HorizontalAngle_Deg(vec);
		List<ActorData> actorsInCone = AreaEffectUtils.GetActorsInCone(vector, num, this.m_coneAngleDegrees, this.m_coneLengthRadiusInSquares, this.m_coneBackwardOffsetInSquares, this.m_conePenetrateLoS, targetingActor, targetingActor.\u0012(), null, false, default(Vector3));
		TargeterUtils.RemoveActorsInvisibleToClient(ref actorsInCone);
		using (List<ActorData>.Enumerator enumerator = actorsInCone.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ActorData actor = enumerator.Current;
				base.AddActorInRange(actor, vector, targetingActor, AbilityTooltipSubject.Primary, false);
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
		Vector3 vector3 = VectorUtils.AngleDegreesToVector(num);
		float d = this.m_coneBackwardOffsetInSquares * Board.\u000E().squareSize;
		float y = 0.1f - BoardSquare.s_LoSHeightOffset;
		Vector3 position = vector + new Vector3(0f, y, 0f) - vector3 * d;
		if (this.m_highlights != null)
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
			if (this.m_highlights.Count > this.m_boltCount)
			{
				goto IL_1D4;
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
		this.m_highlights = new List<GameObject>();
		float radiusInWorld = (this.m_coneLengthRadiusInSquares + this.m_coneBackwardOffsetInSquares) * Board.\u000E().squareSize;
		this.m_highlights.Add(HighlightUtils.Get().CreateConeCursor(radiusInWorld, this.m_coneAngleDegrees));
		for (int i = 0; i < this.m_boltCount; i++)
		{
			this.m_highlights.Add(HighlightUtils.Get().CreateRectangularCursor(1f, 1f, null));
		}
		IL_1D4:
		this.m_highlights[0].transform.position = position;
		this.m_highlights[0].transform.rotation = Quaternion.LookRotation(vector3);
		float squareSize = Board.\u000E().squareSize;
		float widthInWorld = this.m_boltInfo.width * squareSize;
		float maxDistanceInWorld = this.m_boltInfo.range * squareSize;
		float angle = -0.5f * (float)(this.m_boltCount - 1) * this.m_boltAngle;
		Vector3 point = Quaternion.AngleAxis(angle, Vector3.up) * vector3;
		for (int j = 0; j < this.m_boltCount; j++)
		{
			Vector3 vector4 = Quaternion.AngleAxis((float)j * this.m_boltAngle, Vector3.up) * point;
			Vector3 vector5 = vector + (this.m_coneLengthRadiusInSquares + this.m_coneBackwardOffsetInSquares) * squareSize * vector4 - this.m_coneBackwardOffsetInSquares * squareSize * vector3;
			VectorUtils.LaserCoords laserCoords;
			List<ActorData> actorsHitByBolt = this.m_boltInfo.GetActorsHitByBolt(vector5, vector4, targetingActor, AbilityPriority.Combat_Damage, out laserCoords, null, true, false, true);
			for (int k = actorsHitByBolt.Count - 1; k >= 0; k--)
			{
				ActorData item = actorsHitByBolt[k];
				if (actorsInCone.Contains(item))
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
					actorsHitByBolt.Remove(item);
				}
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
			TargeterUtils.RemoveActorsInvisibleToClient(ref actorsHitByBolt);
			using (List<ActorData>.Enumerator enumerator2 = actorsHitByBolt.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					ActorData actorData = enumerator2.Current;
					if (actorData.\u000E() == targetingActor.\u000E())
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
						base.AddActorInRange(actorData, vector5, targetingActor, AbilityTooltipSubject.Ally, false);
					}
					else
					{
						base.AddActorInRange(actorData, vector5, targetingActor, AbilityTooltipSubject.Secondary, false);
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
			VectorUtils.LaserCoords laserCoordinates = VectorUtils.GetLaserCoordinates(vector5, vector4, maxDistanceInWorld, widthInWorld, this.m_boltInfo.penetrateLineOfSight, targetingActor, null);
			VectorUtils.LaserCoords laserCoords2 = laserCoordinates;
			if (actorsHitByBolt.Count > 0)
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
				laserCoords2 = TargeterUtils.GetLaserCoordsToFarthestTarget(laserCoordinates, actorsHitByBolt);
			}
			float magnitude = (laserCoords2.end - laserCoords2.start).magnitude;
			if (magnitude > 0f)
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
				int index = j + 1;
				this.m_highlights[index].transform.position = vector5 + new Vector3(0f, y, 0f);
				this.m_highlights[index].transform.rotation = Quaternion.LookRotation(vector4);
				HighlightUtils.Get().ResizeRectangularCursor(widthInWorld, magnitude, this.m_highlights[index]);
			}
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
}
