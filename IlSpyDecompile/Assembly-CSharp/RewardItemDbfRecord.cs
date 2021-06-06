using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class RewardItemDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_rewardListId;

	[SerializeField]
	private RewardItem.RewardType m_rewardType;

	[SerializeField]
	private int m_quantity = 1;

	[SerializeField]
	private int m_cardId;

	[SerializeField]
	private RewardItem.CardPremiumLevel m_cardPremiumLevel;

	[SerializeField]
	private int m_randomCardBoosterCardSetId;

	[SerializeField]
	private int m_boosterId;

	[SerializeField]
	private RewardItem.BoosterSelector m_boosterSelector;

	[SerializeField]
	private int m_cardBackId;

	[SerializeField]
	private int m_customCoinId;

	[SerializeField]
	private int m_subsetId;

	[SerializeField]
	private bool m_isVirtual;

	[DbfField("REWARD_LIST_ID")]
	public int RewardListId => m_rewardListId;

	[DbfField("REWARD_TYPE")]
	public RewardItem.RewardType RewardType => m_rewardType;

	[DbfField("QUANTITY")]
	public int Quantity => m_quantity;

	[DbfField("CARD")]
	public int Card => m_cardId;

	public CardDbfRecord CardRecord => GameDbf.Card.GetRecord(m_cardId);

	[DbfField("CARD_PREMIUM_LEVEL")]
	public RewardItem.CardPremiumLevel CardPremiumLevel => m_cardPremiumLevel;

	[DbfField("RANDOM_CARD_BOOSTER_CARD_SET")]
	public int RandomCardBoosterCardSet => m_randomCardBoosterCardSetId;

	public BoosterCardSetDbfRecord RandomCardBoosterCardSetRecord => GameDbf.BoosterCardSet.GetRecord(m_randomCardBoosterCardSetId);

	[DbfField("BOOSTER")]
	public int Booster => m_boosterId;

	public BoosterDbfRecord BoosterRecord => GameDbf.Booster.GetRecord(m_boosterId);

	[DbfField("BOOSTER_SELECTOR")]
	public RewardItem.BoosterSelector BoosterSelector => m_boosterSelector;

	[DbfField("CARD_BACK")]
	public int CardBack => m_cardBackId;

	public CardBackDbfRecord CardBackRecord => GameDbf.CardBack.GetRecord(m_cardBackId);

	[DbfField("CUSTOM_COIN")]
	public int CustomCoin => m_customCoinId;

	public CoinDbfRecord CustomCoinRecord => GameDbf.Coin.GetRecord(m_customCoinId);

	[DbfField("SUBSET_ID")]
	public int SubsetId => m_subsetId;

	public SubsetDbfRecord SubsetRecord => GameDbf.Subset.GetRecord(m_subsetId);

	[DbfField("IS_VIRTUAL")]
	public bool IsVirtual => m_isVirtual;

	public void SetRewardListId(int v)
	{
		m_rewardListId = v;
	}

	public void SetRewardType(RewardItem.RewardType v)
	{
		m_rewardType = v;
	}

	public void SetQuantity(int v)
	{
		m_quantity = v;
	}

	public void SetCard(int v)
	{
		m_cardId = v;
	}

	public void SetCardPremiumLevel(RewardItem.CardPremiumLevel v)
	{
		m_cardPremiumLevel = v;
	}

	public void SetRandomCardBoosterCardSet(int v)
	{
		m_randomCardBoosterCardSetId = v;
	}

	public void SetBooster(int v)
	{
		m_boosterId = v;
	}

	public void SetBoosterSelector(RewardItem.BoosterSelector v)
	{
		m_boosterSelector = v;
	}

	public void SetCardBack(int v)
	{
		m_cardBackId = v;
	}

	public void SetCustomCoin(int v)
	{
		m_customCoinId = v;
	}

	public void SetSubsetId(int v)
	{
		m_subsetId = v;
	}

	public void SetIsVirtual(bool v)
	{
		m_isVirtual = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"REWARD_LIST_ID" => m_rewardListId, 
			"REWARD_TYPE" => m_rewardType, 
			"QUANTITY" => m_quantity, 
			"CARD" => m_cardId, 
			"CARD_PREMIUM_LEVEL" => m_cardPremiumLevel, 
			"RANDOM_CARD_BOOSTER_CARD_SET" => m_randomCardBoosterCardSetId, 
			"BOOSTER" => m_boosterId, 
			"BOOSTER_SELECTOR" => m_boosterSelector, 
			"CARD_BACK" => m_cardBackId, 
			"CUSTOM_COIN" => m_customCoinId, 
			"SUBSET_ID" => m_subsetId, 
			"IS_VIRTUAL" => m_isVirtual, 
			_ => null, 
		};
	}

	public override void SetVar(string name, object val)
	{
		switch (name)
		{
		case "ID":
			SetID((int)val);
			break;
		case "REWARD_LIST_ID":
			m_rewardListId = (int)val;
			break;
		case "REWARD_TYPE":
			if (val == null)
			{
				m_rewardType = RewardItem.RewardType.NONE;
			}
			else if (val is RewardItem.RewardType || val is int)
			{
				m_rewardType = (RewardItem.RewardType)val;
			}
			else if (val is string)
			{
				m_rewardType = RewardItem.ParseRewardTypeValue((string)val);
			}
			break;
		case "QUANTITY":
			m_quantity = (int)val;
			break;
		case "CARD":
			m_cardId = (int)val;
			break;
		case "CARD_PREMIUM_LEVEL":
			if (val == null)
			{
				m_cardPremiumLevel = RewardItem.CardPremiumLevel.NORMAL;
			}
			else if (val is RewardItem.CardPremiumLevel || val is int)
			{
				m_cardPremiumLevel = (RewardItem.CardPremiumLevel)val;
			}
			else if (val is string)
			{
				m_cardPremiumLevel = RewardItem.ParseCardPremiumLevelValue((string)val);
			}
			break;
		case "RANDOM_CARD_BOOSTER_CARD_SET":
			m_randomCardBoosterCardSetId = (int)val;
			break;
		case "BOOSTER":
			m_boosterId = (int)val;
			break;
		case "BOOSTER_SELECTOR":
			if (val == null)
			{
				m_boosterSelector = RewardItem.BoosterSelector.NONE;
			}
			else if (val is RewardItem.BoosterSelector || val is int)
			{
				m_boosterSelector = (RewardItem.BoosterSelector)val;
			}
			else if (val is string)
			{
				m_boosterSelector = RewardItem.ParseBoosterSelectorValue((string)val);
			}
			break;
		case "CARD_BACK":
			m_cardBackId = (int)val;
			break;
		case "CUSTOM_COIN":
			m_customCoinId = (int)val;
			break;
		case "SUBSET_ID":
			m_subsetId = (int)val;
			break;
		case "IS_VIRTUAL":
			m_isVirtual = (bool)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"REWARD_LIST_ID" => typeof(int), 
			"REWARD_TYPE" => typeof(RewardItem.RewardType), 
			"QUANTITY" => typeof(int), 
			"CARD" => typeof(int), 
			"CARD_PREMIUM_LEVEL" => typeof(RewardItem.CardPremiumLevel), 
			"RANDOM_CARD_BOOSTER_CARD_SET" => typeof(int), 
			"BOOSTER" => typeof(int), 
			"BOOSTER_SELECTOR" => typeof(RewardItem.BoosterSelector), 
			"CARD_BACK" => typeof(int), 
			"CUSTOM_COIN" => typeof(int), 
			"SUBSET_ID" => typeof(int), 
			"IS_VIRTUAL" => typeof(bool), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRewardItemDbfRecords loadRecords = new LoadRewardItemDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RewardItemDbfAsset rewardItemDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RewardItemDbfAsset)) as RewardItemDbfAsset;
		if (rewardItemDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"RewardItemDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < rewardItemDbfAsset.Records.Count; i++)
		{
			rewardItemDbfAsset.Records[i].StripUnusedLocales();
		}
		records = rewardItemDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
	}
}
