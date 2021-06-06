using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000664 RID: 1636
public class CardBackReward : Reward
{
	// Token: 0x06005C09 RID: 23561 RVA: 0x001DEACC File Offset: 0x001DCCCC
	protected override void InitData()
	{
		base.SetData(new CardBackRewardData(), false);
	}

	// Token: 0x06005C0A RID: 23562 RVA: 0x001DEADC File Offset: 0x001DCCDC
	protected override void ShowReward(bool updateCacheValues)
	{
		CardBackRewardData cardBackRewardData = base.Data as CardBackRewardData;
		if (cardBackRewardData == null)
		{
			Debug.LogWarning(string.Format("CardBackReward.ShowReward() - Data {0} is not CardBackRewardData", base.Data));
			return;
		}
		if (!cardBackRewardData.IsDummyReward && updateCacheValues)
		{
			CardBackManager.Get().AddNewCardBack(cardBackRewardData.CardBackID);
			StoreManager.Get().Catalog.UpdateProductStatus();
		}
		this.m_root.SetActive(true);
		this.m_cardbackBone.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			new Vector3(0f, 0f, 540f),
			"time",
			1.5f,
			"easeType",
			iTween.EaseType.easeOutElastic,
			"space",
			Space.Self
		});
		iTween.RotateAdd(this.m_cardbackBone.gameObject, args);
	}

	// Token: 0x06005C0B RID: 23563 RVA: 0x001DDEDB File Offset: 0x001DC0DB
	protected override void HideReward()
	{
		base.HideReward();
		this.m_root.SetActive(false);
	}

	// Token: 0x06005C0C RID: 23564 RVA: 0x001DEBE4 File Offset: 0x001DCDE4
	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		CardBackRewardData cardBackRewardData = base.Data as CardBackRewardData;
		string headline = GameStrings.Get("GLOBAL_REWARD_CARD_BACK_HEADLINE");
		base.SetRewardText(headline, string.Empty, string.Empty);
		if (cardBackRewardData == null)
		{
			Debug.LogWarning(string.Format("CardBackReward.OnDataSet() - Data {0} is not CardBackRewardData", base.Data));
			return;
		}
		base.SetReady(false);
		CardBackManager.Get().LoadCardBackByIndex(cardBackRewardData.CardBackID, new CardBackManager.LoadCardBackData.LoadCardBackCallback(this.OnFrontCardBackLoaded), true, "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", null);
		CardBackManager.Get().LoadCardBackByIndex(cardBackRewardData.CardBackID, new CardBackManager.LoadCardBackData.LoadCardBackCallback(this.OnBackCardBackLoaded), true, "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", null);
	}

	// Token: 0x06005C0D RID: 23565 RVA: 0x001DEC88 File Offset: 0x001DCE88
	private void OnFrontCardBackLoaded(CardBackManager.LoadCardBackData cardbackData)
	{
		GameObject gameObject = cardbackData.m_GameObject;
		gameObject.transform.parent = this.m_cardbackBone.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
		gameObject.transform.localScale = Vector3.one;
		SceneUtils.SetLayer(gameObject, base.gameObject.layer, null);
		this.m_numCardBacksLoaded++;
		if (2 == this.m_numCardBacksLoaded)
		{
			base.SetReady(true);
		}
	}

	// Token: 0x06005C0E RID: 23566 RVA: 0x001DED20 File Offset: 0x001DCF20
	private void OnBackCardBackLoaded(CardBackManager.LoadCardBackData cardbackData)
	{
		GameObject gameObject = cardbackData.m_GameObject;
		gameObject.transform.parent = this.m_cardbackBone.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
		gameObject.transform.localScale = Vector3.one;
		SceneUtils.SetLayer(gameObject, base.gameObject.layer, null);
		this.m_numCardBacksLoaded++;
		if (2 == this.m_numCardBacksLoaded)
		{
			base.SetReady(true);
		}
	}

	// Token: 0x04004E60 RID: 20064
	public GameObject m_cardbackBone;

	// Token: 0x04004E61 RID: 20065
	private int m_numCardBacksLoaded;
}
