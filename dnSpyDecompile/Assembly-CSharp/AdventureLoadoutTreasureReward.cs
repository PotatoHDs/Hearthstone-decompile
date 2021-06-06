using System;
using System.Collections;
using Hearthstone.DataModels;
using Hearthstone.Progression;
using UnityEngine;

// Token: 0x0200065A RID: 1626
public class AdventureLoadoutTreasureReward : WidgetReward
{
	// Token: 0x06005BC5 RID: 23493 RVA: 0x001DDCAA File Offset: 0x001DBEAA
	protected override void Start()
	{
		base.Start();
	}

	// Token: 0x06005BC6 RID: 23494 RVA: 0x001DDCB2 File Offset: 0x001DBEB2
	private IEnumerator SetDataWhenLoaded()
	{
		while (this.m_rewardWidget == null)
		{
			yield return null;
		}
		AdventureLoadoutTreasureRewardData adventureLoadoutTreasureRewardData = base.Data as AdventureLoadoutTreasureRewardData;
		AdventureLoadoutOptionDataModel adventureLoadoutOptionDataModel = new AdventureLoadoutOptionDataModel();
		bool flag = false;
		if (adventureLoadoutTreasureRewardData != null && adventureLoadoutTreasureRewardData.IsUpgrade)
		{
			flag = true;
			if (!string.IsNullOrEmpty(this.m_loadoutTreasureRecord.UpgradedDescriptionText))
			{
				adventureLoadoutOptionDataModel.LockedText = string.Format(this.m_loadoutTreasureRecord.UpgradedDescriptionText, this.m_loadoutTreasureRecord.UpgradeValue);
			}
			adventureLoadoutOptionDataModel.IsUpgraded = true;
		}
		else
		{
			if (!string.IsNullOrEmpty(this.m_loadoutTreasureRecord.UnlockedDescriptionText))
			{
				int num = 0;
				if (this.m_loadoutTreasureRecord.UnlockAchievement > 0)
				{
					num = AchievementManager.Get().GetAchievementDataModel(this.m_loadoutTreasureRecord.UnlockAchievement).Quota;
				}
				int num2 = this.m_loadoutTreasureRecord.UnlockValue + num;
				adventureLoadoutOptionDataModel.LockedText = string.Format(this.m_loadoutTreasureRecord.UnlockedDescriptionText, num2);
			}
			adventureLoadoutOptionDataModel.IsUpgraded = false;
		}
		this.m_rewardWidget.BindDataModel(adventureLoadoutOptionDataModel, false);
		string text = GameUtils.TranslateDbIdToCardId(flag ? this.m_loadoutTreasureRecord.UpgradedCardId : this.m_loadoutTreasureRecord.CardId, false);
		if (text == null)
		{
			Debug.LogWarningFormat("AdventureLoadoutTreasureReward.SetLoadoutTreasureWhenReady() - No CardId found for DbId {0}!", new object[]
			{
				this.m_loadoutTreasureRecord.CardId
			});
		}
		CardDataModel cardDataModel = new CardDataModel();
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(text);
		cardDataModel.CardId = text;
		cardDataModel.FlavorText = ((cardRecord != null) ? cardRecord.FlavorText : null);
		this.m_rewardWidget.BindDataModel(cardDataModel, false);
		while (this.m_rewardWidget.IsChangingStates)
		{
			yield return null;
		}
		base.SetReady(true);
		yield break;
	}

	// Token: 0x06005BC7 RID: 23495 RVA: 0x001DDCC1 File Offset: 0x001DBEC1
	protected override void InitData()
	{
		base.SetData(new AdventureLoadoutTreasureRewardData(), false);
	}

	// Token: 0x06005BC8 RID: 23496 RVA: 0x001DDCD0 File Offset: 0x001DBED0
	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		AdventureLoadoutTreasureRewardData adventureLoadoutTreasureRewardData = base.Data as AdventureLoadoutTreasureRewardData;
		if (adventureLoadoutTreasureRewardData == null)
		{
			Debug.LogWarningFormat("AdventureLoadoutTreasureReward.OnDataSet() - Data {0} is not LoadoutTreasureRewardData", new object[]
			{
				base.Data
			});
			return;
		}
		this.m_loadoutTreasureRecord = adventureLoadoutTreasureRewardData.LoadoutTreasureRecord;
		if (this.m_loadoutTreasureRecord == null)
		{
			Debug.LogWarningFormat("AdventureLoadoutTreasureReward.OnDataSet() - LoadoutTreasureRecord is null!", Array.Empty<object>());
			return;
		}
		base.SetReady(false);
		base.StartCoroutine(this.SetDataWhenLoaded());
	}

	// Token: 0x04004E49 RID: 20041
	private AdventureLoadoutTreasuresDbfRecord m_loadoutTreasureRecord;
}
