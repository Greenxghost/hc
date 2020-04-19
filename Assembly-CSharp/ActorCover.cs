﻿using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

public class ActorCover : NetworkBehaviour
{
	private bool[] m_hasCover = new bool[4];

	private bool[] m_cachedHasCoverFromBarriers = new bool[4];

	private SyncListTempCoverInfo m_syncTempCoverProviders = new SyncListTempCoverInfo();

	private List<ActorCover.CoverDirections> m_tempCoverProviders = new List<ActorCover.CoverDirections>();

	private List<ActorCover.CoverDirections> m_tempCoverIgnoreMinDist = new List<ActorCover.CoverDirections>();

	private GameObject m_coverParent;

	private ActorData m_owner;

	private GameObject[] m_mouseOverCoverObjs = new GameObject[4];

	private GameObject[] m_actorCoverObjs = new GameObject[4];

	private List<ParticleSystemRenderer[]> m_actorCoverSymbolRenderers = new List<ParticleSystemRenderer[]>();

	private static Vector3[] m_coverDir = new Vector3[4];

	private float m_coverHeight = 2f;

	private float m_coverDirIndicatorHideTime = -1f;

	private float m_coverDirIndicatorFadeStartTime = -1f;

	private float m_coverDirIndicatorSpawnTime = -1f;

	private GameObject m_coverDirHighlight;

	private MeshRenderer[] m_coverDirIndicatorRenderers;

	private EasedFloatCubic m_coverDirIndicatorOpacity = new EasedFloatCubic(1f);

	private static int kListm_syncTempCoverProviders = 0x55B6FA50;

	static ActorCover()
	{
		NetworkBehaviour.RegisterSyncListDelegate(typeof(ActorCover), ActorCover.kListm_syncTempCoverProviders, new NetworkBehaviour.CmdDelegate(ActorCover.InvokeSyncListm_syncTempCoverProviders));
		NetworkCRC.RegisterBehaviour("ActorCover", 0);
	}

	public bool HasAnyCover(bool recalculate = false)
	{
		if (recalculate)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.HasAnyCover(bool)).MethodHandle;
			}
			this.RecalculateCover();
		}
		bool result = false;
		for (int i = 0; i < this.m_hasCover.Length; i++)
		{
			if (this.m_hasCover[i])
			{
				result = true;
			}
		}
		return result;
	}

	private void Awake()
	{
		this.m_coverParent = GameObject.Find("CoverParent");
		if (!this.m_coverParent)
		{
			this.m_coverParent = new GameObject("CoverParent");
		}
		for (int i = 0; i < 4; i++)
		{
			this.m_hasCover[i] = false;
			this.m_cachedHasCoverFromBarriers[i] = false;
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
			RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.Awake()).MethodHandle;
		}
		this.InitCoverObjs(this.m_mouseOverCoverObjs, HighlightUtils.Get().m_coverIndicatorPrefab);
		this.InitCoverObjs(this.m_actorCoverObjs, HighlightUtils.Get().m_coverShieldOnlyPrefab);
		for (int j = 0; j < this.m_actorCoverObjs.Length; j++)
		{
			ParticleSystemRenderer[] item;
			if (this.m_actorCoverObjs[j] != null)
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
				item = this.m_actorCoverObjs[j].GetComponentsInChildren<ParticleSystemRenderer>();
			}
			else
			{
				item = new ParticleSystemRenderer[0];
			}
			this.m_actorCoverSymbolRenderers.Add(item);
		}
		for (;;)
		{
			switch (1)
			{
			case 0:
				continue;
			}
			break;
		}
		ActorCover.m_coverDir[1] = Vector3.left;
		ActorCover.m_coverDir[0] = Vector3.right;
		ActorCover.m_coverDir[3] = Vector3.back;
		ActorCover.m_coverDir[2] = Vector3.forward;
		this.m_owner = base.GetComponent<ActorData>();
		this.m_syncTempCoverProviders.InitializeBehaviour(this, ActorCover.kListm_syncTempCoverProviders);
	}

	public override void OnStartClient()
	{
		this.m_syncTempCoverProviders.Callback = new SyncList<TempCoverInfo>.SyncListChanged(this.SyncListCallbackTempCoverProviders);
	}

	private void SyncListCallbackTempCoverProviders(SyncList<TempCoverInfo>.Operation op, int index)
	{
		this.ResetTempCoverListFromSyncList();
	}

	private void InitCoverObjs(GameObject[] coverObjs, GameObject coverPrefab)
	{
		coverObjs[0] = this.CreateCoverIndicatorObject(-90f, coverPrefab);
		coverObjs[1] = this.CreateCoverIndicatorObject(90f, coverPrefab);
		coverObjs[2] = this.CreateCoverIndicatorObject(180f, coverPrefab);
		coverObjs[3] = this.CreateCoverIndicatorObject(0f, coverPrefab);
		coverObjs[0].SetActive(false);
		coverObjs[1].SetActive(false);
		coverObjs[2].SetActive(false);
		coverObjs[3].SetActive(false);
		coverObjs[0].transform.parent = this.m_coverParent.transform;
		coverObjs[1].transform.parent = this.m_coverParent.transform;
		coverObjs[2].transform.parent = this.m_coverParent.transform;
		coverObjs[3].transform.parent = this.m_coverParent.transform;
	}

	public static void ResetParticleTime(GameObject particleObject)
	{
		ParticleSystem[] componentsInChildren = particleObject.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem particleSystem in componentsInChildren)
		{
			particleSystem.Clear();
			particleSystem.time = 0f;
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
			RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.ResetParticleTime(GameObject)).MethodHandle;
		}
	}

	private GameObject CreateCoverIndicatorObject(float yRotation, GameObject coverPrefab)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(coverPrefab);
		gameObject.transform.Rotate(Vector3.up, yRotation);
		return gameObject;
	}

	private void SetCoverMeshColor(GameObject particleObject, Color color)
	{
		if (particleObject != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.SetCoverMeshColor(GameObject, Color)).MethodHandle;
			}
			ParticleSystemRenderer[] componentsInChildren = particleObject.GetComponentsInChildren<ParticleSystemRenderer>();
			foreach (ParticleSystemRenderer particleSystemRenderer in componentsInChildren)
			{
				AbilityUtil_Targeter.SetMaterialColor(particleSystemRenderer.materials, color, true);
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

	public void DisableCover()
	{
		for (int i = 0; i < 4; i++)
		{
			this.m_hasCover[i] = false;
			this.m_cachedHasCoverFromBarriers[i] = false;
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
			RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.DisableCover()).MethodHandle;
		}
	}

	public Vector3 GetCoverOffset(ActorCover.CoverDirections dir)
	{
		return ActorCover.GetCoverOffsetStatic(dir);
	}

	public static Vector3 GetCoverOffsetStatic(ActorCover.CoverDirections dir)
	{
		float num = Board.\u000E().squareSize * 0.5f;
		Vector3 zero = Vector3.zero;
		if (dir == ActorCover.CoverDirections.X_POS)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.GetCoverOffsetStatic(ActorCover.CoverDirections)).MethodHandle;
			}
			zero = new Vector3(num, 0f, 0f);
		}
		else if (dir == ActorCover.CoverDirections.X_NEG)
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
			zero = new Vector3(-num, 0f, 0f);
		}
		else if (dir == ActorCover.CoverDirections.Y_POS)
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
			zero = new Vector3(0f, 0f, num);
		}
		else if (dir == ActorCover.CoverDirections.Y_NEG)
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
			zero = new Vector3(0f, 0f, -num);
		}
		return zero;
	}

	public static Quaternion GetCoverRotation(ActorCover.CoverDirections dir)
	{
		if (dir == ActorCover.CoverDirections.X_POS)
		{
			return Quaternion.LookRotation(Vector3.left);
		}
		if (dir == ActorCover.CoverDirections.X_NEG)
		{
			return Quaternion.LookRotation(Vector3.right);
		}
		if (dir == ActorCover.CoverDirections.Y_POS)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.GetCoverRotation(ActorCover.CoverDirections)).MethodHandle;
			}
			return Quaternion.LookRotation(Vector3.back);
		}
		return Quaternion.LookRotation(Vector3.forward);
	}

	public bool HasNonThinCover(BoardSquare currentSquare, int xDelta, int yDelta, bool halfHeight)
	{
		bool result = false;
		BoardSquare boardSquare = Board.\u000E().\u0016(currentSquare.x + xDelta, currentSquare.y + yDelta);
		if (boardSquare != null)
		{
			int num = boardSquare.height - currentSquare.height;
			if (halfHeight)
			{
				result = (num == 1);
			}
			else
			{
				result = (num == 2);
			}
		}
		return result;
	}

	public float CoverRating(BoardSquare square)
	{
		List<ActorData> allTeamMembers = GameFlowData.Get().GetAllTeamMembers(this.m_owner.\u0012());
		float num = 0f;
		foreach (ActorData actorData in allTeamMembers)
		{
			if (!actorData.\u000E())
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
					RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.CoverRating(BoardSquare)).MethodHandle;
				}
				if (actorData.\u0012() != null)
				{
					Vector3 vector = actorData.\u0012().transform.position - square.transform.position;
					if (vector.magnitude > Board.\u000E().squareSize * 1.5f)
					{
						if (Mathf.Abs(vector.x) > Mathf.Abs(vector.z))
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
							if (vector.x >= 0f)
							{
								goto IL_11C;
							}
							if (this.HasNonThinCover(square, -1, 0, true))
							{
								goto IL_162;
							}
							for (;;)
							{
								switch (1)
								{
								case 0:
									continue;
								}
								break;
							}
							if (square.\u001D(ActorCover.CoverDirections.X_NEG) == ThinCover.CoverType.Half)
							{
								goto IL_162;
							}
							for (;;)
							{
								switch (4)
								{
								case 0:
									continue;
								}
								goto IL_11C;
							}
							continue;
							IL_11C:
							if (vector.x > 0f)
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
								if (this.HasNonThinCover(square, 1, 0, true))
								{
									goto IL_162;
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
								if (square.\u001D(ActorCover.CoverDirections.X_POS) == ThinCover.CoverType.Half)
								{
									for (;;)
									{
										switch (1)
										{
										case 0:
											continue;
										}
										goto IL_162;
									}
								}
							}
							if (vector.x >= 0f)
							{
								goto IL_1B5;
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
							if (!this.HasNonThinCover(square, -1, 0, false))
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
								if (square.\u001D(ActorCover.CoverDirections.X_NEG) != ThinCover.CoverType.Full)
								{
									for (;;)
									{
										switch (6)
										{
										case 0:
											continue;
										}
										goto IL_1B5;
									}
								}
							}
							IL_1FB:
							num += 0.5f;
							continue;
							IL_1B5:
							if (vector.x <= 0f)
							{
								continue;
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
							if (this.HasNonThinCover(square, 1, 0, false))
							{
								goto IL_1FB;
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
							if (square.\u001D(ActorCover.CoverDirections.X_POS) != ThinCover.CoverType.Full)
							{
								continue;
							}
							for (;;)
							{
								switch (2)
								{
								case 0:
									continue;
								}
								goto IL_1FB;
							}
							IL_162:
							num += 1f;
						}
						else
						{
							if (vector.z >= 0f)
							{
								goto IL_244;
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
							if (!this.HasNonThinCover(square, 0, -1, true) && square.\u001D(ActorCover.CoverDirections.Y_NEG) != ThinCover.CoverType.Half)
							{
								for (;;)
								{
									switch (3)
									{
									case 0:
										continue;
									}
									goto IL_244;
								}
							}
							IL_280:
							num += 1f;
							continue;
							IL_244:
							if (vector.z > 0f)
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
								if (this.HasNonThinCover(square, 0, 1, true))
								{
									goto IL_280;
								}
								if (square.\u001D(ActorCover.CoverDirections.Y_POS) == ThinCover.CoverType.Half)
								{
									for (;;)
									{
										switch (5)
										{
										case 0:
											continue;
										}
										goto IL_280;
									}
								}
							}
							if (vector.z < 0f)
							{
								if (this.HasNonThinCover(square, 0, -1, false))
								{
									goto IL_303;
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
								if (square.\u001D(ActorCover.CoverDirections.Y_NEG) == ThinCover.CoverType.Full)
								{
									goto IL_303;
								}
							}
							if (vector.z <= 0f)
							{
								continue;
							}
							for (;;)
							{
								switch (1)
								{
								case 0:
									continue;
								}
								break;
							}
							if (!this.HasNonThinCover(square, 0, 1, false))
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
								if (square.\u001D(ActorCover.CoverDirections.Y_POS) != ThinCover.CoverType.Full)
								{
									continue;
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
							IL_303:
							num += 0.5f;
						}
					}
				}
			}
		}
		return num;
	}

	internal void UpdateCoverHighlights(BoardSquare currentSquare)
	{
		ActorData owner = this.m_owner;
		if (currentSquare != null && currentSquare.\u0016())
		{
			ActorTurnSM actorTurnSM = owner.\u000E();
			if (actorTurnSM != null)
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
					RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.UpdateCoverHighlights(BoardSquare)).MethodHandle;
				}
				bool flag = actorTurnSM.AmTargetingAction();
				List<BoardSquare> list = null;
				Board.\u000E().\u000E(currentSquare.x, currentSquare.y, ref list);
				if (list != null)
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
					for (int i = 0; i < list.Count; i++)
					{
						BoardSquare boardSquare = list[i];
						if (boardSquare == null)
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
						}
						else
						{
							ActorCover.CoverDirections coverDirection = ActorCover.GetCoverDirection(currentSquare, boardSquare);
							int num = boardSquare.height - currentSquare.height;
							GameObject gameObject;
							if (coverDirection < (ActorCover.CoverDirections)this.m_mouseOverCoverObjs.Length)
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
								gameObject = this.m_mouseOverCoverObjs[(int)coverDirection];
							}
							else
							{
								gameObject = null;
							}
							GameObject gameObject2 = gameObject;
							if (gameObject2 != null)
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
								if (num < 1)
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
									if (currentSquare.\u001D(coverDirection) == ThinCover.CoverType.None)
									{
										goto IL_1E5;
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
								if (!flag)
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
									if (actorTurnSM.CurrentState != TurnStateEnum.PICKING_RESPAWN)
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
										Vector3 vector = new Vector3(currentSquare.worldX, (float)currentSquare.height + this.m_coverHeight, currentSquare.worldY);
										vector += this.GetCoverOffset(coverDirection);
										if (gameObject2.transform.position != vector)
										{
											goto IL_1C6;
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
										if (!gameObject2.activeSelf)
										{
											for (;;)
											{
												switch (1)
												{
												case 0:
													continue;
												}
												goto IL_1C6;
											}
										}
										IL_1E3:
										goto IL_1ED;
										IL_1C6:
										gameObject2.transform.position = vector;
										gameObject2.SetActive(true);
										ActorCover.ResetParticleTime(gameObject2);
										goto IL_1E3;
									}
								}
								IL_1E5:
								gameObject2.SetActive(false);
							}
						}
						IL_1ED:;
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
		}
		else
		{
			for (int j = 0; j < this.m_mouseOverCoverObjs.Length; j++)
			{
				GameObject gameObject3 = this.m_mouseOverCoverObjs[j];
				if (gameObject3)
				{
					gameObject3.SetActive(false);
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
	}

	private void Update()
	{
		if (this.m_coverDirHighlight != null && this.m_coverDirIndicatorRenderers != null)
		{
			float opacity = this.m_coverDirIndicatorOpacity * ActorCover.GetCoverDirInitialOpacity();
			for (int i = 0; i < this.m_coverDirIndicatorRenderers.Length; i++)
			{
				MeshRenderer meshRenderer = this.m_coverDirIndicatorRenderers[i];
				if (meshRenderer != null)
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
						RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.Update()).MethodHandle;
					}
					AbilityUtil_Targeter.SetMaterialOpacity(meshRenderer.materials, opacity);
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
			float opacity2 = this.m_coverDirIndicatorOpacity * ActorCover.GetCoverDirParticleInitialOpacity();
			for (int j = 0; j < this.m_hasCover.Length; j++)
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
				if (j >= this.m_actorCoverSymbolRenderers.Count)
				{
					break;
				}
				if (this.m_hasCover[j])
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
					foreach (ParticleSystemRenderer particleSystemRenderer in this.m_actorCoverSymbolRenderers[j])
					{
						AbilityUtil_Targeter.SetMaterialOpacity(particleSystemRenderer.materials, opacity2);
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
		if (this.m_coverDirIndicatorSpawnTime > 0f && Time.time > this.m_coverDirIndicatorSpawnTime)
		{
			this.ShowAllRelevantCoverIndicator();
			this.m_coverDirIndicatorSpawnTime = -1f;
		}
		if (this.m_coverDirIndicatorFadeStartTime > 0f)
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
			if (Time.time > this.m_coverDirIndicatorFadeStartTime)
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
				this.m_coverDirIndicatorOpacity.EaseTo(0f, ActorCover.GetCoverDirIndicatorDuration() - ActorCover.GetCoverDirFadeoutStartDelay());
				this.m_coverDirIndicatorFadeStartTime = -1f;
			}
		}
		if (this.m_coverDirIndicatorHideTime > 0f)
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
			if (Time.time > this.m_coverDirIndicatorHideTime)
			{
				this.HideRelevantCover();
				this.DestroyCoverDirHighlight();
				this.m_coverDirIndicatorHideTime = -1f;
			}
		}
	}

	public void ShowRelevantCover(Vector3 damageOrigin)
	{
		List<ActorCover.CoverDirections> list = new List<ActorCover.CoverDirections>();
		if (this.IsInCoverWrt(damageOrigin, ref list))
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.ShowRelevantCover(Vector3)).MethodHandle;
			}
			BoardSquare boardSquare = this.m_owner.\u0012();
			for (int i = 0; i < 4; i++)
			{
				ActorCover.CoverDirections coverDirections = (ActorCover.CoverDirections)i;
				if (list.Contains(coverDirections))
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
					Vector3 a = new Vector3(boardSquare.worldX, (float)boardSquare.height + this.m_coverHeight, boardSquare.worldY);
					this.m_actorCoverObjs[i].transform.position = a + this.GetCoverOffset(coverDirections);
					this.m_actorCoverObjs[i].SetActive(true);
				}
				else
				{
					this.m_actorCoverObjs[i].SetActive(false);
				}
			}
		}
		else
		{
			this.HideRelevantCover();
		}
	}

	public void StartShowMoveIntoCoverIndicator()
	{
		if (this.HasAnyCover(false))
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.StartShowMoveIntoCoverIndicator()).MethodHandle;
			}
			this.m_coverDirIndicatorSpawnTime = Time.time + ActorCover.GetCoverDirIndicatorSpawnDelay();
		}
		else
		{
			this.m_coverDirIndicatorSpawnTime = -1f;
		}
	}

	public static GameObject CreateCoverDirIndicator(bool[] hasCoverFlags, Color color, float radiusInSquares)
	{
		float num;
		if (GameplayData.Get())
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.CreateCoverDirIndicator(bool[], Color, float)).MethodHandle;
			}
			num = GameplayData.Get().m_coverProtectionAngle;
		}
		else
		{
			num = 110f;
		}
		float num2 = num;
		float num3 = num2 - 90f;
		GameObject gameObject = new GameObject("CoverDirHighlightParent");
		int num4 = 0;
		bool flag = false;
		for (int i = 0; i < hasCoverFlags.Length; i++)
		{
			if (hasCoverFlags[i])
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
				num4++;
			}
		}
		if (num4 == 2)
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
			flag = (hasCoverFlags[1] == hasCoverFlags[0]);
		}
		float borderStartOffset = 0.7f;
		GameObject gameObject2 = HighlightUtils.Get().CreateDynamicConeMesh(radiusInSquares, num2, false, null);
		HighlightUtils.Get().SetDynamicConeMeshBorderActive(gameObject2, false);
		UIDynamicCone component = gameObject2.GetComponent<UIDynamicCone>();
		if (component != null)
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
			component.SetBorderStartOffset(borderStartOffset);
		}
		Vector3 forward = Vector3.forward;
		if (num4 <= 3)
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
			if (!flag)
			{
				Vector3 a = Vector3.zero;
				for (int j = 0; j < ActorCover.m_coverDir.Length; j++)
				{
					if (hasCoverFlags[j])
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
						a += ActorCover.m_coverDir[j];
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
				forward = (a / (float)num4).normalized;
			}
		}
		if (num4 == 2)
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
			if (flag)
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
				GameObject gameObject3 = HighlightUtils.Get().CreateDynamicConeMesh(radiusInSquares, num2, false, null);
				HighlightUtils.Get().SetDynamicConeMeshBorderActive(gameObject3, false);
				UIDynamicCone component2 = gameObject3.GetComponent<UIDynamicCone>();
				if (component2 != null)
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
					component2.SetBorderStartOffset(borderStartOffset);
				}
				MeshRenderer[] componentsInChildren = gameObject3.GetComponentsInChildren<MeshRenderer>();
				foreach (MeshRenderer meshRenderer in componentsInChildren)
				{
					if (HighlightUtils.Get() != null)
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
						AbilityUtil_Targeter.SetMaterialColor(meshRenderer.materials, color, true);
					}
					AbilityUtil_Targeter.SetMaterialOpacity(meshRenderer.materials, ActorCover.GetCoverDirInitialOpacity());
				}
				if (hasCoverFlags[1])
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
					forward = Vector3.right;
					gameObject3.transform.localRotation = Quaternion.LookRotation(Vector3.left);
				}
				else
				{
					forward = Vector3.forward;
					gameObject3.transform.localRotation = Quaternion.LookRotation(Vector3.back);
				}
				gameObject3.transform.parent = gameObject.transform;
				gameObject3.transform.localPosition = Vector3.zero;
				goto IL_320;
			}
		}
		if (num4 == 2)
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
			HighlightUtils.Get().AdjustDynamicConeMesh(gameObject2, radiusInSquares, 180f + num3);
		}
		else if (num4 == 3)
		{
			HighlightUtils.Get().AdjustDynamicConeMesh(gameObject2, radiusInSquares, 270f + num3);
		}
		else if (num4 == 4)
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
			HighlightUtils.Get().AdjustDynamicConeMesh(gameObject2, radiusInSquares, 360f);
		}
		IL_320:
		gameObject2.transform.parent = gameObject.transform;
		gameObject2.transform.localRotation = Quaternion.LookRotation(forward);
		gameObject2.transform.localPosition = Vector3.zero;
		MeshRenderer[] componentsInChildren2 = gameObject2.GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer meshRenderer2 in componentsInChildren2)
		{
			if (HighlightUtils.Get() != null)
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
				AbilityUtil_Targeter.SetMaterialColor(meshRenderer2.materials, color, true);
			}
			AbilityUtil_Targeter.SetMaterialOpacity(meshRenderer2.materials, ActorCover.GetCoverDirInitialOpacity());
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
		return gameObject;
	}

	private void ShowCoverIndicatorForDirection(ActorCover.CoverDirections dir)
	{
		BoardSquare boardSquare = (!(this.m_owner != null)) ? null : this.m_owner.\u0012();
		if (boardSquare != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.ShowCoverIndicatorForDirection(ActorCover.CoverDirections)).MethodHandle;
			}
			if (dir < (ActorCover.CoverDirections)this.m_hasCover.Length)
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
				if (dir < (ActorCover.CoverDirections)this.m_actorCoverObjs.Length)
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
					if (this.m_actorCoverObjs[(int)dir] != null)
					{
						if (this.m_hasCover[(int)dir])
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
							Vector3 a = new Vector3(boardSquare.worldX, (float)boardSquare.height + this.m_coverHeight, boardSquare.worldY);
							this.m_actorCoverObjs[(int)dir].transform.position = a + this.GetCoverOffset(dir);
							this.m_actorCoverObjs[(int)dir].SetActive(true);
							ActorCover.ResetParticleTime(this.m_actorCoverObjs[(int)dir]);
						}
						else
						{
							this.m_actorCoverObjs[(int)dir].SetActive(false);
						}
					}
				}
			}
		}
	}

	private void ShowAllRelevantCoverIndicator()
	{
		if (!this.HasAnyCover(false))
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.ShowAllRelevantCoverIndicator()).MethodHandle;
			}
			return;
		}
		this.ShowCoverIndicatorForDirection(ActorCover.CoverDirections.X_NEG);
		this.ShowCoverIndicatorForDirection(ActorCover.CoverDirections.X_POS);
		this.ShowCoverIndicatorForDirection(ActorCover.CoverDirections.Y_NEG);
		this.ShowCoverIndicatorForDirection(ActorCover.CoverDirections.Y_POS);
		this.DestroyCoverDirHighlight();
		BoardSquare boardSquare;
		if (this.m_owner != null)
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
			boardSquare = this.m_owner.\u0012();
		}
		else
		{
			boardSquare = null;
		}
		BoardSquare boardSquare2 = boardSquare;
		if (boardSquare2 != null)
		{
			Vector3 position = boardSquare2.ToVector3();
			position.y = HighlightUtils.GetHighlightHeight();
			this.m_coverDirHighlight = ActorCover.CreateCoverDirIndicator(this.m_hasCover, HighlightUtils.Get().m_coverDirIndicatorColor, ActorCover.GetCoverDirIndicatorRadius());
			this.m_coverDirHighlight.transform.position = position;
			this.m_coverDirIndicatorRenderers = this.m_coverDirHighlight.GetComponentsInChildren<MeshRenderer>();
		}
		this.m_coverDirIndicatorOpacity = new EasedFloatCubic(1f);
		this.m_coverDirIndicatorOpacity.EaseTo(1f, 0.1f);
		this.m_coverDirIndicatorHideTime = Time.time + ActorCover.GetCoverDirIndicatorDuration();
		this.m_coverDirIndicatorFadeStartTime = Time.time + ActorCover.GetCoverDirFadeoutStartDelay();
	}

	public void HideRelevantCover()
	{
		for (int i = 0; i < 4; i++)
		{
			this.m_actorCoverObjs[i].SetActive(false);
		}
	}

	private void DestroyCoverDirHighlight()
	{
		this.m_coverDirIndicatorRenderers = null;
		if (this.m_coverDirHighlight != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.DestroyCoverDirHighlight()).MethodHandle;
			}
			HighlightUtils.DestroyObjectAndMaterials(this.m_coverDirHighlight);
		}
	}

	private static float GetCoverDirInitialOpacity()
	{
		if (HighlightUtils.Get() != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.GetCoverDirInitialOpacity()).MethodHandle;
			}
			return HighlightUtils.Get().m_coverDirIndicatorInitialOpacity;
		}
		return 0.08f;
	}

	private static float GetCoverDirParticleInitialOpacity()
	{
		if (HighlightUtils.Get() != null)
		{
			return HighlightUtils.Get().m_coverDirParticleInitialOpacity;
		}
		return 0.5f;
	}

	private static float GetCoverDirIndicatorDuration()
	{
		if (HighlightUtils.Get() != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.GetCoverDirIndicatorDuration()).MethodHandle;
			}
			return Mathf.Max(0.1f, HighlightUtils.Get().m_coverDirIndicatorDuration);
		}
		return 3f;
	}

	private static float GetCoverDirIndicatorSpawnDelay()
	{
		if (HighlightUtils.Get() != null)
		{
			return Mathf.Max(0f, HighlightUtils.Get().m_coverDirIndicatorStartDelay);
		}
		return 0f;
	}

	private static float GetCoverDirFadeoutStartDelay()
	{
		float b = 1f;
		if (HighlightUtils.Get() != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.GetCoverDirFadeoutStartDelay()).MethodHandle;
			}
			b = HighlightUtils.Get().m_coverDirFadeoutStartDelay;
		}
		return Mathf.Min(ActorCover.GetCoverDirIndicatorDuration(), b);
	}

	private static float GetCoverDirIndicatorRadius()
	{
		if (HighlightUtils.Get() != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.GetCoverDirIndicatorRadius()).MethodHandle;
			}
			return HighlightUtils.Get().m_coverDirIndicatorRadiusInSquares;
		}
		return 3f;
	}

	public void AddTempCoverProvider(ActorCover.CoverDirections direction, bool ignoreMinDist)
	{
		if (NetworkServer.active)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.AddTempCoverProvider(ActorCover.CoverDirections, bool)).MethodHandle;
			}
			TempCoverInfo item = new TempCoverInfo(direction, ignoreMinDist);
			this.m_syncTempCoverProviders.Add(item);
			this.ResetTempCoverListFromSyncList();
		}
		this.RecalculateCover();
	}

	public void RemoveTempCoverProvider(ActorCover.CoverDirections direction, bool ignoreMinDist)
	{
		if (NetworkServer.active)
		{
			bool flag = false;
			for (int i = (int)(this.m_syncTempCoverProviders.Count - 1); i >= 0; i--)
			{
				if (this.m_syncTempCoverProviders[i].m_coverDir == direction)
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
						RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.RemoveTempCoverProvider(ActorCover.CoverDirections, bool)).MethodHandle;
					}
					if (this.m_syncTempCoverProviders[i].m_ignoreMinDist == ignoreMinDist)
					{
						this.m_syncTempCoverProviders.RemoveAt(i);
						flag = true;
						break;
					}
				}
			}
			if (flag)
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
				this.ResetTempCoverListFromSyncList();
				this.RecalculateCover();
			}
			else
			{
				Log.Warning("RemoveTempCoverProvider did not find matching entry to remove", new object[0]);
			}
		}
	}

	public void ClearTempCoverProviders()
	{
		if (NetworkServer.active)
		{
			this.m_syncTempCoverProviders.Clear();
			this.ResetTempCoverListFromSyncList();
		}
		this.RecalculateCover();
	}

	public void RecalculateCover()
	{
		ActorData owner = this.m_owner;
		this.UpdateCoverFromBarriers();
		BoardSquare square = owner.\u0012();
		ActorCover.CalcCover(out this.m_hasCover, square, this.m_tempCoverProviders, this.m_tempCoverIgnoreMinDist, this.m_cachedHasCoverFromBarriers, true);
	}

	private void ResetTempCoverListFromSyncList()
	{
		this.m_tempCoverProviders.Clear();
		this.m_tempCoverIgnoreMinDist.Clear();
		for (int i = 0; i < (int)this.m_syncTempCoverProviders.Count; i++)
		{
			if (this.m_syncTempCoverProviders[i].m_ignoreMinDist)
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
					RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.ResetTempCoverListFromSyncList()).MethodHandle;
				}
				this.m_tempCoverIgnoreMinDist.Add(this.m_syncTempCoverProviders[i].m_coverDir);
			}
			else
			{
				this.m_tempCoverProviders.Add(this.m_syncTempCoverProviders[i].m_coverDir);
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
	}

	public void UpdateCoverFromBarriers()
	{
		for (int i = 0; i < this.m_cachedHasCoverFromBarriers.Length; i++)
		{
			this.m_cachedHasCoverFromBarriers[i] = false;
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
		if (!true)
		{
			RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.UpdateCoverFromBarriers()).MethodHandle;
		}
		BoardSquare boardSquare = this.m_owner.\u0012();
		if (BarrierManager.Get() != null)
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
			if (boardSquare != null)
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
				BarrierManager.Get().UpdateCachedCoverDirections(this.m_owner, boardSquare, ref this.m_cachedHasCoverFromBarriers);
			}
		}
	}

	internal static bool CalcCoverLevelGeoOnly(out bool[] hasCover, BoardSquare square)
	{
		return ActorCover.CalcCover(out hasCover, square, null, null, null, true);
	}

	internal unsafe static bool CalcCover(out bool[] hasCover, BoardSquare square, List<ActorCover.CoverDirections> tempCoversNormal, List<ActorCover.CoverDirections> tempCoversIgnoreMinDist, bool[] coverDirFromBarriers, bool minDistOk)
	{
		hasCover = new bool[4];
		bool flag = false;
		if (square != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.CalcCover(bool[]*, BoardSquare, List<ActorCover.CoverDirections>, List<ActorCover.CoverDirections>, bool[], bool)).MethodHandle;
			}
			List<BoardSquare> list = null;
			Board.\u000E().\u000E(square.x, square.y, ref list);
			foreach (BoardSquare boardSquare in list)
			{
				ActorCover.CoverDirections coverDirection = ActorCover.GetCoverDirection(square, boardSquare);
				int num = boardSquare.height - square.height;
				if (!minDistOk)
				{
					bool flag2;
					if (tempCoversIgnoreMinDist != null)
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
						flag2 = tempCoversIgnoreMinDist.Contains(coverDirection);
					}
					else
					{
						flag2 = false;
					}
					bool flag3 = flag2;
					hasCover[(int)coverDirection] = flag3;
					flag = (flag || flag3);
				}
				else
				{
					if (num < 1)
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
						if (square.\u001D(coverDirection) == ThinCover.CoverType.None)
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
							if (tempCoversNormal != null)
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
								if (tempCoversNormal.Contains(coverDirection))
								{
									goto IL_129;
								}
								for (;;)
								{
									switch (1)
									{
									case 0:
										continue;
									}
									break;
								}
							}
							if (tempCoversIgnoreMinDist != null)
							{
								if (tempCoversIgnoreMinDist.Contains(coverDirection))
								{
									goto IL_129;
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
							}
							if (coverDirFromBarriers != null)
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
								if (coverDirFromBarriers[(int)coverDirection])
								{
									for (;;)
									{
										switch (2)
										{
										case 0:
											continue;
										}
										goto IL_129;
									}
								}
							}
							hasCover[(int)coverDirection] = false;
							continue;
						}
					}
					IL_129:
					hasCover[(int)coverDirection] = true;
					flag = true;
				}
			}
		}
		return flag;
	}

	public static ActorCover.CoverDirections GetCoverDirection(BoardSquare srcSquare, BoardSquare destSquare)
	{
		ActorCover.CoverDirections result;
		if (srcSquare.x > destSquare.x)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.GetCoverDirection(BoardSquare, BoardSquare)).MethodHandle;
			}
			result = ActorCover.CoverDirections.X_NEG;
		}
		else if (srcSquare.x < destSquare.x)
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
			result = ActorCover.CoverDirections.X_POS;
		}
		else if (srcSquare.y > destSquare.y)
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
			result = ActorCover.CoverDirections.Y_NEG;
		}
		else if (srcSquare.y < destSquare.y)
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
			result = ActorCover.CoverDirections.Y_POS;
		}
		else
		{
			result = ActorCover.CoverDirections.INVALID;
		}
		return result;
	}

	public bool IsInCoverWrt(Vector3 damageOrigin)
	{
		List<ActorCover.CoverDirections> list = null;
		return this.IsInCoverWrt(damageOrigin, ref list);
	}

	public static bool IsInCoverWrt(Vector3 damageOrigin, BoardSquare targetSquare, List<ActorCover.CoverDirections> tempCoverProviders, List<ActorCover.CoverDirections> tempCoversIgnoreMinDist, bool[] coverDirFromBarriers)
	{
		List<ActorCover.CoverDirections> list = null;
		return ActorCover.IsInCoverWrt(damageOrigin, targetSquare, ref list, tempCoverProviders, tempCoversIgnoreMinDist, coverDirFromBarriers);
	}

	public bool IsInCoverWrt(Vector3 damageOrigin, ref List<ActorCover.CoverDirections> coverDirections)
	{
		ActorData component = base.GetComponent<ActorData>();
		BoardSquare targetSquare = component.\u0012();
		return ActorCover.IsInCoverWrt(damageOrigin, targetSquare, ref coverDirections, this.m_tempCoverProviders, this.m_tempCoverIgnoreMinDist, this.m_cachedHasCoverFromBarriers);
	}

	public bool IsInCoverForDirection(ActorCover.CoverDirections dir)
	{
		return this.m_hasCover[(int)dir];
	}

	public unsafe static bool IsInCoverWrt(Vector3 damageOrigin, BoardSquare targetSquare, ref List<ActorCover.CoverDirections> coverDirections, List<ActorCover.CoverDirections> tempCoverProviders, List<ActorCover.CoverDirections> tempCoversIgnoreMinDist, bool[] coverDirFromBarriers)
	{
		if (targetSquare == null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.IsInCoverWrt(Vector3, BoardSquare, List<ActorCover.CoverDirections>*, List<ActorCover.CoverDirections>, List<ActorCover.CoverDirections>, bool[])).MethodHandle;
			}
			return false;
		}
		Vector3 b = targetSquare.ToVector3();
		Vector3 vector = damageOrigin - b;
		vector.y = 0f;
		float sqrMagnitude = vector.sqrMagnitude;
		float num = GameplayData.Get().m_coverMinDistance * Board.\u000E().squareSize;
		float num2 = num * num;
		bool flag = sqrMagnitude >= num2;
		if (!flag)
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
			if (tempCoversIgnoreMinDist != null)
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
				if (tempCoversIgnoreMinDist.Count != 0)
				{
					goto IL_A1;
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
			return false;
		}
		IL_A1:
		int numCoverSourcesByDirectionOnly = ActorCover.GetNumCoverSourcesByDirectionOnly(damageOrigin, targetSquare, ref coverDirections, tempCoverProviders, tempCoversIgnoreMinDist, coverDirFromBarriers, flag);
		return numCoverSourcesByDirectionOnly > 0;
	}

	public bool IsInCoverWrtDirectionOnly(Vector3 damageOrigin, BoardSquare targetSquare)
	{
		List<ActorCover.CoverDirections> list = null;
		return ActorCover.GetNumCoverSourcesByDirectionOnly(damageOrigin, targetSquare, ref list, this.m_tempCoverProviders, this.m_tempCoverIgnoreMinDist, this.m_cachedHasCoverFromBarriers, true) > 0;
	}

	private unsafe static int GetNumCoverSourcesByDirectionOnly(Vector3 damageOrigin, BoardSquare targetSquare, ref List<ActorCover.CoverDirections> coverDirections, List<ActorCover.CoverDirections> tempCoverProviders, List<ActorCover.CoverDirections> tempCoverIgnoreMinDist, bool[] coverDirFromBarriers, bool minDistOk)
	{
		int num = 0;
		Vector3 vector = targetSquare.ToVector3();
		bool flag = damageOrigin.x < vector.x;
		bool flag2 = damageOrigin.x > vector.x;
		bool flag3 = damageOrigin.z < vector.z;
		bool flag4 = damageOrigin.z > vector.z;
		Vector2 vector2 = new Vector2(damageOrigin.x - vector.x, damageOrigin.z - vector.z);
		Vector2 normalized = vector2.normalized;
		float num2 = 0.5f * Board.\u000E().squareSize;
		float num3 = GameplayData.Get().m_coverProtectionAngle / 2f;
		bool[] array;
		ActorCover.CalcCover(out array, targetSquare, tempCoverProviders, tempCoverIgnoreMinDist, coverDirFromBarriers, minDistOk);
		if (array[1])
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.GetNumCoverSourcesByDirectionOnly(Vector3, BoardSquare, List<ActorCover.CoverDirections>*, List<ActorCover.CoverDirections>, List<ActorCover.CoverDirections>, bool[], bool)).MethodHandle;
			}
			if (flag)
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
				if (vector2.x < -num2)
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
					Vector2 lhs = new Vector2(-1f, 0f);
					float num4 = Mathf.Acos(Vector2.Dot(lhs, normalized));
					float num5 = num4 * 57.29578f;
					if (num5 <= num3)
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
						num++;
						if (coverDirections != null)
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
							coverDirections.Add(ActorCover.CoverDirections.X_NEG);
						}
					}
				}
			}
		}
		if (array[0] && flag2)
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
			if (vector2.x > num2)
			{
				Vector2 lhs2 = new Vector2(1f, 0f);
				float num6 = Mathf.Acos(Vector2.Dot(lhs2, normalized));
				float num7 = num6 * 57.29578f;
				if (num7 <= num3)
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
					num++;
					if (coverDirections != null)
					{
						coverDirections.Add(ActorCover.CoverDirections.X_POS);
					}
				}
			}
		}
		if (array[3] && flag3)
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
			if (vector2.y < -num2)
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
				Vector2 lhs3 = new Vector2(0f, -1f);
				float num8 = Mathf.Acos(Vector2.Dot(lhs3, normalized));
				float num9 = num8 * 57.29578f;
				if (num9 <= num3)
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
					num++;
					if (coverDirections != null)
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
						coverDirections.Add(ActorCover.CoverDirections.Y_NEG);
					}
				}
			}
		}
		if (array[2])
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
			if (flag4 && vector2.y > num2)
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
				Vector2 lhs4 = new Vector2(0f, 1f);
				float num10 = Mathf.Acos(Vector2.Dot(lhs4, normalized));
				float num11 = num10 * 57.29578f;
				if (num11 <= num3)
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
					num++;
					if (coverDirections != null)
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
						coverDirections.Add(ActorCover.CoverDirections.Y_POS);
					}
				}
			}
		}
		return num;
	}

	public bool IsDirInCover(Vector3 dir)
	{
		float angle_deg = VectorUtils.HorizontalAngle_Deg(dir);
		List<CoverRegion> coveredRegions = this.GetCoveredRegions();
		bool result = false;
		using (List<CoverRegion>.Enumerator enumerator = coveredRegions.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				CoverRegion coverRegion = enumerator.Current;
				if (coverRegion.IsDirInCover(angle_deg))
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
						RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.IsDirInCover(Vector3)).MethodHandle;
					}
					return true;
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
		return result;
	}

	public List<CoverRegion> GetCoveredRegions()
	{
		List<CoverRegion> list = new List<CoverRegion>();
		ActorData owner = this.m_owner;
		if (owner == null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.GetCoveredRegions()).MethodHandle;
			}
			Debug.LogError("Trying to get the covered regions for a null actor.");
			return list;
		}
		if (owner.\u000E())
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
			Debug.LogError(string.Concat(new string[]
			{
				"Trying to get the covered regions for the dead actor ",
				owner.DisplayName,
				", a ",
				owner.name,
				"."
			}));
			return list;
		}
		if (owner.\u0012() == null)
		{
			Debug.LogError(string.Concat(new string[]
			{
				"Trying to get the covered regions for the (alive) actor ",
				owner.DisplayName,
				", a ",
				owner.name,
				", but the square is null."
			}));
			return list;
		}
		BoardSquare boardSquare = owner.\u0012();
		Vector3 center = boardSquare.ToVector3();
		float num = GameplayData.Get().m_coverProtectionAngle / 2f;
		for (int i = 0; i < 4; i++)
		{
			if (this.m_hasCover[i])
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
				ActorCover.CoverDirections dir = (ActorCover.CoverDirections)i;
				float centerAngleOfDirection = ActorCover.GetCenterAngleOfDirection(dir);
				CoverRegion item = new CoverRegion(center, centerAngleOfDirection - num, centerAngleOfDirection + num);
				list.Add(item);
			}
		}
		if (list.Count != 0)
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
			if (list.Count != 1)
			{
				if (list.Count == 4)
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
					return new List<CoverRegion>
					{
						new CoverRegion(center, -720f, 720f)
					};
				}
				if (list.Count == 3)
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
					float num2 = list[0].m_startAngle;
					float num3 = list[0].m_endAngle;
					foreach (CoverRegion coverRegion in list)
					{
						num2 = Mathf.Min(num2, coverRegion.m_startAngle);
						num3 = Mathf.Max(num3, coverRegion.m_endAngle);
					}
					return new List<CoverRegion>
					{
						new CoverRegion(center, num2, num3)
					};
				}
				if (list.Count == 2)
				{
					CoverRegion coverRegion2 = list[0];
					CoverRegion coverRegion3 = list[1];
					bool flag;
					if (coverRegion2.m_startAngle <= coverRegion3.m_startAngle)
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
						flag = (coverRegion3.m_startAngle <= coverRegion2.m_endAngle);
					}
					else
					{
						flag = false;
					}
					bool flag2 = flag;
					bool flag3 = coverRegion2.m_startAngle <= coverRegion3.m_endAngle && coverRegion3.m_endAngle <= coverRegion2.m_endAngle;
					if (!flag2)
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
						if (!flag3)
						{
							return list;
						}
					}
					float startAngle = Mathf.Min(coverRegion2.m_startAngle, coverRegion3.m_startAngle);
					float endAngle = Mathf.Max(coverRegion2.m_endAngle, coverRegion3.m_endAngle);
					return new List<CoverRegion>
					{
						new CoverRegion(center, startAngle, endAngle)
					};
				}
				Log.Error(string.Concat(new object[]
				{
					"Actor ",
					owner.DisplayName,
					" in cover in ",
					list.Count,
					" directions."
				}), new object[0]);
				return list;
			}
		}
		return list;
	}

	public unsafe void ClampConeToValidCover(float coneDirAngleDegrees, float coneWidthDegrees, out float newDirAngleDegrees, out Vector3 newConeDir)
	{
		float num = coneDirAngleDegrees;
		List<CoverRegion> coveredRegions = this.GetCoveredRegions();
		bool flag = false;
		float angle_deg = coneDirAngleDegrees - coneWidthDegrees / 2f;
		float angle_deg2 = coneDirAngleDegrees + coneWidthDegrees / 2f;
		foreach (CoverRegion coverRegion in coveredRegions)
		{
			bool flag2 = coverRegion.IsDirInCover(angle_deg);
			bool flag3 = coverRegion.IsDirInCover(angle_deg2);
			if (flag2)
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
					RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.ClampConeToValidCover(float, float, float*, Vector3*)).MethodHandle;
				}
				if (flag3)
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
					flag = true;
					break;
				}
			}
		}
		if (!flag)
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
			using (List<CoverRegion>.Enumerator enumerator2 = coveredRegions.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					CoverRegion coverRegion2 = enumerator2.Current;
					bool flag4 = coverRegion2.IsDirInCover(coneDirAngleDegrees);
					if (flag4)
					{
						bool flag5 = coverRegion2.IsDirInCover(angle_deg) && !coverRegion2.IsDirInCover(angle_deg2);
						bool flag6;
						if (!coverRegion2.IsDirInCover(angle_deg))
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
							flag6 = coverRegion2.IsDirInCover(angle_deg2);
						}
						else
						{
							flag6 = false;
						}
						bool flag7 = flag6;
						if (flag5)
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
							num = coverRegion2.m_endAngle - coneWidthDegrees / 2f;
						}
						else if (flag7)
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
							num = coverRegion2.m_startAngle + coneWidthDegrees / 2f;
						}
					}
				}
				for (;;)
				{
					switch (1)
					{
					case 0:
						continue;
					}
					break;
				}
			}
		}
		newDirAngleDegrees = num;
		newConeDir = VectorUtils.AngleDegreesToVector(num);
	}

	private static float GetCenterAngleOfDirection(ActorCover.CoverDirections dir)
	{
		float result;
		switch (dir)
		{
		case ActorCover.CoverDirections.X_POS:
			result = 0f;
			break;
		case ActorCover.CoverDirections.X_NEG:
			result = 180f;
			break;
		case ActorCover.CoverDirections.Y_POS:
			result = 90f;
			break;
		case ActorCover.CoverDirections.Y_NEG:
			result = 270f;
			break;
		default:
			result = 0f;
			break;
		}
		return result;
	}

	public static bool DoesCoverDirectionProvideCoverFromPos(ActorCover.CoverDirections dir, Vector3 coveredPos, Vector3 attackOriginPos)
	{
		float num = GameplayData.Get().m_coverProtectionAngle / 2f;
		float centerAngleOfDirection = ActorCover.GetCenterAngleOfDirection(dir);
		CoverRegion coverRegion = new CoverRegion(coveredPos, centerAngleOfDirection - num, centerAngleOfDirection + num);
		return coverRegion.IsInCoverFromPos(attackOriginPos);
	}

	private void UNetVersion()
	{
	}

	protected static void InvokeSyncListm_syncTempCoverProviders(NetworkBehaviour obj, NetworkReader reader)
	{
		if (!NetworkClient.active)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.InvokeSyncListm_syncTempCoverProviders(NetworkBehaviour, NetworkReader)).MethodHandle;
			}
			Debug.LogError("SyncList m_syncTempCoverProviders called on server.");
			return;
		}
		((ActorCover)obj).m_syncTempCoverProviders.HandleMsg(reader);
	}

	public override bool OnSerialize(NetworkWriter writer, bool forceAll)
	{
		if (forceAll)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(ActorCover.OnSerialize(NetworkWriter, bool)).MethodHandle;
			}
			GeneratedNetworkCode._WriteStructSyncListTempCoverInfo_None(writer, this.m_syncTempCoverProviders);
			return true;
		}
		bool flag = false;
		if ((base.syncVarDirtyBits & 1U) != 0U)
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
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
				flag = true;
			}
			GeneratedNetworkCode._WriteStructSyncListTempCoverInfo_None(writer, this.m_syncTempCoverProviders);
		}
		if (!flag)
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
			writer.WritePackedUInt32(base.syncVarDirtyBits);
		}
		return flag;
	}

	public override void OnDeserialize(NetworkReader reader, bool initialState)
	{
		if (initialState)
		{
			GeneratedNetworkCode._ReadStructSyncListTempCoverInfo_None(reader, this.m_syncTempCoverProviders);
			return;
		}
		int num = (int)reader.ReadPackedUInt32();
		if ((num & 1) != 0)
		{
			GeneratedNetworkCode._ReadStructSyncListTempCoverInfo_None(reader, this.m_syncTempCoverProviders);
		}
	}

	public enum CoverDirections
	{
		INVALID = -1,
		X_POS,
		X_NEG,
		Y_POS,
		Y_NEG,
		NUM,
		FIRST = 0
	}
}
