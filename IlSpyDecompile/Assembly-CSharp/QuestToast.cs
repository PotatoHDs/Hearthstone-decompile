using System.Linq;
using Hearthstone.UI;
using UnityEngine;

public class QuestToast : MonoBehaviour
{
	public delegate void DelOnCloseQuestToast(object userData);

	private class ToastCallbackData
	{
		public DelOnCloseQuestToast m_onCloseCallback;

		public object m_onCloseCallbackData;

		public RewardData m_toastReward;

		public string m_toastName = string.Empty;

		public string m_toastDescription = string.Empty;

		public bool m_updateCacheValues;

		public Achievement m_quest;
	}

	public UberText m_questName;

	public GameObject m_nameLine;

	public UberText m_requirement;

	public Transform m_rewardBone;

	public PegUIElement m_clickCatcher;

	public Vector3 m_rewardScale;

	public Vector3_MobileOverride m_boosterRewardRootScale;

	public Vector3_MobileOverride m_boosterRewardPosition;

	public Vector3_MobileOverride m_boosterRewardScale;

	public Vector3_MobileOverride m_cardRewardRootScale;

	public Vector3_MobileOverride m_cardRewardScale;

	public Vector3_MobileOverride m_cardRewardLocation;

	public Vector3_MobileOverride m_cardDuplicateRewardScale;

	public Vector3_MobileOverride m_cardDuplicateRewardLocation;

	public Vector3_MobileOverride m_cardBackRootScale;

	public Vector3_MobileOverride m_cardbackRewardScale;

	public Vector3_MobileOverride m_cardbackRewardLocation;

	public Vector3_MobileOverride m_goldRewardScale;

	public Vector3_MobileOverride m_goldBannerOffset;

	public Vector3_MobileOverride m_goldBannerScale;

	public Vector3_MobileOverride m_dustRewardScale;

	public Vector3_MobileOverride m_dustRewardOffset;

	public Vector3_MobileOverride m_dustBannerOffset;

	public Vector3_MobileOverride m_dustBannerScale;

	private Achievement m_quest;

	private DelOnCloseQuestToast m_onCloseCallback;

	private object m_onCloseCallbackData;

	private RewardData m_toastReward;

	private string m_toastName = string.Empty;

	private string m_toastDescription = string.Empty;

	private static bool m_showFullscreenEffects = true;

	private static bool m_isToastActiveOrActivating;

	private static QuestToast m_activeToast;

	public void Awake()
	{
		OverlayUI.Get().AddGameObject(base.gameObject);
	}

	public void OnDestroy()
	{
		if (this == m_activeToast)
		{
			if (m_isToastActiveOrActivating)
			{
				FadeEffectsOut();
				m_isToastActiveOrActivating = false;
			}
			m_activeToast = null;
		}
	}

	public static void ShowQuestToast(UserAttentionBlocker blocker, DelOnCloseQuestToast onClosedCallback, bool updateCacheValues, Achievement quest)
	{
		ShowQuestToast(blocker, onClosedCallback, updateCacheValues, quest, fullScreenEffects: true);
	}

	public static void ShowQuestToast(UserAttentionBlocker blocker, DelOnCloseQuestToast onClosedCallback, bool updateCacheValues, Achievement quest, bool fullScreenEffects)
	{
		ShowQuestToast(blocker, onClosedCallback, null, updateCacheValues, quest, fullScreenEffects);
	}

	public static void ShowQuestToast(UserAttentionBlocker blocker, DelOnCloseQuestToast onClosedCallback, object callbackUserData, bool updateCacheValues, Achievement quest)
	{
		ShowQuestToast(blocker, onClosedCallback, callbackUserData, updateCacheValues, quest, fullscreenEffects: true);
	}

	public static void ShowQuestToast(UserAttentionBlocker blocker, DelOnCloseQuestToast onClosedCallback, object callbackUserData, bool updateCacheValues, Achievement quest, bool fullscreenEffects)
	{
		if (!UserAttentionManager.CanShowAttentionGrabber(blocker, "ShowQuestToast:" + ((quest == null) ? "null" : quest.ID.ToString())))
		{
			onClosedCallback?.Invoke(callbackUserData);
			return;
		}
		Log.Achievements.Print("ShowQuestToast: {0}", quest);
		if (quest.Rewards.Any((RewardData r) => r.RewardType == Reward.Type.ARCANE_ORBS) && Shop.Get() != null)
		{
			StoreManager.Get().GetCurrencyCache(CurrencyType.ARCANE_ORBS).MarkDirty();
		}
		quest.AckCurrentProgressAndRewardNotices();
		if (quest.ID == 56)
		{
			onClosedCallback?.Invoke(callbackUserData);
		}
		else
		{
			ShowQuestToastPopup(blocker, onClosedCallback, callbackUserData, (quest.Rewards == null) ? null : quest.Rewards.FirstOrDefault(), quest.Name, quest.Description, fullscreenEffects, updateCacheValues, quest);
		}
	}

	public static void ShowFixedRewardQuestToast(UserAttentionBlocker blocker, DelOnCloseQuestToast onClosedCallback, RewardData rewardData, string name, string description)
	{
		ShowFixedRewardQuestToast(blocker, onClosedCallback, null, rewardData, name, description, fullscreenEffects: true);
	}

	public static void ShowFixedRewardQuestToast(UserAttentionBlocker blocker, DelOnCloseQuestToast onClosedCallback, object callbackUserData, RewardData rewardData, string name, string description, bool fullscreenEffects)
	{
		ShowQuestToastPopup(blocker, onClosedCallback, callbackUserData, rewardData, name, description, fullscreenEffects, updateCacheValues: true, null);
	}

	public static void ShowGenericRewardQuestToast(UserAttentionBlocker blocker, DelOnCloseQuestToast onClosedCallback, RewardData rewardData, string name, string description)
	{
		ShowGenericRewardQuestToast(blocker, onClosedCallback, null, rewardData, name, description, fullscreenEffects: true);
	}

	public static void ShowGenericRewardQuestToast(UserAttentionBlocker blocker, DelOnCloseQuestToast onClosedCallback, object callbackUserData, RewardData rewardData, string name, string description, bool fullscreenEffects)
	{
		ShowQuestToastPopup(blocker, onClosedCallback, callbackUserData, rewardData, name, description, fullscreenEffects, updateCacheValues: false, null);
	}

	public static void ShowQuestToastPopup(UserAttentionBlocker blocker, DelOnCloseQuestToast onClosedCallback, object callbackUserData, RewardData rewardData, string name, string description, bool fullscreenEffects, bool updateCacheValues, Achievement quest)
	{
		if (!UserAttentionManager.CanShowAttentionGrabber(blocker, "ShowQuestToastPopup:" + ((rewardData == null) ? "null" : string.Concat(rewardData.Origin, ":", rewardData.OriginData, ":", rewardData.RewardType))))
		{
			onClosedCallback?.Invoke(callbackUserData);
			return;
		}
		Log.Achievements.Print("ShowQuestToastPopup: name={0} desc={1}", name, description);
		m_showFullscreenEffects = fullscreenEffects;
		m_isToastActiveOrActivating = true;
		ToastCallbackData callbackData = new ToastCallbackData
		{
			m_toastReward = rewardData,
			m_toastName = name,
			m_toastDescription = description,
			m_onCloseCallback = onClosedCallback,
			m_onCloseCallbackData = callbackUserData,
			m_quest = quest,
			m_updateCacheValues = updateCacheValues
		};
		AssetLoader.Get().InstantiatePrefab("QuestToast.prefab:ebf10185d03f14f41a367b9a7170c4c4", PositionActor, callbackData);
	}

	private static void PositionActor(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.localPosition = new Vector3(0f, 85f, 0f);
		Vector3 localScale = go.transform.localScale;
		go.transform.localScale = 0.01f * Vector3.one;
		go.SetActive(value: true);
		iTween.ScaleTo(go, localScale, 0.5f);
		QuestToast component = go.GetComponent<QuestToast>();
		if (component == null)
		{
			Debug.LogWarning("QuestToast.PositionActor(): actor has no QuestToast component");
			m_isToastActiveOrActivating = false;
			return;
		}
		m_activeToast = component;
		ToastCallbackData toastCallbackData = callbackData as ToastCallbackData;
		component.m_onCloseCallback = toastCallbackData.m_onCloseCallback;
		component.m_toastReward = toastCallbackData.m_toastReward;
		component.m_toastName = toastCallbackData.m_toastName;
		component.m_toastDescription = toastCallbackData.m_toastDescription;
		component.m_onCloseCallbackData = toastCallbackData;
		component.m_quest = toastCallbackData.m_quest;
		component.SetUpToast(toastCallbackData.m_updateCacheValues);
	}

	private void CloseQuestToast(UIEvent e)
	{
		CloseQuestToast();
	}

	public void CloseQuestToast()
	{
		if (base.gameObject == null)
		{
			return;
		}
		m_isToastActiveOrActivating = false;
		m_clickCatcher.RemoveEventListener(UIEventType.RELEASE, CloseQuestToast);
		SoundManager.Get().LoadAndPlay("new_quest_click_and_shrink.prefab:601ba6676276eab43947e38f110f7b99");
		FadeEffectsOut();
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", Vector3.zero, "time", 0.5f, "oncompletetarget", base.gameObject, "oncomplete", "DestroyQuestToast"));
		UIContext.GetRoot().DismissPopup(base.gameObject);
		if (m_onCloseCallback != null)
		{
			ToastCallbackData toastCallbackData = m_onCloseCallbackData as ToastCallbackData;
			if (toastCallbackData != null && toastCallbackData.m_quest != null)
			{
				NarrativeManager.Get().OnAchieveDismissed(toastCallbackData.m_quest);
			}
			m_onCloseCallback(m_onCloseCallbackData);
		}
	}

	public static bool IsQuestActive()
	{
		if (m_isToastActiveOrActivating)
		{
			return m_activeToast != null;
		}
		return false;
	}

	public static QuestToast GetCurrentToast()
	{
		return m_activeToast;
	}

	private void DestroyQuestToast()
	{
		Object.Destroy(base.gameObject);
	}

	public void SetUpToast(bool updateCacheValues)
	{
		m_clickCatcher.AddEventListener(UIEventType.RELEASE, CloseQuestToast);
		m_questName.Text = m_toastName;
		m_requirement.Text = m_toastDescription;
		if (m_toastReward != null)
		{
			if (SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_GOLD_DOUBLED, activeIfDoesNotExist: false) && m_quest != null && m_quest.IsAffectedByDoubleGold && m_toastReward is GoldRewardData)
			{
				GoldRewardData goldRewardData = new GoldRewardData(m_toastReward as GoldRewardData);
				goldRewardData.Amount *= 2L;
				m_toastReward = goldRewardData;
			}
			m_toastReward.LoadRewardObject(RewardObjectLoaded, updateCacheValues);
		}
		UIContext.GetRoot().ShowPopup(base.gameObject, UIContext.BlurType.None);
		FadeEffectsIn();
	}

	private void RewardObjectLoaded(Reward reward, object callbackData)
	{
		if (!(this == null))
		{
			bool updateCacheValues = (bool)callbackData;
			reward.Hide();
			reward.transform.parent = m_rewardBone;
			reward.transform.localEulerAngles = Vector3.zero;
			reward.transform.localScale = m_rewardScale;
			reward.transform.localPosition = Vector3.zero;
			BoosterPackReward componentInChildren = reward.gameObject.GetComponentInChildren<BoosterPackReward>();
			if (componentInChildren != null)
			{
				reward.transform.localScale = m_boosterRewardRootScale;
				reward.m_MeshRoot.transform.localPosition = m_boosterRewardPosition;
				reward.m_MeshRoot.transform.localScale = m_boosterRewardScale;
				componentInChildren.m_Layer = (GameLayer)base.gameObject.layer;
			}
			CardReward componentInChildren2 = reward.gameObject.GetComponentInChildren<CardReward>();
			if (componentInChildren2 != null)
			{
				reward.transform.localScale = m_cardRewardRootScale;
				componentInChildren2.m_cardParent.transform.localScale = m_cardRewardScale;
				componentInChildren2.m_cardParent.transform.localPosition = m_cardRewardLocation;
				componentInChildren2.m_duplicateCardParent.transform.localScale = m_cardDuplicateRewardScale;
				componentInChildren2.m_duplicateCardParent.transform.localPosition = m_cardDuplicateRewardLocation;
			}
			CardBackReward componentInChildren3 = reward.gameObject.GetComponentInChildren<CardBackReward>();
			if (componentInChildren3 != null)
			{
				reward.transform.localScale = m_cardBackRootScale;
				componentInChildren3.m_cardbackBone.transform.localScale = m_cardbackRewardScale;
				componentInChildren3.m_cardbackBone.transform.localPosition = m_cardbackRewardLocation;
			}
			GoldReward componentInChildren4 = reward.gameObject.GetComponentInChildren<GoldReward>();
			if (componentInChildren4 != null)
			{
				componentInChildren4.m_root.transform.localScale = m_goldRewardScale;
				componentInChildren4.m_rewardBannerBone.transform.localPosition += (Vector3)m_goldBannerOffset;
				componentInChildren4.m_rewardBannerBone.transform.localScale = m_goldBannerScale;
			}
			ArcaneDustReward componentInChildren5 = reward.gameObject.GetComponentInChildren<ArcaneDustReward>();
			if (componentInChildren5 != null)
			{
				componentInChildren5.m_root.transform.localScale = m_dustRewardScale;
				componentInChildren5.m_root.transform.localPosition += (Vector3)m_dustRewardOffset;
				componentInChildren5.m_rewardBannerBone.transform.localPosition += (Vector3)m_dustBannerOffset;
				componentInChildren5.m_rewardBannerBone.transform.localScale = m_dustBannerScale;
			}
			PopupRoot component = base.gameObject.GetComponent<PopupRoot>();
			if (component != null)
			{
				component.ApplyPopupRendering(reward.transform);
			}
			reward.Show(updateCacheValues);
		}
	}

	private void FadeEffectsIn()
	{
		if (m_showFullscreenEffects)
		{
			FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
			if (fullScreenFXMgr != null)
			{
				fullScreenFXMgr.SetBlurBrightness(1f);
				fullScreenFXMgr.SetBlurDesaturation(0f);
				fullScreenFXMgr.Vignette();
				fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc);
			}
		}
	}

	private void FadeEffectsOut()
	{
		if (m_showFullscreenEffects)
		{
			FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
			if (fullScreenFXMgr != null)
			{
				fullScreenFXMgr.StopVignette();
				fullScreenFXMgr.StopBlur();
			}
		}
	}
}
