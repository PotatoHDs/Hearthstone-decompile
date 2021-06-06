using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001E6 RID: 486
[Serializable]
public class FixedRewardMapDbfRecord : DbfRecord
{
	// Token: 0x170002F2 RID: 754
	// (get) Token: 0x06001B60 RID: 7008 RVA: 0x0008EAD6 File Offset: 0x0008CCD6
	[DbfField("ACTION_ID")]
	public int ActionId
	{
		get
		{
			return this.m_actionId;
		}
	}

	// Token: 0x170002F3 RID: 755
	// (get) Token: 0x06001B61 RID: 7009 RVA: 0x0008EADE File Offset: 0x0008CCDE
	public FixedRewardActionDbfRecord ActionRecord
	{
		get
		{
			return GameDbf.FixedRewardAction.GetRecord(this.m_actionId);
		}
	}

	// Token: 0x170002F4 RID: 756
	// (get) Token: 0x06001B62 RID: 7010 RVA: 0x0008EAF0 File Offset: 0x0008CCF0
	[DbfField("REWARD_ID")]
	public int RewardId
	{
		get
		{
			return this.m_rewardId;
		}
	}

	// Token: 0x170002F5 RID: 757
	// (get) Token: 0x06001B63 RID: 7011 RVA: 0x0008EAF8 File Offset: 0x0008CCF8
	public FixedRewardDbfRecord RewardRecord
	{
		get
		{
			return GameDbf.FixedReward.GetRecord(this.m_rewardId);
		}
	}

	// Token: 0x170002F6 RID: 758
	// (get) Token: 0x06001B64 RID: 7012 RVA: 0x0008EB0A File Offset: 0x0008CD0A
	[DbfField("REWARD_COUNT")]
	public int RewardCount
	{
		get
		{
			return this.m_rewardCount;
		}
	}

	// Token: 0x170002F7 RID: 759
	// (get) Token: 0x06001B65 RID: 7013 RVA: 0x0008EB12 File Offset: 0x0008CD12
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x170002F8 RID: 760
	// (get) Token: 0x06001B66 RID: 7014 RVA: 0x0008EB1A File Offset: 0x0008CD1A
	[DbfField("USE_QUEST_TOAST")]
	public bool UseQuestToast
	{
		get
		{
			return this.m_useQuestToast;
		}
	}

	// Token: 0x170002F9 RID: 761
	// (get) Token: 0x06001B67 RID: 7015 RVA: 0x0008EB22 File Offset: 0x0008CD22
	[DbfField("REWARD_TIMING")]
	public string RewardTiming
	{
		get
		{
			return this.m_rewardTiming;
		}
	}

	// Token: 0x170002FA RID: 762
	// (get) Token: 0x06001B68 RID: 7016 RVA: 0x0008EB2A File Offset: 0x0008CD2A
	[DbfField("TOAST_NAME")]
	public DbfLocValue ToastName
	{
		get
		{
			return this.m_toastName;
		}
	}

	// Token: 0x170002FB RID: 763
	// (get) Token: 0x06001B69 RID: 7017 RVA: 0x0008EB32 File Offset: 0x0008CD32
	[DbfField("TOAST_DESCRIPTION")]
	public DbfLocValue ToastDescription
	{
		get
		{
			return this.m_toastDescription;
		}
	}

	// Token: 0x170002FC RID: 764
	// (get) Token: 0x06001B6A RID: 7018 RVA: 0x0008EB3A File Offset: 0x0008CD3A
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x06001B6B RID: 7019 RVA: 0x0008EB42 File Offset: 0x0008CD42
	public void SetActionId(int v)
	{
		this.m_actionId = v;
	}

	// Token: 0x06001B6C RID: 7020 RVA: 0x0008EB4B File Offset: 0x0008CD4B
	public void SetRewardId(int v)
	{
		this.m_rewardId = v;
	}

	// Token: 0x06001B6D RID: 7021 RVA: 0x0008EB54 File Offset: 0x0008CD54
	public void SetRewardCount(int v)
	{
		this.m_rewardCount = v;
	}

	// Token: 0x06001B6E RID: 7022 RVA: 0x0008EB5D File Offset: 0x0008CD5D
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001B6F RID: 7023 RVA: 0x0008EB66 File Offset: 0x0008CD66
	public void SetUseQuestToast(bool v)
	{
		this.m_useQuestToast = v;
	}

	// Token: 0x06001B70 RID: 7024 RVA: 0x0008EB6F File Offset: 0x0008CD6F
	public void SetRewardTiming(string v)
	{
		this.m_rewardTiming = v;
	}

	// Token: 0x06001B71 RID: 7025 RVA: 0x0008EB78 File Offset: 0x0008CD78
	public void SetToastName(DbfLocValue v)
	{
		this.m_toastName = v;
		v.SetDebugInfo(base.ID, "TOAST_NAME");
	}

	// Token: 0x06001B72 RID: 7026 RVA: 0x0008EB92 File Offset: 0x0008CD92
	public void SetToastDescription(DbfLocValue v)
	{
		this.m_toastDescription = v;
		v.SetDebugInfo(base.ID, "TOAST_DESCRIPTION");
	}

	// Token: 0x06001B73 RID: 7027 RVA: 0x0008EBAC File Offset: 0x0008CDAC
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x06001B74 RID: 7028 RVA: 0x0008EBB8 File Offset: 0x0008CDB8
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2537485753U)
		{
			if (num <= 1458105184U)
			{
				if (num != 351069574U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return base.ID;
						}
					}
				}
				else if (name == "REWARD_COUNT")
				{
					return this.m_rewardCount;
				}
			}
			else if (num != 1599938405U)
			{
				if (num != 1736527389U)
				{
					if (num == 2537485753U)
					{
						if (name == "REWARD_TIMING")
						{
							return this.m_rewardTiming;
						}
					}
				}
				else if (name == "ACTION_ID")
				{
					return this.m_actionId;
				}
			}
			else if (name == "USE_QUEST_TOAST")
			{
				return this.m_useQuestToast;
			}
		}
		else if (num <= 3384680241U)
		{
			if (num != 3022554311U)
			{
				if (num == 3384680241U)
				{
					if (name == "TOAST_DESCRIPTION")
					{
						return this.m_toastDescription;
					}
				}
			}
			else if (name == "NOTE_DESC")
			{
				return this.m_noteDesc;
			}
		}
		else if (num != 3599926738U)
		{
			if (num != 4083666494U)
			{
				if (num == 4214602626U)
				{
					if (name == "SORT_ORDER")
					{
						return this.m_sortOrder;
					}
				}
			}
			else if (name == "TOAST_NAME")
			{
				return this.m_toastName;
			}
		}
		else if (name == "REWARD_ID")
		{
			return this.m_rewardId;
		}
		return null;
	}

	// Token: 0x06001B75 RID: 7029 RVA: 0x0008ED80 File Offset: 0x0008CF80
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2537485753U)
		{
			if (num <= 1458105184U)
			{
				if (num != 351069574U)
				{
					if (num != 1458105184U)
					{
						return;
					}
					if (!(name == "ID"))
					{
						return;
					}
					base.SetID((int)val);
					return;
				}
				else
				{
					if (!(name == "REWARD_COUNT"))
					{
						return;
					}
					this.m_rewardCount = (int)val;
					return;
				}
			}
			else if (num != 1599938405U)
			{
				if (num != 1736527389U)
				{
					if (num != 2537485753U)
					{
						return;
					}
					if (!(name == "REWARD_TIMING"))
					{
						return;
					}
					this.m_rewardTiming = (string)val;
					return;
				}
				else
				{
					if (!(name == "ACTION_ID"))
					{
						return;
					}
					this.m_actionId = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "USE_QUEST_TOAST"))
				{
					return;
				}
				this.m_useQuestToast = (bool)val;
				return;
			}
		}
		else if (num <= 3384680241U)
		{
			if (num != 3022554311U)
			{
				if (num != 3384680241U)
				{
					return;
				}
				if (!(name == "TOAST_DESCRIPTION"))
				{
					return;
				}
				this.m_toastDescription = (DbfLocValue)val;
				return;
			}
			else
			{
				if (!(name == "NOTE_DESC"))
				{
					return;
				}
				this.m_noteDesc = (string)val;
				return;
			}
		}
		else if (num != 3599926738U)
		{
			if (num != 4083666494U)
			{
				if (num != 4214602626U)
				{
					return;
				}
				if (!(name == "SORT_ORDER"))
				{
					return;
				}
				this.m_sortOrder = (int)val;
				return;
			}
			else
			{
				if (!(name == "TOAST_NAME"))
				{
					return;
				}
				this.m_toastName = (DbfLocValue)val;
				return;
			}
		}
		else
		{
			if (!(name == "REWARD_ID"))
			{
				return;
			}
			this.m_rewardId = (int)val;
			return;
		}
	}

	// Token: 0x06001B76 RID: 7030 RVA: 0x0008EF1C File Offset: 0x0008D11C
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2537485753U)
		{
			if (num <= 1458105184U)
			{
				if (num != 351069574U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "REWARD_COUNT")
				{
					return typeof(int);
				}
			}
			else if (num != 1599938405U)
			{
				if (num != 1736527389U)
				{
					if (num == 2537485753U)
					{
						if (name == "REWARD_TIMING")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "ACTION_ID")
				{
					return typeof(int);
				}
			}
			else if (name == "USE_QUEST_TOAST")
			{
				return typeof(bool);
			}
		}
		else if (num <= 3384680241U)
		{
			if (num != 3022554311U)
			{
				if (num == 3384680241U)
				{
					if (name == "TOAST_DESCRIPTION")
					{
						return typeof(DbfLocValue);
					}
				}
			}
			else if (name == "NOTE_DESC")
			{
				return typeof(string);
			}
		}
		else if (num != 3599926738U)
		{
			if (num != 4083666494U)
			{
				if (num == 4214602626U)
				{
					if (name == "SORT_ORDER")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "TOAST_NAME")
			{
				return typeof(DbfLocValue);
			}
		}
		else if (name == "REWARD_ID")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x06001B77 RID: 7031 RVA: 0x0008F0ED File Offset: 0x0008D2ED
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadFixedRewardMapDbfRecords loadRecords = new LoadFixedRewardMapDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001B78 RID: 7032 RVA: 0x0008F104 File Offset: 0x0008D304
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		FixedRewardMapDbfAsset fixedRewardMapDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(FixedRewardMapDbfAsset)) as FixedRewardMapDbfAsset;
		if (fixedRewardMapDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("FixedRewardMapDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < fixedRewardMapDbfAsset.Records.Count; i++)
		{
			fixedRewardMapDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (fixedRewardMapDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001B79 RID: 7033 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001B7A RID: 7034 RVA: 0x0008F183 File Offset: 0x0008D383
	public override void StripUnusedLocales()
	{
		this.m_toastName.StripUnusedLocales();
		this.m_toastDescription.StripUnusedLocales();
	}

	// Token: 0x04001036 RID: 4150
	[SerializeField]
	private int m_actionId;

	// Token: 0x04001037 RID: 4151
	[SerializeField]
	private int m_rewardId;

	// Token: 0x04001038 RID: 4152
	[SerializeField]
	private int m_rewardCount = 1;

	// Token: 0x04001039 RID: 4153
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x0400103A RID: 4154
	[SerializeField]
	private bool m_useQuestToast;

	// Token: 0x0400103B RID: 4155
	[SerializeField]
	private string m_rewardTiming = "immediate";

	// Token: 0x0400103C RID: 4156
	[SerializeField]
	private DbfLocValue m_toastName;

	// Token: 0x0400103D RID: 4157
	[SerializeField]
	private DbfLocValue m_toastDescription;

	// Token: 0x0400103E RID: 4158
	[SerializeField]
	private int m_sortOrder;
}
