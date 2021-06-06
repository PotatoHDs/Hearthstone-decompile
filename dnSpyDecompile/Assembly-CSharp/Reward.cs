using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000679 RID: 1657
public abstract class Reward : MonoBehaviour
{
	// Token: 0x06005CAD RID: 23725 RVA: 0x001E0F78 File Offset: 0x001DF178
	protected virtual void Awake()
	{
		if (this.m_showBanner && this.m_rewardBannerPrefab != null)
		{
			if (!UniversalInputManager.UsePhoneUI)
			{
				this.m_rewardBanner = UnityEngine.Object.Instantiate<RewardBanner>(this.m_rewardBannerPrefab);
				this.m_rewardBanner.gameObject.SetActive(false);
				this.m_rewardBanner.transform.parent = this.m_rewardBannerBone.transform;
				this.m_rewardBanner.transform.localPosition = Vector3.zero;
			}
			else
			{
				this.m_rewardBanner = (RewardBanner)GameUtils.Instantiate(this.m_rewardBannerPrefab, this.m_rewardBannerBone, false);
			}
		}
		this.EnableClickCatcher(false);
		SoundManager.Get().Load("game_end_reward.prefab:6c28275a79f151a478d49afc04533e72");
	}

	// Token: 0x06005CAE RID: 23726 RVA: 0x001E1037 File Offset: 0x001DF237
	protected virtual void Start()
	{
		if (this.m_clickCatcher != null)
		{
			this.m_clickCatcher.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClickReleased));
		}
		this.Hide(false);
	}

	// Token: 0x06005CAF RID: 23727 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnDestroy()
	{
	}

	// Token: 0x1700057A RID: 1402
	// (get) Token: 0x06005CB0 RID: 23728 RVA: 0x001E1067 File Offset: 0x001DF267
	public Reward.Type RewardType
	{
		get
		{
			return this.Data.RewardType;
		}
	}

	// Token: 0x1700057B RID: 1403
	// (get) Token: 0x06005CB1 RID: 23729 RVA: 0x001E1074 File Offset: 0x001DF274
	public RewardData Data
	{
		get
		{
			return this.m_data;
		}
	}

	// Token: 0x1700057C RID: 1404
	// (get) Token: 0x06005CB2 RID: 23730 RVA: 0x001E107C File Offset: 0x001DF27C
	public bool IsShown
	{
		get
		{
			return this.m_shown;
		}
	}

	// Token: 0x06005CB3 RID: 23731 RVA: 0x001E1084 File Offset: 0x001DF284
	public void Show(bool updateCacheValues)
	{
		this.Data.AcknowledgeNotices();
		if (this.m_MeshRoot != null)
		{
			this.m_MeshRoot.SetActive(true);
		}
		if (this.m_showBanner && this.m_rewardBanner != null)
		{
			this.m_rewardBanner.gameObject.SetActive(true);
		}
		else
		{
			if (this.m_rewardBannerBone != null)
			{
				this.m_rewardBannerBone.SetActive(false);
			}
			if (this.m_rewardBanner != null)
			{
				this.m_rewardBanner.gameObject.SetActive(false);
			}
		}
		if (this.m_playSounds)
		{
			this.PlayShowSounds();
		}
		this.ShowReward(updateCacheValues);
		this.m_shown = true;
	}

	// Token: 0x06005CB4 RID: 23732 RVA: 0x001E1134 File Offset: 0x001DF334
	protected virtual void PlayShowSounds()
	{
		SoundManager.Get().LoadAndPlay("Quest_Complete_Jingle.prefab:4b1a4bf5fece033469acee1944305ab1");
		SoundManager.Get().LoadAndPlay("quest_complete_pop_up.prefab:888f073a3b5d3e8418c2f989f3991bf7");
		SoundManager.Get().LoadAndPlay("tavern_crowd_play_reaction_positive_random.prefab:708bd64f76a706a45956e5566429c6c6");
	}

	// Token: 0x06005CB5 RID: 23733 RVA: 0x001E1172 File Offset: 0x001DF372
	public void HideWithFX()
	{
		base.StartCoroutine(this.HideFXAnimation());
	}

	// Token: 0x06005CB6 RID: 23734 RVA: 0x001E1181 File Offset: 0x001DF381
	private IEnumerator HideFXAnimation()
	{
		if (this.m_EchoAnimator)
		{
			this.m_EchoAnimator.enabled = true;
			yield return new WaitForSeconds(this.m_EchoHideMeshDelay);
			if (this.m_MeshRoot != null)
			{
				this.m_MeshRoot.SetActive(false);
			}
		}
		iTween.FadeTo(base.gameObject, 0f, RewardUtils.REWARD_HIDE_TIME);
		yield break;
	}

	// Token: 0x06005CB7 RID: 23735 RVA: 0x001E1190 File Offset: 0x001DF390
	public virtual void Hide(bool animate = false)
	{
		if (!animate)
		{
			this.OnHideAnimateComplete();
			return;
		}
		iTween.FadeTo(base.gameObject, 0f, RewardUtils.REWARD_HIDE_TIME);
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			RewardUtils.REWARD_HIDDEN_SCALE,
			"time",
			RewardUtils.REWARD_HIDE_TIME,
			"oncomplete",
			"OnHideAnimateComplete",
			"oncompletetarget",
			base.gameObject
		});
		iTween.ScaleTo(base.gameObject, args);
	}

	// Token: 0x06005CB8 RID: 23736 RVA: 0x001E121F File Offset: 0x001DF41F
	private void OnHideAnimateComplete()
	{
		this.HideReward();
		this.m_shown = false;
	}

	// Token: 0x06005CB9 RID: 23737 RVA: 0x001E122E File Offset: 0x001DF42E
	public void SetData(RewardData data, bool updateVisuals)
	{
		this.m_data = data;
		this.OnDataSet(updateVisuals);
	}

	// Token: 0x06005CBA RID: 23738 RVA: 0x001E123E File Offset: 0x001DF43E
	public void NotifyLoadedWhenReady(Reward.LoadRewardCallbackData loadRewardCallbackData)
	{
		base.StartCoroutine(this.WaitThenNotifyLoaded(loadRewardCallbackData));
	}

	// Token: 0x06005CBB RID: 23739 RVA: 0x001E124E File Offset: 0x001DF44E
	public void EnableClickCatcher(bool enabled)
	{
		if (this.m_clickCatcher != null)
		{
			this.m_clickCatcher.gameObject.SetActive(enabled);
		}
	}

	// Token: 0x06005CBC RID: 23740 RVA: 0x001E126F File Offset: 0x001DF46F
	public bool RegisterClickListener(Reward.OnClickedCallback callback)
	{
		return this.RegisterClickListener(callback, null);
	}

	// Token: 0x06005CBD RID: 23741 RVA: 0x001E127C File Offset: 0x001DF47C
	public bool RegisterClickListener(Reward.OnClickedCallback callback, object userData)
	{
		Reward.OnClickedListener onClickedListener = new Reward.OnClickedListener();
		onClickedListener.SetCallback(callback);
		onClickedListener.SetUserData(userData);
		if (this.m_clickListeners.Contains(onClickedListener))
		{
			return false;
		}
		this.m_clickListeners.Add(onClickedListener);
		return true;
	}

	// Token: 0x06005CBE RID: 23742 RVA: 0x001E12BA File Offset: 0x001DF4BA
	public bool RemoveClickListener(Reward.OnClickedCallback callback)
	{
		return this.RemoveClickListener(callback, null);
	}

	// Token: 0x06005CBF RID: 23743 RVA: 0x001E12C4 File Offset: 0x001DF4C4
	public bool RemoveClickListener(Reward.OnClickedCallback callback, object userData)
	{
		Reward.OnClickedListener onClickedListener = new Reward.OnClickedListener();
		onClickedListener.SetCallback(callback);
		onClickedListener.SetUserData(userData);
		return this.m_clickListeners.Remove(onClickedListener);
	}

	// Token: 0x06005CC0 RID: 23744 RVA: 0x001E12F1 File Offset: 0x001DF4F1
	public bool RegisterHideListener(Reward.OnHideCallback callback)
	{
		return this.RegisterHideListener(callback, null);
	}

	// Token: 0x06005CC1 RID: 23745 RVA: 0x001E12FC File Offset: 0x001DF4FC
	public bool RegisterHideListener(Reward.OnHideCallback callback, object userData)
	{
		Reward.OnHideListener onHideListener = new Reward.OnHideListener();
		onHideListener.SetCallback(callback);
		onHideListener.SetUserData(userData);
		if (this.m_hideListeners.Contains(onHideListener))
		{
			return false;
		}
		this.m_hideListeners.Add(onHideListener);
		return true;
	}

	// Token: 0x06005CC2 RID: 23746 RVA: 0x001E133C File Offset: 0x001DF53C
	public void RemoveHideListener(Reward.OnHideCallback callback, object userData)
	{
		Reward.OnHideListener onHideListener = new Reward.OnHideListener();
		onHideListener.SetCallback(callback);
		onHideListener.SetUserData(userData);
		this.m_hideListeners.Remove(onHideListener);
	}

	// Token: 0x06005CC3 RID: 23747
	protected abstract void InitData();

	// Token: 0x06005CC4 RID: 23748 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void ShowReward(bool updateCacheValues)
	{
	}

	// Token: 0x06005CC5 RID: 23749 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnDataSet(bool updateVisuals)
	{
	}

	// Token: 0x06005CC6 RID: 23750 RVA: 0x001E136A File Offset: 0x001DF56A
	protected virtual void HideReward()
	{
		this.OnHide();
	}

	// Token: 0x06005CC7 RID: 23751 RVA: 0x001E1374 File Offset: 0x001DF574
	protected Reward()
	{
		this.InitData();
	}

	// Token: 0x06005CC8 RID: 23752 RVA: 0x001E13C3 File Offset: 0x001DF5C3
	protected void SetReady(bool ready)
	{
		this.m_ready = ready;
	}

	// Token: 0x06005CC9 RID: 23753 RVA: 0x001E13CC File Offset: 0x001DF5CC
	protected void SetRewardText(string headline, string details, string source)
	{
		if (UniversalInputManager.UsePhoneUI && this.RewardType != Reward.Type.GOLD)
		{
			details = "";
		}
		if (this.m_rewardBanner != null)
		{
			this.m_rewardBanner.SetText(headline, details, source);
		}
	}

	// Token: 0x06005CCA RID: 23754 RVA: 0x001E1406 File Offset: 0x001DF606
	private IEnumerator WaitThenNotifyLoaded(Reward.LoadRewardCallbackData loadRewardCallbackData)
	{
		if (loadRewardCallbackData.m_callback == null)
		{
			yield break;
		}
		while (!this.m_ready)
		{
			yield return null;
		}
		loadRewardCallbackData.m_callback(this, loadRewardCallbackData.m_callbackData);
		yield break;
	}

	// Token: 0x06005CCB RID: 23755 RVA: 0x001E141C File Offset: 0x001DF61C
	private void OnClickReleased(UIEvent e)
	{
		Reward.OnClickedListener[] array = this.m_clickListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(this);
		}
	}

	// Token: 0x06005CCC RID: 23756 RVA: 0x001E144C File Offset: 0x001DF64C
	private void OnHide()
	{
		Reward.OnHideListener[] array = this.m_hideListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	// Token: 0x04004EA3 RID: 20131
	public GameObject m_root;

	// Token: 0x04004EA4 RID: 20132
	public bool m_showBanner = true;

	// Token: 0x04004EA5 RID: 20133
	public bool m_playSounds = true;

	// Token: 0x04004EA6 RID: 20134
	public RewardBanner m_rewardBannerPrefab;

	// Token: 0x04004EA7 RID: 20135
	public GameObject m_rewardBannerBone;

	// Token: 0x04004EA8 RID: 20136
	public PegUIElement m_clickCatcher;

	// Token: 0x04004EA9 RID: 20137
	public GameObject m_MeshRoot;

	// Token: 0x04004EAA RID: 20138
	public Animator m_EchoAnimator;

	// Token: 0x04004EAB RID: 20139
	public float m_EchoHideMeshDelay = 0.65f;

	// Token: 0x04004EAC RID: 20140
	protected RewardBanner m_rewardBanner;

	// Token: 0x04004EAD RID: 20141
	private RewardData m_data;

	// Token: 0x04004EAE RID: 20142
	private Reward.Type m_type;

	// Token: 0x04004EAF RID: 20143
	private bool m_ready = true;

	// Token: 0x04004EB0 RID: 20144
	protected bool m_shown;

	// Token: 0x04004EB1 RID: 20145
	private List<Reward.OnClickedListener> m_clickListeners = new List<Reward.OnClickedListener>();

	// Token: 0x04004EB2 RID: 20146
	private List<Reward.OnHideListener> m_hideListeners = new List<Reward.OnHideListener>();

	// Token: 0x0200217E RID: 8574
	public enum Type
	{
		// Token: 0x0400E07B RID: 57467
		NONE = -1,
		// Token: 0x0400E07C RID: 57468
		ARCANE_DUST,
		// Token: 0x0400E07D RID: 57469
		BOOSTER_PACK,
		// Token: 0x0400E07E RID: 57470
		CARD,
		// Token: 0x0400E07F RID: 57471
		CARD_BACK,
		// Token: 0x0400E080 RID: 57472
		CRAFTABLE_CARD,
		// Token: 0x0400E081 RID: 57473
		FORGE_TICKET,
		// Token: 0x0400E082 RID: 57474
		GOLD,
		// Token: 0x0400E083 RID: 57475
		MOUNT,
		// Token: 0x0400E084 RID: 57476
		CLASS_CHALLENGE,
		// Token: 0x0400E085 RID: 57477
		EVENT,
		// Token: 0x0400E086 RID: 57478
		RANDOM_CARD,
		// Token: 0x0400E087 RID: 57479
		BONUS_CHALLENGE,
		// Token: 0x0400E088 RID: 57480
		ADVENTURE_DECK,
		// Token: 0x0400E089 RID: 57481
		ADVENTURE_HERO_POWER,
		// Token: 0x0400E08A RID: 57482
		ARCANE_ORBS,
		// Token: 0x0400E08B RID: 57483
		DECK,
		// Token: 0x0400E08C RID: 57484
		MINI_SET
	}

	// Token: 0x0200217F RID: 8575
	// (Invoke) Token: 0x060123AE RID: 74670
	public delegate void DelOnRewardLoaded(Reward reward, object callbackData);

	// Token: 0x02002180 RID: 8576
	public class LoadRewardCallbackData
	{
		// Token: 0x0400E08D RID: 57485
		public Reward.DelOnRewardLoaded m_callback;

		// Token: 0x0400E08E RID: 57486
		public object m_callbackData;
	}

	// Token: 0x02002181 RID: 8577
	// (Invoke) Token: 0x060123B3 RID: 74675
	public delegate void OnClickedCallback(Reward reward, object userData);

	// Token: 0x02002182 RID: 8578
	private class OnClickedListener : EventListener<Reward.OnClickedCallback>
	{
		// Token: 0x060123B6 RID: 74678 RVA: 0x00501A95 File Offset: 0x004FFC95
		public void Fire(Reward reward)
		{
			this.m_callback(reward, this.m_userData);
		}
	}

	// Token: 0x02002183 RID: 8579
	// (Invoke) Token: 0x060123B9 RID: 74681
	public delegate void OnHideCallback(object userData);

	// Token: 0x02002184 RID: 8580
	private class OnHideListener : EventListener<Reward.OnHideCallback>
	{
		// Token: 0x060123BC RID: 74684 RVA: 0x00501AB1 File Offset: 0x004FFCB1
		public void Fire()
		{
			this.m_callback(this.m_userData);
		}
	}
}
