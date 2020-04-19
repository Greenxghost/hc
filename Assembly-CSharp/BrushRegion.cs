﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BrushRegion : BoardRegion
{
	public GameObject m_functioningVFX;

	public GameObject m_disruptedVFX;

	public List<GameObject> m_perSquareFunctioningVFX;

	public List<GameObject> m_perSquareDisruptedVFX;

	private Dictionary<BoardSquare, byte> m_exteriorSquareFlags;

	private GameObject m_borderVfxParentFunctioning;

	private GameObject m_borderVfxParentDisrupted;

	private bool m_lastBorderCanBeVisible = true;

	private List<PKFxFX> m_borderVfxListFunctioning = new List<PKFxFX>();

	private List<PKFxFX> m_borderVfxListDisrupted = new List<PKFxFX>();

	public override void Initialize()
	{
		base.Initialize();
		if (this.m_quads.Length > 0)
		{
			if (!(this.m_quads[0].m_corner1 == null))
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
					RuntimeMethodHandle runtimeMethodHandle = methodof(BrushRegion.Initialize()).MethodHandle;
				}
				if (!(this.m_quads[0].m_corner2 == null))
				{
					Vector3 position = this.m_quads[0].m_corner1.position;
					BoardSquare boardSquare = Board.\u000E().\u0015(position.x, position.z);
					Vector3 position2 = this.m_quads[0].m_corner2.position;
					BoardSquare boardSquare2 = Board.\u000E().\u0015(position2.x, position2.z);
					if (!(boardSquare != null))
					{
						goto IL_16D;
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
					if (!(boardSquare2 != null))
					{
						goto IL_16D;
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
					Vector3 position3 = (boardSquare.ToVector3() + boardSquare2.ToVector3()) * 0.5f;
					if (this.m_functioningVFX != null)
					{
						this.m_functioningVFX.transform.position = position3;
					}
					if (this.m_disruptedVFX != null)
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
						this.m_disruptedVFX.transform.position = position3;
						goto IL_16D;
					}
					goto IL_16D;
				}
			}
			Log.Error("BrushRegion has null corners; set them or remove the region entirely.", new object[0]);
		}
		IL_16D:
		this.m_perSquareFunctioningVFX = new List<GameObject>();
		this.m_perSquareDisruptedVFX = new List<GameObject>();
		this.m_exteriorSquareFlags = new Dictionary<BoardSquare, byte>();
		List<BoardSquare> list = base.\u001D();
		using (List<BoardSquare>.Enumerator enumerator = list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				BoardSquare boardSquare3 = enumerator.Current;
				if (boardSquare3.\u0016())
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
					Vector3 position4 = boardSquare3.ToVector3();
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
						if (HighlightUtils.Get().m_brushDisruptedSquarePrefab != null)
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
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(HighlightUtils.Get().m_brushDisruptedSquarePrefab);
							gameObject.transform.position = position4;
							gameObject.transform.parent = BrushCoordinator.Get().transform;
							this.m_perSquareDisruptedVFX.Add(gameObject);
						}
						if (HighlightUtils.Get().m_brushFunctioningSquarePrefab != null)
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
							GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(HighlightUtils.Get().m_brushFunctioningSquarePrefab);
							gameObject2.transform.position = position4;
							gameObject2.transform.parent = BrushCoordinator.Get().transform;
							this.m_perSquareFunctioningVFX.Add(gameObject2);
						}
					}
					byte b = 0;
					BrushRegion.MaskSideFlagForSquare(ref b, boardSquare3, list);
					if (b != 0)
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
						this.m_exteriorSquareFlags[boardSquare3] = b;
					}
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
		float x = 0f;
		float y;
		if (HighlightUtils.Get() != null)
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
			y = HighlightUtils.Get().m_brushBorderHeightOffset;
		}
		else
		{
			y = 0f;
		}
		Vector3 localPosition = new Vector3(x, y, 0f);
		if (this.m_borderVfxParentFunctioning != null)
		{
			Log.Error("Brush region border vfx parent already exists when initializing region", new object[0]);
			UnityEngine.Object.Destroy(this.m_borderVfxParentFunctioning);
		}
		this.m_borderVfxParentFunctioning = new GameObject("BrushBorderParent_Functioning");
		this.m_borderVfxParentFunctioning.transform.localPosition = localPosition;
		this.m_borderVfxParentFunctioning.transform.localRotation = Quaternion.identity;
		if (GameFlowData.Get() != null)
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
			this.m_borderVfxParentFunctioning.transform.parent = GameFlowData.Get().GetBrushBordersRoot().transform;
		}
		if (this.m_borderVfxParentDisrupted != null)
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
			Log.Error("Brush region border vfx parent already exists when initializing region", new object[0]);
			UnityEngine.Object.Destroy(this.m_borderVfxParentDisrupted);
		}
		this.m_borderVfxParentDisrupted = new GameObject("BrushBorderParent_Disrupted");
		this.m_borderVfxParentDisrupted.transform.localPosition = localPosition;
		this.m_borderVfxParentDisrupted.transform.localRotation = Quaternion.identity;
		if (GameFlowData.Get() != null)
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
			this.m_borderVfxParentDisrupted.transform.parent = GameFlowData.Get().GetBrushBordersRoot().transform;
		}
		this.m_borderVfxListFunctioning.Clear();
		this.m_borderVfxListDisrupted.Clear();
		GameObject gameObject3;
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
			gameObject3 = HighlightUtils.Get().m_brushFunctioningBorderPrefab;
		}
		else
		{
			gameObject3 = null;
		}
		GameObject functioningPrefab = gameObject3;
		GameObject gameObject4;
		if (HighlightUtils.Get() != null)
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
			gameObject4 = HighlightUtils.Get().m_brushDisruptedBorderPrefab;
		}
		else
		{
			gameObject4 = null;
		}
		GameObject disruptedPrefab = gameObject4;
		using (Dictionary<BoardSquare, byte>.Enumerator enumerator2 = this.m_exteriorSquareFlags.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				KeyValuePair<BoardSquare, byte> keyValuePair = enumerator2.Current;
				BrushRegion.AddSideVfxPrefabs(keyValuePair.Key, keyValuePair.Value, functioningPrefab, disruptedPrefab, this.m_borderVfxParentFunctioning, this.m_borderVfxParentDisrupted, this.m_borderVfxListFunctioning, this.m_borderVfxListDisrupted);
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
		this.m_borderVfxParentDisrupted.SetActive(false);
		this.m_borderVfxParentFunctioning.SetActive(false);
	}

	public static bool HasTeamMemberInRegion(Team team, int regionIndex)
	{
		if (regionIndex >= 0)
		{
			bool result = false;
			List<ActorData> allTeamMembers = GameFlowData.Get().GetAllTeamMembers(team);
			using (List<ActorData>.Enumerator enumerator = allTeamMembers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ActorData actorData = enumerator.Current;
					if (actorData.\u0018() == regionIndex)
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
							RuntimeMethodHandle runtimeMethodHandle = methodof(BrushRegion.HasTeamMemberInRegion(Team, int)).MethodHandle;
						}
						return true;
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
			}
			return result;
		}
		return false;
	}

	public void UpdateBorderVisibility(bool functioning)
	{
		bool flag = !BrushCoordinator.Get().DisableAllBrush();
		bool flag2;
		if (!(CameraManager.Get() == null))
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(BrushRegion.UpdateBorderVisibility(bool)).MethodHandle;
			}
			flag2 = !CameraManager.Get().ShouldHideBrushVfx();
		}
		else
		{
			flag2 = true;
		}
		bool flag3 = flag2;
		if (functioning == this.m_borderVfxParentFunctioning.activeSelf)
		{
			if (this.m_lastBorderCanBeVisible == flag3)
			{
				goto IL_E6;
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
		this.m_borderVfxParentFunctioning.SetActive(functioning);
		for (int i = 0; i < this.m_borderVfxListFunctioning.Count; i++)
		{
			PKFxFX pkfxFX = this.m_borderVfxListFunctioning[i];
			if (pkfxFX != null)
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
				if (functioning)
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
					if (flag && flag3)
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
						pkfxFX.StartEffect();
						goto IL_CA;
					}
				}
				pkfxFX.TerminateEffect();
			}
			IL_CA:;
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
		IL_E6:
		if (functioning == this.m_borderVfxParentDisrupted.activeSelf || this.m_lastBorderCanBeVisible != flag3)
		{
			this.m_borderVfxParentDisrupted.SetActive(!functioning);
			for (int j = 0; j < this.m_borderVfxListDisrupted.Count; j++)
			{
				PKFxFX pkfxFX2 = this.m_borderVfxListDisrupted[j];
				if (pkfxFX2 != null)
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
					if (!functioning)
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
							if (flag3)
							{
								pkfxFX2.StartEffect();
								goto IL_168;
							}
						}
					}
					pkfxFX2.TerminateEffect();
				}
				IL_168:;
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
		}
		this.m_lastBorderCanBeVisible = flag3;
	}

	internal byte GetExteriorSideFlags(BoardSquare square)
	{
		byte result;
		if (this.m_exteriorSquareFlags.ContainsKey(square))
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(BrushRegion.GetExteriorSideFlags(BoardSquare)).MethodHandle;
			}
			result = this.m_exteriorSquareFlags[square];
		}
		else
		{
			result = 0;
		}
		return result;
	}

	internal void DrawOutlineGizmos(bool functioning)
	{
		Color color;
		if (functioning)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(BrushRegion.DrawOutlineGizmos(bool)).MethodHandle;
			}
			color = Color.white;
		}
		else
		{
			color = Color.red;
		}
		Gizmos.color = color;
		if (this.m_exteriorSquareFlags != null)
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
			foreach (KeyValuePair<BoardSquare, byte> keyValuePair in this.m_exteriorSquareFlags)
			{
				BrushRegion.DrawDebugGizmos(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}

	private static void MaskSideFlagForSquare(ref byte sideFlags, BoardSquare centerSquare, List<BoardSquare> squaresInSet)
	{
		BoardSquare squareToTest = Board.\u000E().\u0016(centerSquare.x, centerSquare.y + 1);
		BoardSquare squareToTest2 = Board.\u000E().\u0016(centerSquare.x, centerSquare.y - 1);
		BoardSquare squareToTest3 = Board.\u000E().\u0016(centerSquare.x - 1, centerSquare.y);
		BoardSquare squareToTest4 = Board.\u000E().\u0016(centerSquare.x + 1, centerSquare.y);
		BrushRegion.ApplyMarkForSide(ref sideFlags, SideFlags.Up, squareToTest, squaresInSet);
		BrushRegion.ApplyMarkForSide(ref sideFlags, SideFlags.Down, squareToTest2, squaresInSet);
		BrushRegion.ApplyMarkForSide(ref sideFlags, SideFlags.Left, squareToTest3, squaresInSet);
		BrushRegion.ApplyMarkForSide(ref sideFlags, SideFlags.Right, squareToTest4, squaresInSet);
	}

	private unsafe static void ApplyMarkForSide(ref byte sideFlags, SideFlags mask, BoardSquare squareToTest, List<BoardSquare> squaresInSet)
	{
		if (!(squareToTest == null))
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(BrushRegion.ApplyMarkForSide(byte*, SideFlags, BoardSquare, List<BoardSquare>)).MethodHandle;
			}
			if (squareToTest.\u0016())
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
				if (squaresInSet.Contains(squareToTest))
				{
					return;
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
		sideFlags |= (byte)mask;
	}

	private static void AddSideVfxPrefabs(BoardSquare square, byte sideFlags, GameObject functioningPrefab, GameObject disruptedPrefab, GameObject functioningRoot, GameObject disruptedRoot, List<PKFxFX> functioningVfxList, List<PKFxFX> disruptedVfxList)
	{
		if (square != null)
		{
			Vector3 vfxPos = square.ToVector3();
			IEnumerator enumerator = Enum.GetValues(typeof(SideFlags)).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					SideFlags sideFlags2 = (SideFlags)obj;
					if ((sideFlags & (byte)sideFlags2) != 0)
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
							RuntimeMethodHandle runtimeMethodHandle = methodof(BrushRegion.AddSideVfxPrefabs(BoardSquare, byte, GameObject, GameObject, GameObject, GameObject, List<PKFxFX>, List<PKFxFX>)).MethodHandle;
						}
						BrushRegion.AddSideVfxForSide(sideFlags2, vfxPos, functioningPrefab, disruptedPrefab, functioningRoot, disruptedRoot, functioningVfxList, disruptedVfxList);
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
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
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
					disposable.Dispose();
				}
			}
		}
	}

	private static void AddSideVfxForSide(SideFlags side, Vector3 vfxPos, GameObject functioningPrefab, GameObject disruptedPrefab, GameObject functioningRoot, GameObject disruptedRoot, List<PKFxFX> functioningVfxList, List<PKFxFX> disruptedVfxList)
	{
		Quaternion rotationForSidePrefab = BrushRegion.GetRotationForSidePrefab(side);
		if (functioningPrefab != null)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(BrushRegion.AddSideVfxForSide(SideFlags, Vector3, GameObject, GameObject, GameObject, GameObject, List<PKFxFX>, List<PKFxFX>)).MethodHandle;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(functioningPrefab);
			gameObject.transform.localPosition = vfxPos;
			gameObject.transform.localRotation = rotationForSidePrefab;
			gameObject.transform.parent = functioningRoot.transform;
			PKFxFX[] componentsInChildren = gameObject.GetComponentsInChildren<PKFxFX>();
			foreach (PKFxFX item in componentsInChildren)
			{
				functioningVfxList.Add(item);
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
		if (disruptedPrefab != null)
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
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(disruptedPrefab);
			gameObject2.transform.localPosition = vfxPos;
			gameObject2.transform.localRotation = rotationForSidePrefab;
			gameObject2.transform.parent = disruptedRoot.transform;
			PKFxFX[] componentsInChildren2 = gameObject2.GetComponentsInChildren<PKFxFX>();
			foreach (PKFxFX item2 in componentsInChildren2)
			{
				disruptedVfxList.Add(item2);
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

	private static Quaternion GetRotationForSidePrefab(SideFlags side)
	{
		if (side == SideFlags.Up)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(BrushRegion.GetRotationForSidePrefab(SideFlags)).MethodHandle;
			}
			return Quaternion.identity;
		}
		if (side == SideFlags.Down)
		{
			return Quaternion.Euler(0f, 180f, 0f);
		}
		if (side == SideFlags.Left)
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
			return Quaternion.Euler(0f, 270f, 0f);
		}
		if (side == SideFlags.Right)
		{
			return Quaternion.Euler(0f, 90f, 0f);
		}
		return Quaternion.identity;
	}

	private static void DrawDebugGizmos(BoardSquare square, byte sideFlags)
	{
		if (square != null)
		{
			Vector3 a = square.ToVector3();
			Vector3 b = new Vector3(0f, 0f, 0.5f * Board.\u000E().squareSize);
			Vector3 b2 = new Vector3(0.5f * Board.\u000E().squareSize, 0f, 0f);
			if ((sideFlags & 1) != 0)
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
					RuntimeMethodHandle runtimeMethodHandle = methodof(BrushRegion.DrawDebugGizmos(BoardSquare, byte)).MethodHandle;
				}
				Gizmos.DrawLine(a + b - b2, a + b + b2);
			}
			if ((sideFlags & 2) != 0)
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
				Gizmos.DrawLine(a - b + b2, a - b - b2);
			}
			if ((sideFlags & 4) != 0)
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
				Gizmos.DrawLine(a - b2 + b, a - b2 - b);
			}
			if ((sideFlags & 8) != 0)
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
				Gizmos.DrawLine(a + b2 + b, a + b2 - b);
			}
		}
	}
}
