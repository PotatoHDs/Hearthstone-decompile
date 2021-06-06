using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class LeagueDbfRecord : DbfRecord
{
	[SerializeField]
	private League.LeagueType m_leagueType = League.ParseLeagueTypeValue("unknown");

	[SerializeField]
	private int m_leagueLevel;

	[SerializeField]
	private int m_leagueVersion;

	[SerializeField]
	private int m_initialSeasonId;

	[SerializeField]
	private League.LeagueType m_promoteToLeagueType;

	[SerializeField]
	private bool m_canPromoteSelfManually;

	[SerializeField]
	private bool m_lockWildBoosters;

	[SerializeField]
	private bool m_lockWildCards;

	[SerializeField]
	private int m_lockCardsFromSubsetId;

	[SerializeField]
	private DbfLocValue m_lockedBoosterText;

	[SerializeField]
	private DbfLocValue m_lockedCardUnplayableText;

	[SerializeField]
	private DbfLocValue m_lockedCardPopupTitleText;

	[SerializeField]
	private DbfLocValue m_lockedCardPopupBodyText;

	[SerializeField]
	private int m_seasonRollRewardMinWins;

	[SerializeField]
	private int m_seasonEndRewardChestId;

	[SerializeField]
	private int m_seasonCardBackMinWins;

	[SerializeField]
	private int m_rankedIntroSeenRequirement;

	[SerializeField]
	private int m_bonusStarsPopupSeenRequirement;

	[SerializeField]
	private int m_rewardsVersion;

	[DbfField("LEAGUE_TYPE")]
	public League.LeagueType LeagueType => m_leagueType;

	[DbfField("LEAGUE_LEVEL")]
	public int LeagueLevel => m_leagueLevel;

	[DbfField("LEAGUE_VERSION")]
	public int LeagueVersion => m_leagueVersion;

	[DbfField("INITIAL_SEASON_ID")]
	public int InitialSeasonId => m_initialSeasonId;

	[DbfField("PROMOTE_TO_LEAGUE_TYPE")]
	public League.LeagueType PromoteToLeagueType => m_promoteToLeagueType;

	[DbfField("CAN_PROMOTE_SELF_MANUALLY")]
	public bool CanPromoteSelfManually => m_canPromoteSelfManually;

	[DbfField("LOCK_WILD_BOOSTERS")]
	public bool LockWildBoosters => m_lockWildBoosters;

	[DbfField("LOCK_WILD_CARDS")]
	public bool LockWildCards => m_lockWildCards;

	[DbfField("LOCK_CARDS_FROM_SUBSET_ID")]
	public int LockCardsFromSubsetId => m_lockCardsFromSubsetId;

	public SubsetDbfRecord LockCardsFromSubsetRecord => GameDbf.Subset.GetRecord(m_lockCardsFromSubsetId);

	[DbfField("LOCKED_BOOSTER_TEXT")]
	public DbfLocValue LockedBoosterText => m_lockedBoosterText;

	[DbfField("LOCKED_CARD_UNPLAYABLE_TEXT")]
	public DbfLocValue LockedCardUnplayableText => m_lockedCardUnplayableText;

	[DbfField("LOCKED_CARD_POPUP_TITLE_TEXT")]
	public DbfLocValue LockedCardPopupTitleText => m_lockedCardPopupTitleText;

	[DbfField("LOCKED_CARD_POPUP_BODY_TEXT")]
	public DbfLocValue LockedCardPopupBodyText => m_lockedCardPopupBodyText;

	[DbfField("SEASON_ROLL_REWARD_MIN_WINS")]
	public int SeasonRollRewardMinWins => m_seasonRollRewardMinWins;

	[DbfField("SEASON_END_REWARD_CHEST_ID")]
	public int SeasonEndRewardChestId => m_seasonEndRewardChestId;

	public RewardChestDbfRecord SeasonEndRewardChestRecord => GameDbf.RewardChest.GetRecord(m_seasonEndRewardChestId);

	[DbfField("SEASON_CARD_BACK_MIN_WINS")]
	public int SeasonCardBackMinWins => m_seasonCardBackMinWins;

	[DbfField("RANKED_INTRO_SEEN_REQUIREMENT")]
	public int RankedIntroSeenRequirement => m_rankedIntroSeenRequirement;

	[DbfField("BONUS_STARS_POPUP_SEEN_REQUIREMENT")]
	public int BonusStarsPopupSeenRequirement => m_bonusStarsPopupSeenRequirement;

	[DbfField("REWARDS_VERSION")]
	public int RewardsVersion => m_rewardsVersion;

	public List<LeagueGameTypeDbfRecord> LeagueGameType => GameDbf.LeagueGameType.GetRecords((LeagueGameTypeDbfRecord r) => r.LeagueId == base.ID);

	public List<LeagueRankDbfRecord> Ranks => GameDbf.LeagueRank.GetRecords((LeagueRankDbfRecord r) => r.LeagueId == base.ID);

	public void SetLeagueType(League.LeagueType v)
	{
		m_leagueType = v;
	}

	public void SetLeagueLevel(int v)
	{
		m_leagueLevel = v;
	}

	public void SetLeagueVersion(int v)
	{
		m_leagueVersion = v;
	}

	public void SetInitialSeasonId(int v)
	{
		m_initialSeasonId = v;
	}

	public void SetPromoteToLeagueType(League.LeagueType v)
	{
		m_promoteToLeagueType = v;
	}

	public void SetCanPromoteSelfManually(bool v)
	{
		m_canPromoteSelfManually = v;
	}

	public void SetLockWildBoosters(bool v)
	{
		m_lockWildBoosters = v;
	}

	public void SetLockWildCards(bool v)
	{
		m_lockWildCards = v;
	}

	public void SetLockCardsFromSubsetId(int v)
	{
		m_lockCardsFromSubsetId = v;
	}

	public void SetLockedBoosterText(DbfLocValue v)
	{
		m_lockedBoosterText = v;
		v.SetDebugInfo(base.ID, "LOCKED_BOOSTER_TEXT");
	}

	public void SetLockedCardUnplayableText(DbfLocValue v)
	{
		m_lockedCardUnplayableText = v;
		v.SetDebugInfo(base.ID, "LOCKED_CARD_UNPLAYABLE_TEXT");
	}

	public void SetLockedCardPopupTitleText(DbfLocValue v)
	{
		m_lockedCardPopupTitleText = v;
		v.SetDebugInfo(base.ID, "LOCKED_CARD_POPUP_TITLE_TEXT");
	}

	public void SetLockedCardPopupBodyText(DbfLocValue v)
	{
		m_lockedCardPopupBodyText = v;
		v.SetDebugInfo(base.ID, "LOCKED_CARD_POPUP_BODY_TEXT");
	}

	public void SetSeasonRollRewardMinWins(int v)
	{
		m_seasonRollRewardMinWins = v;
	}

	public void SetSeasonEndRewardChestId(int v)
	{
		m_seasonEndRewardChestId = v;
	}

	public void SetSeasonCardBackMinWins(int v)
	{
		m_seasonCardBackMinWins = v;
	}

	public void SetRankedIntroSeenRequirement(int v)
	{
		m_rankedIntroSeenRequirement = v;
	}

	public void SetBonusStarsPopupSeenRequirement(int v)
	{
		m_bonusStarsPopupSeenRequirement = v;
	}

	public void SetRewardsVersion(int v)
	{
		m_rewardsVersion = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"LEAGUE_TYPE" => m_leagueType, 
			"LEAGUE_LEVEL" => m_leagueLevel, 
			"LEAGUE_VERSION" => m_leagueVersion, 
			"INITIAL_SEASON_ID" => m_initialSeasonId, 
			"PROMOTE_TO_LEAGUE_TYPE" => m_promoteToLeagueType, 
			"CAN_PROMOTE_SELF_MANUALLY" => m_canPromoteSelfManually, 
			"LOCK_WILD_BOOSTERS" => m_lockWildBoosters, 
			"LOCK_WILD_CARDS" => m_lockWildCards, 
			"LOCK_CARDS_FROM_SUBSET_ID" => m_lockCardsFromSubsetId, 
			"LOCKED_BOOSTER_TEXT" => m_lockedBoosterText, 
			"LOCKED_CARD_UNPLAYABLE_TEXT" => m_lockedCardUnplayableText, 
			"LOCKED_CARD_POPUP_TITLE_TEXT" => m_lockedCardPopupTitleText, 
			"LOCKED_CARD_POPUP_BODY_TEXT" => m_lockedCardPopupBodyText, 
			"SEASON_ROLL_REWARD_MIN_WINS" => m_seasonRollRewardMinWins, 
			"SEASON_END_REWARD_CHEST_ID" => m_seasonEndRewardChestId, 
			"SEASON_CARD_BACK_MIN_WINS" => m_seasonCardBackMinWins, 
			"RANKED_INTRO_SEEN_REQUIREMENT" => m_rankedIntroSeenRequirement, 
			"BONUS_STARS_POPUP_SEEN_REQUIREMENT" => m_bonusStarsPopupSeenRequirement, 
			"REWARDS_VERSION" => m_rewardsVersion, 
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
		case "LEAGUE_TYPE":
			if (val == null)
			{
				m_leagueType = League.LeagueType.UNKNOWN;
			}
			else if (val is League.LeagueType || val is int)
			{
				m_leagueType = (League.LeagueType)val;
			}
			else if (val is string)
			{
				m_leagueType = League.ParseLeagueTypeValue((string)val);
			}
			break;
		case "LEAGUE_LEVEL":
			m_leagueLevel = (int)val;
			break;
		case "LEAGUE_VERSION":
			m_leagueVersion = (int)val;
			break;
		case "INITIAL_SEASON_ID":
			m_initialSeasonId = (int)val;
			break;
		case "PROMOTE_TO_LEAGUE_TYPE":
			if (val == null)
			{
				m_promoteToLeagueType = League.LeagueType.UNKNOWN;
			}
			else if (val is League.LeagueType || val is int)
			{
				m_promoteToLeagueType = (League.LeagueType)val;
			}
			else if (val is string)
			{
				m_promoteToLeagueType = League.ParseLeagueTypeValue((string)val);
			}
			break;
		case "CAN_PROMOTE_SELF_MANUALLY":
			m_canPromoteSelfManually = (bool)val;
			break;
		case "LOCK_WILD_BOOSTERS":
			m_lockWildBoosters = (bool)val;
			break;
		case "LOCK_WILD_CARDS":
			m_lockWildCards = (bool)val;
			break;
		case "LOCK_CARDS_FROM_SUBSET_ID":
			m_lockCardsFromSubsetId = (int)val;
			break;
		case "LOCKED_BOOSTER_TEXT":
			m_lockedBoosterText = (DbfLocValue)val;
			break;
		case "LOCKED_CARD_UNPLAYABLE_TEXT":
			m_lockedCardUnplayableText = (DbfLocValue)val;
			break;
		case "LOCKED_CARD_POPUP_TITLE_TEXT":
			m_lockedCardPopupTitleText = (DbfLocValue)val;
			break;
		case "LOCKED_CARD_POPUP_BODY_TEXT":
			m_lockedCardPopupBodyText = (DbfLocValue)val;
			break;
		case "SEASON_ROLL_REWARD_MIN_WINS":
			m_seasonRollRewardMinWins = (int)val;
			break;
		case "SEASON_END_REWARD_CHEST_ID":
			m_seasonEndRewardChestId = (int)val;
			break;
		case "SEASON_CARD_BACK_MIN_WINS":
			m_seasonCardBackMinWins = (int)val;
			break;
		case "RANKED_INTRO_SEEN_REQUIREMENT":
			m_rankedIntroSeenRequirement = (int)val;
			break;
		case "BONUS_STARS_POPUP_SEEN_REQUIREMENT":
			m_bonusStarsPopupSeenRequirement = (int)val;
			break;
		case "REWARDS_VERSION":
			m_rewardsVersion = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"LEAGUE_TYPE" => typeof(League.LeagueType), 
			"LEAGUE_LEVEL" => typeof(int), 
			"LEAGUE_VERSION" => typeof(int), 
			"INITIAL_SEASON_ID" => typeof(int), 
			"PROMOTE_TO_LEAGUE_TYPE" => typeof(League.LeagueType), 
			"CAN_PROMOTE_SELF_MANUALLY" => typeof(bool), 
			"LOCK_WILD_BOOSTERS" => typeof(bool), 
			"LOCK_WILD_CARDS" => typeof(bool), 
			"LOCK_CARDS_FROM_SUBSET_ID" => typeof(int), 
			"LOCKED_BOOSTER_TEXT" => typeof(DbfLocValue), 
			"LOCKED_CARD_UNPLAYABLE_TEXT" => typeof(DbfLocValue), 
			"LOCKED_CARD_POPUP_TITLE_TEXT" => typeof(DbfLocValue), 
			"LOCKED_CARD_POPUP_BODY_TEXT" => typeof(DbfLocValue), 
			"SEASON_ROLL_REWARD_MIN_WINS" => typeof(int), 
			"SEASON_END_REWARD_CHEST_ID" => typeof(int), 
			"SEASON_CARD_BACK_MIN_WINS" => typeof(int), 
			"RANKED_INTRO_SEEN_REQUIREMENT" => typeof(int), 
			"BONUS_STARS_POPUP_SEEN_REQUIREMENT" => typeof(int), 
			"REWARDS_VERSION" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadLeagueDbfRecords loadRecords = new LoadLeagueDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		LeagueDbfAsset leagueDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(LeagueDbfAsset)) as LeagueDbfAsset;
		if (leagueDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"LeagueDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < leagueDbfAsset.Records.Count; i++)
		{
			leagueDbfAsset.Records[i].StripUnusedLocales();
		}
		records = leagueDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_lockedBoosterText.StripUnusedLocales();
		m_lockedCardUnplayableText.StripUnusedLocales();
		m_lockedCardPopupTitleText.StripUnusedLocales();
		m_lockedCardPopupBodyText.StripUnusedLocales();
	}
}
