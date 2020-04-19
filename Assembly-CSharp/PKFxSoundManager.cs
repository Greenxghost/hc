﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

public class PKFxSoundManager : MonoBehaviour
{
	private static PKFxSoundManager.StartSoundDelegate m_onStartSoundDelegate = null;

	private static List<AudioSource> m_spawnedSound = new List<AudioSource>();

	private void OnDestroy()
	{
		PKFxSoundManager.m_spawnedSound.Clear();
	}

	private void Update()
	{
		if (PKFxSoundManager.m_onStartSoundDelegate == null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(PKFxSoundManager.Update()).MethodHandle;
			}
			if (PKFxSoundManager.m_spawnedSound != null)
			{
				for (int i = PKFxSoundManager.m_spawnedSound.Count - 1; i >= 0; i--)
				{
					AudioSource audioSource = PKFxSoundManager.m_spawnedSound[i];
					if (!audioSource.isPlaying)
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
						UnityEngine.Object.Destroy(audioSource.gameObject);
						PKFxSoundManager.m_spawnedSound.RemoveAt(i);
					}
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
			}
		}
	}

	[MonoPInvokeCallback(typeof(PKFxSoundManager.VoidFPtr))]
	public static void OnStartSound(IntPtr actionFactorySound)
	{
		PKFxManager.S_SoundDescriptor desc = (PKFxManager.S_SoundDescriptor)Marshal.PtrToStructure(actionFactorySound, typeof(PKFxManager.S_SoundDescriptor));
		PKFxManager.SoundDescriptor soundDescriptor = new PKFxManager.SoundDescriptor(desc);
		if (PKFxSoundManager.m_onStartSoundDelegate != null)
		{
			PKFxSoundManager.m_onStartSoundDelegate(soundDescriptor);
			return;
		}
		string text = "PKFxSounds/" + Path.ChangeExtension(soundDescriptor.Path, null);
		AudioClip audioClip = Resources.Load(text) as AudioClip;
		if (audioClip != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(PKFxSoundManager.OnStartSound(IntPtr)).MethodHandle;
			}
			GameObject gameObject = new GameObject("FxSound");
			if (gameObject != null)
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
				gameObject.transform.position = soundDescriptor.WorldPosition;
				AudioSource audioSource = gameObject.AddComponent<AudioSource>();
				if (audioSource != null)
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
					audioSource.clip = audioClip;
					audioSource.Play();
					audioSource.volume = soundDescriptor.Volume;
					audioSource.time = soundDescriptor.StartTimeOffsetInSeconds;
					audioSource.spatialBlend = 1f;
					if (soundDescriptor.PlayTimeInSeconds != 0f)
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
						UnityEngine.Object.Destroy(audioSource, soundDescriptor.PlayTimeInSeconds);
					}
					else
					{
						PKFxSoundManager.m_spawnedSound.Add(audioSource);
					}
				}
			}
		}
		else
		{
			Debug.LogError("[PKFX] Could not load sound layer " + text);
		}
	}

	public static void RegisterCustomHandler(PKFxSoundManager.StartSoundDelegate customDelegate)
	{
		PKFxSoundManager.m_onStartSoundDelegate = customDelegate;
	}

	private delegate void VoidFPtr(IntPtr ptr);

	public delegate void StartSoundDelegate(PKFxManager.SoundDescriptor soundDesc);
}
