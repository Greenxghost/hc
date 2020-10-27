using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BarrierManager : NetworkBehaviour
{
	private static BarrierManager s_instance;

	private List<Barrier> m_barriers = new List<Barrier>();
	private Dictionary<Team, int> m_movementStates = new Dictionary<Team, int>();
	private Dictionary<Team, int> m_visionStates = new Dictionary<Team, int>();
	private List<BarrierSerializeInfo> m_clientBarrierInfo = new List<BarrierSerializeInfo>();

	private SyncListInt m_barrierIdSync = new SyncListInt();
	private SyncListInt m_movementStatesSync = new SyncListInt();
	private SyncListInt m_visionStatesSync = new SyncListInt();

	private bool m_clientNeedMovementUpdate;
	private bool m_suppressingAbilityBlocks;
	private bool m_hasAbilityBlockingBarriers;

	private static int kListm_barrierIdSync;
	private static int kListm_movementStatesSync;
	private static int kListm_visionStatesSync;
	private static int kRpcRpcUpdateBarriers;

	static BarrierManager()
	{
		kRpcRpcUpdateBarriers = 73930193;
		NetworkBehaviour.RegisterRpcDelegate(typeof(BarrierManager), kRpcRpcUpdateBarriers, InvokeRpcRpcUpdateBarriers);
		kListm_barrierIdSync = 1647649475;
		NetworkBehaviour.RegisterSyncListDelegate(typeof(BarrierManager), kListm_barrierIdSync, InvokeSyncListm_barrierIdSync);
		kListm_movementStatesSync = -1285657162;
		NetworkBehaviour.RegisterSyncListDelegate(typeof(BarrierManager), kListm_movementStatesSync, InvokeSyncListm_movementStatesSync);
		kListm_visionStatesSync = -1477195729;
		NetworkBehaviour.RegisterSyncListDelegate(typeof(BarrierManager), kListm_visionStatesSync, InvokeSyncListm_visionStatesSync);
		NetworkCRC.RegisterBehaviour("BarrierManager", 0);
	}

	public static BarrierManager Get()
	{
		return s_instance;
	}

	public void SuppressAbilityBlocks_Start()
	{
		if (!m_suppressingAbilityBlocks)
		{
			m_suppressingAbilityBlocks = true;
		}
        else
        {
			Debug.LogError("BarrierManager was told to start suppressing barrier ability-blocks, but it already was.  Ignoring...");
		}
	}

	public void SuppressAbilityBlocks_End()
	{
		if (m_suppressingAbilityBlocks)
		{
			m_suppressingAbilityBlocks = false;
		}
		else
		{
			Debug.LogError("BarrierManager was told to stop suppressing barrier ability-blocks, but it already wasn't.  Ignoring...");
		}
	}

	public bool SuppressingAbilityBlocks()
	{
		return m_suppressingAbilityBlocks;
	}

	private void Awake()
	{
		s_instance = this;
		m_movementStates.Add(Team.TeamA, 0);
		m_movementStates.Add(Team.TeamB, 0);
		m_movementStates.Add(Team.Objects, 0);
		m_visionStates.Add(Team.TeamA, 0);
		m_visionStates.Add(Team.TeamB, 0);
		m_visionStates.Add(Team.Objects, 0);
		m_barrierIdSync.InitializeBehaviour(this, kListm_barrierIdSync);
		m_movementStatesSync.InitializeBehaviour(this, kListm_movementStatesSync);
		m_visionStatesSync.InitializeBehaviour(this, kListm_visionStatesSync);
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public bool IsTeamSupported(Team team)
	{
        return team == Team.TeamA || team == Team.TeamB || team == Team.Objects;
	}

	public override void OnStartClient()
	{
		m_movementStatesSync.Callback = SyncListCallbackMovementStates;
		m_visionStatesSync.Callback = SyncListCallbackVisionStates;
	}

	public override void OnStartServer()
	{
		for (int i = 0; i < 3; i++)
		{
			m_movementStatesSync.Add(0);
			m_visionStatesSync.Add(0);
		}
	}

	private void Update()
	{
		if (NetworkServer.active)
		{
			return;
		}
		if (!m_clientNeedMovementUpdate)
		{
			return;
		}
		for (int i = 0; i < 3; i++)
		{
			Team teamFromSyncIndex = GetTeamFromSyncIndex(i);
			if (m_movementStates[teamFromSyncIndex] != m_movementStatesSync[i])
			{
				m_movementStates[teamFromSyncIndex] = m_movementStatesSync[i];
				ClientUpdateMovementOnSync(teamFromSyncIndex);
			}
		}
		m_clientNeedMovementUpdate = false;
		
	}

	public void AddBarrier(Barrier barrierToAdd, bool delayVisionUpdate, out List<ActorData> visionUpdaters)
	{
		visionUpdaters = new List<ActorData>();
		if (m_barriers.Contains(barrierToAdd))
		{
			Log.Error("Trying to add a barrier we've already added.");
		}
		else
		{
			m_barriers.Add(barrierToAdd);
			if (NetworkServer.active)
			{
				m_barrierIdSync.Add(barrierToAdd.m_guid);
				CallRpcUpdateBarriers();
				if (m_barrierIdSync.Count > 120)
				{
					Debug.LogError("More than " + 120 + " barriers active?");
				}
			}
			barrierToAdd.OnStart(delayVisionUpdate, out visionUpdaters);
		}
		UpdateHasAbilityBlockingBarriers();
	}

	public void RemoveBarrier(Barrier barrierToRemove, bool doRpcUpdate = true)
	{
		if (!m_barriers.Contains(barrierToRemove))
		{
			Log.Error("Trying to remove a barrier we don't have.");
		}
		else
		{
			m_barriers.Remove(barrierToRemove);
			if (NetworkServer.active)
			{
				for (int num = m_barrierIdSync.Count - 1; num >= 0; num--)
				{
					if (m_barrierIdSync[num] == barrierToRemove.m_guid)
					{
						m_barrierIdSync.RemoveAt(num);
					}
				}
				if (doRpcUpdate)
				{
					CallRpcUpdateBarriers();
				}
			}
			barrierToRemove.OnEnd();
		}
		UpdateHasAbilityBlockingBarriers();
	}

	public void AddClientBarrierInfo(BarrierSerializeInfo info)
	{
		m_clientBarrierInfo.Add(info);
		if (m_clientBarrierInfo.Count > 100)
		{
			Debug.LogError("More than 100 client barrier info");
		}
	}

	public void RemoveClientBarrierInfo(int guid)
	{
		for (int num = m_clientBarrierInfo.Count - 1; num >= 0; num--)
		{
			if (m_clientBarrierInfo[num].m_guid == guid)
			{
				if (!m_clientBarrierInfo[num].m_clientSequenceStartAttempted)
				{
					Log.Error("Client did not attempt to spawn barrier sequences before it is removed");
				}
				m_clientBarrierInfo.RemoveAt(num);
			}
		}
	}

	public bool HasBarrier(Barrier barrierToCheck)
	{
		return m_barriers.Contains(barrierToCheck);
	}

	public bool IsVisionBlocked(ActorData viewer, BoardSquare source, BoardSquare dest)
	{
		bool flag = false;
		bool flag2 = false;
		float num = 0.3f;
		int num2 = 0;
		while (true)
		{
			if (num2 < m_barriers.Count)
			{
				Barrier barrier = m_barriers[num2];
				if (!barrier.CanBeSeenThroughBy(viewer))
				{
					break;
				}
				num2++;
				continue;
			}
			break;
		}
		if (num2 < m_barriers.Count)
		{
			Vector3 a = source.ToVector3();
			Vector3 a2 = dest.ToVector3();
			Vector3 b;
			if (Mathf.Abs(source.x - dest.x) <= Mathf.Abs(source.y - dest.y))
			{
				b = new Vector3(Board.Get().squareSize * num, 0f, 0f);
			}
			else
			{
				b = new Vector3(0f, 0f, Board.Get().squareSize * num);
			}
			Vector3 src = a + b;
			Vector3 dest2 = a2 + b;
			Vector3 src2 = a - b;
			Vector3 dest3 = a2 - b;
			for (int i = num2; i < m_barriers.Count; i++)
			{
				if (flag)
				{
					if (flag2)
					{
						break;
					}
				}
				Barrier barrier2 = m_barriers[i];
				if (barrier2.CanBeSeenThroughBy(viewer))
				{
					continue;
				}
				if (!flag)
				{
					if (barrier2.CrossingBarrierForVision(src, dest2))
					{
						flag = true;
					}
				}
				if (flag2)
				{
					continue;
				}
				if (barrier2.CrossingBarrierForVision(src2, dest3))
				{
					flag2 = true;
				}
			}
		}
		int result;
		if (flag)
		{
			result = (flag2 ? 1 : 0);
		}
		else
		{
			result = 0;
		}
		return (byte)result != 0;
	}

	public int GetVisionStateChangesFor(ActorData actor)
	{
		Team team = actor.GetTeam();
		if (!IsTeamSupported(team))
		{
			return -1;
		}
		int syncIndexFromTeam = GetSyncIndexFromTeam(team);
		return m_visionStatesSync[syncIndexFromTeam];
	}

	[Server]
	public void UpdateVisionStateForTeam(Team team)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void BarrierManager::UpdateVisionStateForTeam(Team)' called on client");
			return;
		}
		if (IsTeamSupported(team))
		{
			int syncIndexFromTeam = GetSyncIndexFromTeam(team);
			int num = m_visionStatesSync[syncIndexFromTeam];
			int value = num + 1;
			m_visionStates[team] = value;
			m_visionStatesSync[syncIndexFromTeam] = value;
			return;
		}
		throw new Exception("BarrierManager does not support this team");
	}

	public bool IsMovementBlocked(ActorData mover, BoardSquare source, BoardSquare dest)
	{
		bool result = false;
		for (int i = 0; i < m_barriers.Count; i++)
		{
			Barrier barrier = m_barriers[i];
			if (!barrier.CanBeMovedThroughBy(mover) && barrier.CrossingBarrier(source.ToVector3(), dest.ToVector3()))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public bool IsMovementBlockedOnCrossover(ActorData mover, BoardSquare source, BoardSquare dest)
	{
		bool result = false;
		int num;
		if (mover.GetActorStatus() != null)
		{
			num = (mover.GetActorStatus().HasStatus(StatusType.Unstoppable) ? 1 : 0);
		}
		else
		{
			num = 0;
		}
		if (num == 0)
		{
			int num2 = 0;
			while (true)
			{
				if (num2 < m_barriers.Count)
				{
					Barrier barrier = m_barriers[num2];
					if (!barrier.CanMoveThroughAfterCrossoverBy(mover) && barrier.CrossingBarrier(source.ToVector3(), dest.ToVector3()))
					{
						result = true;
						break;
					}
					num2++;
					continue;
				}
				break;
			}
		}
		return result;
	}

	public int GetMovementStateChangesFor(ActorData mover)
	{
		Team team = mover.GetTeam();
		if (!IsTeamSupported(team))
		{
			return -1;
		}
		return m_movementStates[team];
	}

	[Server]
	public void UpdateMovementStateForTeam(Team team)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void BarrierManager::UpdateMovementStateForTeam(Team)' called on client");
			return;
		}
		if (IsTeamSupported(team))
		{
			int syncIndexFromTeam = GetSyncIndexFromTeam(team);
			int num = m_movementStatesSync[syncIndexFromTeam];
			int value = num + 1;
			m_movementStates[team] = value;
			m_movementStatesSync[syncIndexFromTeam] = value;
		}
		else
		{
			throw new Exception("BarrierManager does not support this team");
		}
	}

	public bool IsPositionTargetingBlocked(ActorData caster, BoardSquare dest)
	{
		if (caster == null)
		{
			return true;
		}
		BoardSquare currentBoardSquare = caster.GetCurrentBoardSquare();
		if (currentBoardSquare == null)
		{
			return true;
		}
		foreach (Barrier barrier in m_barriers)
		{
			if (barrier.IsPositionTargetingBlockedFor(caster) && barrier.CrossingBarrier(currentBoardSquare.ToVector3(), dest.ToVector3()))
			{
				return true;
			}
		}
		return false;
	}

	private void UpdateHasAbilityBlockingBarriers()
	{
		bool hasAbilityBlockingBarriers = false;
		for (int i = 0; i < m_barriers.Count; i++)
		{
			if (m_barriers[i] == null)
			{
				continue;
			}
			if (m_barriers[i].BlocksAbilities != 0)
			{
				hasAbilityBlockingBarriers = true;
				break;
			}
		}
		m_hasAbilityBlockingBarriers = hasAbilityBlockingBarriers;
	}

	public bool HasAbilityBlockingBarriers()
	{
		return m_hasAbilityBlockingBarriers;
	}

	public bool AreAbilitiesBlocked(ActorData caster, BoardSquare source, BoardSquare dest, List<NonActorTargetInfo> nonActorTargetInfo)
	{
		Vector3 startPos = source.ToVector3();
		Vector3 destPos = dest.ToVector3();
		return AreAbilitiesBlocked(caster, startPos, destPos, nonActorTargetInfo);
	}

	public bool AreAbilitiesBlocked(ActorData caster, Vector3 startPos, Vector3 destPos, List<NonActorTargetInfo> nonActorTargetInfo)
	{
		int num;
		if (nonActorTargetInfo != null)
		{
			num = (NetworkServer.active ? 1 : 0);
		}
		else
		{
			num = 0;
		}
		bool flag = (byte)num != 0;
		float num2 = 0f;
		Barrier barrier = null;
		bool flag2 = false;
		for (int i = 0; i < m_barriers.Count; i++)
		{
			Barrier barrier2 = m_barriers[i];
			if (barrier2.CanBeShotThroughBy(caster))
			{
				continue;
			}
			if (barrier2 == null)
			{
				continue;
			}
			if (!barrier2.CrossingBarrier(startPos, destPos))
			{
				continue;
			}
			if (!flag)
			{
				flag2 = true;
				break;
			}
			Vector3 intersectionPoint = barrier2.GetIntersectionPoint(startPos, destPos);
			intersectionPoint.y = startPos.y;
			float magnitude = (intersectionPoint - startPos).magnitude;
			if (flag2)
			{
				if (!(magnitude < num2))
				{
					goto IL_00d2;
				}
			}
			num2 = magnitude;
			goto IL_00d2;
			IL_00d2:
			flag2 = true;
		}
		if (barrier != null)
		{
		}
		return flag2;
	}

	public Vector3 GetAbilityLineEndpoint(ActorData caster, Vector3 lineStart, Vector3 currentEnd, out bool collision, out Vector3 collisionNormal, List<NonActorTargetInfo> nonActorTargetInfo = null)
	{
		Vector3 vector = currentEnd;
		collisionNormal = Vector3.zero;
		collision = false;
		Barrier barrier = null;
		for (int i = 0; i < m_barriers.Count; i++)
		{
			Barrier barrier2 = m_barriers[i];
			if (barrier2.CanBeShotThroughBy(caster))
			{
				continue;
			}
			if (barrier2.CrossingBarrier(lineStart, vector))
			{
				vector = barrier2.GetIntersectionPoint(lineStart, vector);
				collision = true;
				collisionNormal = barrier2.GetCollisionNormal(currentEnd - lineStart);
				barrier = barrier2;
			}
		}
		if (barrier != null)
		{
		}
		return vector;
	}

	public void UpdateCachedCoverDirections(ActorData forActor, BoardSquare centerSquare, ref bool[] cachedBarrierDirs)
	{
		if (centerSquare == null || forActor == null)
		{
			return;
		}
		Vector3 vector = centerSquare.ToVector3();
		for (int i = 0; i < m_barriers.Count; i++)
		{
			Barrier barrier = m_barriers[i];
			if (!barrier.ConsiderAsCover || barrier.GetBarrierTeam() != forActor.GetTeam())
			{
				continue;
			}
			for (int j = 0; j < cachedBarrierDirs.Length; j++)
			{
				if (!cachedBarrierDirs[j])
				{
					Vector3 b = 1.5f * ActorCover.GetCoverOffsetStatic((ActorCover.CoverDirections)j);
					bool flag = barrier.CrossingBarrier(vector + b, vector);
					cachedBarrierDirs[j] = flag;
				}
			}
		}
	}

	private Team GetTeamFromSyncIndex(int index)
	{
		switch (index)
		{
		case 0:
			return Team.TeamA;
		case 1:
			return Team.TeamB;
		case 2:
			return Team.Objects;
		default:
			return Team.Invalid;
		}
	}

	private int GetSyncIndexFromTeam(Team team)
	{
		switch (team)
		{
		case Team.TeamA:
			return 0;
		case Team.TeamB:
			return 1;
		case Team.Objects:
			return 2;
		default:
			Debug.LogError("Invalid team passed to GetSyncIndexFromTeam()");
			return 0;
		}
	}

	[ClientRpc]
	private void RpcUpdateBarriers()
	{
		if (NetworkServer.active)
		{
			return;
		}
		bool hasBarrierCovers = false;
		
		for (int num = 0; num < m_barriers.Count; num++)
		{
			if (m_barriers[num].ConsiderAsCover)
			{
				hasBarrierCovers = true;
				break;
			}
		}
		m_barriers.Clear();
		if (m_barrierIdSync.Count > 50)
		{
			Debug.LogError("More than 50 barriers active?");
		}
		for (int i = 0; i < m_barrierIdSync.Count; i++)
		{
			foreach (BarrierSerializeInfo cached in m_clientBarrierInfo)
			{
				if (cached.m_guid == m_barrierIdSync[i])
				{
					Barrier barrier = Barrier.CreateBarrierFromSerializeInfo(cached);
					if (barrier.ConsiderAsCover)
					{
						hasBarrierCovers = true;
					}
					AddBarrier(barrier, false, out List<ActorData> _);
					break;
				}
			}
		}
		ClientUpdateMovementAndVision();
		UpdateHasAbilityBlockingBarriers();
		if (hasBarrierCovers)
		{
			GameFlowData.Get().UpdateCoverFromBarriersForAllActors();
		}
	}

	private void SyncListCallbackMovementStates(SyncList<int>.Operation op, int _incorrectIndexBugIn51And52)
	{
		m_clientNeedMovementUpdate = true;
	}

	private void SyncListCallbackVisionStates(SyncList<int>.Operation op, int _incorrectIndexBugIn51And52)
	{
		ClientUpdateMovementAndVision();
	}

	[Client]
	private void ClientUpdateMovementAndVision()
	{
		if (!NetworkClient.active)
		{
			Debug.LogWarning("[Client] function 'System.Void BarrierManager::ClientUpdateMovementAndVision()' called on server");
			return;
		}
		if (NetworkServer.active)
		{
			return;
		}
		for (int i = 0; i < 3; i++)
		{
			Team teamFromSyncIndex = GetTeamFromSyncIndex(i);
			if (m_movementStatesSync[i] != m_movementStates[teamFromSyncIndex])
			{
				m_clientNeedMovementUpdate = true;
			}
			if (m_visionStatesSync[i] != m_visionStates[teamFromSyncIndex])
			{
				m_visionStates[teamFromSyncIndex] = m_visionStatesSync[i];
				ClientUpdateVisionOnSync(teamFromSyncIndex);
			}
		}
	}

	[Client]
	private void ClientUpdateMovementOnSync(Team team)
	{
		if (!NetworkClient.active)
		{
			Debug.LogWarning("[Client] function 'System.Void BarrierManager::ClientUpdateMovementOnSync(Team)' called on server");
			return;
		}
		ActorData actorData = GameFlowData.Get()?.activeOwnedActorData;
		if (actorData != null && actorData.GetTeam() == team)
		{
			actorData.GetActorMovement().UpdateSquaresCanMoveTo();
		}
	}

	[Client]
	private void ClientUpdateVisionOnSync(Team team)
	{
		if (!NetworkClient.active)
		{
			Debug.LogWarning("[Client] function 'System.Void BarrierManager::ClientUpdateVisionOnSync(Team)' called on server");
			return;
		}
		ActorData actorData = GameFlowData.Get()?.activeOwnedActorData;
		if (actorData != null && actorData.GetTeam() == team)
		{
			actorData.GetFogOfWar().MarkForRecalculateVisibility();
		}
	}

	private void OnDrawGizmos()
	{
		if (!CameraManager.ShouldDrawGizmosForCurrentCamera() || m_barriers == null)
		{
			return;
		}
		foreach (Barrier barrier in m_barriers)
		{
			barrier.DrawGizmos();
		}
	}

	private void UNetVersion()
	{
	}

	protected static void InvokeSyncListm_barrierIdSync(NetworkBehaviour obj, NetworkReader reader)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("SyncList m_barrierIdSync called on server.");
		}
		else
		{
			((BarrierManager)obj).m_barrierIdSync.HandleMsg(reader);
		}
	}

	protected static void InvokeSyncListm_movementStatesSync(NetworkBehaviour obj, NetworkReader reader)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("SyncList m_movementStatesSync called on server.");
			return;
		}
		((BarrierManager)obj).m_movementStatesSync.HandleMsg(reader);
	}

	protected static void InvokeSyncListm_visionStatesSync(NetworkBehaviour obj, NetworkReader reader)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("SyncList m_visionStatesSync called on server.");
			return;
		}
		((BarrierManager)obj).m_visionStatesSync.HandleMsg(reader);
	}

	protected static void InvokeRpcRpcUpdateBarriers(NetworkBehaviour obj, NetworkReader reader)
	{
		if (!NetworkClient.active)
		{
			Debug.LogError("RPC RpcUpdateBarriers called on server.");
		}
		else
		{
			((BarrierManager)obj).RpcUpdateBarriers();
		}
	}

	public void CallRpcUpdateBarriers()
	{
		if (!NetworkServer.active)
		{
			Debug.LogError("RPC Function RpcUpdateBarriers called on client.");
			return;
		}
		NetworkWriter networkWriter = new NetworkWriter();
		networkWriter.Write((short)0);
		networkWriter.Write((short)2);
		networkWriter.WritePackedUInt32((uint)kRpcRpcUpdateBarriers);
		networkWriter.Write(GetComponent<NetworkIdentity>().netId);
		SendRPCInternal(networkWriter, 0, "RpcUpdateBarriers");
	}

	public override bool OnSerialize(NetworkWriter writer, bool forceAll)
	{
		if (forceAll)
		{
			SyncListInt.WriteInstance(writer, m_barrierIdSync);
			SyncListInt.WriteInstance(writer, m_movementStatesSync);
			SyncListInt.WriteInstance(writer, m_visionStatesSync);
			return true;
		}
		bool flag = false;
		if ((base.syncVarDirtyBits & 1) != 0)
		{
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
				flag = true;
			}
			SyncListInt.WriteInstance(writer, m_barrierIdSync);
		}
		if ((base.syncVarDirtyBits & 2) != 0)
		{
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
				flag = true;
			}
			SyncListInt.WriteInstance(writer, m_movementStatesSync);
		}
		if ((base.syncVarDirtyBits & 4) != 0)
		{
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
				flag = true;
			}
			SyncListInt.WriteInstance(writer, m_visionStatesSync);
		}
		if (!flag)
		{
			writer.WritePackedUInt32(base.syncVarDirtyBits);
		}
		return flag;
	}

	public override void OnDeserialize(NetworkReader reader, bool initialState)
	{
		if (initialState)
		{
			SyncListInt.ReadReference(reader, m_barrierIdSync);
			SyncListInt.ReadReference(reader, m_movementStatesSync);
			SyncListInt.ReadReference(reader, m_visionStatesSync);
			return;
		}
		int num = (int)reader.ReadPackedUInt32();
		if ((num & 1) != 0)
		{
			SyncListInt.ReadReference(reader, m_barrierIdSync);
		}
		if ((num & 2) != 0)
		{
			SyncListInt.ReadReference(reader, m_movementStatesSync);
		}
		if ((num & 4) != 0)
		{
			SyncListInt.ReadReference(reader, m_visionStatesSync);
		}
	}
}
