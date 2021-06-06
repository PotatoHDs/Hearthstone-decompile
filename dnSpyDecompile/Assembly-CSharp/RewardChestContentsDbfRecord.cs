using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000250 RID: 592
[Serializable]
public class RewardChestContentsDbfRecord : DbfRecord
{
	// Token: 0x170003FC RID: 1020
	// (get) Token: 0x06001F12 RID: 7954 RVA: 0x0009C486 File Offset: 0x0009A686
	[DbfField("REWARD_CHEST_ID")]
	public int RewardChestId
	{
		get
		{
			return this.m_rewardChestId;
		}
	}

	// Token: 0x170003FD RID: 1021
	// (get) Token: 0x06001F13 RID: 7955 RVA: 0x0009C48E File Offset: 0x0009A68E
	public RewardChestDbfRecord RewardChestRecord
	{
		get
		{
			return GameDbf.RewardChest.GetRecord(this.m_rewardChestId);
		}
	}

	// Token: 0x170003FE RID: 1022
	// (get) Token: 0x06001F14 RID: 7956 RVA: 0x0009C4A0 File Offset: 0x0009A6A0
	[DbfField("REWARD_LEVEL")]
	public int RewardLevel
	{
		get
		{
			return this.m_rewardLevel;
		}
	}

	// Token: 0x170003FF RID: 1023
	// (get) Token: 0x06001F15 RID: 7957 RVA: 0x0009C4A8 File Offset: 0x0009A6A8
	[DbfField("BAG1")]
	public int Bag1
	{
		get
		{
			return this.m_bag1;
		}
	}

	// Token: 0x17000400 RID: 1024
	// (get) Token: 0x06001F16 RID: 7958 RVA: 0x0009C4B0 File Offset: 0x0009A6B0
	[DbfField("BAG2")]
	public int Bag2
	{
		get
		{
			return this.m_bag2;
		}
	}

	// Token: 0x17000401 RID: 1025
	// (get) Token: 0x06001F17 RID: 7959 RVA: 0x0009C4B8 File Offset: 0x0009A6B8
	[DbfField("BAG3")]
	public int Bag3
	{
		get
		{
			return this.m_bag3;
		}
	}

	// Token: 0x17000402 RID: 1026
	// (get) Token: 0x06001F18 RID: 7960 RVA: 0x0009C4C0 File Offset: 0x0009A6C0
	[DbfField("BAG4")]
	public int Bag4
	{
		get
		{
			return this.m_bag4;
		}
	}

	// Token: 0x17000403 RID: 1027
	// (get) Token: 0x06001F19 RID: 7961 RVA: 0x0009C4C8 File Offset: 0x0009A6C8
	[DbfField("BAG5")]
	public int Bag5
	{
		get
		{
			return this.m_bag5;
		}
	}

	// Token: 0x17000404 RID: 1028
	// (get) Token: 0x06001F1A RID: 7962 RVA: 0x0009C4D0 File Offset: 0x0009A6D0
	[DbfField("BAG6")]
	public int Bag6
	{
		get
		{
			return this.m_bag6;
		}
	}

	// Token: 0x17000405 RID: 1029
	// (get) Token: 0x06001F1B RID: 7963 RVA: 0x0009C4D8 File Offset: 0x0009A6D8
	[DbfField("ICON_TEXTURE")]
	public string IconTexture
	{
		get
		{
			return this.m_iconTexture;
		}
	}

	// Token: 0x17000406 RID: 1030
	// (get) Token: 0x06001F1C RID: 7964 RVA: 0x0009C4E0 File Offset: 0x0009A6E0
	[DbfField("ICON_OFFSET_X")]
	public double IconOffsetX
	{
		get
		{
			return this.m_iconOffsetX;
		}
	}

	// Token: 0x17000407 RID: 1031
	// (get) Token: 0x06001F1D RID: 7965 RVA: 0x0009C4E8 File Offset: 0x0009A6E8
	[DbfField("ICON_OFFSET_Y")]
	public double IconOffsetY
	{
		get
		{
			return this.m_iconOffsetY;
		}
	}

	// Token: 0x06001F1E RID: 7966 RVA: 0x0009C4F0 File Offset: 0x0009A6F0
	public void SetRewardChestId(int v)
	{
		this.m_rewardChestId = v;
	}

	// Token: 0x06001F1F RID: 7967 RVA: 0x0009C4F9 File Offset: 0x0009A6F9
	public void SetRewardLevel(int v)
	{
		this.m_rewardLevel = v;
	}

	// Token: 0x06001F20 RID: 7968 RVA: 0x0009C502 File Offset: 0x0009A702
	public void SetBag1(int v)
	{
		this.m_bag1 = v;
	}

	// Token: 0x06001F21 RID: 7969 RVA: 0x0009C50B File Offset: 0x0009A70B
	public void SetBag2(int v)
	{
		this.m_bag2 = v;
	}

	// Token: 0x06001F22 RID: 7970 RVA: 0x0009C514 File Offset: 0x0009A714
	public void SetBag3(int v)
	{
		this.m_bag3 = v;
	}

	// Token: 0x06001F23 RID: 7971 RVA: 0x0009C51D File Offset: 0x0009A71D
	public void SetBag4(int v)
	{
		this.m_bag4 = v;
	}

	// Token: 0x06001F24 RID: 7972 RVA: 0x0009C526 File Offset: 0x0009A726
	public void SetBag5(int v)
	{
		this.m_bag5 = v;
	}

	// Token: 0x06001F25 RID: 7973 RVA: 0x0009C52F File Offset: 0x0009A72F
	public void SetBag6(int v)
	{
		this.m_bag6 = v;
	}

	// Token: 0x06001F26 RID: 7974 RVA: 0x0009C538 File Offset: 0x0009A738
	public void SetIconTexture(string v)
	{
		this.m_iconTexture = v;
	}

	// Token: 0x06001F27 RID: 7975 RVA: 0x0009C541 File Offset: 0x0009A741
	public void SetIconOffsetX(double v)
	{
		this.m_iconOffsetX = v;
	}

	// Token: 0x06001F28 RID: 7976 RVA: 0x0009C54A File Offset: 0x0009A74A
	public void SetIconOffsetY(double v)
	{
		this.m_iconOffsetY = v;
	}

	// Token: 0x06001F29 RID: 7977 RVA: 0x0009C554 File Offset: 0x0009A754
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 3309372896U)
		{
			if (num <= 1458105184U)
			{
				if (num != 697381708U)
				{
					if (num != 807866572U)
					{
						if (num == 1458105184U)
						{
							if (name == "ID")
							{
								return base.ID;
							}
						}
					}
					else if (name == "REWARD_CHEST_ID")
					{
						return this.m_rewardChestId;
					}
				}
				else if (name == "ICON_TEXTURE")
				{
					return this.m_iconTexture;
				}
			}
			else if (num != 2911217546U)
			{
				if (num != 2927995165U)
				{
					if (num == 3309372896U)
					{
						if (name == "BAG1")
						{
							return this.m_bag1;
						}
					}
				}
				else if (name == "ICON_OFFSET_X")
				{
					return this.m_iconOffsetX;
				}
			}
			else if (name == "ICON_OFFSET_Y")
			{
				return this.m_iconOffsetY;
			}
		}
		else if (num <= 3376483372U)
		{
			if (num != 3342928134U)
			{
				if (num != 3359705753U)
				{
					if (num == 3376483372U)
					{
						if (name == "BAG5")
						{
							return this.m_bag5;
						}
					}
				}
				else if (name == "BAG2")
				{
					return this.m_bag2;
				}
			}
			else if (name == "BAG3")
			{
				return this.m_bag3;
			}
		}
		else if (num != 3393260991U)
		{
			if (num != 3426816229U)
			{
				if (num == 3456804607U)
				{
					if (name == "REWARD_LEVEL")
					{
						return this.m_rewardLevel;
					}
				}
			}
			else if (name == "BAG6")
			{
				return this.m_bag6;
			}
		}
		else if (name == "BAG4")
		{
			return this.m_bag4;
		}
		return null;
	}

	// Token: 0x06001F2A RID: 7978 RVA: 0x0009C79C File Offset: 0x0009A99C
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 3309372896U)
		{
			if (num <= 1458105184U)
			{
				if (num != 697381708U)
				{
					if (num != 807866572U)
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
						if (!(name == "REWARD_CHEST_ID"))
						{
							return;
						}
						this.m_rewardChestId = (int)val;
						return;
					}
				}
				else
				{
					if (!(name == "ICON_TEXTURE"))
					{
						return;
					}
					this.m_iconTexture = (string)val;
					return;
				}
			}
			else if (num != 2911217546U)
			{
				if (num != 2927995165U)
				{
					if (num != 3309372896U)
					{
						return;
					}
					if (!(name == "BAG1"))
					{
						return;
					}
					this.m_bag1 = (int)val;
					return;
				}
				else
				{
					if (!(name == "ICON_OFFSET_X"))
					{
						return;
					}
					this.m_iconOffsetX = (double)val;
					return;
				}
			}
			else
			{
				if (!(name == "ICON_OFFSET_Y"))
				{
					return;
				}
				this.m_iconOffsetY = (double)val;
				return;
			}
		}
		else if (num <= 3376483372U)
		{
			if (num != 3342928134U)
			{
				if (num != 3359705753U)
				{
					if (num != 3376483372U)
					{
						return;
					}
					if (!(name == "BAG5"))
					{
						return;
					}
					this.m_bag5 = (int)val;
					return;
				}
				else
				{
					if (!(name == "BAG2"))
					{
						return;
					}
					this.m_bag2 = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "BAG3"))
				{
					return;
				}
				this.m_bag3 = (int)val;
				return;
			}
		}
		else if (num != 3393260991U)
		{
			if (num != 3426816229U)
			{
				if (num != 3456804607U)
				{
					return;
				}
				if (!(name == "REWARD_LEVEL"))
				{
					return;
				}
				this.m_rewardLevel = (int)val;
				return;
			}
			else
			{
				if (!(name == "BAG6"))
				{
					return;
				}
				this.m_bag6 = (int)val;
				return;
			}
		}
		else
		{
			if (!(name == "BAG4"))
			{
				return;
			}
			this.m_bag4 = (int)val;
			return;
		}
	}

	// Token: 0x06001F2B RID: 7979 RVA: 0x0009C9A8 File Offset: 0x0009ABA8
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 3309372896U)
		{
			if (num <= 1458105184U)
			{
				if (num != 697381708U)
				{
					if (num != 807866572U)
					{
						if (num == 1458105184U)
						{
							if (name == "ID")
							{
								return typeof(int);
							}
						}
					}
					else if (name == "REWARD_CHEST_ID")
					{
						return typeof(int);
					}
				}
				else if (name == "ICON_TEXTURE")
				{
					return typeof(string);
				}
			}
			else if (num != 2911217546U)
			{
				if (num != 2927995165U)
				{
					if (num == 3309372896U)
					{
						if (name == "BAG1")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "ICON_OFFSET_X")
				{
					return typeof(double);
				}
			}
			else if (name == "ICON_OFFSET_Y")
			{
				return typeof(double);
			}
		}
		else if (num <= 3376483372U)
		{
			if (num != 3342928134U)
			{
				if (num != 3359705753U)
				{
					if (num == 3376483372U)
					{
						if (name == "BAG5")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "BAG2")
				{
					return typeof(int);
				}
			}
			else if (name == "BAG3")
			{
				return typeof(int);
			}
		}
		else if (num != 3393260991U)
		{
			if (num != 3426816229U)
			{
				if (num == 3456804607U)
				{
					if (name == "REWARD_LEVEL")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "BAG6")
			{
				return typeof(int);
			}
		}
		else if (name == "BAG4")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x06001F2C RID: 7980 RVA: 0x0009CBE4 File Offset: 0x0009ADE4
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRewardChestContentsDbfRecords loadRecords = new LoadRewardChestContentsDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001F2D RID: 7981 RVA: 0x0009CBFC File Offset: 0x0009ADFC
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RewardChestContentsDbfAsset rewardChestContentsDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RewardChestContentsDbfAsset)) as RewardChestContentsDbfAsset;
		if (rewardChestContentsDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("RewardChestContentsDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < rewardChestContentsDbfAsset.Records.Count; i++)
		{
			rewardChestContentsDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (rewardChestContentsDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001F2E RID: 7982 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001F2F RID: 7983 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x040011CE RID: 4558
	[SerializeField]
	private int m_rewardChestId;

	// Token: 0x040011CF RID: 4559
	[SerializeField]
	private int m_rewardLevel;

	// Token: 0x040011D0 RID: 4560
	[SerializeField]
	private int m_bag1;

	// Token: 0x040011D1 RID: 4561
	[SerializeField]
	private int m_bag2;

	// Token: 0x040011D2 RID: 4562
	[SerializeField]
	private int m_bag3;

	// Token: 0x040011D3 RID: 4563
	[SerializeField]
	private int m_bag4;

	// Token: 0x040011D4 RID: 4564
	[SerializeField]
	private int m_bag5;

	// Token: 0x040011D5 RID: 4565
	[SerializeField]
	private int m_bag6;

	// Token: 0x040011D6 RID: 4566
	[SerializeField]
	private string m_iconTexture;

	// Token: 0x040011D7 RID: 4567
	[SerializeField]
	private double m_iconOffsetX;

	// Token: 0x040011D8 RID: 4568
	[SerializeField]
	private double m_iconOffsetY;
}
