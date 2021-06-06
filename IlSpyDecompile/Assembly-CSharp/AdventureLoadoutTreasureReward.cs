using System.Collections;
using Hearthstone.DataModels;
using Hearthstone.Progression;
using UnityEngine;

public class AdventureLoadoutTreasureReward : WidgetReward
{
	private AdventureLoadoutTreasuresDbfRecord m_loadoutTreasureRecord;

	protected override void Start()
	{
		base.Start();
	}

	private IEnumerator SetDataWhenLoaded()
	{
		while (m_rewardWidget == null)
		{
			yield return null;
		}
		AdventureLoadoutTreasureRewardData adventureLoadoutTreasureRewardData = base.Data as AdventureLoadoutTreasureRewardData;
		AdventureLoadoutOptionDataModel adventureLoadoutOptionDataModel = new AdventureLoadoutOptionDataModel();
		bool flag = false;
		if (adventureLoadoutTreasureRewardData != null && adventureLoadoutTreasureRewardData.IsUpgrade)
		{
			flag = true;
			if (!string.IsNullOrEmpty(m_loadoutTreasureRecord.UpgradedDescriptionText))
			{
				adventureLoadoutOptionDataModel.LockedText = string.Format(m_loadoutTreasureRecord.UpgradedDescriptionText, m_loadoutTreasureRecord.UpgradeValue);
			}
			adventureLoadoutOptionDataModel.IsUpgraded = true;
		}
		else
		{
			if (!string.IsNullOrEmpty(m_loadoutTreasureRecord.UnlockedDescriptionText))
			{
				int num = 0;
				if (m_loadoutTreasureRecord.UnlockAchievement > 0)
				{
					num = AchievementManager.Get().GetAchievementDataModel(m_loadoutTreasureRecord.UnlockAchievement).Quota;
				}
				int num2 = m_loadoutTreasureRecord.UnlockValue + num;
				adventureLoadoutOptionDataModel.LockedText = string.Format(m_loadoutTreasureRecord.UnlockedDescriptionText, num2);
			}
			adventureLoadoutOptionDataModel.IsUpgraded = false;
		}
		m_rewardWidget.BindDataModel(adventureLoadoutOptionDataModel);
		string text = GameUtils.TranslateDbIdToCardId(flag ? m_loadoutTreasureRecord.UpgradedCardId : m_loadoutTreasureRecord.CardId);
		if (text == null)
		{
			Debug.LogWarningFormat("AdventureLoadoutTreasureReward.SetLoadoutTreasureWhenReady() - No CardId found for DbId {0}!", m_loadoutTreasureRecord.CardId);
		}
		CardDataModel cardDataModel = new CardDataModel();
		CardDbfRecord cardRecord = GameDbf.GetIndex().GetCardRecord(text);
		cardDataModel.CardId = text;
		cardDataModel.FlavorText = cardRecord?.FlavorText;
		m_rewardWidget.BindDataModel(cardDataModel);
		while (m_rewardWidget.IsChangingStates)
		{
			yield return null;
		}
		SetReady(ready: true);
	}

	protected override void InitData()
	{
		SetData(new AdventureLoadoutTreasureRewardData(), updateVisuals: false);
	}

	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		AdventureLoadoutTreasureRewardData adventureLoadoutTreasureRewardData = base.Data as AdventureLoadoutTreasureRewardData;
		if (adventureLoadoutTreasureRewardData == null)
		{
			Debug.LogWarningFormat("AdventureLoadoutTreasureReward.OnDataSet() - Data {0} is not LoadoutTreasureRewardData", base.Data);
			return;
		}
		m_loadoutTreasureRecord = adventureLoadoutTreasureRewardData.LoadoutTreasureRecord;
		if (m_loadoutTreasureRecord == null)
		{
			Debug.LogWarningFormat("AdventureLoadoutTreasureReward.OnDataSet() - LoadoutTreasureRecord is null!");
			return;
		}
		SetReady(ready: false);
		StartCoroutine(SetDataWhenLoaded());
	}
}
