using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameplayResponseForActor
{
	public StandardEffectInfo m_effect;
	[Space(5f)]
	public int m_credits;
	public int m_healing;
	public int m_damage;
	public int m_techPoints;
	public AbilityStatMod[] m_permanentStatMods;
	public StatusType[] m_permanentStatusChanges;
	public GameObject m_sequenceToPlay;

	public virtual void ReportAbilityTooltipNumbers(ref List<AbilityTooltipNumber> numbers, AbilityTooltipSubject subject)
	{
		if (m_damage != 0)
		{
			numbers.Add(new AbilityTooltipNumber(AbilityTooltipSymbol.Damage, subject, m_damage));
		}
		if (m_healing != 0)
		{
			numbers.Add(new AbilityTooltipNumber(AbilityTooltipSymbol.Healing, subject, m_healing));
		}
		m_effect.ReportAbilityTooltipNumbers(ref numbers, subject);
	}

	public GameplayResponseForActor GetShallowCopy()
	{
		return (GameplayResponseForActor)MemberwiseClone();
	}

	public void AddTooltipTokens(List<TooltipTokenEntry> tokens, string name, bool addCompare, GameplayResponseForActor other)
	{
		bool addDiff = addCompare && other != null;
		int damageVal = addDiff ? other.m_damage : 0;
		AbilityMod.AddToken_IntDiff(tokens, name + "_Damage", "damage on response", m_damage, addDiff, damageVal);
		int healingVal = addDiff ? other.m_healing : 0;
		AbilityMod.AddToken_IntDiff(tokens, name + "_Healing", "healing on response", m_healing, addDiff, healingVal);
		StandardEffectInfo effectVal = addDiff ? other.m_effect : null;
		AbilityMod.AddToken_EffectInfo(tokens, m_effect, name + "_Effect", effectVal);
	}

	public string GetInEditorDescription(string header = "- Response -", string indent = "", bool showDiff = false, GameplayResponseForActor other = null)
	{
		bool addDiff = showDiff && other != null;
		string otherSep = "\t        \t | in base  =";
		string desc = "\n" + InEditorDescHelper.BoldedStirng(header) + "\n";
		float creditsVal = addDiff ? other.m_credits : 0;		
		desc += InEditorDescHelper.AssembleFieldWithDiff("[ Credits ] = ", indent, otherSep, m_credits, addDiff, creditsVal, ((float f) => f != 0f));
		int healingVal = addDiff ? other.m_healing : 0;
		desc += InEditorDescHelper.AssembleFieldWithDiff("[ Healing ] = ", indent, otherSep, m_healing, addDiff, healingVal);
		int damageVal = addDiff ? other.m_damage : 0;
		desc += InEditorDescHelper.AssembleFieldWithDiff("[ Damage ] = ", indent, otherSep, m_damage, addDiff, damageVal);
		float techPointsVal = addDiff ? other.m_techPoints : 0;		
		desc += InEditorDescHelper.AssembleFieldWithDiff("[ TechPoints ] = ", indent, otherSep, m_techPoints, addDiff, techPointsVal, ((float f) => f != 0f));
		StandardEffectInfo effectVal = addDiff ? other.m_effect : null;
		desc += AbilityModHelper.GetModEffectInfoDesc(m_effect, "{ Effect on Moved-Through }", indent, addDiff, effectVal);
		AbilityStatMod[] permModsVal = addDiff ? other.m_permanentStatMods : null;
		desc += InEditorDescHelper.GetListDiffString("Permanent Stat Mods:", indent, m_permanentStatMods, addDiff, permModsVal);
		StatusType[] permStatusVal = addDiff ? other.m_permanentStatusChanges : null;
		desc += InEditorDescHelper.GetListDiffString("Permanent Status Changes:", indent, m_permanentStatusChanges, addDiff, permStatusVal);
		GameObject sequenceVal = addDiff ? other.m_sequenceToPlay : null;
		desc += InEditorDescHelper.AssembleFieldWithDiff("Response Hit Sequence", indent, otherSep, m_sequenceToPlay, addDiff, sequenceVal);
		return desc + indent + "-- END of Move-Through Response Output --\n";
	}

	public bool HasResponse()
	{
		return m_effect.m_applyEffect
			|| m_credits != 0
			|| m_healing != 0
			|| m_damage != 0
			|| m_techPoints != 0
			|| m_permanentStatMods != null && m_permanentStatMods.Length > 0
			|| m_permanentStatusChanges != null && m_permanentStatusChanges.Length > 0
			|| m_sequenceToPlay != null;
	}
}
