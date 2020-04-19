﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIWaitingForGameScreen : MonoBehaviour
{
	public TextMeshProUGUI m_queueMatchTypeLabel;

	public TextMeshProUGUI m_statusLabel;

	public _ButtonSwapSprite m_cancelButton;

	public GameObject m_WaitingInQueueWorldParent;

	public Animator m_circleWidgetAnimController;

	public RectTransform[] m_containers;

	private void Start()
	{
		UIEventTriggerUtils.AddListener(this.m_cancelButton.gameObject, EventTriggerType.PointerClick, new UIEventTriggerUtils.EventDelegate(this.CancelClicked));
	}

	public void SetVisible(bool visible)
	{
		for (int i = 0; i < this.m_containers.Length; i++)
		{
			UIManager.SetGameObjectActive(this.m_containers[i], visible, null);
		}
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
			RuntimeMethodHandle runtimeMethodHandle = methodof(UIWaitingForGameScreen.SetVisible(bool)).MethodHandle;
		}
		UIManager.SetGameObjectActive(this.m_circleWidgetAnimController, visible, null);
		if (visible)
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
			this.m_circleWidgetAnimController.Play("searchingForMatchesAnimationLoop");
		}
		else
		{
			this.m_circleWidgetAnimController.Play("Idle");
		}
	}

	public void CancelClicked(BaseEventData data)
	{
		AppState_WaitingForGame.Get().HandleCancelClicked();
	}

	public void Setup(GameType gameType)
	{
		string text = string.Empty;
		if (gameType != GameType.Coop)
		{
			if (gameType != GameType.PvP)
			{
				if (gameType == GameType.Custom)
				{
					text = StringUtil.TR("JOINCUSTOMMATCH", "Global");
				}
			}
			else
			{
				text = StringUtil.TR("JOINVERSUSMATCH", "Global");
			}
		}
		else
		{
			text = StringUtil.TR("JOINCOOPERATIVEMATCH", "Global");
		}
		this.m_queueMatchTypeLabel.text = text;
	}

	public void UpdateQueueStatus()
	{
		if (this.m_statusLabel)
		{
			this.m_statusLabel.text = ClientGameManager.Get().GenerateQueueLabel();
		}
	}

	public void InitQueueStatus()
	{
		this.UpdateQueueStatus();
	}

	public void UpdateGameStatus(LobbyGameInfo gameInfo)
	{
		if (this.m_statusLabel)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(UIWaitingForGameScreen.UpdateGameStatus(LobbyGameInfo)).MethodHandle;
			}
			if (gameInfo.GameStatus != GameStatus.Assembling)
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
				if (gameInfo.GameStatus == GameStatus.FreelancerSelecting)
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
				}
				else
				{
					if (gameInfo.GameStatus == GameStatus.LoadoutSelecting)
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
						this.m_statusLabel.text = StringUtil.TR("SelectingLoadouts", "Global");
						return;
					}
					if (gameInfo.GameStatus == GameStatus.Launching)
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
						this.m_statusLabel.text = StringUtil.TR("GameServerStarting", "Global");
						return;
					}
					if (gameInfo.GameStatus == GameStatus.Launched)
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
						this.m_statusLabel.text = StringUtil.TR("GameServerReady", "Global");
						return;
					}
					return;
				}
			}
			if (gameInfo.ActivePlayers >= gameInfo.GameConfig.TotalHumanPlayers)
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
				this.m_statusLabel.text = StringUtil.TR("CreatingGame", "Global");
			}
			else
			{
				this.m_statusLabel.text = string.Format(StringUtil.TR("PlayersReadyCount", "Global"), gameInfo.ActivePlayers, gameInfo.GameConfig.TotalHumanPlayers);
			}
		}
	}
}
