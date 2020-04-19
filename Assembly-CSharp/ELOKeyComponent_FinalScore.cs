﻿using System;
using System.Collections.Generic;

public class ELOKeyComponent_FinalScore : ELOKeyComponent
{
	private ELOKeyComponent_FinalScore.GameTypeMode m_gameTypeMode;

	public override ELOKeyComponent.KeyModeEnum KeyMode
	{
		get
		{
			return ELOKeyComponent.KeyModeEnum.BINARY;
		}
	}

	public override ELOKeyComponent.BinaryModePhaseEnum BinaryModePhase
	{
		get
		{
			ELOKeyComponent.BinaryModePhaseEnum result;
			if (this.m_gameTypeMode == ELOKeyComponent_FinalScore.GameTypeMode.ABSOLUTE)
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
					RuntimeMethodHandle runtimeMethodHandle = methodof(ELOKeyComponent_FinalScore.get_BinaryModePhase()).MethodHandle;
				}
				result = ELOKeyComponent.BinaryModePhaseEnum.PRIMARY;
			}
			else
			{
				result = ELOKeyComponent.BinaryModePhaseEnum.SECONDARY;
			}
			return result;
		}
	}

	public static uint PhaseWidth
	{
		get
		{
			return 2U;
		}
	}

	public override char GetComponentChar()
	{
		if (this.m_gameTypeMode == ELOKeyComponent_FinalScore.GameTypeMode.ABSOLUTE)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ELOKeyComponent_FinalScore.GetComponentChar()).MethodHandle;
			}
			return '-';
		}
		if (this.m_gameTypeMode == ELOKeyComponent_FinalScore.GameTypeMode.RELATIVE)
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
			return 'R';
		}
		return '?';
	}

	public override char GetPhaseChar()
	{
		return (this.m_gameTypeMode != ELOKeyComponent_FinalScore.GameTypeMode.ABSOLUTE) ? 'R' : '0';
	}

	public override string GetPhaseDescription()
	{
		string result;
		if (this.m_gameTypeMode == ELOKeyComponent_FinalScore.GameTypeMode.ABSOLUTE)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ELOKeyComponent_FinalScore.GetPhaseDescription()).MethodHandle;
			}
			result = "absolute";
		}
		else
		{
			result = "relative";
		}
		return result;
	}

	public override void Initialize(ELOKeyComponent.BinaryModePhaseEnum phase, GameType gameType, bool isCasual)
	{
		ELOKeyComponent_FinalScore.GameTypeMode gameTypeMode;
		if (phase == ELOKeyComponent.BinaryModePhaseEnum.PRIMARY)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ELOKeyComponent_FinalScore.Initialize(ELOKeyComponent.BinaryModePhaseEnum, GameType, bool)).MethodHandle;
			}
			gameTypeMode = ELOKeyComponent_FinalScore.GameTypeMode.ABSOLUTE;
		}
		else
		{
			gameTypeMode = ELOKeyComponent_FinalScore.GameTypeMode.RELATIVE;
		}
		this.m_gameTypeMode = gameTypeMode;
	}

	public override void Initialize(List<MatchmakingQueueConfig.EloKeyFlags> flags, GameType gameType, bool isCasual)
	{
		this.m_gameTypeMode = ((!flags.Contains(MatchmakingQueueConfig.EloKeyFlags.RELATIVE)) ? ELOKeyComponent_FinalScore.GameTypeMode.ABSOLUTE : ELOKeyComponent_FinalScore.GameTypeMode.RELATIVE);
	}

	public override bool MatchesFlag(MatchmakingQueueConfig.EloKeyFlags flag)
	{
		return flag == MatchmakingQueueConfig.EloKeyFlags.RELATIVE;
	}

	public override void InitializePerCharacter(byte groupSize)
	{
	}

	internal float GetActualResult(Team team, GameResult gameResultAbsolute, float gameResultFraction)
	{
		if (gameResultAbsolute == GameResult.TieGame)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ELOKeyComponent_FinalScore.GetActualResult(Team, GameResult, float)).MethodHandle;
			}
			return 0.5f;
		}
		if (gameResultFraction < 0.5f)
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
			gameResultFraction = 0.5f;
		}
		else if (gameResultFraction > 1f)
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
			gameResultFraction = 1f;
		}
		if (gameResultAbsolute == GameResult.TeamAWon)
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
			if (team == Team.TeamA)
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
				return (this.m_gameTypeMode != ELOKeyComponent_FinalScore.GameTypeMode.ABSOLUTE) ? gameResultFraction : 1f;
			}
			if (team == Team.TeamB)
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
				return (this.m_gameTypeMode != ELOKeyComponent_FinalScore.GameTypeMode.ABSOLUTE) ? (1f - gameResultFraction) : 0f;
			}
			throw new Exception("Unexpected victor");
		}
		else
		{
			if (gameResultAbsolute != GameResult.TeamBWon)
			{
				throw new Exception("Unexpected game result");
			}
			if (team == Team.TeamB)
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
				float result;
				if (this.m_gameTypeMode == ELOKeyComponent_FinalScore.GameTypeMode.ABSOLUTE)
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
					result = 1f;
				}
				else
				{
					result = gameResultFraction;
				}
				return result;
			}
			if (team == Team.TeamA)
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
				float result2;
				if (this.m_gameTypeMode == ELOKeyComponent_FinalScore.GameTypeMode.ABSOLUTE)
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
					result2 = 0f;
				}
				else
				{
					result2 = 1f - gameResultFraction;
				}
				return result2;
			}
			throw new Exception("Unexpected victor");
		}
	}

	public bool IsRelative()
	{
		return this.m_gameTypeMode == ELOKeyComponent_FinalScore.GameTypeMode.RELATIVE;
	}

	public enum GameTypeMode
	{
		ABSOLUTE,
		RELATIVE
	}
}
