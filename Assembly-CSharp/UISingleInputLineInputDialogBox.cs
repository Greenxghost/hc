﻿using System;
using TMPro;
using UnityEngine.EventSystems;

public class UISingleInputLineInputDialogBox : UIDialogBox
{
	public TextMeshProUGUI m_Title;

	public TextMeshProUGUI m_Info;

	public _SelectableBtn m_firstButton;

	public _SelectableBtn m_secondButton;

	public TextMeshProUGUI[] m_firstButtonLabel;

	public TextMeshProUGUI[] m_secondButtonLabel;

	public TMP_InputField m_descriptionBoxInputField;

	private UIDialogBox.DialogButtonCallback firstButtonCallback;

	private UIDialogBox.DialogButtonCallback secondButtonCallback;

	public override void ClearCallback()
	{
		this.firstButtonCallback = null;
		this.secondButtonCallback = null;
	}

	protected override void CloseCallback()
	{
	}

	public void FirstButtonClicked(BaseEventData data)
	{
		if (this.firstButtonCallback != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(UISingleInputLineInputDialogBox.FirstButtonClicked(BaseEventData)).MethodHandle;
			}
			this.firstButtonCallback(this);
		}
		UIDialogPopupManager.Get().CloseDialog(this);
	}

	public void SecondButtonClicked(BaseEventData data)
	{
		if (this.secondButtonCallback != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(UISingleInputLineInputDialogBox.SecondButtonClicked(BaseEventData)).MethodHandle;
			}
			this.secondButtonCallback(this);
		}
		UIDialogPopupManager.Get().CloseDialog(this);
	}

	public void Start()
	{
		if (this.m_secondButton != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(UISingleInputLineInputDialogBox.Start()).MethodHandle;
			}
			this.m_secondButton.spriteController.callback = new _ButtonSwapSprite.ButtonClickCallback(this.SecondButtonClicked);
		}
		if (this.m_firstButton != null)
		{
			this.m_firstButton.spriteController.callback = new _ButtonSwapSprite.ButtonClickCallback(this.FirstButtonClicked);
		}
		this.m_descriptionBoxInputField.Select();
	}

	private void SetFirstButtonLabels(string text)
	{
		for (int i = 0; i < this.m_firstButtonLabel.Length; i++)
		{
			this.m_firstButtonLabel[i].text = text;
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
			RuntimeMethodHandle runtimeMethodHandle = methodof(UISingleInputLineInputDialogBox.SetFirstButtonLabels(string)).MethodHandle;
		}
	}

	private void SetSecondButtonLabels(string text)
	{
		for (int i = 0; i < this.m_secondButtonLabel.Length; i++)
		{
			this.m_secondButtonLabel[i].text = text;
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
			RuntimeMethodHandle runtimeMethodHandle = methodof(UISingleInputLineInputDialogBox.SetSecondButtonLabels(string)).MethodHandle;
		}
	}

	public void Setup(string Title, string Description, string LeftButtonLabel, string RightButtonLabel, UIDialogBox.DialogButtonCallback sendCallback = null, UIDialogBox.DialogButtonCallback cancelCallback = null)
	{
		this.m_Title.text = Title;
		this.m_Info.text = Description;
		this.firstButtonCallback = sendCallback;
		this.secondButtonCallback = cancelCallback;
		this.SetFirstButtonLabels(LeftButtonLabel);
		this.SetSecondButtonLabels(RightButtonLabel);
	}
}
