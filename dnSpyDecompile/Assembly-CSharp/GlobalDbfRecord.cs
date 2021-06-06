using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001F3 RID: 499
[Serializable]
public class GlobalDbfRecord : DbfRecord
{
	// Token: 0x1700030C RID: 780
	// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x00091456 File Offset: 0x0008F656
	[DbfField("ASSET_FLAGS")]
	public Global.AssetFlags AssetFlags
	{
		get
		{
			return this.m_assetFlags;
		}
	}

	// Token: 0x1700030D RID: 781
	// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x0009145E File Offset: 0x0008F65E
	[DbfField("PRESENCE_STATUS")]
	public Global.PresenceStatus PresenceStatus
	{
		get
		{
			return this.m_presenceStatus;
		}
	}

	// Token: 0x1700030E RID: 782
	// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x00091466 File Offset: 0x0008F666
	[DbfField("REGION")]
	public Global.Region Region
	{
		get
		{
			return this.m_region;
		}
	}

	// Token: 0x1700030F RID: 783
	// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x0009146E File Offset: 0x0008F66E
	[DbfField("FORMAT_TYPE")]
	public Global.FormatType FormatType
	{
		get
		{
			return this.m_formatType;
		}
	}

	// Token: 0x17000310 RID: 784
	// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x00091476 File Offset: 0x0008F676
	[DbfField("REWARD_TYPE")]
	public Global.RewardType RewardType
	{
		get
		{
			return this.m_rewardType;
		}
	}

	// Token: 0x17000311 RID: 785
	// (get) Token: 0x06001BDA RID: 7130 RVA: 0x0009147E File Offset: 0x0008F67E
	[DbfField("CARD_PREMIUM_LEVEL")]
	public Global.CardPremiumLevel CardPremiumLevel
	{
		get
		{
			return this.m_cardPremiumLevel;
		}
	}

	// Token: 0x17000312 RID: 786
	// (get) Token: 0x06001BDB RID: 7131 RVA: 0x00091486 File Offset: 0x0008F686
	[DbfField("BNET_GAME_TYPE")]
	public Global.BnetGameType BnetGameType
	{
		get
		{
			return this.m_bnetGameType;
		}
	}

	// Token: 0x17000313 RID: 787
	// (get) Token: 0x06001BDC RID: 7132 RVA: 0x0009148E File Offset: 0x0008F68E
	[DbfField("SOUND_CATEGORY")]
	public Global.SoundCategory SoundCategory
	{
		get
		{
			return this.m_soundCategory;
		}
	}

	// Token: 0x17000314 RID: 788
	// (get) Token: 0x06001BDD RID: 7133 RVA: 0x00091496 File Offset: 0x0008F696
	[DbfField("GAME_STRING_CATEGORY")]
	public Global.GameStringCategory GameStringCategory
	{
		get
		{
			return this.m_gameStringCategory;
		}
	}

	// Token: 0x06001BDE RID: 7134 RVA: 0x0009149E File Offset: 0x0008F69E
	public void SetAssetFlags(Global.AssetFlags v)
	{
		this.m_assetFlags = v;
	}

	// Token: 0x06001BDF RID: 7135 RVA: 0x000914A7 File Offset: 0x0008F6A7
	public void SetPresenceStatus(Global.PresenceStatus v)
	{
		this.m_presenceStatus = v;
	}

	// Token: 0x06001BE0 RID: 7136 RVA: 0x000914B0 File Offset: 0x0008F6B0
	public void SetRegion(Global.Region v)
	{
		this.m_region = v;
	}

	// Token: 0x06001BE1 RID: 7137 RVA: 0x000914B9 File Offset: 0x0008F6B9
	public void SetFormatType(Global.FormatType v)
	{
		this.m_formatType = v;
	}

	// Token: 0x06001BE2 RID: 7138 RVA: 0x000914C2 File Offset: 0x0008F6C2
	public void SetRewardType(Global.RewardType v)
	{
		this.m_rewardType = v;
	}

	// Token: 0x06001BE3 RID: 7139 RVA: 0x000914CB File Offset: 0x0008F6CB
	public void SetCardPremiumLevel(Global.CardPremiumLevel v)
	{
		this.m_cardPremiumLevel = v;
	}

	// Token: 0x06001BE4 RID: 7140 RVA: 0x000914D4 File Offset: 0x0008F6D4
	public void SetBnetGameType(Global.BnetGameType v)
	{
		this.m_bnetGameType = v;
	}

	// Token: 0x06001BE5 RID: 7141 RVA: 0x000914DD File Offset: 0x0008F6DD
	public void SetSoundCategory(Global.SoundCategory v)
	{
		this.m_soundCategory = v;
	}

	// Token: 0x06001BE6 RID: 7142 RVA: 0x000914E6 File Offset: 0x0008F6E6
	public void SetGameStringCategory(Global.GameStringCategory v)
	{
		this.m_gameStringCategory = v;
	}

	// Token: 0x06001BE7 RID: 7143 RVA: 0x000914F0 File Offset: 0x0008F6F0
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1098446823U)
		{
			if (num <= 393858368U)
			{
				if (num != 96691247U)
				{
					if (num == 393858368U)
					{
						if (name == "BNET_GAME_TYPE")
						{
							return this.m_bnetGameType;
						}
					}
				}
				else if (name == "FORMAT_TYPE")
				{
					return this.m_formatType;
				}
			}
			else if (num != 1019332525U)
			{
				if (num == 1098446823U)
				{
					if (name == "REWARD_TYPE")
					{
						return this.m_rewardType;
					}
				}
			}
			else if (name == "SOUND_CATEGORY")
			{
				return this.m_soundCategory;
			}
		}
		else if (num <= 1615316160U)
		{
			if (num != 1399452099U)
			{
				if (num == 1615316160U)
				{
					if (name == "CARD_PREMIUM_LEVEL")
					{
						return this.m_cardPremiumLevel;
					}
				}
			}
			else if (name == "PRESENCE_STATUS")
			{
				return this.m_presenceStatus;
			}
		}
		else if (num != 2674204159U)
		{
			if (num != 2906194274U)
			{
				if (num == 3781468093U)
				{
					if (name == "REGION")
					{
						return this.m_region;
					}
				}
			}
			else if (name == "GAME_STRING_CATEGORY")
			{
				return this.m_gameStringCategory;
			}
		}
		else if (name == "ASSET_FLAGS")
		{
			return this.m_assetFlags;
		}
		return null;
	}

	// Token: 0x06001BE8 RID: 7144 RVA: 0x0009169C File Offset: 0x0008F89C
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1098446823U)
		{
			if (num <= 393858368U)
			{
				if (num != 96691247U)
				{
					if (num != 393858368U)
					{
						return;
					}
					if (!(name == "BNET_GAME_TYPE"))
					{
						return;
					}
					if (val == null)
					{
						this.m_bnetGameType = Global.BnetGameType.BGT_UNKNOWN;
						return;
					}
					if (val is Global.BnetGameType || val is int)
					{
						this.m_bnetGameType = (Global.BnetGameType)val;
						return;
					}
					if (val is string)
					{
						this.m_bnetGameType = Global.ParseBnetGameTypeValue((string)val);
						return;
					}
				}
				else
				{
					if (!(name == "FORMAT_TYPE"))
					{
						return;
					}
					if (val == null)
					{
						this.m_formatType = Global.FormatType.FT_UNKNOWN;
						return;
					}
					if (val is Global.FormatType || val is int)
					{
						this.m_formatType = (Global.FormatType)val;
						return;
					}
					if (val is string)
					{
						this.m_formatType = Global.ParseFormatTypeValue((string)val);
						return;
					}
				}
			}
			else if (num != 1019332525U)
			{
				if (num != 1098446823U)
				{
					return;
				}
				if (!(name == "REWARD_TYPE"))
				{
					return;
				}
				if (val == null)
				{
					this.m_rewardType = Global.RewardType.NONE;
					return;
				}
				if (val is Global.RewardType || val is int)
				{
					this.m_rewardType = (Global.RewardType)val;
					return;
				}
				if (val is string)
				{
					this.m_rewardType = Global.ParseRewardTypeValue((string)val);
					return;
				}
			}
			else
			{
				if (!(name == "SOUND_CATEGORY"))
				{
					return;
				}
				if (val == null)
				{
					this.m_soundCategory = Global.SoundCategory.NONE;
					return;
				}
				if (val is Global.SoundCategory || val is int)
				{
					this.m_soundCategory = (Global.SoundCategory)val;
					return;
				}
				if (val is string)
				{
					this.m_soundCategory = Global.ParseSoundCategoryValue((string)val);
					return;
				}
			}
		}
		else if (num <= 1615316160U)
		{
			if (num != 1399452099U)
			{
				if (num != 1615316160U)
				{
					return;
				}
				if (!(name == "CARD_PREMIUM_LEVEL"))
				{
					return;
				}
				if (val == null)
				{
					this.m_cardPremiumLevel = Global.CardPremiumLevel.NORMAL;
					return;
				}
				if (val is Global.CardPremiumLevel || val is int)
				{
					this.m_cardPremiumLevel = (Global.CardPremiumLevel)val;
					return;
				}
				if (val is string)
				{
					this.m_cardPremiumLevel = Global.ParseCardPremiumLevelValue((string)val);
					return;
				}
			}
			else
			{
				if (!(name == "PRESENCE_STATUS"))
				{
					return;
				}
				if (val == null)
				{
					this.m_presenceStatus = Global.PresenceStatus.LOGIN;
					return;
				}
				if (val is Global.PresenceStatus || val is int)
				{
					this.m_presenceStatus = (Global.PresenceStatus)val;
					return;
				}
				if (val is string)
				{
					this.m_presenceStatus = Global.ParsePresenceStatusValue((string)val);
					return;
				}
			}
		}
		else if (num != 2674204159U)
		{
			if (num != 2906194274U)
			{
				if (num != 3781468093U)
				{
					return;
				}
				if (!(name == "REGION"))
				{
					return;
				}
				if (val == null)
				{
					this.m_region = Global.Region.REGION_UNKNOWN;
					return;
				}
				if (val is Global.Region || val is int)
				{
					this.m_region = (Global.Region)val;
					return;
				}
				if (val is string)
				{
					this.m_region = Global.ParseRegionValue((string)val);
					return;
				}
			}
			else
			{
				if (!(name == "GAME_STRING_CATEGORY"))
				{
					return;
				}
				if (val == null)
				{
					this.m_gameStringCategory = Global.GameStringCategory.INVALID;
					return;
				}
				if (val is Global.GameStringCategory || val is int)
				{
					this.m_gameStringCategory = (Global.GameStringCategory)val;
					return;
				}
				if (val is string)
				{
					this.m_gameStringCategory = Global.ParseGameStringCategoryValue((string)val);
				}
			}
		}
		else
		{
			if (!(name == "ASSET_FLAGS"))
			{
				return;
			}
			if (val == null)
			{
				this.m_assetFlags = Global.AssetFlags.NONE;
				return;
			}
			if (val is Global.AssetFlags || val is int)
			{
				this.m_assetFlags = (Global.AssetFlags)val;
				return;
			}
			if (val is string)
			{
				this.m_assetFlags = Global.ParseAssetFlagsValue((string)val);
				return;
			}
		}
	}

	// Token: 0x06001BE9 RID: 7145 RVA: 0x00091A20 File Offset: 0x0008FC20
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1098446823U)
		{
			if (num <= 393858368U)
			{
				if (num != 96691247U)
				{
					if (num == 393858368U)
					{
						if (name == "BNET_GAME_TYPE")
						{
							return typeof(Global.BnetGameType);
						}
					}
				}
				else if (name == "FORMAT_TYPE")
				{
					return typeof(Global.FormatType);
				}
			}
			else if (num != 1019332525U)
			{
				if (num == 1098446823U)
				{
					if (name == "REWARD_TYPE")
					{
						return typeof(Global.RewardType);
					}
				}
			}
			else if (name == "SOUND_CATEGORY")
			{
				return typeof(Global.SoundCategory);
			}
		}
		else if (num <= 1615316160U)
		{
			if (num != 1399452099U)
			{
				if (num == 1615316160U)
				{
					if (name == "CARD_PREMIUM_LEVEL")
					{
						return typeof(Global.CardPremiumLevel);
					}
				}
			}
			else if (name == "PRESENCE_STATUS")
			{
				return typeof(Global.PresenceStatus);
			}
		}
		else if (num != 2674204159U)
		{
			if (num != 2906194274U)
			{
				if (num == 3781468093U)
				{
					if (name == "REGION")
					{
						return typeof(Global.Region);
					}
				}
			}
			else if (name == "GAME_STRING_CATEGORY")
			{
				return typeof(Global.GameStringCategory);
			}
		}
		else if (name == "ASSET_FLAGS")
		{
			return typeof(Global.AssetFlags);
		}
		return null;
	}

	// Token: 0x06001BEA RID: 7146 RVA: 0x00091BC3 File Offset: 0x0008FDC3
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadGlobalDbfRecords loadRecords = new LoadGlobalDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001BEB RID: 7147 RVA: 0x00091BDC File Offset: 0x0008FDDC
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		GlobalDbfAsset globalDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(GlobalDbfAsset)) as GlobalDbfAsset;
		if (globalDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("GlobalDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < globalDbfAsset.Records.Count; i++)
		{
			globalDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (globalDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001BEC RID: 7148 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001BED RID: 7149 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x040010C1 RID: 4289
	[SerializeField]
	private Global.AssetFlags m_assetFlags = Global.AssetFlags.NOT_PACKAGED_IN_CLIENT;

	// Token: 0x040010C2 RID: 4290
	[SerializeField]
	private Global.PresenceStatus m_presenceStatus;

	// Token: 0x040010C3 RID: 4291
	[SerializeField]
	private Global.Region m_region;

	// Token: 0x040010C4 RID: 4292
	[SerializeField]
	private Global.FormatType m_formatType;

	// Token: 0x040010C5 RID: 4293
	[SerializeField]
	private Global.RewardType m_rewardType;

	// Token: 0x040010C6 RID: 4294
	[SerializeField]
	private Global.CardPremiumLevel m_cardPremiumLevel;

	// Token: 0x040010C7 RID: 4295
	[SerializeField]
	private Global.BnetGameType m_bnetGameType;

	// Token: 0x040010C8 RID: 4296
	[SerializeField]
	private Global.SoundCategory m_soundCategory;

	// Token: 0x040010C9 RID: 4297
	[SerializeField]
	private Global.GameStringCategory m_gameStringCategory;
}
