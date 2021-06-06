using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000202 RID: 514
[Serializable]
public class LeagueDbfRecord : DbfRecord
{
	// Token: 0x17000328 RID: 808
	// (get) Token: 0x06001C46 RID: 7238 RVA: 0x00092B2A File Offset: 0x00090D2A
	[DbfField("LEAGUE_TYPE")]
	public League.LeagueType LeagueType
	{
		get
		{
			return this.m_leagueType;
		}
	}

	// Token: 0x17000329 RID: 809
	// (get) Token: 0x06001C47 RID: 7239 RVA: 0x00092B32 File Offset: 0x00090D32
	[DbfField("LEAGUE_LEVEL")]
	public int LeagueLevel
	{
		get
		{
			return this.m_leagueLevel;
		}
	}

	// Token: 0x1700032A RID: 810
	// (get) Token: 0x06001C48 RID: 7240 RVA: 0x00092B3A File Offset: 0x00090D3A
	[DbfField("LEAGUE_VERSION")]
	public int LeagueVersion
	{
		get
		{
			return this.m_leagueVersion;
		}
	}

	// Token: 0x1700032B RID: 811
	// (get) Token: 0x06001C49 RID: 7241 RVA: 0x00092B42 File Offset: 0x00090D42
	[DbfField("INITIAL_SEASON_ID")]
	public int InitialSeasonId
	{
		get
		{
			return this.m_initialSeasonId;
		}
	}

	// Token: 0x1700032C RID: 812
	// (get) Token: 0x06001C4A RID: 7242 RVA: 0x00092B4A File Offset: 0x00090D4A
	[DbfField("PROMOTE_TO_LEAGUE_TYPE")]
	public League.LeagueType PromoteToLeagueType
	{
		get
		{
			return this.m_promoteToLeagueType;
		}
	}

	// Token: 0x1700032D RID: 813
	// (get) Token: 0x06001C4B RID: 7243 RVA: 0x00092B52 File Offset: 0x00090D52
	[DbfField("CAN_PROMOTE_SELF_MANUALLY")]
	public bool CanPromoteSelfManually
	{
		get
		{
			return this.m_canPromoteSelfManually;
		}
	}

	// Token: 0x1700032E RID: 814
	// (get) Token: 0x06001C4C RID: 7244 RVA: 0x00092B5A File Offset: 0x00090D5A
	[DbfField("LOCK_WILD_BOOSTERS")]
	public bool LockWildBoosters
	{
		get
		{
			return this.m_lockWildBoosters;
		}
	}

	// Token: 0x1700032F RID: 815
	// (get) Token: 0x06001C4D RID: 7245 RVA: 0x00092B62 File Offset: 0x00090D62
	[DbfField("LOCK_WILD_CARDS")]
	public bool LockWildCards
	{
		get
		{
			return this.m_lockWildCards;
		}
	}

	// Token: 0x17000330 RID: 816
	// (get) Token: 0x06001C4E RID: 7246 RVA: 0x00092B6A File Offset: 0x00090D6A
	[DbfField("LOCK_CARDS_FROM_SUBSET_ID")]
	public int LockCardsFromSubsetId
	{
		get
		{
			return this.m_lockCardsFromSubsetId;
		}
	}

	// Token: 0x17000331 RID: 817
	// (get) Token: 0x06001C4F RID: 7247 RVA: 0x00092B72 File Offset: 0x00090D72
	public SubsetDbfRecord LockCardsFromSubsetRecord
	{
		get
		{
			return GameDbf.Subset.GetRecord(this.m_lockCardsFromSubsetId);
		}
	}

	// Token: 0x17000332 RID: 818
	// (get) Token: 0x06001C50 RID: 7248 RVA: 0x00092B84 File Offset: 0x00090D84
	[DbfField("LOCKED_BOOSTER_TEXT")]
	public DbfLocValue LockedBoosterText
	{
		get
		{
			return this.m_lockedBoosterText;
		}
	}

	// Token: 0x17000333 RID: 819
	// (get) Token: 0x06001C51 RID: 7249 RVA: 0x00092B8C File Offset: 0x00090D8C
	[DbfField("LOCKED_CARD_UNPLAYABLE_TEXT")]
	public DbfLocValue LockedCardUnplayableText
	{
		get
		{
			return this.m_lockedCardUnplayableText;
		}
	}

	// Token: 0x17000334 RID: 820
	// (get) Token: 0x06001C52 RID: 7250 RVA: 0x00092B94 File Offset: 0x00090D94
	[DbfField("LOCKED_CARD_POPUP_TITLE_TEXT")]
	public DbfLocValue LockedCardPopupTitleText
	{
		get
		{
			return this.m_lockedCardPopupTitleText;
		}
	}

	// Token: 0x17000335 RID: 821
	// (get) Token: 0x06001C53 RID: 7251 RVA: 0x00092B9C File Offset: 0x00090D9C
	[DbfField("LOCKED_CARD_POPUP_BODY_TEXT")]
	public DbfLocValue LockedCardPopupBodyText
	{
		get
		{
			return this.m_lockedCardPopupBodyText;
		}
	}

	// Token: 0x17000336 RID: 822
	// (get) Token: 0x06001C54 RID: 7252 RVA: 0x00092BA4 File Offset: 0x00090DA4
	[DbfField("SEASON_ROLL_REWARD_MIN_WINS")]
	public int SeasonRollRewardMinWins
	{
		get
		{
			return this.m_seasonRollRewardMinWins;
		}
	}

	// Token: 0x17000337 RID: 823
	// (get) Token: 0x06001C55 RID: 7253 RVA: 0x00092BAC File Offset: 0x00090DAC
	[DbfField("SEASON_END_REWARD_CHEST_ID")]
	public int SeasonEndRewardChestId
	{
		get
		{
			return this.m_seasonEndRewardChestId;
		}
	}

	// Token: 0x17000338 RID: 824
	// (get) Token: 0x06001C56 RID: 7254 RVA: 0x00092BB4 File Offset: 0x00090DB4
	public RewardChestDbfRecord SeasonEndRewardChestRecord
	{
		get
		{
			return GameDbf.RewardChest.GetRecord(this.m_seasonEndRewardChestId);
		}
	}

	// Token: 0x17000339 RID: 825
	// (get) Token: 0x06001C57 RID: 7255 RVA: 0x00092BC6 File Offset: 0x00090DC6
	[DbfField("SEASON_CARD_BACK_MIN_WINS")]
	public int SeasonCardBackMinWins
	{
		get
		{
			return this.m_seasonCardBackMinWins;
		}
	}

	// Token: 0x1700033A RID: 826
	// (get) Token: 0x06001C58 RID: 7256 RVA: 0x00092BCE File Offset: 0x00090DCE
	[DbfField("RANKED_INTRO_SEEN_REQUIREMENT")]
	public int RankedIntroSeenRequirement
	{
		get
		{
			return this.m_rankedIntroSeenRequirement;
		}
	}

	// Token: 0x1700033B RID: 827
	// (get) Token: 0x06001C59 RID: 7257 RVA: 0x00092BD6 File Offset: 0x00090DD6
	[DbfField("BONUS_STARS_POPUP_SEEN_REQUIREMENT")]
	public int BonusStarsPopupSeenRequirement
	{
		get
		{
			return this.m_bonusStarsPopupSeenRequirement;
		}
	}

	// Token: 0x1700033C RID: 828
	// (get) Token: 0x06001C5A RID: 7258 RVA: 0x00092BDE File Offset: 0x00090DDE
	[DbfField("REWARDS_VERSION")]
	public int RewardsVersion
	{
		get
		{
			return this.m_rewardsVersion;
		}
	}

	// Token: 0x1700033D RID: 829
	// (get) Token: 0x06001C5B RID: 7259 RVA: 0x00092BE6 File Offset: 0x00090DE6
	public List<LeagueGameTypeDbfRecord> LeagueGameType
	{
		get
		{
			return GameDbf.LeagueGameType.GetRecords((LeagueGameTypeDbfRecord r) => r.LeagueId == base.ID, -1);
		}
	}

	// Token: 0x1700033E RID: 830
	// (get) Token: 0x06001C5C RID: 7260 RVA: 0x00092BFF File Offset: 0x00090DFF
	public List<LeagueRankDbfRecord> Ranks
	{
		get
		{
			return GameDbf.LeagueRank.GetRecords((LeagueRankDbfRecord r) => r.LeagueId == base.ID, -1);
		}
	}

	// Token: 0x06001C5D RID: 7261 RVA: 0x00092C18 File Offset: 0x00090E18
	public void SetLeagueType(League.LeagueType v)
	{
		this.m_leagueType = v;
	}

	// Token: 0x06001C5E RID: 7262 RVA: 0x00092C21 File Offset: 0x00090E21
	public void SetLeagueLevel(int v)
	{
		this.m_leagueLevel = v;
	}

	// Token: 0x06001C5F RID: 7263 RVA: 0x00092C2A File Offset: 0x00090E2A
	public void SetLeagueVersion(int v)
	{
		this.m_leagueVersion = v;
	}

	// Token: 0x06001C60 RID: 7264 RVA: 0x00092C33 File Offset: 0x00090E33
	public void SetInitialSeasonId(int v)
	{
		this.m_initialSeasonId = v;
	}

	// Token: 0x06001C61 RID: 7265 RVA: 0x00092C3C File Offset: 0x00090E3C
	public void SetPromoteToLeagueType(League.LeagueType v)
	{
		this.m_promoteToLeagueType = v;
	}

	// Token: 0x06001C62 RID: 7266 RVA: 0x00092C45 File Offset: 0x00090E45
	public void SetCanPromoteSelfManually(bool v)
	{
		this.m_canPromoteSelfManually = v;
	}

	// Token: 0x06001C63 RID: 7267 RVA: 0x00092C4E File Offset: 0x00090E4E
	public void SetLockWildBoosters(bool v)
	{
		this.m_lockWildBoosters = v;
	}

	// Token: 0x06001C64 RID: 7268 RVA: 0x00092C57 File Offset: 0x00090E57
	public void SetLockWildCards(bool v)
	{
		this.m_lockWildCards = v;
	}

	// Token: 0x06001C65 RID: 7269 RVA: 0x00092C60 File Offset: 0x00090E60
	public void SetLockCardsFromSubsetId(int v)
	{
		this.m_lockCardsFromSubsetId = v;
	}

	// Token: 0x06001C66 RID: 7270 RVA: 0x00092C69 File Offset: 0x00090E69
	public void SetLockedBoosterText(DbfLocValue v)
	{
		this.m_lockedBoosterText = v;
		v.SetDebugInfo(base.ID, "LOCKED_BOOSTER_TEXT");
	}

	// Token: 0x06001C67 RID: 7271 RVA: 0x00092C83 File Offset: 0x00090E83
	public void SetLockedCardUnplayableText(DbfLocValue v)
	{
		this.m_lockedCardUnplayableText = v;
		v.SetDebugInfo(base.ID, "LOCKED_CARD_UNPLAYABLE_TEXT");
	}

	// Token: 0x06001C68 RID: 7272 RVA: 0x00092C9D File Offset: 0x00090E9D
	public void SetLockedCardPopupTitleText(DbfLocValue v)
	{
		this.m_lockedCardPopupTitleText = v;
		v.SetDebugInfo(base.ID, "LOCKED_CARD_POPUP_TITLE_TEXT");
	}

	// Token: 0x06001C69 RID: 7273 RVA: 0x00092CB7 File Offset: 0x00090EB7
	public void SetLockedCardPopupBodyText(DbfLocValue v)
	{
		this.m_lockedCardPopupBodyText = v;
		v.SetDebugInfo(base.ID, "LOCKED_CARD_POPUP_BODY_TEXT");
	}

	// Token: 0x06001C6A RID: 7274 RVA: 0x00092CD1 File Offset: 0x00090ED1
	public void SetSeasonRollRewardMinWins(int v)
	{
		this.m_seasonRollRewardMinWins = v;
	}

	// Token: 0x06001C6B RID: 7275 RVA: 0x00092CDA File Offset: 0x00090EDA
	public void SetSeasonEndRewardChestId(int v)
	{
		this.m_seasonEndRewardChestId = v;
	}

	// Token: 0x06001C6C RID: 7276 RVA: 0x00092CE3 File Offset: 0x00090EE3
	public void SetSeasonCardBackMinWins(int v)
	{
		this.m_seasonCardBackMinWins = v;
	}

	// Token: 0x06001C6D RID: 7277 RVA: 0x00092CEC File Offset: 0x00090EEC
	public void SetRankedIntroSeenRequirement(int v)
	{
		this.m_rankedIntroSeenRequirement = v;
	}

	// Token: 0x06001C6E RID: 7278 RVA: 0x00092CF5 File Offset: 0x00090EF5
	public void SetBonusStarsPopupSeenRequirement(int v)
	{
		this.m_bonusStarsPopupSeenRequirement = v;
	}

	// Token: 0x06001C6F RID: 7279 RVA: 0x00092CFE File Offset: 0x00090EFE
	public void SetRewardsVersion(int v)
	{
		this.m_rewardsVersion = v;
	}

	// Token: 0x06001C70 RID: 7280 RVA: 0x00092D08 File Offset: 0x00090F08
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1701516602U)
		{
			if (num <= 849258464U)
			{
				if (num <= 361397132U)
				{
					if (num != 132552325U)
					{
						if (num == 361397132U)
						{
							if (name == "LOCKED_CARD_POPUP_TITLE_TEXT")
							{
								return this.m_lockedCardPopupTitleText;
							}
						}
					}
					else if (name == "LEAGUE_LEVEL")
					{
						return this.m_leagueLevel;
					}
				}
				else if (num != 746782651U)
				{
					if (num != 845898672U)
					{
						if (num == 849258464U)
						{
							if (name == "PROMOTE_TO_LEAGUE_TYPE")
							{
								return this.m_promoteToLeagueType;
							}
						}
					}
					else if (name == "REWARDS_VERSION")
					{
						return this.m_rewardsVersion;
					}
				}
				else if (name == "LOCK_WILD_CARDS")
				{
					return this.m_lockWildCards;
				}
			}
			else if (num <= 1238047468U)
			{
				if (num != 953089362U)
				{
					if (num == 1238047468U)
					{
						if (name == "LOCKED_CARD_UNPLAYABLE_TEXT")
						{
							return this.m_lockedCardUnplayableText;
						}
					}
				}
				else if (name == "LOCK_CARDS_FROM_SUBSET_ID")
				{
					return this.m_lockCardsFromSubsetId;
				}
			}
			else if (num != 1458105184U)
			{
				if (num != 1682456859U)
				{
					if (num == 1701516602U)
					{
						if (name == "LOCKED_CARD_POPUP_BODY_TEXT")
						{
							return this.m_lockedCardPopupBodyText;
						}
					}
				}
				else if (name == "RANKED_INTRO_SEEN_REQUIREMENT")
				{
					return this.m_rankedIntroSeenRequirement;
				}
			}
			else if (name == "ID")
			{
				return base.ID;
			}
		}
		else if (num <= 2657574562U)
		{
			if (num <= 2290669199U)
			{
				if (num != 1949384501U)
				{
					if (num == 2290669199U)
					{
						if (name == "INITIAL_SEASON_ID")
						{
							return this.m_initialSeasonId;
						}
					}
				}
				else if (name == "LEAGUE_TYPE")
				{
					return this.m_leagueType;
				}
			}
			else if (num != 2303752987U)
			{
				if (num != 2526651862U)
				{
					if (num == 2657574562U)
					{
						if (name == "LOCKED_BOOSTER_TEXT")
						{
							return this.m_lockedBoosterText;
						}
					}
				}
				else if (name == "SEASON_END_REWARD_CHEST_ID")
				{
					return this.m_seasonEndRewardChestId;
				}
			}
			else if (name == "BONUS_STARS_POPUP_SEEN_REQUIREMENT")
			{
				return this.m_bonusStarsPopupSeenRequirement;
			}
		}
		else if (num <= 3426902054U)
		{
			if (num != 3281464495U)
			{
				if (num == 3426902054U)
				{
					if (name == "SEASON_CARD_BACK_MIN_WINS")
					{
						return this.m_seasonCardBackMinWins;
					}
				}
			}
			else if (name == "LEAGUE_VERSION")
			{
				return this.m_leagueVersion;
			}
		}
		else if (num != 3511510115U)
		{
			if (num != 3534282901U)
			{
				if (num == 3884215779U)
				{
					if (name == "SEASON_ROLL_REWARD_MIN_WINS")
					{
						return this.m_seasonRollRewardMinWins;
					}
				}
			}
			else if (name == "LOCK_WILD_BOOSTERS")
			{
				return this.m_lockWildBoosters;
			}
		}
		else if (name == "CAN_PROMOTE_SELF_MANUALLY")
		{
			return this.m_canPromoteSelfManually;
		}
		return null;
	}

	// Token: 0x06001C71 RID: 7281 RVA: 0x000930DC File Offset: 0x000912DC
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1701516602U)
		{
			if (num <= 849258464U)
			{
				if (num <= 361397132U)
				{
					if (num != 132552325U)
					{
						if (num != 361397132U)
						{
							return;
						}
						if (!(name == "LOCKED_CARD_POPUP_TITLE_TEXT"))
						{
							return;
						}
						this.m_lockedCardPopupTitleText = (DbfLocValue)val;
						return;
					}
					else
					{
						if (!(name == "LEAGUE_LEVEL"))
						{
							return;
						}
						this.m_leagueLevel = (int)val;
						return;
					}
				}
				else if (num != 746782651U)
				{
					if (num != 845898672U)
					{
						if (num != 849258464U)
						{
							return;
						}
						if (!(name == "PROMOTE_TO_LEAGUE_TYPE"))
						{
							return;
						}
						if (val == null)
						{
							this.m_promoteToLeagueType = League.LeagueType.UNKNOWN;
							return;
						}
						if (val is League.LeagueType || val is int)
						{
							this.m_promoteToLeagueType = (League.LeagueType)val;
							return;
						}
						if (val is string)
						{
							this.m_promoteToLeagueType = League.ParseLeagueTypeValue((string)val);
							return;
						}
					}
					else
					{
						if (!(name == "REWARDS_VERSION"))
						{
							return;
						}
						this.m_rewardsVersion = (int)val;
					}
				}
				else
				{
					if (!(name == "LOCK_WILD_CARDS"))
					{
						return;
					}
					this.m_lockWildCards = (bool)val;
					return;
				}
			}
			else if (num <= 1238047468U)
			{
				if (num != 953089362U)
				{
					if (num != 1238047468U)
					{
						return;
					}
					if (!(name == "LOCKED_CARD_UNPLAYABLE_TEXT"))
					{
						return;
					}
					this.m_lockedCardUnplayableText = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "LOCK_CARDS_FROM_SUBSET_ID"))
					{
						return;
					}
					this.m_lockCardsFromSubsetId = (int)val;
					return;
				}
			}
			else if (num != 1458105184U)
			{
				if (num != 1682456859U)
				{
					if (num != 1701516602U)
					{
						return;
					}
					if (!(name == "LOCKED_CARD_POPUP_BODY_TEXT"))
					{
						return;
					}
					this.m_lockedCardPopupBodyText = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "RANKED_INTRO_SEEN_REQUIREMENT"))
					{
						return;
					}
					this.m_rankedIntroSeenRequirement = (int)val;
					return;
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
		else if (num <= 2657574562U)
		{
			if (num <= 2290669199U)
			{
				if (num != 1949384501U)
				{
					if (num != 2290669199U)
					{
						return;
					}
					if (!(name == "INITIAL_SEASON_ID"))
					{
						return;
					}
					this.m_initialSeasonId = (int)val;
					return;
				}
				else
				{
					if (!(name == "LEAGUE_TYPE"))
					{
						return;
					}
					if (val == null)
					{
						this.m_leagueType = League.LeagueType.UNKNOWN;
						return;
					}
					if (val is League.LeagueType || val is int)
					{
						this.m_leagueType = (League.LeagueType)val;
						return;
					}
					if (val is string)
					{
						this.m_leagueType = League.ParseLeagueTypeValue((string)val);
						return;
					}
				}
			}
			else if (num != 2303752987U)
			{
				if (num != 2526651862U)
				{
					if (num != 2657574562U)
					{
						return;
					}
					if (!(name == "LOCKED_BOOSTER_TEXT"))
					{
						return;
					}
					this.m_lockedBoosterText = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "SEASON_END_REWARD_CHEST_ID"))
					{
						return;
					}
					this.m_seasonEndRewardChestId = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "BONUS_STARS_POPUP_SEEN_REQUIREMENT"))
				{
					return;
				}
				this.m_bonusStarsPopupSeenRequirement = (int)val;
				return;
			}
		}
		else if (num <= 3426902054U)
		{
			if (num != 3281464495U)
			{
				if (num != 3426902054U)
				{
					return;
				}
				if (!(name == "SEASON_CARD_BACK_MIN_WINS"))
				{
					return;
				}
				this.m_seasonCardBackMinWins = (int)val;
				return;
			}
			else
			{
				if (!(name == "LEAGUE_VERSION"))
				{
					return;
				}
				this.m_leagueVersion = (int)val;
				return;
			}
		}
		else if (num != 3511510115U)
		{
			if (num != 3534282901U)
			{
				if (num != 3884215779U)
				{
					return;
				}
				if (!(name == "SEASON_ROLL_REWARD_MIN_WINS"))
				{
					return;
				}
				this.m_seasonRollRewardMinWins = (int)val;
				return;
			}
			else
			{
				if (!(name == "LOCK_WILD_BOOSTERS"))
				{
					return;
				}
				this.m_lockWildBoosters = (bool)val;
				return;
			}
		}
		else
		{
			if (!(name == "CAN_PROMOTE_SELF_MANUALLY"))
			{
				return;
			}
			this.m_canPromoteSelfManually = (bool)val;
			return;
		}
	}

	// Token: 0x06001C72 RID: 7282 RVA: 0x000934D0 File Offset: 0x000916D0
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1701516602U)
		{
			if (num <= 849258464U)
			{
				if (num <= 361397132U)
				{
					if (num != 132552325U)
					{
						if (num == 361397132U)
						{
							if (name == "LOCKED_CARD_POPUP_TITLE_TEXT")
							{
								return typeof(DbfLocValue);
							}
						}
					}
					else if (name == "LEAGUE_LEVEL")
					{
						return typeof(int);
					}
				}
				else if (num != 746782651U)
				{
					if (num != 845898672U)
					{
						if (num == 849258464U)
						{
							if (name == "PROMOTE_TO_LEAGUE_TYPE")
							{
								return typeof(League.LeagueType);
							}
						}
					}
					else if (name == "REWARDS_VERSION")
					{
						return typeof(int);
					}
				}
				else if (name == "LOCK_WILD_CARDS")
				{
					return typeof(bool);
				}
			}
			else if (num <= 1238047468U)
			{
				if (num != 953089362U)
				{
					if (num == 1238047468U)
					{
						if (name == "LOCKED_CARD_UNPLAYABLE_TEXT")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "LOCK_CARDS_FROM_SUBSET_ID")
				{
					return typeof(int);
				}
			}
			else if (num != 1458105184U)
			{
				if (num != 1682456859U)
				{
					if (num == 1701516602U)
					{
						if (name == "LOCKED_CARD_POPUP_BODY_TEXT")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "RANKED_INTRO_SEEN_REQUIREMENT")
				{
					return typeof(int);
				}
			}
			else if (name == "ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 2657574562U)
		{
			if (num <= 2290669199U)
			{
				if (num != 1949384501U)
				{
					if (num == 2290669199U)
					{
						if (name == "INITIAL_SEASON_ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "LEAGUE_TYPE")
				{
					return typeof(League.LeagueType);
				}
			}
			else if (num != 2303752987U)
			{
				if (num != 2526651862U)
				{
					if (num == 2657574562U)
					{
						if (name == "LOCKED_BOOSTER_TEXT")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "SEASON_END_REWARD_CHEST_ID")
				{
					return typeof(int);
				}
			}
			else if (name == "BONUS_STARS_POPUP_SEEN_REQUIREMENT")
			{
				return typeof(int);
			}
		}
		else if (num <= 3426902054U)
		{
			if (num != 3281464495U)
			{
				if (num == 3426902054U)
				{
					if (name == "SEASON_CARD_BACK_MIN_WINS")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "LEAGUE_VERSION")
			{
				return typeof(int);
			}
		}
		else if (num != 3511510115U)
		{
			if (num != 3534282901U)
			{
				if (num == 3884215779U)
				{
					if (name == "SEASON_ROLL_REWARD_MIN_WINS")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "LOCK_WILD_BOOSTERS")
			{
				return typeof(bool);
			}
		}
		else if (name == "CAN_PROMOTE_SELF_MANUALLY")
		{
			return typeof(bool);
		}
		return null;
	}

	// Token: 0x06001C73 RID: 7283 RVA: 0x000938A1 File Offset: 0x00091AA1
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadLeagueDbfRecords loadRecords = new LoadLeagueDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001C74 RID: 7284 RVA: 0x000938B8 File Offset: 0x00091AB8
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		LeagueDbfAsset leagueDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(LeagueDbfAsset)) as LeagueDbfAsset;
		if (leagueDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("LeagueDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < leagueDbfAsset.Records.Count; i++)
		{
			leagueDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (leagueDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001C75 RID: 7285 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001C76 RID: 7286 RVA: 0x00093937 File Offset: 0x00091B37
	public override void StripUnusedLocales()
	{
		this.m_lockedBoosterText.StripUnusedLocales();
		this.m_lockedCardUnplayableText.StripUnusedLocales();
		this.m_lockedCardPopupTitleText.StripUnusedLocales();
		this.m_lockedCardPopupBodyText.StripUnusedLocales();
	}

	// Token: 0x040010E4 RID: 4324
	[SerializeField]
	private League.LeagueType m_leagueType = League.ParseLeagueTypeValue("unknown");

	// Token: 0x040010E5 RID: 4325
	[SerializeField]
	private int m_leagueLevel;

	// Token: 0x040010E6 RID: 4326
	[SerializeField]
	private int m_leagueVersion;

	// Token: 0x040010E7 RID: 4327
	[SerializeField]
	private int m_initialSeasonId;

	// Token: 0x040010E8 RID: 4328
	[SerializeField]
	private League.LeagueType m_promoteToLeagueType;

	// Token: 0x040010E9 RID: 4329
	[SerializeField]
	private bool m_canPromoteSelfManually;

	// Token: 0x040010EA RID: 4330
	[SerializeField]
	private bool m_lockWildBoosters;

	// Token: 0x040010EB RID: 4331
	[SerializeField]
	private bool m_lockWildCards;

	// Token: 0x040010EC RID: 4332
	[SerializeField]
	private int m_lockCardsFromSubsetId;

	// Token: 0x040010ED RID: 4333
	[SerializeField]
	private DbfLocValue m_lockedBoosterText;

	// Token: 0x040010EE RID: 4334
	[SerializeField]
	private DbfLocValue m_lockedCardUnplayableText;

	// Token: 0x040010EF RID: 4335
	[SerializeField]
	private DbfLocValue m_lockedCardPopupTitleText;

	// Token: 0x040010F0 RID: 4336
	[SerializeField]
	private DbfLocValue m_lockedCardPopupBodyText;

	// Token: 0x040010F1 RID: 4337
	[SerializeField]
	private int m_seasonRollRewardMinWins;

	// Token: 0x040010F2 RID: 4338
	[SerializeField]
	private int m_seasonEndRewardChestId;

	// Token: 0x040010F3 RID: 4339
	[SerializeField]
	private int m_seasonCardBackMinWins;

	// Token: 0x040010F4 RID: 4340
	[SerializeField]
	private int m_rankedIntroSeenRequirement;

	// Token: 0x040010F5 RID: 4341
	[SerializeField]
	private int m_bonusStarsPopupSeenRequirement;

	// Token: 0x040010F6 RID: 4342
	[SerializeField]
	private int m_rewardsVersion;
}
