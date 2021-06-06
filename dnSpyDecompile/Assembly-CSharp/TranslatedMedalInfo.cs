using System;
using Assets;
using Hearthstone.DataModels;
using PegasusShared;
using UnityEngine;

// Token: 0x02000641 RID: 1601
public class TranslatedMedalInfo
{
	// Token: 0x17000548 RID: 1352
	// (get) Token: 0x06005A38 RID: 23096 RVA: 0x001D733E File Offset: 0x001D553E
	public LeagueDbfRecord LeagueConfig
	{
		get
		{
			return RankMgr.Get().GetLeagueRecord(this.leagueId);
		}
	}

	// Token: 0x17000549 RID: 1353
	// (get) Token: 0x06005A39 RID: 23097 RVA: 0x001D7350 File Offset: 0x001D5550
	public LeagueRankDbfRecord RankConfig
	{
		get
		{
			return RankMgr.Get().GetLeagueRankRecord(this.leagueId, this.starLevel);
		}
	}

	// Token: 0x06005A3A RID: 23098 RVA: 0x001D7368 File Offset: 0x001D5568
	public TranslatedMedalInfo ShallowCopy()
	{
		return base.MemberwiseClone() as TranslatedMedalInfo;
	}

	// Token: 0x06005A3B RID: 23099 RVA: 0x001D7375 File Offset: 0x001D5575
	public bool IsLegendRank()
	{
		return this.RankConfig.ShowIndividualRanking;
	}

	// Token: 0x06005A3C RID: 23100 RVA: 0x001D7382 File Offset: 0x001D5582
	public bool IsNewPlayer()
	{
		return this.LeagueConfig.LeagueType == League.LeagueType.NEW_PLAYER;
	}

	// Token: 0x06005A3D RID: 23101 RVA: 0x001D7392 File Offset: 0x001D5592
	public FormatType GetFormatType()
	{
		return this.format;
	}

	// Token: 0x06005A3E RID: 23102 RVA: 0x001D739A File Offset: 0x001D559A
	public bool CanLoseStars()
	{
		return this.RankConfig.CanLoseStars;
	}

	// Token: 0x06005A3F RID: 23103 RVA: 0x001D73A7 File Offset: 0x001D55A7
	public bool CanLoseLevel()
	{
		return this.RankConfig.CanLoseLevel;
	}

	// Token: 0x06005A40 RID: 23104 RVA: 0x001D73B4 File Offset: 0x001D55B4
	public string GetRankName()
	{
		if (this.RankConfig.RankName != null)
		{
			return this.RankConfig.RankName.GetString(true);
		}
		return string.Empty;
	}

	// Token: 0x06005A41 RID: 23105 RVA: 0x001D73DA File Offset: 0x001D55DA
	public string GetMedalText()
	{
		if (this.RankConfig.MedalText != null)
		{
			return this.RankConfig.MedalText.GetString(true);
		}
		return string.Empty;
	}

	// Token: 0x06005A42 RID: 23106 RVA: 0x001D7400 File Offset: 0x001D5600
	public int GetMaxStarsAtRank()
	{
		return this.RankConfig.Stars;
	}

	// Token: 0x06005A43 RID: 23107 RVA: 0x001D740D File Offset: 0x001D560D
	public bool IsChestAwardedAtRank()
	{
		return this.RankConfig.RewardChestIdV1 > 0;
	}

	// Token: 0x06005A44 RID: 23108 RVA: 0x001D741D File Offset: 0x001D561D
	public int GetMaxStarLevel()
	{
		return RankMgr.Get().GetMaxStarLevel(this.leagueId);
	}

	// Token: 0x06005A45 RID: 23109 RVA: 0x001D742F File Offset: 0x001D562F
	public void CreateOrUpdateDataModel(ref RankedPlayDataModel dataModel, RankedMedal.DisplayMode mode, bool isTooltipEnabled = false, bool hasEarnedCardBack = false, Action<RankedPlayDataModel> dataModelReadyCallback = null)
	{
		if (dataModel == null)
		{
			dataModel = this.CreateDataModel(mode, isTooltipEnabled, hasEarnedCardBack, dataModelReadyCallback);
			return;
		}
		this.UpdateDataModel(dataModel, mode, isTooltipEnabled, hasEarnedCardBack, dataModelReadyCallback);
	}

	// Token: 0x06005A46 RID: 23110 RVA: 0x001D7454 File Offset: 0x001D5654
	public RankedPlayDataModel CreateDataModel(RankedMedal.DisplayMode mode, bool isTooltipEnabled = false, bool hasEarnedCardBack = false, Action<RankedPlayDataModel> dataModelReadyCallback = null)
	{
		RankedPlayDataModel rankedPlayDataModel = new RankedPlayDataModel();
		this.UpdateDataModel(rankedPlayDataModel, mode, isTooltipEnabled, hasEarnedCardBack, dataModelReadyCallback);
		return rankedPlayDataModel;
	}

	// Token: 0x06005A47 RID: 23111 RVA: 0x001D7474 File Offset: 0x001D5674
	public void UpdateDataModel(RankedPlayDataModel dataModel, RankedMedal.DisplayMode mode, bool isTooltipEnabled, bool hasEarnedCardBack, Action<RankedPlayDataModel> dataModelReadyCallback)
	{
		if (dataModel == null)
		{
			Debug.LogError("TranslatedMedalInfo.UpdateDataModel - ranked play data model was null!");
			return;
		}
		dataModel.DisplayMode = mode;
		dataModel.IsTooltipEnabled = isTooltipEnabled;
		dataModel.HasEarnedCardBack = hasEarnedCardBack;
		dataModel.Stars = this.earnedStars;
		dataModel.MaxStars = this.RankConfig.Stars;
		dataModel.StarMultiplier = this.starsPerWin;
		dataModel.StarLevel = this.starLevel;
		dataModel.MedalText = this.GetMedalText();
		dataModel.RankName = this.GetRankName();
		dataModel.IsNewPlayer = this.IsNewPlayer();
		dataModel.IsLegend = this.IsLegendRank();
		dataModel.LegendRank = this.legendIndex;
		dataModel.FormatType = this.GetFormatType();
		if (mode != RankedMedal.DisplayMode.Chest && !string.IsNullOrEmpty(this.RankConfig.MedalMaterial))
		{
			AssetLoader.ObjectCallback callback = delegate(AssetReference assetRef, UnityEngine.Object materialObj, object data)
			{
				dataModel.MedalMaterial = (materialObj as Material);
				Action<RankedPlayDataModel> dataModelReadyCallback3 = dataModelReadyCallback;
				if (dataModelReadyCallback3 == null)
				{
					return;
				}
				dataModelReadyCallback3(dataModel);
			};
			AssetLoader.Get().LoadMaterial(this.RankConfig.MedalMaterial, callback, null, false, false);
			return;
		}
		if (mode != RankedMedal.DisplayMode.Chest && !string.IsNullOrEmpty(this.RankConfig.MedalTexture))
		{
			AssetLoader.ObjectCallback callback2 = delegate(AssetReference assetRef, UnityEngine.Object textureObj, object data)
			{
				dataModel.MedalTexture = (textureObj as Texture);
				Action<RankedPlayDataModel> dataModelReadyCallback3 = dataModelReadyCallback;
				if (dataModelReadyCallback3 == null)
				{
					return;
				}
				dataModelReadyCallback3(dataModel);
			};
			AssetLoader.Get().LoadTexture(this.RankConfig.MedalTexture, callback2, null, false, false);
			return;
		}
		Action<RankedPlayDataModel> dataModelReadyCallback2 = dataModelReadyCallback;
		if (dataModelReadyCallback2 == null)
		{
			return;
		}
		dataModelReadyCallback2(dataModel);
	}

	// Token: 0x06005A48 RID: 23112 RVA: 0x001D761C File Offset: 0x001D581C
	public override string ToString()
	{
		return string.Format("[leagueId={0} starLevel={1} earnedStars={2} starsPerWin={3}]", new object[]
		{
			this.leagueId,
			this.starLevel,
			this.earnedStars,
			this.starsPerWin
		});
	}

	// Token: 0x04004D2F RID: 19759
	public int leagueId;

	// Token: 0x04004D30 RID: 19760
	public int earnedStars;

	// Token: 0x04004D31 RID: 19761
	public int starLevel;

	// Token: 0x04004D32 RID: 19762
	public int bestStarLevel;

	// Token: 0x04004D33 RID: 19763
	public int winStreak;

	// Token: 0x04004D34 RID: 19764
	public int legendIndex;

	// Token: 0x04004D35 RID: 19765
	public int seasonId;

	// Token: 0x04004D36 RID: 19766
	public int seasonWins;

	// Token: 0x04004D37 RID: 19767
	public int seasonGames;

	// Token: 0x04004D38 RID: 19768
	public int bestEverLeagueId;

	// Token: 0x04004D39 RID: 19769
	public int bestEverStarLevel;

	// Token: 0x04004D3A RID: 19770
	public int starsPerWin;

	// Token: 0x04004D3B RID: 19771
	public FormatType format;
}
