﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FriendListBannerMenu : UITooltipBase
{
	public TextMeshProUGUI m_playerName;

	public Color m_unhighlightedMenuItemColor;

	public FriendListBannerMenu.FriendListTooltipBannerButton[] m_menuButtons;

	public FriendListMenuGroupChat m_groupSubMenu;

	private FriendInfo m_friendInfo;

	public void Start()
	{
		for (int i = 0; i < this.m_menuButtons.Length; i++)
		{
			FriendListBannerMenu.FriendMenuButtonAction action = (FriendListBannerMenu.FriendMenuButtonAction)i;
			if (this.IsValidButtonAction(action, true))
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
					RuntimeMethodHandle runtimeMethodHandle = methodof(FriendListBannerMenu.Start()).MethodHandle;
				}
				UIEventTriggerUtils.AddListener(this.m_menuButtons[i].m_button.gameObject, EventTriggerType.PointerEnter, new UIEventTriggerUtils.EventDelegate(this.OnGroupChatMouseOver));
				UIEventTriggerUtils.AddListener(this.m_menuButtons[i].m_button.gameObject, EventTriggerType.PointerExit, new UIEventTriggerUtils.EventDelegate(this.OnGroupChatMouseExit));
				UIEventTriggerUtils.AddListener(this.m_menuButtons[i].m_button.gameObject, EventTriggerType.PointerClick, new UIEventTriggerUtils.EventDelegate(this.OnGroupChatMouseClicked));
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

	public bool IsValidButtonAction(FriendListBannerMenu.FriendMenuButtonAction action, bool IsForSetup = false)
	{
		if (action == FriendListBannerMenu.FriendMenuButtonAction.InviteToGame)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(FriendListBannerMenu.IsValidButtonAction(FriendListBannerMenu.FriendMenuButtonAction, bool)).MethodHandle;
			}
			if (GameManager.Get() != null)
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
				if (GameManager.Get().GameInfo != null && GameManager.Get().GameInfo.GameConfig != null)
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
					bool result;
					if (GameManager.Get().GameInfo.GameConfig.GameType == GameType.Custom)
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
						result = (GameManager.Get().GameInfo.GameStatus != GameStatus.Stopped);
					}
					else
					{
						result = false;
					}
					return result;
				}
			}
			return IsForSetup;
		}
		if (action == FriendListBannerMenu.FriendMenuButtonAction.InviteToParty)
		{
			if (GameManager.Get() != null)
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
				if (GameManager.Get().GameInfo != null)
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
					if (GameManager.Get().GameInfo.GameConfig != null)
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
						return GameManager.Get().GameInfo.GameConfig.GameType != GameType.Custom || GameManager.Get().GameInfo.GameStatus == GameStatus.Stopped;
					}
				}
			}
			return true;
		}
		if (action == FriendListBannerMenu.FriendMenuButtonAction.ObserveGame)
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
			if (GameManager.Get() != null)
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
				if (GameManager.Get().GameplayOverrides != null)
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
					return this.m_friendInfo.IsJoinable(GameManager.Get().GameplayOverrides);
				}
			}
			return true;
		}
		if (action != FriendListBannerMenu.FriendMenuButtonAction.SendMessage)
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
			if (action != FriendListBannerMenu.FriendMenuButtonAction.BlockPlayer)
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
				if (action != FriendListBannerMenu.FriendMenuButtonAction.RemoveFriend)
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
					if (action != FriendListBannerMenu.FriendMenuButtonAction.ReportPlayer)
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
						return action == FriendListBannerMenu.FriendMenuButtonAction.AddNote;
					}
				}
			}
		}
		return true;
	}

	private void OpenAddNoteBox()
	{
		string title = StringUtil.TR("FriendNote", "Global");
		string description = string.Format(StringUtil.TR("AddANoteFor", "Global"), this.m_friendInfo.FriendHandle);
		UIDialogPopupManager.OpenSingleLineInputDialog(title, description, StringUtil.TR("Ok", "Global"), StringUtil.TR("Cancel", "Global"), delegate(UIDialogBox box)
		{
			string text = (box as UISingleInputLineInputDialogBox).m_descriptionBoxInputField.text;
			string arguments = string.Format("{0} {1} {2}", StringUtil.TR("NoteFriend", "SlashCommand"), this.m_friendInfo.FriendHandle, text);
			SlashCommands.Get().RunSlashCommand("/friend", arguments);
		}, null);
	}

	public void OnGroupChatMouseClicked(BaseEventData data)
	{
		for (int i = 0; i < this.m_menuButtons.Length; i++)
		{
			if (this.IsValidButtonAction((FriendListBannerMenu.FriendMenuButtonAction)i, false))
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
					RuntimeMethodHandle runtimeMethodHandle = methodof(FriendListBannerMenu.OnGroupChatMouseClicked(BaseEventData)).MethodHandle;
				}
				if ((data as PointerEventData).pointerCurrentRaycast.gameObject == this.m_menuButtons[i].m_button.gameObject)
				{
					switch (i)
					{
					case 0:
						FriendListPanel.Get().RequestToSendMessage(this.m_friendInfo);
						break;
					case 1:
						FriendListPanel.Get().RequestToInviteToParty(this.m_friendInfo);
						break;
					case 2:
						FriendListPanel.Get().RequestToViewProfile(this.m_friendInfo);
						break;
					case 4:
						FriendListPanel.Get().RequestToBlockPlayer(this.m_friendInfo);
						break;
					case 5:
						UILandingPageFullScreenMenus.Get().SetReportContainerVisible(true, this.m_friendInfo.FriendHandle, this.m_friendInfo.FriendAccountId, false);
						break;
					case 6:
						FriendListPanel.Get().RequestToRemoveFriend(this.m_friendInfo);
						break;
					case 7:
						FriendListPanel.Get().RequestToInviteToGame(this.m_friendInfo);
						break;
					case 8:
						FriendListPanel.Get().RequestToObserveGame(this.m_friendInfo);
						break;
					case 9:
						this.OpenAddNoteBox();
						break;
					}
					break;
				}
			}
		}
		base.SetVisible(false);
	}

	private void OnDisable()
	{
	}

	public void OnGroupChatMouseOver(BaseEventData data)
	{
		for (int i = 0; i < this.m_menuButtons.Length; i++)
		{
			FriendListBannerMenu.FriendMenuButtonAction action = (FriendListBannerMenu.FriendMenuButtonAction)i;
			if (this.IsValidButtonAction(action, false))
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
					RuntimeMethodHandle runtimeMethodHandle = methodof(FriendListBannerMenu.OnGroupChatMouseOver(BaseEventData)).MethodHandle;
				}
				if ((data as PointerEventData).pointerCurrentRaycast.gameObject == this.m_menuButtons[i].m_button.gameObject)
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
					this.m_menuButtons[i].m_icon.color = Color.white;
					this.m_menuButtons[i].m_label.color = Color.white;
				}
				else
				{
					this.m_menuButtons[i].m_icon.color = this.m_unhighlightedMenuItemColor;
					this.m_menuButtons[i].m_label.color = this.m_unhighlightedMenuItemColor;
				}
			}
			else
			{
				this.m_menuButtons[i].m_icon.color = Color.gray;
				this.m_menuButtons[i].m_label.color = Color.gray;
			}
		}
		if ((data as PointerEventData).pointerCurrentRaycast.gameObject == this.m_menuButtons[3].m_button.gameObject)
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
			UIManager.SetGameObjectActive(this.m_groupSubMenu, true, null);
			this.m_groupSubMenu.Setup();
		}
		else
		{
			UIManager.SetGameObjectActive(this.m_groupSubMenu, false, null);
		}
	}

	public void OnGroupChatMouseExit(BaseEventData data)
	{
	}

	public void Setup(FriendInfo friendInfo)
	{
		this.m_friendInfo = friendInfo;
		this.m_playerName.text = friendInfo.FriendHandle;
		UIManager.SetGameObjectActive(this.m_groupSubMenu, false, null);
		for (int i = 0; i < this.m_menuButtons.Length; i++)
		{
			FriendListBannerMenu.FriendMenuButtonAction action = (FriendListBannerMenu.FriendMenuButtonAction)i;
			if (this.IsValidButtonAction(action, false))
			{
				this.m_menuButtons[i].m_icon.color = this.m_unhighlightedMenuItemColor;
				this.m_menuButtons[i].m_label.color = this.m_unhighlightedMenuItemColor;
			}
			else
			{
				this.m_menuButtons[i].m_icon.color = Color.gray;
				this.m_menuButtons[i].m_label.color = Color.gray;
			}
		}
	}

	public enum FriendMenuButtonAction
	{
		SendMessage,
		InviteToParty,
		ViewProfile,
		InviteToGroupChat,
		BlockPlayer,
		ReportPlayer,
		RemoveFriend,
		InviteToGame,
		ObserveGame,
		AddNote
	}

	[Serializable]
	public struct FriendListTooltipBannerButton
	{
		public Image m_icon;

		public TextMeshProUGUI m_label;

		public Button m_button;
	}
}
