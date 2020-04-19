﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerBanner : MonoBehaviour
{
	public Image m_bannerImage;

	public Image m_profileImage;

	public Image m_ribbonImage;

	public TextMeshProUGUI m_playerName;

	public TextMeshProUGUI m_playerTitle;

	public TextMeshProUGUI m_playerLevel;

	public const string standardResourceString = "Banners/Background/02_blue";

	public const string standardEmblemResourceString = "Banners/Emblems/Chest01";

	public const string rankedModeEnemyBannerResourceString = "Banners/Background/rankedRedDefault";

	private Action<PersistedAccountData> m_onAccountDataUpdated;

	private Action<GameBalanceVars.PlayerBanner, GameBalanceVars.PlayerBanner> m_onPlayerBannerChange;

	private Action<GameBalanceVars.PlayerRibbon> m_onPlayerRibbonChange;

	private void Start()
	{
		this.m_onAccountDataUpdated = delegate(PersistedAccountData accountData)
		{
			this.SetBanner(ClientGameManager.Get().GetCurrentBackgroundBanner(), GameBalanceVars.PlayerBanner.BannerType.Background);
			this.SetBanner(ClientGameManager.Get().GetCurrentForegroundBanner(), GameBalanceVars.PlayerBanner.BannerType.Foreground);
			this.SetRibbon(ClientGameManager.Get().GetCurrentRibbon());
			if (this.m_playerName != null)
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
					RuntimeMethodHandle runtimeMethodHandle2 = methodof(UIPlayerBanner.<Start>m__0(PersistedAccountData)).MethodHandle;
				}
				this.m_playerName.text = accountData.UserName;
			}
			if (this.m_playerTitle != null)
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
				this.m_playerTitle.text = GameBalanceVars.Get().GetTitle(accountData.AccountComponent.SelectedTitleID, string.Empty, -1);
			}
			if (this.m_playerLevel != null)
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
				this.m_playerLevel.text = ClientGameManager.Get().GetDisplayedStatString(accountData);
			}
		};
		ClientGameManager.Get().OnAccountDataUpdated += this.m_onAccountDataUpdated;
		if (ClientGameManager.Get().IsPlayerAccountDataAvailable())
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(UIPlayerBanner.Start()).MethodHandle;
			}
			this.m_onAccountDataUpdated(ClientGameManager.Get().GetPlayerAccountData());
		}
		this.SetBanner(ClientGameManager.Get().GetCurrentBackgroundBanner(), GameBalanceVars.PlayerBanner.BannerType.Background);
		this.SetBanner(ClientGameManager.Get().GetCurrentForegroundBanner(), GameBalanceVars.PlayerBanner.BannerType.Foreground);
		this.SetRibbon(ClientGameManager.Get().GetCurrentRibbon());
		this.m_onPlayerBannerChange = delegate(GameBalanceVars.PlayerBanner foregroundBanner, GameBalanceVars.PlayerBanner backgroundBanner)
		{
			this.SetBanner(backgroundBanner, GameBalanceVars.PlayerBanner.BannerType.Background);
			this.SetBanner(foregroundBanner, GameBalanceVars.PlayerBanner.BannerType.Foreground);
		};
		ClientGameManager.Get().OnPlayerBannerChange += this.m_onPlayerBannerChange;
		this.m_onPlayerRibbonChange = delegate(GameBalanceVars.PlayerRibbon ribbon)
		{
			this.SetRibbon(ClientGameManager.Get().GetCurrentRibbon());
		};
		ClientGameManager.Get().OnPlayerRibbonChange += this.m_onPlayerRibbonChange;
	}

	private void OnDestroy()
	{
		ClientGameManager clientGameManager = ClientGameManager.Get();
		if (clientGameManager == null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(UIPlayerBanner.OnDestroy()).MethodHandle;
			}
			return;
		}
		clientGameManager.OnAccountDataUpdated -= this.m_onAccountDataUpdated;
		clientGameManager.OnPlayerBannerChange -= this.m_onPlayerBannerChange;
		clientGameManager.OnPlayerRibbonChange -= this.m_onPlayerRibbonChange;
	}

	private void SetBanner(GameBalanceVars.PlayerBanner banner, GameBalanceVars.PlayerBanner.BannerType bannerType)
	{
		Sprite sprite;
		if (banner != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(UIPlayerBanner.SetBanner(GameBalanceVars.PlayerBanner, GameBalanceVars.PlayerBanner.BannerType)).MethodHandle;
			}
			sprite = (Sprite)Resources.Load(banner.m_resourceString, typeof(Sprite));
			if (sprite == null)
			{
				Log.Warning(Log.Category.UI, string.Format("Could not load banner resource from [{0}] as sprite.", banner.m_resourceString), new object[0]);
			}
		}
		else
		{
			sprite = (Sprite)Resources.Load("Banners/Background/02_blue", typeof(Sprite));
			if (sprite == null)
			{
				Log.Warning(Log.Category.UI, string.Format("Could not load banner resource from [{0}] as sprite.", "Banners/Background/02_blue"), new object[0]);
			}
		}
		if (sprite != null)
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
			if (bannerType == GameBalanceVars.PlayerBanner.BannerType.Background)
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
				this.m_bannerImage.sprite = sprite;
			}
			else
			{
				this.m_profileImage.sprite = sprite;
			}
		}
	}

	private void SetRibbon(GameBalanceVars.PlayerRibbon ribbon)
	{
		if (this.m_ribbonImage == null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(UIPlayerBanner.SetRibbon(GameBalanceVars.PlayerRibbon)).MethodHandle;
			}
			return;
		}
		Sprite sprite = null;
		if (ribbon != null)
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
			sprite = Resources.Load<Sprite>(ribbon.m_resourceString);
			if (sprite == null)
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
				Log.Warning(Log.Category.UI, string.Format("Could not load ribbon resource from [{0}] as sprite.", ribbon.m_resourceString), new object[0]);
			}
		}
		UIManager.SetGameObjectActive(this.m_ribbonImage, sprite != null, null);
		this.m_ribbonImage.sprite = sprite;
	}
}
