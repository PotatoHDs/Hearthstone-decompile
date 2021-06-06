using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000247 RID: 583
[Serializable]
public class QuestPoolDbfRecord : DbfRecord
{
	// Token: 0x170003EF RID: 1007
	// (get) Token: 0x06001ED4 RID: 7892 RVA: 0x0009B936 File Offset: 0x00099B36
	[DbfField("GRANT_DAY_OF_WEEK")]
	public int GrantDayOfWeek
	{
		get
		{
			return this.m_grantDayOfWeek;
		}
	}

	// Token: 0x170003F0 RID: 1008
	// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x0009B93E File Offset: 0x00099B3E
	[DbfField("GRANT_HOUR_OF_DAY")]
	public int GrantHourOfDay
	{
		get
		{
			return this.m_grantHourOfDay;
		}
	}

	// Token: 0x170003F1 RID: 1009
	// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x0009B946 File Offset: 0x00099B46
	[DbfField("NUM_QUESTS_GRANTED")]
	public int NumQuestsGranted
	{
		get
		{
			return this.m_numQuestsGranted;
		}
	}

	// Token: 0x170003F2 RID: 1010
	// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x0009B94E File Offset: 0x00099B4E
	[DbfField("MAX_QUESTS_ACTIVE")]
	public int MaxQuestsActive
	{
		get
		{
			return this.m_maxQuestsActive;
		}
	}

	// Token: 0x170003F3 RID: 1011
	// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x0009B956 File Offset: 0x00099B56
	[DbfField("REROLL_COUNT_MAX")]
	public int RerollCountMax
	{
		get
		{
			return this.m_rerollCountMax;
		}
	}

	// Token: 0x170003F4 RID: 1012
	// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x0009B95E File Offset: 0x00099B5E
	[DbfField("QUEST_POOL_TYPE")]
	public QuestPool.QuestPoolType QuestPoolType
	{
		get
		{
			return this.m_questPoolType;
		}
	}

	// Token: 0x06001EDA RID: 7898 RVA: 0x0009B966 File Offset: 0x00099B66
	public void SetGrantDayOfWeek(int v)
	{
		this.m_grantDayOfWeek = v;
	}

	// Token: 0x06001EDB RID: 7899 RVA: 0x0009B96F File Offset: 0x00099B6F
	public void SetGrantHourOfDay(int v)
	{
		this.m_grantHourOfDay = v;
	}

	// Token: 0x06001EDC RID: 7900 RVA: 0x0009B978 File Offset: 0x00099B78
	public void SetNumQuestsGranted(int v)
	{
		this.m_numQuestsGranted = v;
	}

	// Token: 0x06001EDD RID: 7901 RVA: 0x0009B981 File Offset: 0x00099B81
	public void SetMaxQuestsActive(int v)
	{
		this.m_maxQuestsActive = v;
	}

	// Token: 0x06001EDE RID: 7902 RVA: 0x0009B98A File Offset: 0x00099B8A
	public void SetRerollCountMax(int v)
	{
		this.m_rerollCountMax = v;
	}

	// Token: 0x06001EDF RID: 7903 RVA: 0x0009B993 File Offset: 0x00099B93
	public void SetQuestPoolType(QuestPool.QuestPoolType v)
	{
		this.m_questPoolType = v;
	}

	// Token: 0x06001EE0 RID: 7904 RVA: 0x0009B99C File Offset: 0x00099B9C
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1188156935U)
		{
			if (num != 295297505U)
			{
				if (num != 810012553U)
				{
					if (num == 1188156935U)
					{
						if (name == "NUM_QUESTS_GRANTED")
						{
							return this.m_numQuestsGranted;
						}
					}
				}
				else if (name == "GRANT_HOUR_OF_DAY")
				{
					return this.m_grantHourOfDay;
				}
			}
			else if (name == "QUEST_POOL_TYPE")
			{
				return this.m_questPoolType;
			}
		}
		else if (num <= 2510957890U)
		{
			if (num != 1458105184U)
			{
				if (num == 2510957890U)
				{
					if (name == "REROLL_COUNT_MAX")
					{
						return this.m_rerollCountMax;
					}
				}
			}
			else if (name == "ID")
			{
				return base.ID;
			}
		}
		else if (num != 3136795921U)
		{
			if (num == 4002087504U)
			{
				if (name == "MAX_QUESTS_ACTIVE")
				{
					return this.m_maxQuestsActive;
				}
			}
		}
		else if (name == "GRANT_DAY_OF_WEEK")
		{
			return this.m_grantDayOfWeek;
		}
		return null;
	}

	// Token: 0x06001EE1 RID: 7905 RVA: 0x0009BAD4 File Offset: 0x00099CD4
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1188156935U)
		{
			if (num != 295297505U)
			{
				if (num != 810012553U)
				{
					if (num != 1188156935U)
					{
						return;
					}
					if (!(name == "NUM_QUESTS_GRANTED"))
					{
						return;
					}
					this.m_numQuestsGranted = (int)val;
					return;
				}
				else
				{
					if (!(name == "GRANT_HOUR_OF_DAY"))
					{
						return;
					}
					this.m_grantHourOfDay = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "QUEST_POOL_TYPE"))
				{
					return;
				}
				if (val == null)
				{
					this.m_questPoolType = QuestPool.QuestPoolType.NONE;
					return;
				}
				if (val is QuestPool.QuestPoolType || val is int)
				{
					this.m_questPoolType = (QuestPool.QuestPoolType)val;
					return;
				}
				if (val is string)
				{
					this.m_questPoolType = QuestPool.ParseQuestPoolTypeValue((string)val);
				}
				return;
			}
		}
		else if (num <= 2510957890U)
		{
			if (num != 1458105184U)
			{
				if (num != 2510957890U)
				{
					return;
				}
				if (!(name == "REROLL_COUNT_MAX"))
				{
					return;
				}
				this.m_rerollCountMax = (int)val;
				return;
			}
			else
			{
				if (!(name == "ID"))
				{
					return;
				}
				base.SetID((int)val);
				return;
			}
		}
		else if (num != 3136795921U)
		{
			if (num != 4002087504U)
			{
				return;
			}
			if (!(name == "MAX_QUESTS_ACTIVE"))
			{
				return;
			}
			this.m_maxQuestsActive = (int)val;
			return;
		}
		else
		{
			if (!(name == "GRANT_DAY_OF_WEEK"))
			{
				return;
			}
			this.m_grantDayOfWeek = (int)val;
			return;
		}
	}

	// Token: 0x06001EE2 RID: 7906 RVA: 0x0009BC28 File Offset: 0x00099E28
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1188156935U)
		{
			if (num != 295297505U)
			{
				if (num != 810012553U)
				{
					if (num == 1188156935U)
					{
						if (name == "NUM_QUESTS_GRANTED")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "GRANT_HOUR_OF_DAY")
				{
					return typeof(int);
				}
			}
			else if (name == "QUEST_POOL_TYPE")
			{
				return typeof(QuestPool.QuestPoolType);
			}
		}
		else if (num <= 2510957890U)
		{
			if (num != 1458105184U)
			{
				if (num == 2510957890U)
				{
					if (name == "REROLL_COUNT_MAX")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "ID")
			{
				return typeof(int);
			}
		}
		else if (num != 3136795921U)
		{
			if (num == 4002087504U)
			{
				if (name == "MAX_QUESTS_ACTIVE")
				{
					return typeof(int);
				}
			}
		}
		else if (name == "GRANT_DAY_OF_WEEK")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x06001EE3 RID: 7907 RVA: 0x0009BD56 File Offset: 0x00099F56
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadQuestPoolDbfRecords loadRecords = new LoadQuestPoolDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001EE4 RID: 7908 RVA: 0x0009BD6C File Offset: 0x00099F6C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		QuestPoolDbfAsset questPoolDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(QuestPoolDbfAsset)) as QuestPoolDbfAsset;
		if (questPoolDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("QuestPoolDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < questPoolDbfAsset.Records.Count; i++)
		{
			questPoolDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (questPoolDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001EE5 RID: 7909 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001EE6 RID: 7910 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x040011BB RID: 4539
	[SerializeField]
	private int m_grantDayOfWeek = -1;

	// Token: 0x040011BC RID: 4540
	[SerializeField]
	private int m_grantHourOfDay;

	// Token: 0x040011BD RID: 4541
	[SerializeField]
	private int m_numQuestsGranted = 1;

	// Token: 0x040011BE RID: 4542
	[SerializeField]
	private int m_maxQuestsActive = 1;

	// Token: 0x040011BF RID: 4543
	[SerializeField]
	private int m_rerollCountMax;

	// Token: 0x040011C0 RID: 4544
	[SerializeField]
	private QuestPool.QuestPoolType m_questPoolType = QuestPool.QuestPoolType.DAILY;
}
