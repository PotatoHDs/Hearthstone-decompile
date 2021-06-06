using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000208 RID: 520
[Serializable]
public class LeagueRankDbfRecord : DbfRecord
{
	// Token: 0x17000342 RID: 834
	// (get) Token: 0x06001C90 RID: 7312 RVA: 0x00093D62 File Offset: 0x00091F62
	[DbfField("LEAGUE_ID")]
	public int LeagueId
	{
		get
		{
			return this.m_leagueId;
		}
	}

	// Token: 0x17000343 RID: 835
	// (get) Token: 0x06001C91 RID: 7313 RVA: 0x00093D6A File Offset: 0x00091F6A
	[DbfField("STAR_LEVEL")]
	public int StarLevel
	{
		get
		{
			return this.m_starLevel;
		}
	}

	// Token: 0x17000344 RID: 836
	// (get) Token: 0x06001C92 RID: 7314 RVA: 0x00093D72 File Offset: 0x00091F72
	[DbfField("STARS")]
	public int Stars
	{
		get
		{
			return this.m_stars;
		}
	}

	// Token: 0x17000345 RID: 837
	// (get) Token: 0x06001C93 RID: 7315 RVA: 0x00093D7A File Offset: 0x00091F7A
	[DbfField("SHOW_INDIVIDUAL_RANKING")]
	public bool ShowIndividualRanking
	{
		get
		{
			return this.m_showIndividualRanking;
		}
	}

	// Token: 0x17000346 RID: 838
	// (get) Token: 0x06001C94 RID: 7316 RVA: 0x00093D82 File Offset: 0x00091F82
	[DbfField("RANK_NAME")]
	public DbfLocValue RankName
	{
		get
		{
			return this.m_rankName;
		}
	}

	// Token: 0x17000347 RID: 839
	// (get) Token: 0x06001C95 RID: 7317 RVA: 0x00093D8A File Offset: 0x00091F8A
	[DbfField("MEDAL_TEXT")]
	public DbfLocValue MedalText
	{
		get
		{
			return this.m_medalText;
		}
	}

	// Token: 0x17000348 RID: 840
	// (get) Token: 0x06001C96 RID: 7318 RVA: 0x00093D92 File Offset: 0x00091F92
	[DbfField("MEDAL_TEXTURE")]
	public string MedalTexture
	{
		get
		{
			return this.m_medalTexture;
		}
	}

	// Token: 0x17000349 RID: 841
	// (get) Token: 0x06001C97 RID: 7319 RVA: 0x00093D9A File Offset: 0x00091F9A
	[DbfField("MEDAL_MATERIAL")]
	public string MedalMaterial
	{
		get
		{
			return this.m_medalMaterial;
		}
	}

	// Token: 0x1700034A RID: 842
	// (get) Token: 0x06001C98 RID: 7320 RVA: 0x00093DA2 File Offset: 0x00091FA2
	[DbfField("CHEAT_NAME")]
	public string CheatName
	{
		get
		{
			return this.m_cheatName;
		}
	}

	// Token: 0x1700034B RID: 843
	// (get) Token: 0x06001C99 RID: 7321 RVA: 0x00093DAA File Offset: 0x00091FAA
	[DbfField("CAN_LOSE_STARS")]
	public bool CanLoseStars
	{
		get
		{
			return this.m_canLoseStars;
		}
	}

	// Token: 0x1700034C RID: 844
	// (get) Token: 0x06001C9A RID: 7322 RVA: 0x00093DB2 File Offset: 0x00091FB2
	[DbfField("CAN_LOSE_LEVEL")]
	public bool CanLoseLevel
	{
		get
		{
			return this.m_canLoseLevel;
		}
	}

	// Token: 0x1700034D RID: 845
	// (get) Token: 0x06001C9B RID: 7323 RVA: 0x00093DBA File Offset: 0x00091FBA
	[Obsolete("Only used by Legacy system.")]
	[DbfField("MAX_BEST_EVER_STAR_LEVEL")]
	public int MaxBestEverStarLevel
	{
		get
		{
			return this.m_maxBestEverStarLevel;
		}
	}

	// Token: 0x1700034E RID: 846
	// (get) Token: 0x06001C9C RID: 7324 RVA: 0x00093DC2 File Offset: 0x00091FC2
	[DbfField("WIN_STREAK_THRESHOLD")]
	public int WinStreakThreshold
	{
		get
		{
			return this.m_winStreakThreshold;
		}
	}

	// Token: 0x1700034F RID: 847
	// (get) Token: 0x06001C9D RID: 7325 RVA: 0x00093DCA File Offset: 0x00091FCA
	[DbfField("REWARD_CHEST_ID_V1")]
	public int RewardChestIdV1
	{
		get
		{
			return this.m_rewardChestIdV1Id;
		}
	}

	// Token: 0x17000350 RID: 848
	// (get) Token: 0x06001C9E RID: 7326 RVA: 0x00093DD2 File Offset: 0x00091FD2
	public RewardChestDbfRecord RewardChestIdV1Record
	{
		get
		{
			return GameDbf.RewardChest.GetRecord(this.m_rewardChestIdV1Id);
		}
	}

	// Token: 0x17000351 RID: 849
	// (get) Token: 0x06001C9F RID: 7327 RVA: 0x00093DE4 File Offset: 0x00091FE4
	[DbfField("REWARD_BAG_ID")]
	public int RewardBagId
	{
		get
		{
			return this.m_rewardBagId;
		}
	}

	// Token: 0x17000352 RID: 850
	// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x00093DEC File Offset: 0x00091FEC
	[DbfField("REWARD_CHEST_VISUAL_INDEX")]
	public int RewardChestVisualIndex
	{
		get
		{
			return this.m_rewardChestVisualIndex;
		}
	}

	// Token: 0x17000353 RID: 851
	// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x00093DF4 File Offset: 0x00091FF4
	[DbfField("SHOW_TOAST_ON_ATTAINED")]
	public bool ShowToastOnAttained
	{
		get
		{
			return this.m_showToastOnAttained;
		}
	}

	// Token: 0x17000354 RID: 852
	// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x00093DFC File Offset: 0x00091FFC
	[DbfField("SHOW_OPPONENT_RANK_IN_GAME")]
	public bool ShowOpponentRankInGame
	{
		get
		{
			return this.m_showOpponentRankInGame;
		}
	}

	// Token: 0x06001CA3 RID: 7331 RVA: 0x00093E04 File Offset: 0x00092004
	public void SetLeagueId(int v)
	{
		this.m_leagueId = v;
	}

	// Token: 0x06001CA4 RID: 7332 RVA: 0x00093E0D File Offset: 0x0009200D
	public void SetStarLevel(int v)
	{
		this.m_starLevel = v;
	}

	// Token: 0x06001CA5 RID: 7333 RVA: 0x00093E16 File Offset: 0x00092016
	public void SetStars(int v)
	{
		this.m_stars = v;
	}

	// Token: 0x06001CA6 RID: 7334 RVA: 0x00093E1F File Offset: 0x0009201F
	public void SetShowIndividualRanking(bool v)
	{
		this.m_showIndividualRanking = v;
	}

	// Token: 0x06001CA7 RID: 7335 RVA: 0x00093E28 File Offset: 0x00092028
	public void SetRankName(DbfLocValue v)
	{
		this.m_rankName = v;
		v.SetDebugInfo(base.ID, "RANK_NAME");
	}

	// Token: 0x06001CA8 RID: 7336 RVA: 0x00093E42 File Offset: 0x00092042
	public void SetMedalText(DbfLocValue v)
	{
		this.m_medalText = v;
		v.SetDebugInfo(base.ID, "MEDAL_TEXT");
	}

	// Token: 0x06001CA9 RID: 7337 RVA: 0x00093E5C File Offset: 0x0009205C
	public void SetMedalTexture(string v)
	{
		this.m_medalTexture = v;
	}

	// Token: 0x06001CAA RID: 7338 RVA: 0x00093E65 File Offset: 0x00092065
	public void SetMedalMaterial(string v)
	{
		this.m_medalMaterial = v;
	}

	// Token: 0x06001CAB RID: 7339 RVA: 0x00093E6E File Offset: 0x0009206E
	public void SetCheatName(string v)
	{
		this.m_cheatName = v;
	}

	// Token: 0x06001CAC RID: 7340 RVA: 0x00093E77 File Offset: 0x00092077
	public void SetCanLoseStars(bool v)
	{
		this.m_canLoseStars = v;
	}

	// Token: 0x06001CAD RID: 7341 RVA: 0x00093E80 File Offset: 0x00092080
	public void SetCanLoseLevel(bool v)
	{
		this.m_canLoseLevel = v;
	}

	// Token: 0x06001CAE RID: 7342 RVA: 0x00093E89 File Offset: 0x00092089
	[Obsolete("Only used by Legacy system.")]
	public void SetMaxBestEverStarLevel(int v)
	{
		this.m_maxBestEverStarLevel = v;
	}

	// Token: 0x06001CAF RID: 7343 RVA: 0x00093E92 File Offset: 0x00092092
	public void SetWinStreakThreshold(int v)
	{
		this.m_winStreakThreshold = v;
	}

	// Token: 0x06001CB0 RID: 7344 RVA: 0x00093E9B File Offset: 0x0009209B
	public void SetRewardChestIdV1(int v)
	{
		this.m_rewardChestIdV1Id = v;
	}

	// Token: 0x06001CB1 RID: 7345 RVA: 0x00093EA4 File Offset: 0x000920A4
	public void SetRewardBagId(int v)
	{
		this.m_rewardBagId = v;
	}

	// Token: 0x06001CB2 RID: 7346 RVA: 0x00093EAD File Offset: 0x000920AD
	public void SetRewardChestVisualIndex(int v)
	{
		this.m_rewardChestVisualIndex = v;
	}

	// Token: 0x06001CB3 RID: 7347 RVA: 0x00093EB6 File Offset: 0x000920B6
	public void SetShowToastOnAttained(bool v)
	{
		this.m_showToastOnAttained = v;
	}

	// Token: 0x06001CB4 RID: 7348 RVA: 0x00093EBF File Offset: 0x000920BF
	public void SetShowOpponentRankInGame(bool v)
	{
		this.m_showOpponentRankInGame = v;
	}

	// Token: 0x06001CB5 RID: 7349 RVA: 0x00093EC8 File Offset: 0x000920C8
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2628940820U)
		{
			if (num <= 1458105184U)
			{
				if (num <= 177512838U)
				{
					if (num != 65678001U)
					{
						if (num == 177512838U)
						{
							if (name == "STARS")
							{
								return this.m_stars;
							}
						}
					}
					else if (name == "CAN_LOSE_STARS")
					{
						return this.m_canLoseStars;
					}
				}
				else if (num != 258923658U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return base.ID;
						}
					}
				}
				else if (name == "MEDAL_TEXT")
				{
					return this.m_medalText;
				}
			}
			else if (num <= 1800900461U)
			{
				if (num != 1655746952U)
				{
					if (num == 1800900461U)
					{
						if (name == "REWARD_BAG_ID")
						{
							return this.m_rewardBagId;
						}
					}
				}
				else if (name == "STAR_LEVEL")
				{
					return this.m_starLevel;
				}
			}
			else if (num != 1810449186U)
			{
				if (num != 2451523201U)
				{
					if (num == 2628940820U)
					{
						if (name == "REWARD_CHEST_ID_V1")
						{
							return this.m_rewardChestIdV1Id;
						}
					}
				}
				else if (name == "SHOW_TOAST_ON_ATTAINED")
				{
					return this.m_showToastOnAttained;
				}
			}
			else if (name == "CHEAT_NAME")
			{
				return this.m_cheatName;
			}
		}
		else if (num <= 3100184203U)
		{
			if (num <= 2748631019U)
			{
				if (num != 2639515172U)
				{
					if (num == 2748631019U)
					{
						if (name == "MAX_BEST_EVER_STAR_LEVEL")
						{
							return this.m_maxBestEverStarLevel;
						}
					}
				}
				else if (name == "MEDAL_MATERIAL")
				{
					return this.m_medalMaterial;
				}
			}
			else if (num != 2854743859U)
			{
				if (num != 2875816430U)
				{
					if (num == 3100184203U)
					{
						if (name == "RANK_NAME")
						{
							return this.m_rankName;
						}
					}
				}
				else if (name == "CAN_LOSE_LEVEL")
				{
					return this.m_canLoseLevel;
				}
			}
			else if (name == "SHOW_INDIVIDUAL_RANKING")
			{
				return this.m_showIndividualRanking;
			}
		}
		else if (num <= 3518094994U)
		{
			if (num != 3353298088U)
			{
				if (num == 3518094994U)
				{
					if (name == "REWARD_CHEST_VISUAL_INDEX")
					{
						return this.m_rewardChestVisualIndex;
					}
				}
			}
			else if (name == "LEAGUE_ID")
			{
				return this.m_leagueId;
			}
		}
		else if (num != 3826267352U)
		{
			if (num != 4013976586U)
			{
				if (num == 4137139096U)
				{
					if (name == "WIN_STREAK_THRESHOLD")
					{
						return this.m_winStreakThreshold;
					}
				}
			}
			else if (name == "SHOW_OPPONENT_RANK_IN_GAME")
			{
				return this.m_showOpponentRankInGame;
			}
		}
		else if (name == "MEDAL_TEXTURE")
		{
			return this.m_medalTexture;
		}
		return null;
	}

	// Token: 0x06001CB6 RID: 7350 RVA: 0x00094268 File Offset: 0x00092468
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2628940820U)
		{
			if (num <= 1458105184U)
			{
				if (num <= 177512838U)
				{
					if (num != 65678001U)
					{
						if (num != 177512838U)
						{
							return;
						}
						if (!(name == "STARS"))
						{
							return;
						}
						this.m_stars = (int)val;
						return;
					}
					else
					{
						if (!(name == "CAN_LOSE_STARS"))
						{
							return;
						}
						this.m_canLoseStars = (bool)val;
						return;
					}
				}
				else if (num != 258923658U)
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
					if (!(name == "MEDAL_TEXT"))
					{
						return;
					}
					this.m_medalText = (DbfLocValue)val;
					return;
				}
			}
			else if (num <= 1800900461U)
			{
				if (num != 1655746952U)
				{
					if (num != 1800900461U)
					{
						return;
					}
					if (!(name == "REWARD_BAG_ID"))
					{
						return;
					}
					this.m_rewardBagId = (int)val;
					return;
				}
				else
				{
					if (!(name == "STAR_LEVEL"))
					{
						return;
					}
					this.m_starLevel = (int)val;
					return;
				}
			}
			else if (num != 1810449186U)
			{
				if (num != 2451523201U)
				{
					if (num != 2628940820U)
					{
						return;
					}
					if (!(name == "REWARD_CHEST_ID_V1"))
					{
						return;
					}
					this.m_rewardChestIdV1Id = (int)val;
					return;
				}
				else
				{
					if (!(name == "SHOW_TOAST_ON_ATTAINED"))
					{
						return;
					}
					this.m_showToastOnAttained = (bool)val;
					return;
				}
			}
			else
			{
				if (!(name == "CHEAT_NAME"))
				{
					return;
				}
				this.m_cheatName = (string)val;
				return;
			}
		}
		else if (num <= 3100184203U)
		{
			if (num <= 2748631019U)
			{
				if (num != 2639515172U)
				{
					if (num != 2748631019U)
					{
						return;
					}
					if (!(name == "MAX_BEST_EVER_STAR_LEVEL"))
					{
						return;
					}
					this.m_maxBestEverStarLevel = (int)val;
					return;
				}
				else
				{
					if (!(name == "MEDAL_MATERIAL"))
					{
						return;
					}
					this.m_medalMaterial = (string)val;
					return;
				}
			}
			else if (num != 2854743859U)
			{
				if (num != 2875816430U)
				{
					if (num != 3100184203U)
					{
						return;
					}
					if (!(name == "RANK_NAME"))
					{
						return;
					}
					this.m_rankName = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "CAN_LOSE_LEVEL"))
					{
						return;
					}
					this.m_canLoseLevel = (bool)val;
					return;
				}
			}
			else
			{
				if (!(name == "SHOW_INDIVIDUAL_RANKING"))
				{
					return;
				}
				this.m_showIndividualRanking = (bool)val;
				return;
			}
		}
		else if (num <= 3518094994U)
		{
			if (num != 3353298088U)
			{
				if (num != 3518094994U)
				{
					return;
				}
				if (!(name == "REWARD_CHEST_VISUAL_INDEX"))
				{
					return;
				}
				this.m_rewardChestVisualIndex = (int)val;
				return;
			}
			else
			{
				if (!(name == "LEAGUE_ID"))
				{
					return;
				}
				this.m_leagueId = (int)val;
				return;
			}
		}
		else if (num != 3826267352U)
		{
			if (num != 4013976586U)
			{
				if (num != 4137139096U)
				{
					return;
				}
				if (!(name == "WIN_STREAK_THRESHOLD"))
				{
					return;
				}
				this.m_winStreakThreshold = (int)val;
				return;
			}
			else
			{
				if (!(name == "SHOW_OPPONENT_RANK_IN_GAME"))
				{
					return;
				}
				this.m_showOpponentRankInGame = (bool)val;
				return;
			}
		}
		else
		{
			if (!(name == "MEDAL_TEXTURE"))
			{
				return;
			}
			this.m_medalTexture = (string)val;
			return;
		}
	}

	// Token: 0x06001CB7 RID: 7351 RVA: 0x000945C4 File Offset: 0x000927C4
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2628940820U)
		{
			if (num <= 1458105184U)
			{
				if (num <= 177512838U)
				{
					if (num != 65678001U)
					{
						if (num == 177512838U)
						{
							if (name == "STARS")
							{
								return typeof(int);
							}
						}
					}
					else if (name == "CAN_LOSE_STARS")
					{
						return typeof(bool);
					}
				}
				else if (num != 258923658U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "MEDAL_TEXT")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (num <= 1800900461U)
			{
				if (num != 1655746952U)
				{
					if (num == 1800900461U)
					{
						if (name == "REWARD_BAG_ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "STAR_LEVEL")
				{
					return typeof(int);
				}
			}
			else if (num != 1810449186U)
			{
				if (num != 2451523201U)
				{
					if (num == 2628940820U)
					{
						if (name == "REWARD_CHEST_ID_V1")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "SHOW_TOAST_ON_ATTAINED")
				{
					return typeof(bool);
				}
			}
			else if (name == "CHEAT_NAME")
			{
				return typeof(string);
			}
		}
		else if (num <= 3100184203U)
		{
			if (num <= 2748631019U)
			{
				if (num != 2639515172U)
				{
					if (num == 2748631019U)
					{
						if (name == "MAX_BEST_EVER_STAR_LEVEL")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "MEDAL_MATERIAL")
				{
					return typeof(string);
				}
			}
			else if (num != 2854743859U)
			{
				if (num != 2875816430U)
				{
					if (num == 3100184203U)
					{
						if (name == "RANK_NAME")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "CAN_LOSE_LEVEL")
				{
					return typeof(bool);
				}
			}
			else if (name == "SHOW_INDIVIDUAL_RANKING")
			{
				return typeof(bool);
			}
		}
		else if (num <= 3518094994U)
		{
			if (num != 3353298088U)
			{
				if (num == 3518094994U)
				{
					if (name == "REWARD_CHEST_VISUAL_INDEX")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "LEAGUE_ID")
			{
				return typeof(int);
			}
		}
		else if (num != 3826267352U)
		{
			if (num != 4013976586U)
			{
				if (num == 4137139096U)
				{
					if (name == "WIN_STREAK_THRESHOLD")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "SHOW_OPPONENT_RANK_IN_GAME")
			{
				return typeof(bool);
			}
		}
		else if (name == "MEDAL_TEXTURE")
		{
			return typeof(string);
		}
		return null;
	}

	// Token: 0x06001CB8 RID: 7352 RVA: 0x0009496A File Offset: 0x00092B6A
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadLeagueRankDbfRecords loadRecords = new LoadLeagueRankDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001CB9 RID: 7353 RVA: 0x00094980 File Offset: 0x00092B80
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		LeagueRankDbfAsset leagueRankDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(LeagueRankDbfAsset)) as LeagueRankDbfAsset;
		if (leagueRankDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("LeagueRankDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < leagueRankDbfAsset.Records.Count; i++)
		{
			leagueRankDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (leagueRankDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001CBA RID: 7354 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001CBB RID: 7355 RVA: 0x000949FF File Offset: 0x00092BFF
	public override void StripUnusedLocales()
	{
		this.m_rankName.StripUnusedLocales();
		this.m_medalText.StripUnusedLocales();
	}

	// Token: 0x040010FE RID: 4350
	[SerializeField]
	private int m_leagueId;

	// Token: 0x040010FF RID: 4351
	[SerializeField]
	private int m_starLevel;

	// Token: 0x04001100 RID: 4352
	[SerializeField]
	private int m_stars;

	// Token: 0x04001101 RID: 4353
	[SerializeField]
	private bool m_showIndividualRanking;

	// Token: 0x04001102 RID: 4354
	[SerializeField]
	private DbfLocValue m_rankName;

	// Token: 0x04001103 RID: 4355
	[SerializeField]
	private DbfLocValue m_medalText;

	// Token: 0x04001104 RID: 4356
	[SerializeField]
	private string m_medalTexture;

	// Token: 0x04001105 RID: 4357
	[SerializeField]
	private string m_medalMaterial;

	// Token: 0x04001106 RID: 4358
	[SerializeField]
	private string m_cheatName;

	// Token: 0x04001107 RID: 4359
	[SerializeField]
	private bool m_canLoseStars;

	// Token: 0x04001108 RID: 4360
	[SerializeField]
	private bool m_canLoseLevel;

	// Token: 0x04001109 RID: 4361
	[SerializeField]
	private int m_maxBestEverStarLevel;

	// Token: 0x0400110A RID: 4362
	[SerializeField]
	private int m_winStreakThreshold;

	// Token: 0x0400110B RID: 4363
	[SerializeField]
	private int m_rewardChestIdV1Id;

	// Token: 0x0400110C RID: 4364
	[SerializeField]
	private int m_rewardBagId;

	// Token: 0x0400110D RID: 4365
	[SerializeField]
	private int m_rewardChestVisualIndex;

	// Token: 0x0400110E RID: 4366
	[SerializeField]
	private bool m_showToastOnAttained;

	// Token: 0x0400110F RID: 4367
	[SerializeField]
	private bool m_showOpponentRankInGame;
}
