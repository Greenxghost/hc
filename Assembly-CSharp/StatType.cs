﻿using System;

public enum StatType
{
	INVALID = -1,
	OutgoingDamage,
	IncomingDamage,
	OutgoingHealing,
	IncomingHealing,
	Movement_Horizontal,
	Movement_Upward,
	Movement_Downward,
	MaxHitPoints,
	MaxTechPoints,
	HitPointRegen,
	TechPointRegen,
	SightRange,
	CreditsPerTurn,
	ControlPointCaptureSpeed,
	OutgoingAbsorb,
	CoverIncomingDamageMultiplier,
	OutgoingDamage_ToCover,
	OutgoingDamage_FromCover,
	LifestealPerHit,
	LifestealPerDamage,
	EnergyOnDamageTaken,
	EnergyPerDamageTaken,
	HitPointRegenPercentOfMax,
	MechanicPointRegen,
	NUM
}
