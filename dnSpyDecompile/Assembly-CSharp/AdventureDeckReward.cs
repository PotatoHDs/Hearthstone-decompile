using System;
using System.Collections;
using Hearthstone.DataModels;
using UnityEngine;

// Token: 0x02000656 RID: 1622
public class AdventureDeckReward : WidgetReward
{
	// Token: 0x06005BAD RID: 23469 RVA: 0x001DD9BD File Offset: 0x001DBBBD
	protected override void Start()
	{
		base.Start();
		base.StartCoroutine(this.SetDeckPouchPropertiesWhenStateReady());
	}

	// Token: 0x06005BAE RID: 23470 RVA: 0x001DD9D2 File Offset: 0x001DBBD2
	private IEnumerator SetDeckPouchPropertiesWhenStateReady()
	{
		while (this.m_rewardWidget == null)
		{
			yield return null;
		}
		while (this.m_deckRecord == null)
		{
			yield return null;
		}
		string name;
		string text;
		CollectionManager.Get().LoadDeckFromDBF(this.m_deckRecord.DeckId, out name, out text);
		AdventureLoadoutOptionDataModel dataModel = new AdventureLoadoutOptionDataModel();
		dataModel.Name = name;
		if (!string.IsNullOrEmpty(this.m_deckRecord.UnlockedDescriptionText))
		{
			dataModel.LockedText = string.Format(this.m_deckRecord.UnlockedDescriptionText, this.m_deckRecord.UnlockValue);
		}
		dataModel.DisplayColor = CollectionPageManager.ColorForClass((TAG_CLASS)this.m_deckRecord.ClassId);
		bool waitingForTexture = false;
		if (string.IsNullOrEmpty(this.m_deckRecord.DisplayTexture))
		{
			dataModel.DisplayTexture = null;
		}
		else
		{
			AssetLoader.ObjectCallback callback = delegate(AssetReference assetRef, UnityEngine.Object materialObj, object data)
			{
				dataModel.DisplayTexture = (materialObj as Material);
				waitingForTexture = false;
			};
			waitingForTexture = true;
			AssetLoader.Get().LoadMaterial(this.m_deckRecord.DisplayTexture, callback, null, false, false);
		}
		this.m_rewardWidget.BindDataModel(dataModel, false);
		while (waitingForTexture || this.m_rewardWidget.IsChangingStates)
		{
			yield return null;
		}
		base.SetReady(true);
		yield break;
	}

	// Token: 0x06005BAF RID: 23471 RVA: 0x001DD9E1 File Offset: 0x001DBBE1
	protected override void InitData()
	{
		base.SetData(new AdventureDeckRewardData(), false);
	}

	// Token: 0x06005BB0 RID: 23472 RVA: 0x001DD9F0 File Offset: 0x001DBBF0
	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		AdventureDeckRewardData adventureDeckRewardData = base.Data as AdventureDeckRewardData;
		if (adventureDeckRewardData == null)
		{
			Debug.LogWarningFormat("AdventureDeckReward.OnDataSet() - Data {0} is not DeckRewardData", new object[]
			{
				base.Data
			});
			return;
		}
		base.SetReady(false);
		this.m_deckRecord = adventureDeckRewardData.DeckRecord;
	}

	// Token: 0x04004E44 RID: 20036
	private AdventureDeckDbfRecord m_deckRecord;
}
