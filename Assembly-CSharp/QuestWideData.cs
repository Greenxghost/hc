﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestWideData : MonoBehaviour
{
	private static QuestWideData s_instance;

	[Header("Quest Global Values")]
	public int m_generalSlotCount;

	public int m_questResetHour;

	public DayOfWeek m_questResetDayOfWeek;

	public int m_questBonusPerRejection = 0xA;

	public int m_questMaxRejectPercentage = 0x32;

	public int m_notIncludedWeightIncrease;

	public int m_rejectedWeightDecrease;

	public int m_completedWeightDecrease;

	public int m_abandonedWeightDecrease;

	[Header("Quests")]
	public List<QuestTemplate> m_quests;

	[Header("Style Groups")]
	public List<QuestStyleGroup> m_styleGroups;

	[Header("Daily Quest Pools")]
	public List<QuestPool> m_dailyQuestPools;

	private static Dictionary<int, bool> m_dailyQuestsCache = new Dictionary<int, bool>();

	public static QuestWideData Get()
	{
		return QuestWideData.s_instance;
	}

	private void Awake()
	{
		QuestWideData.s_instance = this;
		if (this.m_quests.Count == 0)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(QuestWideData.Awake()).MethodHandle;
			}
			throw new Exception("QuestWideData failed to load");
		}
		QuestWideData.m_dailyQuestsCache = new Dictionary<int, bool>();
		using (List<QuestPool>.Enumerator enumerator = this.m_dailyQuestPools.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				QuestPool questPool = enumerator.Current;
				if (questPool.Valid)
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
					using (List<QuestPool.Quest>.Enumerator enumerator2 = questPool.Quests.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							QuestPool.Quest quest = enumerator2.Current;
							if (!QuestWideData.m_dailyQuestsCache.ContainsKey(quest.QuestId))
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
								QuestWideData.m_dailyQuestsCache.Add(quest.QuestId, true);
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

	public QuestTemplate GetQuestTemplate(int templateId)
	{
		if (templateId > 0 && this.m_quests.Count >= templateId)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(QuestWideData.GetQuestTemplate(int)).MethodHandle;
			}
			return this.m_quests[templateId - 1];
		}
		return null;
	}

	private void OnDestroy()
	{
		QuestWideData.s_instance = null;
	}

	public bool IsDailyQuest(int questId)
	{
		bool flag2;
		bool flag = QuestWideData.m_dailyQuestsCache.TryGetValue(questId, out flag2);
		bool result;
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
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(QuestWideData.IsDailyQuest(int)).MethodHandle;
			}
			result = flag2;
		}
		else
		{
			result = false;
		}
		return result;
	}

	public bool CheckAllIndices()
	{
		bool result = false;
		if (Application.isEditor)
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
				RuntimeMethodHandle runtimeMethodHandle = methodof(QuestWideData.CheckAllIndices()).MethodHandle;
			}
			int num = 1;
			foreach (QuestTemplate questTemplate in this.m_quests)
			{
				if (questTemplate.Index >= num)
				{
					num = questTemplate.Index + 1;
				}
			}
			using (List<QuestTemplate>.Enumerator enumerator2 = this.m_quests.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					QuestTemplate questTemplate2 = enumerator2.Current;
					bool flag = false;
					if (questTemplate2.Index == 0)
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
						flag = true;
					}
					else
					{
						int num2 = 0;
						foreach (QuestTemplate questTemplate3 in this.m_quests)
						{
							if (questTemplate3.Index == questTemplate2.Index)
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
								num2++;
							}
						}
						if (num2 > 1)
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
						}
					}
					if (flag)
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
						questTemplate2.Index = num;
						num++;
						result = true;
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
		return result;
	}

	public static bool IsCharacterQuest(List<QuestObjective> Objectives, CharacterResourceLink charLink)
	{
		using (List<QuestObjective>.Enumerator enumerator = Objectives.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				QuestObjective questObjective = enumerator.Current;
				using (List<QuestTrigger>.Enumerator enumerator2 = questObjective.Triggers.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						QuestTrigger questTrigger = enumerator2.Current;
						using (List<QuestCondition>.Enumerator enumerator3 = questTrigger.Conditions.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								QuestCondition questCondition = enumerator3.Current;
								if (questCondition.ConditionType == QuestConditionType.UsingCharacter)
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
										RuntimeMethodHandle runtimeMethodHandle = methodof(QuestWideData.IsCharacterQuest(List<QuestObjective>, CharacterResourceLink)).MethodHandle;
									}
									if (questCondition.typeSpecificData == (int)charLink.m_characterType)
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
										return true;
									}
								}
								if (questCondition.ConditionType == QuestConditionType.HasCharacterLevel)
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
									if (questCondition.typeSpecificData == (int)charLink.m_characterType)
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
										return true;
									}
								}
								if (questCondition.ConditionType == QuestConditionType.UsingCharacterRole && questCondition.typeSpecificData == (int)charLink.m_characterRole)
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
									return true;
								}
								if (questCondition.ConditionType == QuestConditionType.UsingCharacterFaction)
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
									FactionCompetition factionCompetition = FactionWideData.Get().GetFactionCompetition(questCondition.typeSpecificData);
									FactionGroup factionGroup = FactionWideData.Get().GetFactionGroup(factionCompetition.Factions[questCondition.typeSpecificData2].FactionGroupIDToUse);
									bool flag = factionGroup.Characters.Exists((CharacterType x) => x == charLink.m_characterType);
									if (flag)
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
										return true;
									}
								}
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

	public static int GetEndLevel(QuestPrerequisites prereqs, int seasonIndex)
	{
		Queue<LogicOpClass> queue = new Queue<LogicOpClass>();
		string text = prereqs.LogicStatement;
		if (text.IsNullOrEmpty())
		{
			int num = 0x41;
			for (int i = 0; i < prereqs.Conditions.Count; i++)
			{
				if (text.IsNullOrEmpty())
				{
					text = Convert.ToChar(num).ToString();
				}
				else
				{
					text = text + " & " + Convert.ToChar(num);
				}
				num++;
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
			if (!true)
			{
				RuntimeMethodHandle runtimeMethodHandle = methodof(QuestWideData.GetEndLevel(QuestPrerequisites, int)).MethodHandle;
			}
		}
		queue.Enqueue(LogicStatement.EvaluateLogicStatement(text));
		int result = 0;
		while (queue.Count > 0)
		{
			LogicOpClass logicOpClass = queue.Dequeue();
			if (logicOpClass is ConstantLogicOpClass)
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
			}
			else if (logicOpClass is AndLogicOpClass)
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
				AndLogicOpClass andLogicOpClass = (AndLogicOpClass)logicOpClass;
				queue.Enqueue(andLogicOpClass.m_left);
				queue.Enqueue(andLogicOpClass.m_right);
			}
			else if (logicOpClass is OrLogicOpClass)
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
				OrLogicOpClass orLogicOpClass = (OrLogicOpClass)logicOpClass;
				queue.Enqueue(orLogicOpClass.m_left);
				queue.Enqueue(orLogicOpClass.m_right);
			}
			else if (!(logicOpClass is NegateLogicOpClass))
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
			}
			else
			{
				NegateLogicOpClass negateLogicOpClass = (NegateLogicOpClass)logicOpClass;
				if (negateLogicOpClass.m_target is ConstantLogicOpClass)
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
					int myIndex = ((ConstantLogicOpClass)negateLogicOpClass.m_target).myIndex;
					if (prereqs.Conditions[myIndex].ConditionType == QuestConditionType.HasSeasonLevel)
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
						if (prereqs.Conditions[myIndex].typeSpecificData == seasonIndex)
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
							result = prereqs.Conditions[myIndex].typeSpecificData2;
						}
					}
				}
				else if (negateLogicOpClass.m_target is AndLogicOpClass)
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
					AndLogicOpClass andLogicOpClass2 = (AndLogicOpClass)negateLogicOpClass.m_target;
					queue.Enqueue(new NegateLogicOpClass
					{
						m_target = andLogicOpClass2.m_left
					});
					queue.Enqueue(new NegateLogicOpClass
					{
						m_target = andLogicOpClass2.m_right
					});
				}
				else if (negateLogicOpClass.m_target is OrLogicOpClass)
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
					OrLogicOpClass orLogicOpClass2 = (OrLogicOpClass)negateLogicOpClass.m_target;
					queue.Enqueue(new NegateLogicOpClass
					{
						m_target = orLogicOpClass2.m_left
					});
					queue.Enqueue(new NegateLogicOpClass
					{
						m_target = orLogicOpClass2.m_right
					});
				}
				else if (negateLogicOpClass.m_target is NegateLogicOpClass)
				{
					negateLogicOpClass = (NegateLogicOpClass)negateLogicOpClass.m_target;
					queue.Enqueue(negateLogicOpClass.m_target);
				}
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
		return result;
	}

	public static bool AreConditionsMet(List<QuestCondition> conditions, string logicStatement, bool tryUseCharDataOnInitialLoad = false)
	{
		List<bool> list = new List<bool>(conditions.Count);
		for (int i = 0; i < conditions.Count; i++)
		{
			QuestCondition questCondition = conditions[i];
			if (questCondition.ConditionType == QuestConditionType.HasDateTimePassed)
			{
				DateTime t = new DateTime(questCondition.typeSpecificDate[0], questCondition.typeSpecificDate[1], questCondition.typeSpecificDate[2], questCondition.typeSpecificDate[3], questCondition.typeSpecificDate[4], questCondition.typeSpecificDate[5]);
				if (ClientGameManager.Get().PacificNow() > t)
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
						RuntimeMethodHandle runtimeMethodHandle = methodof(QuestWideData.AreConditionsMet(List<QuestCondition>, string, bool)).MethodHandle;
					}
					list.Add(true);
				}
				else
				{
					list.Add(false);
				}
			}
			else if (questCondition.ConditionType == QuestConditionType.HasCompletedQuest)
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
				list.Add(ClientGameManager.Get().GetPlayerAccountData().QuestComponent.GetCompletedCount(questCondition.typeSpecificData) > 0);
			}
			else if (questCondition.ConditionType == QuestConditionType.HasPurchasedGame)
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
				list.Add(ClientGameManager.Get().HasPurchasedGame);
			}
			else if (questCondition.ConditionType == QuestConditionType.HasUnlockedCharacter)
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
				list.Add(ClientGameManager.Get().GetPlayerCharacterData((CharacterType)questCondition.typeSpecificData).CharacterComponent.Unlocked);
			}
			else if (questCondition.ConditionType == QuestConditionType.HasUnlockedStyle)
			{
				bool item = false;
				CharacterType typeSpecificData = (CharacterType)questCondition.typeSpecificData;
				PersistedCharacterData persistedCharacterData;
				if (tryUseCharDataOnInitialLoad)
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
					persistedCharacterData = ClientGameManager.Get().GetCharacterDataOnInitialLoad(typeSpecificData);
				}
				else
				{
					persistedCharacterData = ClientGameManager.Get().GetPlayerCharacterData(typeSpecificData);
				}
				PersistedCharacterData persistedCharacterData2 = persistedCharacterData;
				if (persistedCharacterData2 != null)
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
					item = persistedCharacterData2.CharacterComponent.IsStyleUnlocked(questCondition.typeSpecificData2, questCondition.typeSpecificData3, questCondition.typeSpecificData4);
				}
				list.Add(item);
			}
			else if (questCondition.ConditionType == QuestConditionType.HasUnlockedTitle)
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
				list.Add(ClientGameManager.Get().GetPlayerAccountData().AccountComponent.IsTitleUnlocked(questCondition.typeSpecificData));
			}
			else if (questCondition.ConditionType == QuestConditionType.HasUnlockedChatEmoji)
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
				list.Add(ClientGameManager.Get().GetPlayerAccountData().AccountComponent.IsChatEmojiUnlocked(questCondition.typeSpecificData));
			}
			else if (questCondition.ConditionType == QuestConditionType.HasUnlockedOvercon)
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
				list.Add(ClientGameManager.Get().GetPlayerAccountData().AccountComponent.IsOverconUnlocked(questCondition.typeSpecificData));
			}
			else if (questCondition.ConditionType == QuestConditionType.HasUnlockedTaunt)
			{
				bool item2 = false;
				CharacterType typeSpecificData2 = (CharacterType)questCondition.typeSpecificData;
				PersistedCharacterData persistedCharacterData3;
				if (tryUseCharDataOnInitialLoad)
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
					persistedCharacterData3 = ClientGameManager.Get().GetCharacterDataOnInitialLoad(typeSpecificData2);
				}
				else
				{
					persistedCharacterData3 = ClientGameManager.Get().GetPlayerCharacterData(typeSpecificData2);
				}
				PersistedCharacterData persistedCharacterData4 = persistedCharacterData3;
				if (persistedCharacterData4 != null)
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
					if (questCondition.typeSpecificData2 < persistedCharacterData4.CharacterComponent.Taunts.Count)
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
						item2 = persistedCharacterData4.CharacterComponent.GetTaunt(questCondition.typeSpecificData2).Unlocked;
					}
				}
				list.Add(item2);
			}
			else
			{
				if (questCondition.ConditionType != QuestConditionType.HasSeasonAccess)
				{
					list.Add(false);
					throw new Exception("Unimplemented quest condition: " + questCondition.ConditionType);
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
				list.Add(ClientGameManager.Get().GetPlayerAccountData().QuestComponent.ActiveSeason == questCondition.typeSpecificData);
			}
		}
		if (logicStatement == string.Empty)
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
			bool result = true;
			using (List<bool>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current)
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
						result = false;
					}
				}
			}
			return result;
		}
		return LogicStatement.EvaluateLogicStatement(logicStatement).GetValue(list);
	}
}
