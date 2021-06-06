using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008CD RID: 2253
public class GameToastMgr : MonoBehaviour
{
	// Token: 0x06007CA9 RID: 31913 RVA: 0x00288E57 File Offset: 0x00287057
	private void Awake()
	{
		GameToastMgr.s_instance = this;
	}

	// Token: 0x06007CAA RID: 31914 RVA: 0x00288E5F File Offset: 0x0028705F
	private void OnDestroy()
	{
		GameToastMgr.s_instance = null;
	}

	// Token: 0x06007CAB RID: 31915 RVA: 0x00288E67 File Offset: 0x00287067
	public static GameToastMgr Get()
	{
		return GameToastMgr.s_instance;
	}

	// Token: 0x06007CAC RID: 31916 RVA: 0x00288E70 File Offset: 0x00287070
	public bool RegisterQuestProgressToastShownListener(GameToastMgr.QuestProgressToastShownCallback callback)
	{
		if (callback == null)
		{
			return false;
		}
		GameToastMgr.QuestProgressToastShownListener questProgressToastShownListener = new GameToastMgr.QuestProgressToastShownListener();
		questProgressToastShownListener.SetCallback(callback);
		questProgressToastShownListener.SetUserData(null);
		if (this.m_questProgressToastShownListeners.Contains(questProgressToastShownListener))
		{
			return false;
		}
		this.m_questProgressToastShownListeners.Add(questProgressToastShownListener);
		return true;
	}

	// Token: 0x06007CAD RID: 31917 RVA: 0x00288EB4 File Offset: 0x002870B4
	public bool RemoveQuestProgressToastShownListener(GameToastMgr.QuestProgressToastShownCallback callback)
	{
		if (callback == null)
		{
			return false;
		}
		GameToastMgr.QuestProgressToastShownListener questProgressToastShownListener = new GameToastMgr.QuestProgressToastShownListener();
		questProgressToastShownListener.SetCallback(callback);
		questProgressToastShownListener.SetUserData(null);
		return this.m_questProgressToastShownListeners.Remove(questProgressToastShownListener);
	}

	// Token: 0x06007CAE RID: 31918 RVA: 0x00288EE8 File Offset: 0x002870E8
	private void FireAllQuestProgressListeners(int achieveId, int progress)
	{
		GameToastMgr.QuestProgressToastShownListener[] array = this.m_questProgressToastShownListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(achieveId);
		}
	}

	// Token: 0x06007CAF RID: 31919 RVA: 0x00288F18 File Offset: 0x00287118
	private bool AddToast(GameToast toast)
	{
		toast.transform.parent = OverlayUI.Get().m_QuestProgressToastBone.transform;
		toast.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
		toast.transform.localScale = new Vector3(110f, 1f, 110f);
		toast.transform.localPosition = Vector3.zero;
		this.m_toasts.Add(toast);
		RenderUtils.SetAlpha(toast.gameObject, 0f);
		this.UpdateToastPositions();
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			1f,
			"time",
			0.25f,
			"delay",
			0.25f,
			"easeType",
			iTween.EaseType.easeInCubic,
			"oncomplete",
			"FadeOutToast",
			"oncompletetarget",
			base.gameObject,
			"oncompleteparams",
			toast
		});
		iTween.FadeTo(toast.gameObject, args);
		return true;
	}

	// Token: 0x06007CB0 RID: 31920 RVA: 0x00289050 File Offset: 0x00287250
	public bool AreToastsActive()
	{
		return this.m_toasts.Count > 0;
	}

	// Token: 0x06007CB1 RID: 31921 RVA: 0x00289060 File Offset: 0x00287260
	private void FadeOutToast(GameToast toast)
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			0f,
			"delay",
			4f,
			"time",
			0.25f,
			"easeType",
			iTween.EaseType.easeInCubic,
			"oncomplete",
			"DeactivateToast",
			"oncompletetarget",
			base.gameObject,
			"oncompleteparams",
			toast
		});
		iTween.FadeTo(toast.gameObject, args);
	}

	// Token: 0x06007CB2 RID: 31922 RVA: 0x00289108 File Offset: 0x00287308
	private void DeactivateToast(GameToast toast)
	{
		toast.gameObject.SetActive(false);
		this.m_toasts.Remove(toast);
		this.UpdateToastPositions();
	}

	// Token: 0x06007CB3 RID: 31923 RVA: 0x0028912C File Offset: 0x0028732C
	private void UpdateToastPositions()
	{
		int num = 0;
		foreach (GameToast gameToast in this.m_toasts)
		{
			if (num > 0)
			{
				TransformUtil.SetPoint(gameToast.gameObject, Anchor.BOTTOM, this.m_toasts[num - 1].gameObject, Anchor.TOP, this.MULTIPLE_TOAST_OFFSET);
			}
			num++;
		}
	}

	// Token: 0x06007CB4 RID: 31924 RVA: 0x002891B0 File Offset: 0x002873B0
	public void UpdateQuestProgressToasts()
	{
		this.ShowQuestProgressToasts(AchieveManager.Get().GetNewlyProgressedQuests());
	}

	// Token: 0x06007CB5 RID: 31925 RVA: 0x002891C4 File Offset: 0x002873C4
	public void ShowQuestProgressToasts(List<Achievement> achievements)
	{
		foreach (Achievement achievement in achievements)
		{
			this.AddQuestProgressToast(achievement.ID, achievement.Name, achievement.Description, achievement.Progress, achievement.MaxProgress);
			achievement.AckCurrentProgressAndRewardNotices(true);
		}
	}

	// Token: 0x06007CB6 RID: 31926 RVA: 0x00289238 File Offset: 0x00287438
	public void AddQuestProgressToast(int achieveId, string questName, string questDescription, int progress, int maxProgress)
	{
		QuestProgressToast questProgressToast = UnityEngine.Object.Instantiate<QuestProgressToast>(this.m_questProgressToastPrefab);
		questProgressToast.UpdateDisplay(questName, questDescription, progress, maxProgress);
		if (this.AddToast(questProgressToast))
		{
			this.FireAllQuestProgressListeners(achieveId, progress);
		}
	}

	// Token: 0x04006566 RID: 25958
	private const float FADE_IN_TIME = 0.25f;

	// Token: 0x04006567 RID: 25959
	private const float FADE_OUT_TIME = 0.5f;

	// Token: 0x04006568 RID: 25960
	private const float HOLD_TIME = 4f;

	// Token: 0x04006569 RID: 25961
	private PlatformDependentValue<Vector3> MULTIPLE_TOAST_OFFSET = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
	{
		PC = new Vector3(0f, 0f, 30f),
		Phone = new Vector3(0f, 0f, 80f)
	};

	// Token: 0x0400656A RID: 25962
	public QuestProgressToast m_questProgressToastPrefab;

	// Token: 0x0400656B RID: 25963
	private static GameToastMgr s_instance;

	// Token: 0x0400656C RID: 25964
	private List<GameToast> m_toasts = new List<GameToast>();

	// Token: 0x0400656D RID: 25965
	private List<GameToastMgr.QuestProgressToastShownListener> m_questProgressToastShownListeners = new List<GameToastMgr.QuestProgressToastShownListener>();

	// Token: 0x02002554 RID: 9556
	// (Invoke) Token: 0x060132A8 RID: 78504
	public delegate void QuestProgressToastShownCallback(int achieveId);

	// Token: 0x02002555 RID: 9557
	private class QuestProgressToastShownListener : EventListener<GameToastMgr.QuestProgressToastShownCallback>
	{
		// Token: 0x060132AB RID: 78507 RVA: 0x0052B3A5 File Offset: 0x005295A5
		public void Fire(int achieveId)
		{
			this.m_callback(achieveId);
		}
	}
}
