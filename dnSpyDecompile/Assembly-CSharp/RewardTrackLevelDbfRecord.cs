using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000262 RID: 610
[Serializable]
public class RewardTrackLevelDbfRecord : DbfRecord
{
	// Token: 0x1700042D RID: 1069
	// (get) Token: 0x06001FB3 RID: 8115 RVA: 0x0009E476 File Offset: 0x0009C676
	[DbfField("REWARD_TRACK_ID")]
	public int RewardTrackId
	{
		get
		{
			return this.m_rewardTrackId;
		}
	}

	// Token: 0x1700042E RID: 1070
	// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x0009E47E File Offset: 0x0009C67E
	[DbfField("LEVEL")]
	public int Level
	{
		get
		{
			return this.m_level;
		}
	}

	// Token: 0x1700042F RID: 1071
	// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x0009E486 File Offset: 0x0009C686
	[DbfField("XP_NEEDED")]
	public int XpNeeded
	{
		get
		{
			return this.m_xpNeeded;
		}
	}

	// Token: 0x17000430 RID: 1072
	// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x0009E48E File Offset: 0x0009C68E
	[DbfField("STYLE_NAME")]
	public string StyleName
	{
		get
		{
			return this.m_styleName;
		}
	}

	// Token: 0x17000431 RID: 1073
	// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x0009E496 File Offset: 0x0009C696
	[DbfField("FREE_REWARD_LIST")]
	public int FreeRewardList
	{
		get
		{
			return this.m_freeRewardListId;
		}
	}

	// Token: 0x17000432 RID: 1074
	// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x0009E49E File Offset: 0x0009C69E
	public RewardListDbfRecord FreeRewardListRecord
	{
		get
		{
			return GameDbf.RewardList.GetRecord(this.m_freeRewardListId);
		}
	}

	// Token: 0x17000433 RID: 1075
	// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x0009E4B0 File Offset: 0x0009C6B0
	[DbfField("PAID_REWARD_LIST")]
	public int PaidRewardList
	{
		get
		{
			return this.m_paidRewardListId;
		}
	}

	// Token: 0x17000434 RID: 1076
	// (get) Token: 0x06001FBA RID: 8122 RVA: 0x0009E4B8 File Offset: 0x0009C6B8
	public RewardListDbfRecord PaidRewardListRecord
	{
		get
		{
			return GameDbf.RewardList.GetRecord(this.m_paidRewardListId);
		}
	}

	// Token: 0x06001FBB RID: 8123 RVA: 0x0009E4CA File Offset: 0x0009C6CA
	public void SetRewardTrackId(int v)
	{
		this.m_rewardTrackId = v;
	}

	// Token: 0x06001FBC RID: 8124 RVA: 0x0009E4D3 File Offset: 0x0009C6D3
	public void SetLevel(int v)
	{
		this.m_level = v;
	}

	// Token: 0x06001FBD RID: 8125 RVA: 0x0009E4DC File Offset: 0x0009C6DC
	public void SetXpNeeded(int v)
	{
		this.m_xpNeeded = v;
	}

	// Token: 0x06001FBE RID: 8126 RVA: 0x0009E4E5 File Offset: 0x0009C6E5
	public void SetStyleName(string v)
	{
		this.m_styleName = v;
	}

	// Token: 0x06001FBF RID: 8127 RVA: 0x0009E4EE File Offset: 0x0009C6EE
	public void SetFreeRewardList(int v)
	{
		this.m_freeRewardListId = v;
	}

	// Token: 0x06001FC0 RID: 8128 RVA: 0x0009E4F7 File Offset: 0x0009C6F7
	public void SetPaidRewardList(int v)
	{
		this.m_paidRewardListId = v;
	}

	// Token: 0x06001FC1 RID: 8129 RVA: 0x0009E500 File Offset: 0x0009C700
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num != 91504271U)
			{
				if (num != 258433776U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return base.ID;
						}
					}
				}
				else if (name == "STYLE_NAME")
				{
					return this.m_styleName;
				}
			}
			else if (name == "XP_NEEDED")
			{
				return this.m_xpNeeded;
			}
		}
		else if (num <= 2932297230U)
		{
			if (num != 2129446653U)
			{
				if (num == 2932297230U)
				{
					if (name == "REWARD_TRACK_ID")
					{
						return this.m_rewardTrackId;
					}
				}
			}
			else if (name == "LEVEL")
			{
				return this.m_level;
			}
		}
		else if (num != 3512786996U)
		{
			if (num == 3839112424U)
			{
				if (name == "PAID_REWARD_LIST")
				{
					return this.m_paidRewardListId;
				}
			}
		}
		else if (name == "FREE_REWARD_LIST")
		{
			return this.m_freeRewardListId;
		}
		return null;
	}

	// Token: 0x06001FC2 RID: 8130 RVA: 0x0009E634 File Offset: 0x0009C834
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num != 91504271U)
			{
				if (num != 258433776U)
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
					if (!(name == "STYLE_NAME"))
					{
						return;
					}
					this.m_styleName = (string)val;
					return;
				}
			}
			else
			{
				if (!(name == "XP_NEEDED"))
				{
					return;
				}
				this.m_xpNeeded = (int)val;
				return;
			}
		}
		else if (num <= 2932297230U)
		{
			if (num != 2129446653U)
			{
				if (num != 2932297230U)
				{
					return;
				}
				if (!(name == "REWARD_TRACK_ID"))
				{
					return;
				}
				this.m_rewardTrackId = (int)val;
				return;
			}
			else
			{
				if (!(name == "LEVEL"))
				{
					return;
				}
				this.m_level = (int)val;
				return;
			}
		}
		else if (num != 3512786996U)
		{
			if (num != 3839112424U)
			{
				return;
			}
			if (!(name == "PAID_REWARD_LIST"))
			{
				return;
			}
			this.m_paidRewardListId = (int)val;
			return;
		}
		else
		{
			if (!(name == "FREE_REWARD_LIST"))
			{
				return;
			}
			this.m_freeRewardListId = (int)val;
			return;
		}
	}

	// Token: 0x06001FC3 RID: 8131 RVA: 0x0009E750 File Offset: 0x0009C950
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num != 91504271U)
			{
				if (num != 258433776U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "STYLE_NAME")
				{
					return typeof(string);
				}
			}
			else if (name == "XP_NEEDED")
			{
				return typeof(int);
			}
		}
		else if (num <= 2932297230U)
		{
			if (num != 2129446653U)
			{
				if (num == 2932297230U)
				{
					if (name == "REWARD_TRACK_ID")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "LEVEL")
			{
				return typeof(int);
			}
		}
		else if (num != 3512786996U)
		{
			if (num == 3839112424U)
			{
				if (name == "PAID_REWARD_LIST")
				{
					return typeof(int);
				}
			}
		}
		else if (name == "FREE_REWARD_LIST")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x06001FC4 RID: 8132 RVA: 0x0009E881 File Offset: 0x0009CA81
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRewardTrackLevelDbfRecords loadRecords = new LoadRewardTrackLevelDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001FC5 RID: 8133 RVA: 0x0009E898 File Offset: 0x0009CA98
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RewardTrackLevelDbfAsset rewardTrackLevelDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RewardTrackLevelDbfAsset)) as RewardTrackLevelDbfAsset;
		if (rewardTrackLevelDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("RewardTrackLevelDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < rewardTrackLevelDbfAsset.Records.Count; i++)
		{
			rewardTrackLevelDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (rewardTrackLevelDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001FC6 RID: 8134 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001FC7 RID: 8135 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x040011FF RID: 4607
	[SerializeField]
	private int m_rewardTrackId;

	// Token: 0x04001200 RID: 4608
	[SerializeField]
	private int m_level;

	// Token: 0x04001201 RID: 4609
	[SerializeField]
	private int m_xpNeeded;

	// Token: 0x04001202 RID: 4610
	[SerializeField]
	private string m_styleName;

	// Token: 0x04001203 RID: 4611
	[SerializeField]
	private int m_freeRewardListId;

	// Token: 0x04001204 RID: 4612
	[SerializeField]
	private int m_paidRewardListId;
}
