﻿using System;
using System.Collections.Generic;

[Serializable]
public class LobbyGameSummary
{
	public string GameServerAddress;

	public GameResult GameResult;

	public float GameResultFraction = 0.5f;

	public string TimeText = string.Empty;

	public int NumOfTurns;

	public int TeamAPoints;

	public int TeamBPoints;

	public TimeSpan MatchTime;

	public List<PlayerGameSummary> PlayerGameSummaryList = new List<PlayerGameSummary>();

	public Dictionary<Team, Dictionary<int, ELODancecard>> m_ELODancecard = new Dictionary<Team, Dictionary<int, ELODancecard>>();

	public List<BadgeAndParticipantInfo> BadgeAndParticipantsInfo;

	public ELODancecard GetEloDancecardByAccountId(long accountId)
	{
		using (Dictionary<Team, Dictionary<int, ELODancecard>>.Enumerator enumerator = this.m_ELODancecard.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<Team, Dictionary<int, ELODancecard>> keyValuePair = enumerator.Current;
				foreach (ELODancecard elodancecard in keyValuePair.Value.Values)
				{
					if (elodancecard.m_accountId == accountId)
					{
						return elodancecard;
					}
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
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(LobbyGameSummary.GetEloDancecardByAccountId(long)).MethodHandle;
			}
		}
		Log.Warning("Account {0}'s dancecard not found in game", new object[]
		{
			accountId
		});
		return null;
	}

	public ELODancecard GetEloDancecardByPlayerId(int playerId)
	{
		using (Dictionary<Team, Dictionary<int, ELODancecard>>.Enumerator enumerator = this.m_ELODancecard.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<Team, Dictionary<int, ELODancecard>> keyValuePair = enumerator.Current;
				ELODancecard result;
				if (keyValuePair.Value.TryGetValue(playerId, out result))
				{
					return result;
				}
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
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(LobbyGameSummary.GetEloDancecardByPlayerId(int)).MethodHandle;
			}
		}
		Log.Warning("PlayerId {0}'s dancecard not found in game", new object[]
		{
			playerId
		});
		return null;
	}

	public void CreateELODancecard(int playerId, Team teamId, long accountId, long groupId, byte groupSize)
	{
		Dictionary<int, ELODancecard> dictionary;
		if (this.m_ELODancecard.TryGetValue(teamId, out dictionary))
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(LobbyGameSummary.CreateELODancecard(int, Team, long, long, byte)).MethodHandle;
			}
			dictionary.Add(playerId, ELODancecard.Create(accountId, groupId, groupSize));
		}
		else
		{
			this.m_ELODancecard.Add(teamId, new Dictionary<int, ELODancecard>());
			this.m_ELODancecard[teamId].Add(playerId, ELODancecard.Create(accountId, groupId, groupSize));
		}
	}

	public void UpdateELODancecard(int playerId, Team teamId, long accountId, bool isBot, BotDifficulty difficulty)
	{
		Dictionary<int, ELODancecard> dictionary;
		if (this.m_ELODancecard.TryGetValue(teamId, out dictionary))
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(LobbyGameSummary.UpdateELODancecard(int, Team, long, bool, BotDifficulty)).MethodHandle;
			}
			ELODancecard elodancecard;
			if (dictionary.TryGetValue(playerId, out elodancecard))
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
				elodancecard.Increment(isBot, difficulty);
			}
			else
			{
				dictionary.Add(playerId, ELODancecard.Create(accountId, isBot, difficulty));
			}
		}
		else
		{
			this.m_ELODancecard.Add(teamId, new Dictionary<int, ELODancecard>());
			this.m_ELODancecard[teamId].Add(playerId, ELODancecard.Create(accountId, isBot, difficulty));
		}
	}
}
