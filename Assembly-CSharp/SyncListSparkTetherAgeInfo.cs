﻿using System;
using UnityEngine.Networking;

public class SyncListSparkTetherAgeInfo : SyncListStruct<SparkBeamTrackerComponent.ActorIndexToTetherAge>
{
	public override void SerializeItem(NetworkWriter writer, SparkBeamTrackerComponent.ActorIndexToTetherAge item)
	{
		writer.WritePackedUInt32((uint)item.m_actorIndex);
		writer.WritePackedUInt32((uint)item.m_tetherAge);
	}

	public override SparkBeamTrackerComponent.ActorIndexToTetherAge DeserializeItem(NetworkReader reader)
	{
		return new SparkBeamTrackerComponent.ActorIndexToTetherAge
		{
			m_actorIndex = (int)reader.ReadPackedUInt32(),
			m_tetherAge = (int)reader.ReadPackedUInt32()
		};
	}
}
