using System;
using System.Collections;
using Hearthstone.DataModels;
using Hearthstone.Progression;
using UnityEngine;

// Token: 0x02000658 RID: 1624
public class AdventureHeroPowerReward : WidgetReward
{
	// Token: 0x06005BB8 RID: 23480 RVA: 0x001DDAD6 File Offset: 0x001DBCD6
	protected override void Start()
	{
		base.Start();
		base.StartCoroutine(this.SetHeroPowerWhenReady());
	}

	// Token: 0x06005BB9 RID: 23481 RVA: 0x001DDAEB File Offset: 0x001DBCEB
	protected override void OnDestroy()
	{
		DefLoader.DisposableFullDef heroPowerFullDef = this.m_heroPowerFullDef;
		if (heroPowerFullDef != null)
		{
			heroPowerFullDef.Dispose();
		}
		this.m_heroPowerFullDef = null;
		base.OnDestroy();
	}

	// Token: 0x06005BBA RID: 23482 RVA: 0x001DDB0B File Offset: 0x001DBD0B
	private IEnumerator SetHeroPowerWhenReady()
	{
		while (this.m_rewardWidget == null)
		{
			yield return null;
		}
		while (this.m_heroPowerRecord == null)
		{
			yield return null;
		}
		AdventureLoadoutOptionDataModel adventureLoadoutOptionDataModel = new AdventureLoadoutOptionDataModel();
		if (!string.IsNullOrEmpty(this.m_heroPowerRecord.UnlockedDescriptionText))
		{
			int num = 0;
			if (this.m_heroPowerRecord.UnlockAchievement > 0)
			{
				num = AchievementManager.Get().GetAchievementDataModel(this.m_heroPowerRecord.UnlockAchievement).Quota;
			}
			int num2 = this.m_heroPowerRecord.UnlockValue + num;
			adventureLoadoutOptionDataModel.LockedText = string.Format(this.m_heroPowerRecord.UnlockedDescriptionText, num2);
		}
		this.m_rewardWidget.BindDataModel(adventureLoadoutOptionDataModel, false);
		while (this.m_rewardWidget.IsChangingStates)
		{
			yield return null;
		}
		while (this.m_heroPowerFullDef == null)
		{
			yield return null;
		}
		Actor componentInChildren = this.m_rewardWidget.GetComponentInChildren<Actor>();
		componentInChildren.SetFullDef(this.m_heroPowerFullDef);
		componentInChildren.UpdateAllComponents();
		base.SetReady(true);
		yield break;
	}

	// Token: 0x06005BBB RID: 23483 RVA: 0x001DDB1A File Offset: 0x001DBD1A
	private void OnFullDefLoaded(string cardID, DefLoader.DisposableFullDef def, object userData)
	{
		if (def == null)
		{
			Debug.LogErrorFormat("Unable to load FullDef for cardID={0}", new object[]
			{
				cardID
			});
			return;
		}
		DefLoader.DisposableFullDef heroPowerFullDef = this.m_heroPowerFullDef;
		if (heroPowerFullDef != null)
		{
			heroPowerFullDef.Dispose();
		}
		this.m_heroPowerFullDef = def.Share();
	}

	// Token: 0x06005BBC RID: 23484 RVA: 0x001DDB51 File Offset: 0x001DBD51
	protected override void InitData()
	{
		base.SetData(new AdventureHeroPowerRewardData(), false);
	}

	// Token: 0x06005BBD RID: 23485 RVA: 0x001DDB60 File Offset: 0x001DBD60
	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		AdventureHeroPowerRewardData adventureHeroPowerRewardData = base.Data as AdventureHeroPowerRewardData;
		if (adventureHeroPowerRewardData == null)
		{
			Debug.LogWarningFormat("AdventureHeroPowerReward.OnDataSet() - Data {0} is not HeroPowerRewardData", new object[]
			{
				base.Data
			});
			return;
		}
		this.m_heroPowerRecord = adventureHeroPowerRewardData.HeroPowerRecord;
		if (this.m_heroPowerRecord == null)
		{
			Debug.LogWarningFormat("AdventureHeroPowerReward.OnDataSet() - HeroPowerRecord is null!", Array.Empty<object>());
			return;
		}
		string text = GameUtils.TranslateDbIdToCardId(this.m_heroPowerRecord.CardId, false);
		if (text == null)
		{
			Debug.LogWarningFormat("AdventureHeroPowerReward.OnDataSet() - No CardId found for DbId {0}!", new object[]
			{
				this.m_heroPowerRecord.CardId
			});
			return;
		}
		base.SetReady(false);
		DefLoader.Get().LoadFullDef(text, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnFullDefLoaded), null, null);
	}

	// Token: 0x04004E46 RID: 20038
	private AdventureHeroPowerDbfRecord m_heroPowerRecord;

	// Token: 0x04004E47 RID: 20039
	private DefLoader.DisposableFullDef m_heroPowerFullDef;
}
