using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000256 RID: 598
[Serializable]
public class RewardItemDbfRecord : DbfRecord
{
	// Token: 0x1700040C RID: 1036
	// (get) Token: 0x06001F49 RID: 8009 RVA: 0x0009D05E File Offset: 0x0009B25E
	[DbfField("REWARD_LIST_ID")]
	public int RewardListId
	{
		get
		{
			return this.m_rewardListId;
		}
	}

	// Token: 0x1700040D RID: 1037
	// (get) Token: 0x06001F4A RID: 8010 RVA: 0x0009D066 File Offset: 0x0009B266
	[DbfField("REWARD_TYPE")]
	public RewardItem.RewardType RewardType
	{
		get
		{
			return this.m_rewardType;
		}
	}

	// Token: 0x1700040E RID: 1038
	// (get) Token: 0x06001F4B RID: 8011 RVA: 0x0009D06E File Offset: 0x0009B26E
	[DbfField("QUANTITY")]
	public int Quantity
	{
		get
		{
			return this.m_quantity;
		}
	}

	// Token: 0x1700040F RID: 1039
	// (get) Token: 0x06001F4C RID: 8012 RVA: 0x0009D076 File Offset: 0x0009B276
	[DbfField("CARD")]
	public int Card
	{
		get
		{
			return this.m_cardId;
		}
	}

	// Token: 0x17000410 RID: 1040
	// (get) Token: 0x06001F4D RID: 8013 RVA: 0x0009D07E File Offset: 0x0009B27E
	public CardDbfRecord CardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_cardId);
		}
	}

	// Token: 0x17000411 RID: 1041
	// (get) Token: 0x06001F4E RID: 8014 RVA: 0x0009D090 File Offset: 0x0009B290
	[DbfField("CARD_PREMIUM_LEVEL")]
	public RewardItem.CardPremiumLevel CardPremiumLevel
	{
		get
		{
			return this.m_cardPremiumLevel;
		}
	}

	// Token: 0x17000412 RID: 1042
	// (get) Token: 0x06001F4F RID: 8015 RVA: 0x0009D098 File Offset: 0x0009B298
	[DbfField("RANDOM_CARD_BOOSTER_CARD_SET")]
	public int RandomCardBoosterCardSet
	{
		get
		{
			return this.m_randomCardBoosterCardSetId;
		}
	}

	// Token: 0x17000413 RID: 1043
	// (get) Token: 0x06001F50 RID: 8016 RVA: 0x0009D0A0 File Offset: 0x0009B2A0
	public BoosterCardSetDbfRecord RandomCardBoosterCardSetRecord
	{
		get
		{
			return GameDbf.BoosterCardSet.GetRecord(this.m_randomCardBoosterCardSetId);
		}
	}

	// Token: 0x17000414 RID: 1044
	// (get) Token: 0x06001F51 RID: 8017 RVA: 0x0009D0B2 File Offset: 0x0009B2B2
	[DbfField("BOOSTER")]
	public int Booster
	{
		get
		{
			return this.m_boosterId;
		}
	}

	// Token: 0x17000415 RID: 1045
	// (get) Token: 0x06001F52 RID: 8018 RVA: 0x0009D0BA File Offset: 0x0009B2BA
	public BoosterDbfRecord BoosterRecord
	{
		get
		{
			return GameDbf.Booster.GetRecord(this.m_boosterId);
		}
	}

	// Token: 0x17000416 RID: 1046
	// (get) Token: 0x06001F53 RID: 8019 RVA: 0x0009D0CC File Offset: 0x0009B2CC
	[DbfField("BOOSTER_SELECTOR")]
	public RewardItem.BoosterSelector BoosterSelector
	{
		get
		{
			return this.m_boosterSelector;
		}
	}

	// Token: 0x17000417 RID: 1047
	// (get) Token: 0x06001F54 RID: 8020 RVA: 0x0009D0D4 File Offset: 0x0009B2D4
	[DbfField("CARD_BACK")]
	public int CardBack
	{
		get
		{
			return this.m_cardBackId;
		}
	}

	// Token: 0x17000418 RID: 1048
	// (get) Token: 0x06001F55 RID: 8021 RVA: 0x0009D0DC File Offset: 0x0009B2DC
	public CardBackDbfRecord CardBackRecord
	{
		get
		{
			return GameDbf.CardBack.GetRecord(this.m_cardBackId);
		}
	}

	// Token: 0x17000419 RID: 1049
	// (get) Token: 0x06001F56 RID: 8022 RVA: 0x0009D0EE File Offset: 0x0009B2EE
	[DbfField("CUSTOM_COIN")]
	public int CustomCoin
	{
		get
		{
			return this.m_customCoinId;
		}
	}

	// Token: 0x1700041A RID: 1050
	// (get) Token: 0x06001F57 RID: 8023 RVA: 0x0009D0F6 File Offset: 0x0009B2F6
	public CoinDbfRecord CustomCoinRecord
	{
		get
		{
			return GameDbf.Coin.GetRecord(this.m_customCoinId);
		}
	}

	// Token: 0x1700041B RID: 1051
	// (get) Token: 0x06001F58 RID: 8024 RVA: 0x0009D108 File Offset: 0x0009B308
	[DbfField("SUBSET_ID")]
	public int SubsetId
	{
		get
		{
			return this.m_subsetId;
		}
	}

	// Token: 0x1700041C RID: 1052
	// (get) Token: 0x06001F59 RID: 8025 RVA: 0x0009D110 File Offset: 0x0009B310
	public SubsetDbfRecord SubsetRecord
	{
		get
		{
			return GameDbf.Subset.GetRecord(this.m_subsetId);
		}
	}

	// Token: 0x1700041D RID: 1053
	// (get) Token: 0x06001F5A RID: 8026 RVA: 0x0009D122 File Offset: 0x0009B322
	[DbfField("IS_VIRTUAL")]
	public bool IsVirtual
	{
		get
		{
			return this.m_isVirtual;
		}
	}

	// Token: 0x06001F5B RID: 8027 RVA: 0x0009D12A File Offset: 0x0009B32A
	public void SetRewardListId(int v)
	{
		this.m_rewardListId = v;
	}

	// Token: 0x06001F5C RID: 8028 RVA: 0x0009D133 File Offset: 0x0009B333
	public void SetRewardType(RewardItem.RewardType v)
	{
		this.m_rewardType = v;
	}

	// Token: 0x06001F5D RID: 8029 RVA: 0x0009D13C File Offset: 0x0009B33C
	public void SetQuantity(int v)
	{
		this.m_quantity = v;
	}

	// Token: 0x06001F5E RID: 8030 RVA: 0x0009D145 File Offset: 0x0009B345
	public void SetCard(int v)
	{
		this.m_cardId = v;
	}

	// Token: 0x06001F5F RID: 8031 RVA: 0x0009D14E File Offset: 0x0009B34E
	public void SetCardPremiumLevel(RewardItem.CardPremiumLevel v)
	{
		this.m_cardPremiumLevel = v;
	}

	// Token: 0x06001F60 RID: 8032 RVA: 0x0009D157 File Offset: 0x0009B357
	public void SetRandomCardBoosterCardSet(int v)
	{
		this.m_randomCardBoosterCardSetId = v;
	}

	// Token: 0x06001F61 RID: 8033 RVA: 0x0009D160 File Offset: 0x0009B360
	public void SetBooster(int v)
	{
		this.m_boosterId = v;
	}

	// Token: 0x06001F62 RID: 8034 RVA: 0x0009D169 File Offset: 0x0009B369
	public void SetBoosterSelector(RewardItem.BoosterSelector v)
	{
		this.m_boosterSelector = v;
	}

	// Token: 0x06001F63 RID: 8035 RVA: 0x0009D172 File Offset: 0x0009B372
	public void SetCardBack(int v)
	{
		this.m_cardBackId = v;
	}

	// Token: 0x06001F64 RID: 8036 RVA: 0x0009D17B File Offset: 0x0009B37B
	public void SetCustomCoin(int v)
	{
		this.m_customCoinId = v;
	}

	// Token: 0x06001F65 RID: 8037 RVA: 0x0009D184 File Offset: 0x0009B384
	public void SetSubsetId(int v)
	{
		this.m_subsetId = v;
	}

	// Token: 0x06001F66 RID: 8038 RVA: 0x0009D18D File Offset: 0x0009B38D
	public void SetIsVirtual(bool v)
	{
		this.m_isVirtual = v;
	}

	// Token: 0x06001F67 RID: 8039 RVA: 0x0009D198 File Offset: 0x0009B398
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1619614552U)
		{
			if (num <= 1098446823U)
			{
				if (num != 679831291U)
				{
					if (num != 699650505U)
					{
						if (num == 1098446823U)
						{
							if (name == "REWARD_TYPE")
							{
								return this.m_rewardType;
							}
						}
					}
					else if (name == "SUBSET_ID")
					{
						return this.m_subsetId;
					}
				}
				else if (name == "BOOSTER")
				{
					return this.m_boosterId;
				}
			}
			else if (num != 1458105184U)
			{
				if (num != 1615316160U)
				{
					if (num == 1619614552U)
					{
						if (name == "QUANTITY")
						{
							return this.m_quantity;
						}
					}
				}
				else if (name == "CARD_PREMIUM_LEVEL")
				{
					return this.m_cardPremiumLevel;
				}
			}
			else if (name == "ID")
			{
				return base.ID;
			}
		}
		else if (num <= 2645619338U)
		{
			if (num != 2239413407U)
			{
				if (num != 2392581014U)
				{
					if (num == 2645619338U)
					{
						if (name == "RANDOM_CARD_BOOSTER_CARD_SET")
						{
							return this.m_randomCardBoosterCardSetId;
						}
					}
				}
				else if (name == "CUSTOM_COIN")
				{
					return this.m_customCoinId;
				}
			}
			else if (name == "CARD")
			{
				return this.m_cardId;
			}
		}
		else if (num <= 3189893097U)
		{
			if (num != 3060627597U)
			{
				if (num == 3189893097U)
				{
					if (name == "BOOSTER_SELECTOR")
					{
						return this.m_boosterSelector;
					}
				}
			}
			else if (name == "IS_VIRTUAL")
			{
				return this.m_isVirtual;
			}
		}
		else if (num != 3549743771U)
		{
			if (num == 4014551637U)
			{
				if (name == "REWARD_LIST_ID")
				{
					return this.m_rewardListId;
				}
			}
		}
		else if (name == "CARD_BACK")
		{
			return this.m_cardBackId;
		}
		return null;
	}

	// Token: 0x06001F68 RID: 8040 RVA: 0x0009D420 File Offset: 0x0009B620
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1619614552U)
		{
			if (num <= 1098446823U)
			{
				if (num != 679831291U)
				{
					if (num != 699650505U)
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
							this.m_rewardType = RewardItem.RewardType.NONE;
							return;
						}
						if (val is RewardItem.RewardType || val is int)
						{
							this.m_rewardType = (RewardItem.RewardType)val;
							return;
						}
						if (val is string)
						{
							this.m_rewardType = RewardItem.ParseRewardTypeValue((string)val);
							return;
						}
					}
					else
					{
						if (!(name == "SUBSET_ID"))
						{
							return;
						}
						this.m_subsetId = (int)val;
						return;
					}
				}
				else
				{
					if (!(name == "BOOSTER"))
					{
						return;
					}
					this.m_boosterId = (int)val;
					return;
				}
			}
			else if (num != 1458105184U)
			{
				if (num != 1615316160U)
				{
					if (num != 1619614552U)
					{
						return;
					}
					if (!(name == "QUANTITY"))
					{
						return;
					}
					this.m_quantity = (int)val;
					return;
				}
				else
				{
					if (!(name == "CARD_PREMIUM_LEVEL"))
					{
						return;
					}
					if (val == null)
					{
						this.m_cardPremiumLevel = RewardItem.CardPremiumLevel.NORMAL;
						return;
					}
					if (val is RewardItem.CardPremiumLevel || val is int)
					{
						this.m_cardPremiumLevel = (RewardItem.CardPremiumLevel)val;
						return;
					}
					if (val is string)
					{
						this.m_cardPremiumLevel = RewardItem.ParseCardPremiumLevelValue((string)val);
						return;
					}
				}
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
		else if (num <= 2645619338U)
		{
			if (num != 2239413407U)
			{
				if (num != 2392581014U)
				{
					if (num != 2645619338U)
					{
						return;
					}
					if (!(name == "RANDOM_CARD_BOOSTER_CARD_SET"))
					{
						return;
					}
					this.m_randomCardBoosterCardSetId = (int)val;
					return;
				}
				else
				{
					if (!(name == "CUSTOM_COIN"))
					{
						return;
					}
					this.m_customCoinId = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "CARD"))
				{
					return;
				}
				this.m_cardId = (int)val;
				return;
			}
		}
		else if (num <= 3189893097U)
		{
			if (num != 3060627597U)
			{
				if (num != 3189893097U)
				{
					return;
				}
				if (!(name == "BOOSTER_SELECTOR"))
				{
					return;
				}
				if (val == null)
				{
					this.m_boosterSelector = RewardItem.BoosterSelector.NONE;
					return;
				}
				if (val is RewardItem.BoosterSelector || val is int)
				{
					this.m_boosterSelector = (RewardItem.BoosterSelector)val;
					return;
				}
				if (val is string)
				{
					this.m_boosterSelector = RewardItem.ParseBoosterSelectorValue((string)val);
					return;
				}
			}
			else
			{
				if (!(name == "IS_VIRTUAL"))
				{
					return;
				}
				this.m_isVirtual = (bool)val;
			}
		}
		else if (num != 3549743771U)
		{
			if (num != 4014551637U)
			{
				return;
			}
			if (!(name == "REWARD_LIST_ID"))
			{
				return;
			}
			this.m_rewardListId = (int)val;
			return;
		}
		else
		{
			if (!(name == "CARD_BACK"))
			{
				return;
			}
			this.m_cardBackId = (int)val;
			return;
		}
	}

	// Token: 0x06001F69 RID: 8041 RVA: 0x0009D70C File Offset: 0x0009B90C
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1619614552U)
		{
			if (num <= 1098446823U)
			{
				if (num != 679831291U)
				{
					if (num != 699650505U)
					{
						if (num == 1098446823U)
						{
							if (name == "REWARD_TYPE")
							{
								return typeof(RewardItem.RewardType);
							}
						}
					}
					else if (name == "SUBSET_ID")
					{
						return typeof(int);
					}
				}
				else if (name == "BOOSTER")
				{
					return typeof(int);
				}
			}
			else if (num != 1458105184U)
			{
				if (num != 1615316160U)
				{
					if (num == 1619614552U)
					{
						if (name == "QUANTITY")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "CARD_PREMIUM_LEVEL")
				{
					return typeof(RewardItem.CardPremiumLevel);
				}
			}
			else if (name == "ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 2645619338U)
		{
			if (num != 2239413407U)
			{
				if (num != 2392581014U)
				{
					if (num == 2645619338U)
					{
						if (name == "RANDOM_CARD_BOOSTER_CARD_SET")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "CUSTOM_COIN")
				{
					return typeof(int);
				}
			}
			else if (name == "CARD")
			{
				return typeof(int);
			}
		}
		else if (num <= 3189893097U)
		{
			if (num != 3060627597U)
			{
				if (num == 3189893097U)
				{
					if (name == "BOOSTER_SELECTOR")
					{
						return typeof(RewardItem.BoosterSelector);
					}
				}
			}
			else if (name == "IS_VIRTUAL")
			{
				return typeof(bool);
			}
		}
		else if (num != 3549743771U)
		{
			if (num == 4014551637U)
			{
				if (name == "REWARD_LIST_ID")
				{
					return typeof(int);
				}
			}
		}
		else if (name == "CARD_BACK")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x06001F6A RID: 8042 RVA: 0x0009D986 File Offset: 0x0009BB86
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRewardItemDbfRecords loadRecords = new LoadRewardItemDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001F6B RID: 8043 RVA: 0x0009D99C File Offset: 0x0009BB9C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RewardItemDbfAsset rewardItemDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RewardItemDbfAsset)) as RewardItemDbfAsset;
		if (rewardItemDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("RewardItemDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < rewardItemDbfAsset.Records.Count; i++)
		{
			rewardItemDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (rewardItemDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001F6C RID: 8044 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001F6D RID: 8045 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x040011E1 RID: 4577
	[SerializeField]
	private int m_rewardListId;

	// Token: 0x040011E2 RID: 4578
	[SerializeField]
	private RewardItem.RewardType m_rewardType;

	// Token: 0x040011E3 RID: 4579
	[SerializeField]
	private int m_quantity = 1;

	// Token: 0x040011E4 RID: 4580
	[SerializeField]
	private int m_cardId;

	// Token: 0x040011E5 RID: 4581
	[SerializeField]
	private RewardItem.CardPremiumLevel m_cardPremiumLevel;

	// Token: 0x040011E6 RID: 4582
	[SerializeField]
	private int m_randomCardBoosterCardSetId;

	// Token: 0x040011E7 RID: 4583
	[SerializeField]
	private int m_boosterId;

	// Token: 0x040011E8 RID: 4584
	[SerializeField]
	private RewardItem.BoosterSelector m_boosterSelector;

	// Token: 0x040011E9 RID: 4585
	[SerializeField]
	private int m_cardBackId;

	// Token: 0x040011EA RID: 4586
	[SerializeField]
	private int m_customCoinId;

	// Token: 0x040011EB RID: 4587
	[SerializeField]
	private int m_subsetId;

	// Token: 0x040011EC RID: 4588
	[SerializeField]
	private bool m_isVirtual;
}
