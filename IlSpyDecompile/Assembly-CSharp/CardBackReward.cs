using System.Collections;
using UnityEngine;

public class CardBackReward : Reward
{
	public GameObject m_cardbackBone;

	private int m_numCardBacksLoaded;

	protected override void InitData()
	{
		SetData(new CardBackRewardData(), updateVisuals: false);
	}

	protected override void ShowReward(bool updateCacheValues)
	{
		CardBackRewardData cardBackRewardData = base.Data as CardBackRewardData;
		if (cardBackRewardData == null)
		{
			Debug.LogWarning($"CardBackReward.ShowReward() - Data {base.Data} is not CardBackRewardData");
			return;
		}
		if (!cardBackRewardData.IsDummyReward && updateCacheValues)
		{
			CardBackManager.Get().AddNewCardBack(cardBackRewardData.CardBackID);
			StoreManager.Get().Catalog.UpdateProductStatus();
		}
		m_root.SetActive(value: true);
		m_cardbackBone.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
		Hashtable args = iTween.Hash("amount", new Vector3(0f, 0f, 540f), "time", 1.5f, "easeType", iTween.EaseType.easeOutElastic, "space", Space.Self);
		iTween.RotateAdd(m_cardbackBone.gameObject, args);
	}

	protected override void HideReward()
	{
		base.HideReward();
		m_root.SetActive(value: false);
	}

	protected override void OnDataSet(bool updateVisuals)
	{
		if (updateVisuals)
		{
			CardBackRewardData cardBackRewardData = base.Data as CardBackRewardData;
			string headline = GameStrings.Get("GLOBAL_REWARD_CARD_BACK_HEADLINE");
			SetRewardText(headline, string.Empty, string.Empty);
			if (cardBackRewardData == null)
			{
				Debug.LogWarning($"CardBackReward.OnDataSet() - Data {base.Data} is not CardBackRewardData");
				return;
			}
			SetReady(ready: false);
			CardBackManager.Get().LoadCardBackByIndex(cardBackRewardData.CardBackID, OnFrontCardBackLoaded, unlit: true);
			CardBackManager.Get().LoadCardBackByIndex(cardBackRewardData.CardBackID, OnBackCardBackLoaded, unlit: true);
		}
	}

	private void OnFrontCardBackLoaded(CardBackManager.LoadCardBackData cardbackData)
	{
		GameObject obj = cardbackData.m_GameObject;
		obj.transform.parent = m_cardbackBone.transform;
		obj.transform.localPosition = Vector3.zero;
		obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
		obj.transform.localScale = Vector3.one;
		SceneUtils.SetLayer(obj, base.gameObject.layer);
		m_numCardBacksLoaded++;
		if (2 == m_numCardBacksLoaded)
		{
			SetReady(ready: true);
		}
	}

	private void OnBackCardBackLoaded(CardBackManager.LoadCardBackData cardbackData)
	{
		GameObject obj = cardbackData.m_GameObject;
		obj.transform.parent = m_cardbackBone.transform;
		obj.transform.localPosition = Vector3.zero;
		obj.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
		obj.transform.localScale = Vector3.one;
		SceneUtils.SetLayer(obj, base.gameObject.layer);
		m_numCardBacksLoaded++;
		if (2 == m_numCardBacksLoaded)
		{
			SetReady(ready: true);
		}
	}
}
