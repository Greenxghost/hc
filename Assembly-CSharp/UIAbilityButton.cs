﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIAbilityButton : MonoBehaviour
{
	public Image[] m_phaseIndicators;

	public RectTransform m_ultimateBorder;

	public RectTransform m_persistentAuraBorder;

	public Mask m_ToggleAbility;

	public Animator m_AbilityButtonAnimator;

	public Image m_borderGlow;

	public GameObject m_tutorialTip;

	public RectTransform m_chargeCounterContainer;

	public UIAbilityChargeCounter[] chargeCounters;

	public Image m_background;

	public Image m_default;

	public Image m_selected;

	public Image m_pressedIdle;

	public Image m_pressedIdle2;

	public Image m_hover;

	public Image m_pressed;

	public Image m_selectedGlow;

	public Image m_disabled;

	public Image m_charIcon;

	public Image m_freeactionIcon;

	public TextMeshProUGUI m_hotkeyLabel;

	public TextMeshProUGUI m_cooldownLabel;

	public Button m_hitbox;

	private bool m_mouseDown;

	private bool m_mouseOver;

	private bool m_mouseOverForRangePreview;

	private AbilityData.AbilityEntry m_abilityEntry;

	private AbilityData.ActionType m_actionType;

	private AbilityData m_abilityData;

	private ActorData m_actorData;

	private ActorTurnSM m_turnSM;

	public void Awake()
	{
		UIManager.SetGameObjectActive(this.m_borderGlow, false, null);
		UIManager.SetGameObjectActive(this.m_ToggleAbility, false, null);
		UIManager.SetGameObjectActive(this.m_tutorialTip, false, null);
		this.m_hitbox.GetComponent<UITooltipHoverObject>().Setup(TooltipType.Ability, new TooltipPopulateCall(this.ShowTooltip), null);
	}

	public void Start()
	{
		if (this.m_hitbox != null)
		{
			UIEventTriggerUtils.AddListener(this.m_hitbox.gameObject, EventTriggerType.PointerClick, new UIEventTriggerUtils.EventDelegate(this.OnAbilityButtonClick));
			UIEventTriggerUtils.AddListener(this.m_hitbox.gameObject, EventTriggerType.PointerDown, new UIEventTriggerUtils.EventDelegate(this.OnAbilityButtonDown));
			UIEventTriggerUtils.AddListener(this.m_hitbox.gameObject, EventTriggerType.PointerUp, new UIEventTriggerUtils.EventDelegate(this.OnAbilityButtonUp));
			UIEventTriggerUtils.AddListener(this.m_hitbox.gameObject, EventTriggerType.PointerEnter, new UIEventTriggerUtils.EventDelegate(this.OnPointerEnter));
			UIEventTriggerUtils.AddListener(this.m_hitbox.gameObject, EventTriggerType.PointerExit, new UIEventTriggerUtils.EventDelegate(this.OnPointerExit));
			this.m_hitbox.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.4f;
		}
		UIManager.SetGameObjectActive(this.m_background, true, null);
		if (this.m_ultimateBorder != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(UIAbilityButton.Start()).MethodHandle;
			}
			UIManager.SetGameObjectActive(this.m_ultimateBorder, false, null);
		}
		if (this.m_persistentAuraBorder != null)
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
			UIManager.SetGameObjectActive(this.m_persistentAuraBorder, false, null);
		}
	}

	public void Setup(AbilityData.AbilityEntry abilityEntry, AbilityData.ActionType actionType, AbilityData abilityData)
	{
		bool doActive = false;
		this.m_abilityEntry = abilityEntry;
		this.m_actionType = actionType;
		this.m_abilityData = abilityData;
		ActorData actorData;
		if (this.m_abilityData != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(UIAbilityButton.Setup(AbilityData.AbilityEntry, AbilityData.ActionType, AbilityData)).MethodHandle;
			}
			actorData = this.m_abilityData.GetComponent<ActorData>();
		}
		else
		{
			actorData = null;
		}
		this.m_actorData = actorData;
		this.m_turnSM = this.m_abilityData.GetComponent<ActorTurnSM>();
		if (this.m_abilityEntry != null)
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
			if (this.m_abilityEntry.ability != null)
			{
				UIManager.SetGameObjectActive(this.m_charIcon, true, null);
				this.m_charIcon.sprite = this.m_abilityEntry.ability.sprite;
				this.m_hotkeyLabel.text = this.m_abilityEntry.hotkey;
				doActive = this.m_abilityEntry.ability.IsFreeAction();
				int num = UIBaseButton.PhaseIndexForUIPhase(UIQueueListPanel.GetUIPhaseFromAbilityPriority(this.m_abilityEntry.ability.RunPriority));
				for (int i = 0; i < this.m_phaseIndicators.Length; i++)
				{
					Component component = this.m_phaseIndicators[i];
					bool doActive2;
					if (i == num)
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
						doActive2 = (num != -1);
					}
					else
					{
						doActive2 = false;
					}
					UIManager.SetGameObjectActive(component, doActive2, null);
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
				goto IL_1B5;
			}
		}
		if (this.m_abilityEntry != null)
		{
			this.m_cooldownLabel.text = string.Empty;
			UIManager.SetGameObjectActive(this.m_charIcon, false, null);
			this.m_hitbox.enabled = false;
			this.m_hotkeyLabel.text = this.m_abilityEntry.hotkey;
		}
		else
		{
			this.m_cooldownLabel.text = string.Empty;
			UIManager.SetGameObjectActive(this.m_charIcon, false, null);
			this.m_hitbox.enabled = false;
		}
		IL_1B5:
		UIManager.SetGameObjectActive(this.m_freeactionIcon, doActive, null);
		this.m_hotkeyLabel.color = Color.white;
	}

	public void RefreshHotkey()
	{
		if (this.m_abilityEntry != null)
		{
			this.m_abilityEntry.InitHotkey();
			this.m_hotkeyLabel.text = this.m_abilityEntry.hotkey;
		}
	}

	public KeyPreference GetKeyPreference()
	{
		if (this.m_abilityEntry != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(UIAbilityButton.GetKeyPreference()).MethodHandle;
			}
			return this.m_abilityEntry.keyPreference;
		}
		return KeyPreference.NullPreference;
	}

	private bool ShowTooltip(UITooltipBase tooltip)
	{
		UIAbilityTooltip uiabilityTooltip = (UIAbilityTooltip)tooltip;
		if (this.m_abilityEntry != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(UIAbilityButton.ShowTooltip(UITooltipBase)).MethodHandle;
			}
			if (this.m_abilityEntry.ability != null)
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
				uiabilityTooltip.Setup(this.m_abilityEntry.ability, this.m_abilityEntry.ability.CurrentAbilityMod);
				return true;
			}
		}
		return false;
	}

	public void UnqueueAnimationDone()
	{
		UIManager.SetGameObjectActive(this.m_selected, false, null);
		UIManager.SetGameObjectActive(this.m_selectedGlow, false, null);
	}

	public void HoverOutAnimationDone()
	{
	}

	public void HoverInAnimationDone()
	{
	}

	public void OutlineFadeOutAnimationDone()
	{
	}

	public void OutlineFadeInAnimationDone()
	{
	}

	private void PlayAnimation(string animName)
	{
		this.m_AbilityButtonAnimator.Play(animName);
	}

	private void PlayAnimation(string animName, int layer)
	{
		this.m_AbilityButtonAnimator.Play(animName, layer);
	}

	public void OnPointerExit(BaseEventData data)
	{
		this.m_mouseOver = false;
		this.m_mouseOverForRangePreview = false;
		this.m_mouseDown = false;
	}

	public void OnPointerEnter(BaseEventData data)
	{
		if (!this.m_disabled.gameObject.activeSelf)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(UIAbilityButton.OnPointerEnter(BaseEventData)).MethodHandle;
			}
			this.m_mouseOver = true;
		}
		this.m_mouseOverForRangePreview = true;
	}

	public void Update()
	{
		if (this.m_abilityEntry != null && this.m_abilityEntry.ability != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(UIAbilityButton.Update()).MethodHandle;
			}
			ActorTurnSM turnSM = this.m_turnSM;
			ActorData actorData = this.m_actorData;
			bool flag = this.m_abilityData.HasQueuedAction(this.m_actionType);
			bool flag2;
			if (this.m_abilityEntry.ability.GetModdedCost() > actorData.TechPoints)
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
				flag2 = flag;
			}
			else
			{
				flag2 = true;
			}
			bool flag3 = flag2;
			bool flag4 = this.m_abilityEntry.ability == this.m_abilityData.GetSelectedAbility();
			bool flag5;
			if (this.m_abilityEntry.ability.GetModdedCost() > 0)
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
				if (this.m_abilityEntry.ability.GetModdedCost() <= actorData.TechPoints)
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
					flag5 = (this.m_abilityData.GetCooldownRemaining(this.m_actionType) <= 0);
					goto IL_103;
				}
			}
			flag5 = false;
			IL_103:
			bool doActive = flag5;
			bool doActive2 = this.m_abilityEntry.ability.ShouldShowPersistentAuraUI();
			if (this.m_ultimateBorder != null)
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
				UIManager.SetGameObjectActive(this.m_ultimateBorder, doActive, null);
			}
			if (this.m_persistentAuraBorder != null)
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
				UIManager.SetGameObjectActive(this.m_persistentAuraBorder, doActive2, null);
			}
			int num = UIBaseButton.PhaseIndexForUIPhase(UIQueueListPanel.GetUIPhaseFromAbilityPriority(this.m_abilityEntry.ability.RunPriority));
			for (int i = 0; i < this.m_phaseIndicators.Length; i++)
			{
				UIManager.SetGameObjectActive(this.m_phaseIndicators[i], i == num && num != -1, null);
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
			string text = string.Empty;
			if (!this.m_mouseOver)
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
				this.m_mouseDown = false;
			}
			bool flag6 = flag4 || this.m_mouseDown;
			bool flag7 = true;
			if (this.m_mouseOver)
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
				if (!flag6)
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
					if (!flag)
					{
						if (!this.m_hover.gameObject.activeSelf)
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
							this.PlayAnimation("AbilityButtonHover");
						}
						flag7 = false;
						UIManager.SetGameObjectActive(this.m_hover, true, null);
						goto IL_267;
					}
				}
			}
			UIManager.SetGameObjectActive(this.m_hover, false, null);
			IL_267:
			if (flag6 && !flag)
			{
				if (!this.m_pressed.gameObject.activeSelf)
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
					this.PlayAnimation("AbilityButtonSelected");
					if (this.m_mouseOver)
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
						this.PlayAnimation("AbilityButtonSelectedNonLoop", 1);
					}
				}
				flag7 = false;
				UIManager.SetGameObjectActive(this.m_pressedIdle, true, null);
				UIManager.SetGameObjectActive(this.m_pressedIdle2, true, null);
				UIManager.SetGameObjectActive(this.m_pressed, true, null);
			}
			else
			{
				UIManager.SetGameObjectActive(this.m_pressedIdle, false, null);
				UIManager.SetGameObjectActive(this.m_pressedIdle2, false, null);
				UIManager.SetGameObjectActive(this.m_pressed, false, null);
			}
			bool flag8 = false;
			if (flag)
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
				if (!this.m_selected.gameObject.activeSelf)
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
					this.PlayAnimation("AbilityButtonBarQueued");
				}
				flag7 = false;
				UIManager.SetGameObjectActive(this.m_selected, true, null);
				UIManager.SetGameObjectActive(this.m_selectedGlow, true, null);
			}
			else
			{
				this.PlayAnimation("AbilityButtonBarQueuedExit");
			}
			if (flag7)
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
				if (!flag8)
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
					if (!this.m_AbilityButtonAnimator.GetCurrentAnimatorStateInfo(0).IsName("AbilityButtonBarQueuedExit"))
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
					}
				}
			}
			bool flag9;
			if (this.m_abilityEntry.ability.IsSimpleAction())
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
				flag9 = turnSM.CanQueueSimpleAction();
			}
			else
			{
				flag9 = turnSM.CanSelectAbility();
			}
			bool flag10;
			if (GameFlowData.Get().gameState == GameState.BothTeams_Decision)
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
				flag10 = this.m_abilityData.ValidateActionIsRequestable(this.m_actionType);
			}
			else
			{
				flag10 = false;
			}
			bool flag11 = flag10;
			bool flag12;
			if (turnSM.CurrentState == TurnStateEnum.VALIDATING_ACTION_REQUEST)
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
				if (!flag11)
				{
					flag12 = true;
					goto IL_48E;
				}
			}
			if (GameFlowData.Get().gameState != GameState.BothTeams_Resolve)
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
				if (turnSM.CurrentState != TurnStateEnum.CONFIRMED)
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
					if (turnSM.CurrentState != TurnStateEnum.WAITING)
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
						flag12 = (turnSM.CurrentState == TurnStateEnum.RESOLVING && GameFlowData.Get().gameState == GameState.BothTeams_Decision);
						goto IL_48B;
					}
				}
			}
			flag12 = true;
			IL_48B:
			IL_48E:
			bool flag13 = flag12;
			if (flag9)
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
				bool flag14;
				if (!flag13)
				{
					if (!flag11)
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
						flag14 = !flag;
					}
					else
					{
						flag14 = false;
					}
				}
				else
				{
					flag14 = true;
				}
				flag13 = flag14;
			}
			if (flag)
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
				flag13 = false;
			}
			else
			{
				flag13 = (flag13 || !flag11);
			}
			if (flag13)
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
				this.m_charIcon.color = HUD_UIResources.Get().m_disabledAbilityButtonIconColor;
				this.m_hotkeyLabel.color = HUD_UIResources.Get().m_disabledAbilityButtonIconColor;
				UIManager.SetGameObjectActive(this.m_disabled, true, null);
				UIManager.SetGameObjectActive(this.m_default, false, null);
			}
			else
			{
				if (this.m_abilityEntry.ability.UseCustomAbilityIconColor())
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
					this.m_charIcon.color = this.m_abilityEntry.ability.GetCustomAbilityIconColor(actorData);
				}
				else
				{
					this.m_charIcon.color = Color.white;
				}
				this.m_hotkeyLabel.color = Color.white;
				UIManager.SetGameObjectActive(this.m_disabled, false, null);
				UIManager.SetGameObjectActive(this.m_default, flag7, null);
			}
			bool flag15 = true;
			if (!actorData.\u000E())
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
				flag15 = this.m_abilityEntry.ability.CustomCanCastValidation(actorData);
			}
			if (this.m_abilityEntry.ability.GetModdedMaxStocks() <= 0)
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
				UIManager.SetGameObjectActive(this.m_chargeCounterContainer, false, null);
				if (this.m_abilityEntry.GetCooldownRemaining() == 0 && flag3)
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
					if (flag15)
					{
						goto IL_6D6;
					}
				}
				if (this.m_abilityEntry.GetCooldownRemaining() > 0)
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
					if (actorData.TechPoints + actorData.ReservedTechPoints < actorData.\u0016() || !AbilityUtils.AbilityHasTag(this.m_abilityEntry.ability, AbilityTags.IgnoreCooldownIfFullEnergy))
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
						text = this.m_abilityEntry.GetCooldownRemaining().ToString();
					}
				}
				else if (this.m_abilityEntry.GetCooldownRemaining() == -1)
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
					text = "~";
				}
				else if (!flag15)
				{
					text = "X";
				}
				IL_6D6:;
			}
			else
			{
				UIManager.SetGameObjectActive(this.m_chargeCounterContainer, true, null);
				int moddedMaxStocks = this.m_abilityEntry.ability.GetModdedMaxStocks();
				for (int j = 0; j < this.chargeCounters.Length; j++)
				{
					if (j < moddedMaxStocks)
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
						UIManager.SetGameObjectActive(this.chargeCounters[j], true, null);
					}
					else
					{
						UIManager.SetGameObjectActive(this.chargeCounters[j], false, null);
					}
				}
				for (;;)
				{
					switch (7)
					{
					case 0:
						continue;
					}
					break;
				}
				AbilityData.ActionType actionTypeOfAbility = this.m_abilityData.GetActionTypeOfAbility(this.m_abilityEntry.ability);
				if (actionTypeOfAbility != AbilityData.ActionType.INVALID_ACTION)
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
					int consumedStocksCount = this.m_abilityData.GetConsumedStocksCount(actionTypeOfAbility);
					int stockRefreshCountdown = this.m_abilityData.GetStockRefreshCountdown(actionTypeOfAbility);
					int moddedStockRefreshDuration = this.m_abilityEntry.ability.GetModdedStockRefreshDuration();
					int num2 = moddedMaxStocks - consumedStocksCount;
					int k;
					for (k = 0; k < num2; k++)
					{
						this.chargeCounters[k].SetTick(3);
					}
					if (k < this.chargeCounters.Length)
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
						UIAbilityChargeCounter uiabilityChargeCounter = this.chargeCounters[k];
						int tick;
						if (moddedStockRefreshDuration >= 0)
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
							tick = 3 - stockRefreshCountdown;
						}
						else
						{
							tick = 0;
						}
						uiabilityChargeCounter.SetTick(tick);
						k++;
					}
					while (k < this.chargeCounters.Length)
					{
						this.chargeCounters[k].SetTick(0);
						k++;
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
					if (!this.m_abilityData.IsAbilityAllowedByUnlockTurns(actionTypeOfAbility))
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
						text = this.m_abilityData.GetTurnsTillUnlock(actionTypeOfAbility).ToString();
					}
					else if (stockRefreshCountdown > 0)
					{
						if (num2 != 0)
						{
							if (!this.m_abilityEntry.ability.RefillAllStockOnRefresh())
							{
								goto IL_8B8;
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
						}
						text = stockRefreshCountdown.ToString();
					}
				}
			}
			IL_8B8:
			if (this.m_cooldownLabel.text != text)
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
				this.m_cooldownLabel.text = text;
			}
			bool flag16 = this.m_abilityEntry.ability.IsFreeAction();
			if (this.m_freeactionIcon != null)
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
				if (this.m_freeactionIcon.gameObject.activeSelf != flag16)
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
					UIManager.SetGameObjectActive(this.m_freeactionIcon, flag16, null);
				}
			}
			HighlightUtils.Get().SetRangeIndicatorMouseOverFlag((int)this.m_actionType, this.m_mouseOverForRangePreview);
		}
		else if (this.m_ultimateBorder)
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
			UIManager.SetGameObjectActive(this.m_ultimateBorder, false, null);
		}
	}

	private void OnEnable()
	{
		this.Update();
	}

	public void OnAbilityButtonDown(BaseEventData data)
	{
		if (this.m_disabled.gameObject.activeSelf)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(UIAbilityButton.OnAbilityButtonDown(BaseEventData)).MethodHandle;
			}
			return;
		}
		this.m_mouseDown = true;
	}

	public void OnAbilityButtonUp(BaseEventData data)
	{
		this.m_mouseDown = false;
	}

	public void OnAbilityButtonClick(BaseEventData data)
	{
		if (!(this.m_abilityData == null))
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(UIAbilityButton.OnAbilityButtonClick(BaseEventData)).MethodHandle;
			}
			if (!(this.m_turnSM == null))
			{
				this.m_abilityData.AbilityButtonPressed(this.m_actionType, this.m_abilityEntry.ability);
				return;
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
		}
	}
}
