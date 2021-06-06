using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class LeagueRankDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_leagueId;

	[SerializeField]
	private int m_starLevel;

	[SerializeField]
	private int m_stars;

	[SerializeField]
	private bool m_showIndividualRanking;

	[SerializeField]
	private DbfLocValue m_rankName;

	[SerializeField]
	private DbfLocValue m_medalText;

	[SerializeField]
	private string m_medalTexture;

	[SerializeField]
	private string m_medalMaterial;

	[SerializeField]
	private string m_cheatName;

	[SerializeField]
	private bool m_canLoseStars;

	[SerializeField]
	private bool m_canLoseLevel;

	[SerializeField]
	private int m_maxBestEverStarLevel;

	[SerializeField]
	private int m_winStreakThreshold;

	[SerializeField]
	private int m_rewardChestIdV1Id;

	[SerializeField]
	private int m_rewardBagId;

	[SerializeField]
	private int m_rewardChestVisualIndex;

	[SerializeField]
	private bool m_showToastOnAttained;

	[SerializeField]
	private bool m_showOpponentRankInGame;

	[DbfField("LEAGUE_ID")]
	public int LeagueId => m_leagueId;

	[DbfField("STAR_LEVEL")]
	public int StarLevel => m_starLevel;

	[DbfField("STARS")]
	public int Stars => m_stars;

	[DbfField("SHOW_INDIVIDUAL_RANKING")]
	public bool ShowIndividualRanking => m_showIndividualRanking;

	[DbfField("RANK_NAME")]
	public DbfLocValue RankName => m_rankName;

	[DbfField("MEDAL_TEXT")]
	public DbfLocValue MedalText => m_medalText;

	[DbfField("MEDAL_TEXTURE")]
	public string MedalTexture => m_medalTexture;

	[DbfField("MEDAL_MATERIAL")]
	public string MedalMaterial => m_medalMaterial;

	[DbfField("CHEAT_NAME")]
	public string CheatName => m_cheatName;

	[DbfField("CAN_LOSE_STARS")]
	public bool CanLoseStars => m_canLoseStars;

	[DbfField("CAN_LOSE_LEVEL")]
	public bool CanLoseLevel => m_canLoseLevel;

	[Obsolete("Only used by Legacy system.")]
	[DbfField("MAX_BEST_EVER_STAR_LEVEL")]
	public int MaxBestEverStarLevel => m_maxBestEverStarLevel;

	[DbfField("WIN_STREAK_THRESHOLD")]
	public int WinStreakThreshold => m_winStreakThreshold;

	[DbfField("REWARD_CHEST_ID_V1")]
	public int RewardChestIdV1 => m_rewardChestIdV1Id;

	public RewardChestDbfRecord RewardChestIdV1Record => GameDbf.RewardChest.GetRecord(m_rewardChestIdV1Id);

	[DbfField("REWARD_BAG_ID")]
	public int RewardBagId => m_rewardBagId;

	[DbfField("REWARD_CHEST_VISUAL_INDEX")]
	public int RewardChestVisualIndex => m_rewardChestVisualIndex;

	[DbfField("SHOW_TOAST_ON_ATTAINED")]
	public bool ShowToastOnAttained => m_showToastOnAttained;

	[DbfField("SHOW_OPPONENT_RANK_IN_GAME")]
	public bool ShowOpponentRankInGame => m_showOpponentRankInGame;

	public void SetLeagueId(int v)
	{
		m_leagueId = v;
	}

	public void SetStarLevel(int v)
	{
		m_starLevel = v;
	}

	public void SetStars(int v)
	{
		m_stars = v;
	}

	public void SetShowIndividualRanking(bool v)
	{
		m_showIndividualRanking = v;
	}

	public void SetRankName(DbfLocValue v)
	{
		m_rankName = v;
		v.SetDebugInfo(base.ID, "RANK_NAME");
	}

	public void SetMedalText(DbfLocValue v)
	{
		m_medalText = v;
		v.SetDebugInfo(base.ID, "MEDAL_TEXT");
	}

	public void SetMedalTexture(string v)
	{
		m_medalTexture = v;
	}

	public void SetMedalMaterial(string v)
	{
		m_medalMaterial = v;
	}

	public void SetCheatName(string v)
	{
		m_cheatName = v;
	}

	public void SetCanLoseStars(bool v)
	{
		m_canLoseStars = v;
	}

	public void SetCanLoseLevel(bool v)
	{
		m_canLoseLevel = v;
	}

	[Obsolete("Only used by Legacy system.")]
	public void SetMaxBestEverStarLevel(int v)
	{
		m_maxBestEverStarLevel = v;
	}

	public void SetWinStreakThreshold(int v)
	{
		m_winStreakThreshold = v;
	}

	public void SetRewardChestIdV1(int v)
	{
		m_rewardChestIdV1Id = v;
	}

	public void SetRewardBagId(int v)
	{
		m_rewardBagId = v;
	}

	public void SetRewardChestVisualIndex(int v)
	{
		m_rewardChestVisualIndex = v;
	}

	public void SetShowToastOnAttained(bool v)
	{
		m_showToastOnAttained = v;
	}

	public void SetShowOpponentRankInGame(bool v)
	{
		m_showOpponentRankInGame = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"LEAGUE_ID" => m_leagueId, 
			"STAR_LEVEL" => m_starLevel, 
			"STARS" => m_stars, 
			"SHOW_INDIVIDUAL_RANKING" => m_showIndividualRanking, 
			"RANK_NAME" => m_rankName, 
			"MEDAL_TEXT" => m_medalText, 
			"MEDAL_TEXTURE" => m_medalTexture, 
			"MEDAL_MATERIAL" => m_medalMaterial, 
			"CHEAT_NAME" => m_cheatName, 
			"CAN_LOSE_STARS" => m_canLoseStars, 
			"CAN_LOSE_LEVEL" => m_canLoseLevel, 
			"MAX_BEST_EVER_STAR_LEVEL" => m_maxBestEverStarLevel, 
			"WIN_STREAK_THRESHOLD" => m_winStreakThreshold, 
			"REWARD_CHEST_ID_V1" => m_rewardChestIdV1Id, 
			"REWARD_BAG_ID" => m_rewardBagId, 
			"REWARD_CHEST_VISUAL_INDEX" => m_rewardChestVisualIndex, 
			"SHOW_TOAST_ON_ATTAINED" => m_showToastOnAttained, 
			"SHOW_OPPONENT_RANK_IN_GAME" => m_showOpponentRankInGame, 
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
		case "LEAGUE_ID":
			m_leagueId = (int)val;
			break;
		case "STAR_LEVEL":
			m_starLevel = (int)val;
			break;
		case "STARS":
			m_stars = (int)val;
			break;
		case "SHOW_INDIVIDUAL_RANKING":
			m_showIndividualRanking = (bool)val;
			break;
		case "RANK_NAME":
			m_rankName = (DbfLocValue)val;
			break;
		case "MEDAL_TEXT":
			m_medalText = (DbfLocValue)val;
			break;
		case "MEDAL_TEXTURE":
			m_medalTexture = (string)val;
			break;
		case "MEDAL_MATERIAL":
			m_medalMaterial = (string)val;
			break;
		case "CHEAT_NAME":
			m_cheatName = (string)val;
			break;
		case "CAN_LOSE_STARS":
			m_canLoseStars = (bool)val;
			break;
		case "CAN_LOSE_LEVEL":
			m_canLoseLevel = (bool)val;
			break;
		case "MAX_BEST_EVER_STAR_LEVEL":
			m_maxBestEverStarLevel = (int)val;
			break;
		case "WIN_STREAK_THRESHOLD":
			m_winStreakThreshold = (int)val;
			break;
		case "REWARD_CHEST_ID_V1":
			m_rewardChestIdV1Id = (int)val;
			break;
		case "REWARD_BAG_ID":
			m_rewardBagId = (int)val;
			break;
		case "REWARD_CHEST_VISUAL_INDEX":
			m_rewardChestVisualIndex = (int)val;
			break;
		case "SHOW_TOAST_ON_ATTAINED":
			m_showToastOnAttained = (bool)val;
			break;
		case "SHOW_OPPONENT_RANK_IN_GAME":
			m_showOpponentRankInGame = (bool)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"LEAGUE_ID" => typeof(int), 
			"STAR_LEVEL" => typeof(int), 
			"STARS" => typeof(int), 
			"SHOW_INDIVIDUAL_RANKING" => typeof(bool), 
			"RANK_NAME" => typeof(DbfLocValue), 
			"MEDAL_TEXT" => typeof(DbfLocValue), 
			"MEDAL_TEXTURE" => typeof(string), 
			"MEDAL_MATERIAL" => typeof(string), 
			"CHEAT_NAME" => typeof(string), 
			"CAN_LOSE_STARS" => typeof(bool), 
			"CAN_LOSE_LEVEL" => typeof(bool), 
			"MAX_BEST_EVER_STAR_LEVEL" => typeof(int), 
			"WIN_STREAK_THRESHOLD" => typeof(int), 
			"REWARD_CHEST_ID_V1" => typeof(int), 
			"REWARD_BAG_ID" => typeof(int), 
			"REWARD_CHEST_VISUAL_INDEX" => typeof(int), 
			"SHOW_TOAST_ON_ATTAINED" => typeof(bool), 
			"SHOW_OPPONENT_RANK_IN_GAME" => typeof(bool), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadLeagueRankDbfRecords loadRecords = new LoadLeagueRankDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		LeagueRankDbfAsset leagueRankDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(LeagueRankDbfAsset)) as LeagueRankDbfAsset;
		if (leagueRankDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"LeagueRankDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < leagueRankDbfAsset.Records.Count; i++)
		{
			leagueRankDbfAsset.Records[i].StripUnusedLocales();
		}
		records = leagueRankDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_rankName.StripUnusedLocales();
		m_medalText.StripUnusedLocales();
	}
}
