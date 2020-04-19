﻿using System;
using System.Collections.Generic;

public class MatchObjectiveKill : MatchObjective
{
	public MatchObjectiveKill.KillObjectiveType killType = MatchObjectiveKill.KillObjectiveType.Player;

	public string m_tag = string.Empty;

	public int pointAdjustForKillingTeam = 1;

	public int pointAdjustForDyingTeam;

	public List<MatchObjectiveKill.CharacterKillPointAdjustOverride> m_characterTypeOverrides;

	public bool IsActorRelevant(ActorData actor)
	{
		bool result;
		switch (this.killType)
		{
		case MatchObjectiveKill.KillObjectiveType.Actor:
			result = true;
			break;
		case MatchObjectiveKill.KillObjectiveType.NPC:
			result = !GameplayUtils.IsPlayerControlled(actor);
			break;
		case MatchObjectiveKill.KillObjectiveType.Player:
			result = GameplayUtils.IsPlayerControlled(actor);
			break;
		case MatchObjectiveKill.KillObjectiveType.Minion:
			result = GameplayUtils.IsMinion(actor);
			break;
		case MatchObjectiveKill.KillObjectiveType.ActorWithTag:
			result = actor.HasTag(this.m_tag);
			break;
		case MatchObjectiveKill.KillObjectiveType.ActorWithoutTag:
			result = !actor.HasTag(this.m_tag);
			break;
		default:
			result = false;
			break;
		}
		return result;
	}

	private unsafe void GetPointAdjusts(ActorData actor, out int pointsForDyingTeam, out int pointsForKillingTeam)
	{
		pointsForDyingTeam = this.pointAdjustForDyingTeam;
		pointsForKillingTeam = this.pointAdjustForKillingTeam;
		if (this.m_characterTypeOverrides != null)
		{
			for (int i = 0; i < this.m_characterTypeOverrides.Count; i++)
			{
				if (this.m_characterTypeOverrides[i].IsOverrideRelevantForActor(actor))
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
						RuntimeMethodHandle runtimeMethodHandle = methodof(MatchObjectiveKill.GetPointAdjusts(ActorData, int*, int*)).MethodHandle;
					}
					pointsForDyingTeam = this.m_characterTypeOverrides[i].m_pointAdjustOverrideForDyingTeam;
					pointsForKillingTeam = this.m_characterTypeOverrides[i].m_pointAdjustOverrideForKillingTeam;
					return;
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
		}
	}

	public override void Server_OnActorDeath(ActorData actor)
	{
		Log.Info(Log.Category.Temp, "MatchObjectiveKill.OnActorDeath", new object[0]);
		ObjectivePoints objectivePoints = ObjectivePoints.Get();
		if (objectivePoints != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(MatchObjectiveKill.Server_OnActorDeath(ActorData)).MethodHandle;
			}
			if (this.IsActorRelevant(actor))
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
				int adjustAmount;
				int adjustAmount2;
				this.GetPointAdjusts(actor, out adjustAmount, out adjustAmount2);
				Team team = actor.\u000E();
				objectivePoints.AdjustPoints(adjustAmount, team);
				objectivePoints.AdjustPoints(adjustAmount2, (team != Team.TeamA) ? Team.TeamA : Team.TeamB);
			}
		}
	}

	public override void Client_OnActorDeath(ActorData actor)
	{
		ObjectivePoints objectivePoints = ObjectivePoints.Get();
		if (objectivePoints != null && this.IsActorRelevant(actor))
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(MatchObjectiveKill.Client_OnActorDeath(ActorData)).MethodHandle;
			}
			int adjustAmount;
			int num;
			this.GetPointAdjusts(actor, out adjustAmount, out num);
			Team team = actor.\u000E();
			objectivePoints.AdjustUnresolvedPoints(adjustAmount, team);
			ObjectivePoints objectivePoints2 = objectivePoints;
			int adjustAmount2 = num;
			Team teamToAdjust;
			if (team == Team.TeamA)
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
				teamToAdjust = Team.TeamB;
			}
			else
			{
				teamToAdjust = Team.TeamA;
			}
			objectivePoints2.AdjustUnresolvedPoints(adjustAmount2, teamToAdjust);
		}
	}

	[Serializable]
	public class CharacterKillPointAdjustOverride
	{
		public List<CharacterType> m_killedCharacterTypes;

		public int m_pointAdjustOverrideForKillingTeam;

		public int m_pointAdjustOverrideForDyingTeam;

		public bool IsOverrideRelevantForActor(ActorData actor)
		{
			if (!(actor == null))
			{
				if (!(actor.\u000E() == null))
				{
					return this.m_killedCharacterTypes.Contains(actor.\u000E().m_characterType);
				}
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
					RuntimeMethodHandle runtimeMethodHandle = methodof(MatchObjectiveKill.CharacterKillPointAdjustOverride.IsOverrideRelevantForActor(ActorData)).MethodHandle;
				}
			}
			return false;
		}
	}

	public enum KillObjectiveType
	{
		Actor,
		NPC,
		Player,
		Minion,
		ActorWithTag,
		ActorWithoutTag
	}
}
