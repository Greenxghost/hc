﻿using System;
using UnityEngine;

[RequireComponent(typeof(GUITexture))]
public class GUIT_Button : MonoBehaviour
{
	public Color labelColor;

	public Texture t_on;

	public Texture t_off;

	public Texture t_on_over;

	public Texture t_off_over;

	public GameObject callbackObject;

	public string callback;

	private bool over;

	public bool on;

	private void Awake()
	{
		base.GetComponentInChildren<GUIText>().material.color = this.labelColor;
		this.UpdateImage();
	}

	private void Update()
	{
		if (base.GetComponent<GUITexture>().GetScreenRect().Contains(Input.mousePosition))
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(GUIT_Button.Update()).MethodHandle;
			}
			if (!this.over)
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
				this.OnOver();
			}
			if (Input.GetMouseButtonDown(0))
			{
				this.OnClick();
			}
		}
		else if (this.over)
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
			this.OnOut();
		}
	}

	private void OnClick()
	{
		this.on = !this.on;
		this.callbackObject.SendMessage(this.callback);
		this.UpdateImage();
	}

	private void OnOver()
	{
		this.over = true;
		this.UpdateImage();
	}

	private void OnOut()
	{
		this.over = false;
		this.UpdateImage();
	}

	private void UpdateImage()
	{
		if (this.over)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(GUIT_Button.UpdateImage()).MethodHandle;
			}
			GUITexture component = base.GetComponent<GUITexture>();
			Texture texture;
			if (this.on)
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
				texture = this.t_on_over;
			}
			else
			{
				texture = this.t_off_over;
			}
			component.texture = texture;
		}
		else
		{
			GUITexture component2 = base.GetComponent<GUITexture>();
			Texture texture2;
			if (this.on)
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
				texture2 = this.t_on;
			}
			else
			{
				texture2 = this.t_off;
			}
			component2.texture = texture2;
		}
	}

	public void UpdateState(bool b)
	{
		this.on = b;
		this.UpdateImage();
	}
}
