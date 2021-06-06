using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reward : MonoBehaviour
{
	public enum Type
	{
		NONE = -1,
		ARCANE_DUST,
		BOOSTER_PACK,
		CARD,
		CARD_BACK,
		CRAFTABLE_CARD,
		FORGE_TICKET,
		GOLD,
		MOUNT,
		CLASS_CHALLENGE,
		EVENT,
		RANDOM_CARD,
		BONUS_CHALLENGE,
		ADVENTURE_DECK,
		ADVENTURE_HERO_POWER,
		ARCANE_ORBS,
		DECK,
		MINI_SET
	}

	public delegate void DelOnRewardLoaded(Reward reward, object callbackData);

	public class LoadRewardCallbackData
	{
		public DelOnRewardLoaded m_callback;

		public object m_callbackData;
	}

	public delegate void OnClickedCallback(Reward reward, object userData);

	private class OnClickedListener : EventListener<OnClickedCallback>
	{
		public void Fire(Reward reward)
		{
			m_callback(reward, m_userData);
		}
	}

	public delegate void OnHideCallback(object userData);

	private class OnHideListener : EventListener<OnHideCallback>
	{
		public void Fire()
		{
			m_callback(m_userData);
		}
	}

	public GameObject m_root;

	public bool m_showBanner = true;

	public bool m_playSounds = true;

	public RewardBanner m_rewardBannerPrefab;

	public GameObject m_rewardBannerBone;

	public PegUIElement m_clickCatcher;

	public GameObject m_MeshRoot;

	public Animator m_EchoAnimator;

	public float m_EchoHideMeshDelay = 0.65f;

	protected RewardBanner m_rewardBanner;

	private RewardData m_data;

	private Type m_type;

	private bool m_ready = true;

	protected bool m_shown;

	private List<OnClickedListener> m_clickListeners = new List<OnClickedListener>();

	private List<OnHideListener> m_hideListeners = new List<OnHideListener>();

	public Type RewardType => Data.RewardType;

	public RewardData Data => m_data;

	public bool IsShown => m_shown;

	protected virtual void Awake()
	{
		if (m_showBanner && m_rewardBannerPrefab != null)
		{
			if (!UniversalInputManager.UsePhoneUI)
			{
				m_rewardBanner = Object.Instantiate(m_rewardBannerPrefab);
				m_rewardBanner.gameObject.SetActive(value: false);
				m_rewardBanner.transform.parent = m_rewardBannerBone.transform;
				m_rewardBanner.transform.localPosition = Vector3.zero;
			}
			else
			{
				m_rewardBanner = (RewardBanner)GameUtils.Instantiate(m_rewardBannerPrefab, m_rewardBannerBone);
			}
		}
		EnableClickCatcher(enabled: false);
		SoundManager.Get().Load("game_end_reward.prefab:6c28275a79f151a478d49afc04533e72");
	}

	protected virtual void Start()
	{
		if (m_clickCatcher != null)
		{
			m_clickCatcher.AddEventListener(UIEventType.RELEASE, OnClickReleased);
		}
		Hide();
	}

	protected virtual void OnDestroy()
	{
	}

	public void Show(bool updateCacheValues)
	{
		Data.AcknowledgeNotices();
		if (m_MeshRoot != null)
		{
			m_MeshRoot.SetActive(value: true);
		}
		if (m_showBanner && m_rewardBanner != null)
		{
			m_rewardBanner.gameObject.SetActive(value: true);
		}
		else
		{
			if (m_rewardBannerBone != null)
			{
				m_rewardBannerBone.SetActive(value: false);
			}
			if (m_rewardBanner != null)
			{
				m_rewardBanner.gameObject.SetActive(value: false);
			}
		}
		if (m_playSounds)
		{
			PlayShowSounds();
		}
		ShowReward(updateCacheValues);
		m_shown = true;
	}

	protected virtual void PlayShowSounds()
	{
		SoundManager.Get().LoadAndPlay("Quest_Complete_Jingle.prefab:4b1a4bf5fece033469acee1944305ab1");
		SoundManager.Get().LoadAndPlay("quest_complete_pop_up.prefab:888f073a3b5d3e8418c2f989f3991bf7");
		SoundManager.Get().LoadAndPlay("tavern_crowd_play_reaction_positive_random.prefab:708bd64f76a706a45956e5566429c6c6");
	}

	public void HideWithFX()
	{
		StartCoroutine(HideFXAnimation());
	}

	private IEnumerator HideFXAnimation()
	{
		if ((bool)m_EchoAnimator)
		{
			m_EchoAnimator.enabled = true;
			yield return new WaitForSeconds(m_EchoHideMeshDelay);
			if (m_MeshRoot != null)
			{
				m_MeshRoot.SetActive(value: false);
			}
		}
		iTween.FadeTo(base.gameObject, 0f, RewardUtils.REWARD_HIDE_TIME);
	}

	public virtual void Hide(bool animate = false)
	{
		if (!animate)
		{
			OnHideAnimateComplete();
			return;
		}
		iTween.FadeTo(base.gameObject, 0f, RewardUtils.REWARD_HIDE_TIME);
		Hashtable args = iTween.Hash("scale", RewardUtils.REWARD_HIDDEN_SCALE, "time", RewardUtils.REWARD_HIDE_TIME, "oncomplete", "OnHideAnimateComplete", "oncompletetarget", base.gameObject);
		iTween.ScaleTo(base.gameObject, args);
	}

	private void OnHideAnimateComplete()
	{
		HideReward();
		m_shown = false;
	}

	public void SetData(RewardData data, bool updateVisuals)
	{
		m_data = data;
		OnDataSet(updateVisuals);
	}

	public void NotifyLoadedWhenReady(LoadRewardCallbackData loadRewardCallbackData)
	{
		StartCoroutine(WaitThenNotifyLoaded(loadRewardCallbackData));
	}

	public void EnableClickCatcher(bool enabled)
	{
		if (m_clickCatcher != null)
		{
			m_clickCatcher.gameObject.SetActive(enabled);
		}
	}

	public bool RegisterClickListener(OnClickedCallback callback)
	{
		return RegisterClickListener(callback, null);
	}

	public bool RegisterClickListener(OnClickedCallback callback, object userData)
	{
		OnClickedListener onClickedListener = new OnClickedListener();
		onClickedListener.SetCallback(callback);
		onClickedListener.SetUserData(userData);
		if (m_clickListeners.Contains(onClickedListener))
		{
			return false;
		}
		m_clickListeners.Add(onClickedListener);
		return true;
	}

	public bool RemoveClickListener(OnClickedCallback callback)
	{
		return RemoveClickListener(callback, null);
	}

	public bool RemoveClickListener(OnClickedCallback callback, object userData)
	{
		OnClickedListener onClickedListener = new OnClickedListener();
		onClickedListener.SetCallback(callback);
		onClickedListener.SetUserData(userData);
		return m_clickListeners.Remove(onClickedListener);
	}

	public bool RegisterHideListener(OnHideCallback callback)
	{
		return RegisterHideListener(callback, null);
	}

	public bool RegisterHideListener(OnHideCallback callback, object userData)
	{
		OnHideListener onHideListener = new OnHideListener();
		onHideListener.SetCallback(callback);
		onHideListener.SetUserData(userData);
		if (m_hideListeners.Contains(onHideListener))
		{
			return false;
		}
		m_hideListeners.Add(onHideListener);
		return true;
	}

	public void RemoveHideListener(OnHideCallback callback, object userData)
	{
		OnHideListener onHideListener = new OnHideListener();
		onHideListener.SetCallback(callback);
		onHideListener.SetUserData(userData);
		m_hideListeners.Remove(onHideListener);
	}

	protected abstract void InitData();

	protected virtual void ShowReward(bool updateCacheValues)
	{
	}

	protected virtual void OnDataSet(bool updateVisuals)
	{
	}

	protected virtual void HideReward()
	{
		OnHide();
	}

	protected Reward()
	{
		InitData();
	}

	protected void SetReady(bool ready)
	{
		m_ready = ready;
	}

	protected void SetRewardText(string headline, string details, string source)
	{
		if ((bool)UniversalInputManager.UsePhoneUI && RewardType != Type.GOLD)
		{
			details = "";
		}
		if (m_rewardBanner != null)
		{
			m_rewardBanner.SetText(headline, details, source);
		}
	}

	private IEnumerator WaitThenNotifyLoaded(LoadRewardCallbackData loadRewardCallbackData)
	{
		if (loadRewardCallbackData.m_callback != null)
		{
			while (!m_ready)
			{
				yield return null;
			}
			loadRewardCallbackData.m_callback(this, loadRewardCallbackData.m_callbackData);
		}
	}

	private void OnClickReleased(UIEvent e)
	{
		OnClickedListener[] array = m_clickListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(this);
		}
	}

	private void OnHide()
	{
		OnHideListener[] array = m_hideListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}
}
