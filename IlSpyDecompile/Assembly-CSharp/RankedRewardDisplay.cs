using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class RankedRewardDisplay : MonoBehaviour
{
	[CustomEditField(Sections = "Animate In")]
	public Vector3_MobileOverride m_startScale;

	[CustomEditField(Sections = "Animate In")]
	public Vector3_MobileOverride m_punchScale;

	[CustomEditField(Sections = "Animate In")]
	public Vector3_MobileOverride m_afterPunchScale;

	public PlayMakerFSM m_fsm;

	public PegUIElement m_debugClickCatcher;

	private Widget m_widget;

	private List<RewardListDataModel> m_rewardListDataModels = new List<RewardListDataModel>();

	private Action m_closedCallback;

	private bool m_isHidePending;

	private bool m_isAnimating;

	private int m_numAnimationsRemaining;

	private bool m_doPositionForDebugShow;

	private bool IsReady
	{
		get
		{
			if (m_widget != null && m_widget.IsReady)
			{
				return !m_widget.IsChangingStates;
			}
			return false;
		}
	}

	private void Awake()
	{
		m_widget = GetComponent<WidgetTemplate>();
		Reset();
	}

	private void OnDestroy()
	{
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().m_hitbox.RemoveEventListener(UIEventType.RELEASE, OnClick);
		}
	}

	public void Initialize(TranslatedMedalInfo medalInfo, List<List<RewardData>> rewardDataList, Action callback)
	{
		if (medalInfo == null)
		{
			return;
		}
		m_closedCallback = callback;
		RankedPlayDataModel dataModel = medalInfo.CreateDataModel(RankedMedal.DisplayMode.Chest);
		m_widget.BindDataModel(dataModel);
		foreach (List<RewardData> rewardData in rewardDataList)
		{
			RewardListDataModel rewardListDataModel = new RewardListDataModel();
			foreach (RewardData item in rewardData)
			{
				if (item != null)
				{
					rewardListDataModel.Items.Add(RewardUtils.RewardDataToRewardItemDataModel(item));
				}
			}
			if (rewardListDataModel.Items.Count > 0)
			{
				m_rewardListDataModels.Add(rewardListDataModel);
			}
		}
		m_numAnimationsRemaining = m_rewardListDataModels.Count;
		BindNextRewardItemDataModel();
		m_widget.Hide();
	}

	[ContextMenu("Reset")]
	public void Reset()
	{
		m_debugClickCatcher.gameObject.SetActive(value: false);
		m_widget.Hide();
		m_isAnimating = false;
		m_isHidePending = false;
		m_fsm.SendEvent("Reset");
	}

	public void Show()
	{
		StartCoroutine(ShowWhenReady());
	}

	private IEnumerator ShowWhenReady()
	{
		while (!IsReady)
		{
			yield return null;
		}
		if (m_doPositionForDebugShow)
		{
			PositionForDebugShow();
		}
		m_widget.Show();
		AnimationUtil.ShowWithPunch(base.gameObject, m_startScale, m_punchScale, m_afterPunchScale, "OnShown", noFade: true);
		m_fsm.SendEvent("Birth");
	}

	private void OnShown()
	{
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().m_hitbox.AddEventListener(UIEventType.RELEASE, OnClick);
		}
	}

	private void BindNextRewardItemDataModel()
	{
		if (m_rewardListDataModels.Count > 0)
		{
			RewardListDataModel dataModel = m_rewardListDataModels[0];
			m_rewardListDataModels.RemoveAt(0);
			m_widget.BindDataModel(dataModel);
		}
	}

	private void OnPlayMakerNextRewardItem()
	{
		m_isAnimating = false;
		m_numAnimationsRemaining--;
		if (m_numAnimationsRemaining > 0)
		{
			BindNextRewardItemDataModel();
			m_widget.TriggerEvent("RevealRewardItem");
		}
		else
		{
			SendPlayMakerDeath();
		}
	}

	private void OnPlayMakerFinished()
	{
		Hide();
	}

	private void OnClick(UIEvent e)
	{
		if (m_numAnimationsRemaining > 0)
		{
			if (!m_isAnimating)
			{
				m_widget.TriggerEvent("AnimateRewardItem");
				m_isAnimating = true;
			}
		}
		else
		{
			SendPlayMakerDeath();
		}
	}

	private void SendPlayMakerDeath()
	{
		if (!m_isHidePending)
		{
			m_fsm.SendEvent("Death");
			m_isHidePending = true;
		}
	}

	private void Hide()
	{
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().m_hitbox.RemoveEventListener(UIEventType.RELEASE, OnClick);
		}
		if (base.gameObject != null)
		{
			AnimationUtil.ScaleFade(base.gameObject, new Vector3(0.01f, 0.01f, 0.01f), "OnClosed");
		}
	}

	private void OnClosed()
	{
		m_closedCallback?.Invoke();
	}

	public static void DebugShowFake(int leagueId, int starLevel, FormatType formatType, List<List<RewardData>> rewardData)
	{
		TranslatedMedalInfo tmi = MedalInfoTranslator.CreateTranslatedMedalInfo(formatType, leagueId, starLevel, 1337);
		Widget widget = WidgetInstance.Create(RankMgr.RANKED_REWARD_DISPLAY_PREFAB);
		widget.RegisterReadyListener(delegate
		{
			RankedRewardDisplay componentInChildren = widget.GetComponentInChildren<RankedRewardDisplay>();
			componentInChildren.ActivateDebugEquivalentsOfEndGameScreen();
			componentInChildren.Initialize(tmi, rewardData, componentInChildren.OnDebugShowComplete);
			componentInChildren.Show();
		});
	}

	private void ActivateDebugEquivalentsOfEndGameScreen()
	{
		FullScreenFXMgr.Get().SetBlurDesaturation(0.5f);
		FullScreenFXMgr.Get().Blur(1f, 0.5f, iTween.EaseType.easeInCirc);
		m_debugClickCatcher.gameObject.SetActive(value: true);
		m_debugClickCatcher.AddEventListener(UIEventType.RELEASE, OnClick);
		m_doPositionForDebugShow = true;
	}

	private void PositionForDebugShow()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			base.transform.localPosition = new Vector3(0f, 156.5f, 1.4f);
		}
		else
		{
			base.transform.localPosition = new Vector3(0f, 292f, -9f);
		}
	}

	private void OnDebugShowComplete()
	{
		FullScreenFXMgr.Get().StopAllEffects();
		UnityEngine.Object.Destroy(base.transform.parent.gameObject);
	}
}
