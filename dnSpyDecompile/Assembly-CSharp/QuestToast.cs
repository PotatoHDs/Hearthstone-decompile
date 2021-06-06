using System;
using System.Linq;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000639 RID: 1593
public class QuestToast : MonoBehaviour
{
	// Token: 0x060059A9 RID: 22953 RVA: 0x001D3E30 File Offset: 0x001D2030
	public void Awake()
	{
		OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
	}

	// Token: 0x060059AA RID: 22954 RVA: 0x001D3E45 File Offset: 0x001D2045
	public void OnDestroy()
	{
		if (this == QuestToast.m_activeToast)
		{
			if (QuestToast.m_isToastActiveOrActivating)
			{
				this.FadeEffectsOut();
				QuestToast.m_isToastActiveOrActivating = false;
			}
			QuestToast.m_activeToast = null;
		}
	}

	// Token: 0x060059AB RID: 22955 RVA: 0x001D3E6D File Offset: 0x001D206D
	public static void ShowQuestToast(UserAttentionBlocker blocker, QuestToast.DelOnCloseQuestToast onClosedCallback, bool updateCacheValues, Achievement quest)
	{
		QuestToast.ShowQuestToast(blocker, onClosedCallback, updateCacheValues, quest, true);
	}

	// Token: 0x060059AC RID: 22956 RVA: 0x001D3E79 File Offset: 0x001D2079
	public static void ShowQuestToast(UserAttentionBlocker blocker, QuestToast.DelOnCloseQuestToast onClosedCallback, bool updateCacheValues, Achievement quest, bool fullScreenEffects)
	{
		QuestToast.ShowQuestToast(blocker, onClosedCallback, null, updateCacheValues, quest, fullScreenEffects);
	}

	// Token: 0x060059AD RID: 22957 RVA: 0x001D3E87 File Offset: 0x001D2087
	public static void ShowQuestToast(UserAttentionBlocker blocker, QuestToast.DelOnCloseQuestToast onClosedCallback, object callbackUserData, bool updateCacheValues, Achievement quest)
	{
		QuestToast.ShowQuestToast(blocker, onClosedCallback, callbackUserData, updateCacheValues, quest, true);
	}

	// Token: 0x060059AE RID: 22958 RVA: 0x001D3E98 File Offset: 0x001D2098
	public static void ShowQuestToast(UserAttentionBlocker blocker, QuestToast.DelOnCloseQuestToast onClosedCallback, object callbackUserData, bool updateCacheValues, Achievement quest, bool fullscreenEffects)
	{
		if (!UserAttentionManager.CanShowAttentionGrabber(blocker, "ShowQuestToast:" + ((quest == null) ? "null" : quest.ID.ToString())))
		{
			if (onClosedCallback != null)
			{
				onClosedCallback(callbackUserData);
			}
			return;
		}
		Log.Achievements.Print("ShowQuestToast: {0}", new object[]
		{
			quest
		});
		if (quest.Rewards.Any((RewardData r) => r.RewardType == Reward.Type.ARCANE_ORBS) && Shop.Get() != null)
		{
			StoreManager.Get().GetCurrencyCache(CurrencyType.ARCANE_ORBS).MarkDirty();
		}
		quest.AckCurrentProgressAndRewardNotices();
		if (quest.ID == 56)
		{
			if (onClosedCallback != null)
			{
				onClosedCallback(callbackUserData);
			}
			return;
		}
		QuestToast.ShowQuestToastPopup(blocker, onClosedCallback, callbackUserData, (quest.Rewards == null) ? null : quest.Rewards.FirstOrDefault<RewardData>(), quest.Name, quest.Description, fullscreenEffects, updateCacheValues, quest);
	}

	// Token: 0x060059AF RID: 22959 RVA: 0x001D3F90 File Offset: 0x001D2190
	public static void ShowFixedRewardQuestToast(UserAttentionBlocker blocker, QuestToast.DelOnCloseQuestToast onClosedCallback, RewardData rewardData, string name, string description)
	{
		QuestToast.ShowFixedRewardQuestToast(blocker, onClosedCallback, null, rewardData, name, description, true);
	}

	// Token: 0x060059B0 RID: 22960 RVA: 0x001D3FA0 File Offset: 0x001D21A0
	public static void ShowFixedRewardQuestToast(UserAttentionBlocker blocker, QuestToast.DelOnCloseQuestToast onClosedCallback, object callbackUserData, RewardData rewardData, string name, string description, bool fullscreenEffects)
	{
		QuestToast.ShowQuestToastPopup(blocker, onClosedCallback, callbackUserData, rewardData, name, description, fullscreenEffects, true, null);
	}

	// Token: 0x060059B1 RID: 22961 RVA: 0x001D3FBE File Offset: 0x001D21BE
	public static void ShowGenericRewardQuestToast(UserAttentionBlocker blocker, QuestToast.DelOnCloseQuestToast onClosedCallback, RewardData rewardData, string name, string description)
	{
		QuestToast.ShowGenericRewardQuestToast(blocker, onClosedCallback, null, rewardData, name, description, true);
	}

	// Token: 0x060059B2 RID: 22962 RVA: 0x001D3FD0 File Offset: 0x001D21D0
	public static void ShowGenericRewardQuestToast(UserAttentionBlocker blocker, QuestToast.DelOnCloseQuestToast onClosedCallback, object callbackUserData, RewardData rewardData, string name, string description, bool fullscreenEffects)
	{
		QuestToast.ShowQuestToastPopup(blocker, onClosedCallback, callbackUserData, rewardData, name, description, fullscreenEffects, false, null);
	}

	// Token: 0x060059B3 RID: 22963 RVA: 0x001D3FF0 File Offset: 0x001D21F0
	public static void ShowQuestToastPopup(UserAttentionBlocker blocker, QuestToast.DelOnCloseQuestToast onClosedCallback, object callbackUserData, RewardData rewardData, string name, string description, bool fullscreenEffects, bool updateCacheValues, Achievement quest)
	{
		if (!UserAttentionManager.CanShowAttentionGrabber(blocker, "ShowQuestToastPopup:" + ((rewardData == null) ? "null" : string.Concat(new object[]
		{
			rewardData.Origin,
			":",
			rewardData.OriginData,
			":",
			rewardData.RewardType
		}))))
		{
			if (onClosedCallback != null)
			{
				onClosedCallback(callbackUserData);
			}
			return;
		}
		Log.Achievements.Print("ShowQuestToastPopup: name={0} desc={1}", new object[]
		{
			name,
			description
		});
		QuestToast.m_showFullscreenEffects = fullscreenEffects;
		QuestToast.m_isToastActiveOrActivating = true;
		QuestToast.ToastCallbackData callbackData = new QuestToast.ToastCallbackData
		{
			m_toastReward = rewardData,
			m_toastName = name,
			m_toastDescription = description,
			m_onCloseCallback = onClosedCallback,
			m_onCloseCallbackData = callbackUserData,
			m_quest = quest,
			m_updateCacheValues = updateCacheValues
		};
		AssetLoader.Get().InstantiatePrefab("QuestToast.prefab:ebf10185d03f14f41a367b9a7170c4c4", new PrefabCallback<GameObject>(QuestToast.PositionActor), callbackData, AssetLoadingOptions.None);
	}

	// Token: 0x060059B4 RID: 22964 RVA: 0x001D40F4 File Offset: 0x001D22F4
	private static void PositionActor(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.localPosition = new Vector3(0f, 85f, 0f);
		Vector3 localScale = go.transform.localScale;
		go.transform.localScale = 0.01f * Vector3.one;
		go.SetActive(true);
		iTween.ScaleTo(go, localScale, 0.5f);
		QuestToast component = go.GetComponent<QuestToast>();
		if (component == null)
		{
			Debug.LogWarning("QuestToast.PositionActor(): actor has no QuestToast component");
			QuestToast.m_isToastActiveOrActivating = false;
			return;
		}
		QuestToast.m_activeToast = component;
		QuestToast.ToastCallbackData toastCallbackData = callbackData as QuestToast.ToastCallbackData;
		component.m_onCloseCallback = toastCallbackData.m_onCloseCallback;
		component.m_toastReward = toastCallbackData.m_toastReward;
		component.m_toastName = toastCallbackData.m_toastName;
		component.m_toastDescription = toastCallbackData.m_toastDescription;
		component.m_onCloseCallbackData = toastCallbackData;
		component.m_quest = toastCallbackData.m_quest;
		component.SetUpToast(toastCallbackData.m_updateCacheValues);
	}

	// Token: 0x060059B5 RID: 22965 RVA: 0x001D41D6 File Offset: 0x001D23D6
	private void CloseQuestToast(UIEvent e)
	{
		this.CloseQuestToast();
	}

	// Token: 0x060059B6 RID: 22966 RVA: 0x001D41E0 File Offset: 0x001D23E0
	public void CloseQuestToast()
	{
		if (base.gameObject == null)
		{
			return;
		}
		QuestToast.m_isToastActiveOrActivating = false;
		this.m_clickCatcher.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.CloseQuestToast));
		SoundManager.Get().LoadAndPlay("new_quest_click_and_shrink.prefab:601ba6676276eab43947e38f110f7b99");
		this.FadeEffectsOut();
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			Vector3.zero,
			"time",
			0.5f,
			"oncompletetarget",
			base.gameObject,
			"oncomplete",
			"DestroyQuestToast"
		}));
		UIContext.GetRoot().DismissPopup(base.gameObject);
		if (this.m_onCloseCallback == null)
		{
			return;
		}
		QuestToast.ToastCallbackData toastCallbackData = this.m_onCloseCallbackData as QuestToast.ToastCallbackData;
		if (toastCallbackData != null && toastCallbackData.m_quest != null)
		{
			NarrativeManager.Get().OnAchieveDismissed(toastCallbackData.m_quest);
		}
		this.m_onCloseCallback(this.m_onCloseCallbackData);
	}

	// Token: 0x060059B7 RID: 22967 RVA: 0x001D42E7 File Offset: 0x001D24E7
	public static bool IsQuestActive()
	{
		return QuestToast.m_isToastActiveOrActivating && QuestToast.m_activeToast != null;
	}

	// Token: 0x060059B8 RID: 22968 RVA: 0x001D42FD File Offset: 0x001D24FD
	public static QuestToast GetCurrentToast()
	{
		return QuestToast.m_activeToast;
	}

	// Token: 0x060059B9 RID: 22969 RVA: 0x0003DCF6 File Offset: 0x0003BEF6
	private void DestroyQuestToast()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060059BA RID: 22970 RVA: 0x001D4304 File Offset: 0x001D2504
	public void SetUpToast(bool updateCacheValues)
	{
		this.m_clickCatcher.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.CloseQuestToast));
		this.m_questName.Text = this.m_toastName;
		this.m_requirement.Text = this.m_toastDescription;
		if (this.m_toastReward != null)
		{
			if (SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_GOLD_DOUBLED, false) && this.m_quest != null && this.m_quest.IsAffectedByDoubleGold && this.m_toastReward is GoldRewardData)
			{
				GoldRewardData goldRewardData = new GoldRewardData(this.m_toastReward as GoldRewardData);
				goldRewardData.Amount *= 2L;
				this.m_toastReward = goldRewardData;
			}
			this.m_toastReward.LoadRewardObject(new Reward.DelOnRewardLoaded(this.RewardObjectLoaded), updateCacheValues);
		}
		UIContext.GetRoot().ShowPopup(base.gameObject, UIContext.BlurType.None);
		this.FadeEffectsIn();
	}

	// Token: 0x060059BB RID: 22971 RVA: 0x001D43E8 File Offset: 0x001D25E8
	private void RewardObjectLoaded(Reward reward, object callbackData)
	{
		if (this == null)
		{
			return;
		}
		bool updateCacheValues = (bool)callbackData;
		reward.Hide(false);
		reward.transform.parent = this.m_rewardBone;
		reward.transform.localEulerAngles = Vector3.zero;
		reward.transform.localScale = this.m_rewardScale;
		reward.transform.localPosition = Vector3.zero;
		BoosterPackReward componentInChildren = reward.gameObject.GetComponentInChildren<BoosterPackReward>();
		if (componentInChildren != null)
		{
			reward.transform.localScale = this.m_boosterRewardRootScale;
			reward.m_MeshRoot.transform.localPosition = this.m_boosterRewardPosition;
			reward.m_MeshRoot.transform.localScale = this.m_boosterRewardScale;
			componentInChildren.m_Layer = (GameLayer)base.gameObject.layer;
		}
		CardReward componentInChildren2 = reward.gameObject.GetComponentInChildren<CardReward>();
		if (componentInChildren2 != null)
		{
			reward.transform.localScale = this.m_cardRewardRootScale;
			componentInChildren2.m_cardParent.transform.localScale = this.m_cardRewardScale;
			componentInChildren2.m_cardParent.transform.localPosition = this.m_cardRewardLocation;
			componentInChildren2.m_duplicateCardParent.transform.localScale = this.m_cardDuplicateRewardScale;
			componentInChildren2.m_duplicateCardParent.transform.localPosition = this.m_cardDuplicateRewardLocation;
		}
		CardBackReward componentInChildren3 = reward.gameObject.GetComponentInChildren<CardBackReward>();
		if (componentInChildren3 != null)
		{
			reward.transform.localScale = this.m_cardBackRootScale;
			componentInChildren3.m_cardbackBone.transform.localScale = this.m_cardbackRewardScale;
			componentInChildren3.m_cardbackBone.transform.localPosition = this.m_cardbackRewardLocation;
		}
		GoldReward componentInChildren4 = reward.gameObject.GetComponentInChildren<GoldReward>();
		if (componentInChildren4 != null)
		{
			componentInChildren4.m_root.transform.localScale = this.m_goldRewardScale;
			componentInChildren4.m_rewardBannerBone.transform.localPosition += this.m_goldBannerOffset;
			componentInChildren4.m_rewardBannerBone.transform.localScale = this.m_goldBannerScale;
		}
		ArcaneDustReward componentInChildren5 = reward.gameObject.GetComponentInChildren<ArcaneDustReward>();
		if (componentInChildren5 != null)
		{
			componentInChildren5.m_root.transform.localScale = this.m_dustRewardScale;
			componentInChildren5.m_root.transform.localPosition += this.m_dustRewardOffset;
			componentInChildren5.m_rewardBannerBone.transform.localPosition += this.m_dustBannerOffset;
			componentInChildren5.m_rewardBannerBone.transform.localScale = this.m_dustBannerScale;
		}
		PopupRoot component = base.gameObject.GetComponent<PopupRoot>();
		if (component != null)
		{
			component.ApplyPopupRendering(reward.transform);
		}
		reward.Show(updateCacheValues);
	}

	// Token: 0x060059BC RID: 22972 RVA: 0x001D4700 File Offset: 0x001D2900
	private void FadeEffectsIn()
	{
		if (!QuestToast.m_showFullscreenEffects)
		{
			return;
		}
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr != null)
		{
			fullScreenFXMgr.SetBlurBrightness(1f);
			fullScreenFXMgr.SetBlurDesaturation(0f);
			fullScreenFXMgr.Vignette();
			fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc, null);
		}
	}

	// Token: 0x060059BD RID: 22973 RVA: 0x001D4750 File Offset: 0x001D2950
	private void FadeEffectsOut()
	{
		if (!QuestToast.m_showFullscreenEffects)
		{
			return;
		}
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr != null)
		{
			fullScreenFXMgr.StopVignette();
			fullScreenFXMgr.StopBlur();
		}
	}

	// Token: 0x04004CB3 RID: 19635
	public UberText m_questName;

	// Token: 0x04004CB4 RID: 19636
	public GameObject m_nameLine;

	// Token: 0x04004CB5 RID: 19637
	public UberText m_requirement;

	// Token: 0x04004CB6 RID: 19638
	public Transform m_rewardBone;

	// Token: 0x04004CB7 RID: 19639
	public PegUIElement m_clickCatcher;

	// Token: 0x04004CB8 RID: 19640
	public Vector3 m_rewardScale;

	// Token: 0x04004CB9 RID: 19641
	public Vector3_MobileOverride m_boosterRewardRootScale;

	// Token: 0x04004CBA RID: 19642
	public Vector3_MobileOverride m_boosterRewardPosition;

	// Token: 0x04004CBB RID: 19643
	public Vector3_MobileOverride m_boosterRewardScale;

	// Token: 0x04004CBC RID: 19644
	public Vector3_MobileOverride m_cardRewardRootScale;

	// Token: 0x04004CBD RID: 19645
	public Vector3_MobileOverride m_cardRewardScale;

	// Token: 0x04004CBE RID: 19646
	public Vector3_MobileOverride m_cardRewardLocation;

	// Token: 0x04004CBF RID: 19647
	public Vector3_MobileOverride m_cardDuplicateRewardScale;

	// Token: 0x04004CC0 RID: 19648
	public Vector3_MobileOverride m_cardDuplicateRewardLocation;

	// Token: 0x04004CC1 RID: 19649
	public Vector3_MobileOverride m_cardBackRootScale;

	// Token: 0x04004CC2 RID: 19650
	public Vector3_MobileOverride m_cardbackRewardScale;

	// Token: 0x04004CC3 RID: 19651
	public Vector3_MobileOverride m_cardbackRewardLocation;

	// Token: 0x04004CC4 RID: 19652
	public Vector3_MobileOverride m_goldRewardScale;

	// Token: 0x04004CC5 RID: 19653
	public Vector3_MobileOverride m_goldBannerOffset;

	// Token: 0x04004CC6 RID: 19654
	public Vector3_MobileOverride m_goldBannerScale;

	// Token: 0x04004CC7 RID: 19655
	public Vector3_MobileOverride m_dustRewardScale;

	// Token: 0x04004CC8 RID: 19656
	public Vector3_MobileOverride m_dustRewardOffset;

	// Token: 0x04004CC9 RID: 19657
	public Vector3_MobileOverride m_dustBannerOffset;

	// Token: 0x04004CCA RID: 19658
	public Vector3_MobileOverride m_dustBannerScale;

	// Token: 0x04004CCB RID: 19659
	private Achievement m_quest;

	// Token: 0x04004CCC RID: 19660
	private QuestToast.DelOnCloseQuestToast m_onCloseCallback;

	// Token: 0x04004CCD RID: 19661
	private object m_onCloseCallbackData;

	// Token: 0x04004CCE RID: 19662
	private RewardData m_toastReward;

	// Token: 0x04004CCF RID: 19663
	private string m_toastName = string.Empty;

	// Token: 0x04004CD0 RID: 19664
	private string m_toastDescription = string.Empty;

	// Token: 0x04004CD1 RID: 19665
	private static bool m_showFullscreenEffects = true;

	// Token: 0x04004CD2 RID: 19666
	private static bool m_isToastActiveOrActivating;

	// Token: 0x04004CD3 RID: 19667
	private static QuestToast m_activeToast;

	// Token: 0x0200213E RID: 8510
	// (Invoke) Token: 0x060122BC RID: 74428
	public delegate void DelOnCloseQuestToast(object userData);

	// Token: 0x0200213F RID: 8511
	private class ToastCallbackData
	{
		// Token: 0x0400DFAD RID: 57261
		public QuestToast.DelOnCloseQuestToast m_onCloseCallback;

		// Token: 0x0400DFAE RID: 57262
		public object m_onCloseCallbackData;

		// Token: 0x0400DFAF RID: 57263
		public RewardData m_toastReward;

		// Token: 0x0400DFB0 RID: 57264
		public string m_toastName = string.Empty;

		// Token: 0x0400DFB1 RID: 57265
		public string m_toastDescription = string.Empty;

		// Token: 0x0400DFB2 RID: 57266
		public bool m_updateCacheValues;

		// Token: 0x0400DFB3 RID: 57267
		public Achievement m_quest;
	}
}
