using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

// Token: 0x0200064B RID: 1611
[CustomEditClass]
public class RankedRewardDisplay : MonoBehaviour
{
	// Token: 0x06005ACE RID: 23246 RVA: 0x001D9F53 File Offset: 0x001D8153
	private void Awake()
	{
		this.m_widget = base.GetComponent<WidgetTemplate>();
		this.Reset();
	}

	// Token: 0x06005ACF RID: 23247 RVA: 0x001D9F67 File Offset: 0x001D8167
	private void OnDestroy()
	{
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().m_hitbox.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClick));
		}
	}

	// Token: 0x06005AD0 RID: 23248 RVA: 0x001D9F94 File Offset: 0x001D8194
	public void Initialize(TranslatedMedalInfo medalInfo, List<List<RewardData>> rewardDataList, Action callback)
	{
		if (medalInfo == null)
		{
			return;
		}
		this.m_closedCallback = callback;
		RankedPlayDataModel dataModel = medalInfo.CreateDataModel(RankedMedal.DisplayMode.Chest, false, false, null);
		this.m_widget.BindDataModel(dataModel, false);
		foreach (List<RewardData> list in rewardDataList)
		{
			RewardListDataModel rewardListDataModel = new RewardListDataModel();
			foreach (RewardData rewardData in list)
			{
				if (rewardData != null)
				{
					rewardListDataModel.Items.Add(RewardUtils.RewardDataToRewardItemDataModel(rewardData));
				}
			}
			if (rewardListDataModel.Items.Count > 0)
			{
				this.m_rewardListDataModels.Add(rewardListDataModel);
			}
		}
		this.m_numAnimationsRemaining = this.m_rewardListDataModels.Count;
		this.BindNextRewardItemDataModel();
		this.m_widget.Hide();
	}

	// Token: 0x06005AD1 RID: 23249 RVA: 0x001DA08C File Offset: 0x001D828C
	[ContextMenu("Reset")]
	public void Reset()
	{
		this.m_debugClickCatcher.gameObject.SetActive(false);
		this.m_widget.Hide();
		this.m_isAnimating = false;
		this.m_isHidePending = false;
		this.m_fsm.SendEvent("Reset");
	}

	// Token: 0x06005AD2 RID: 23250 RVA: 0x001DA0C8 File Offset: 0x001D82C8
	public void Show()
	{
		base.StartCoroutine(this.ShowWhenReady());
	}

	// Token: 0x17000550 RID: 1360
	// (get) Token: 0x06005AD3 RID: 23251 RVA: 0x001DA0D7 File Offset: 0x001D82D7
	private bool IsReady
	{
		get
		{
			return this.m_widget != null && this.m_widget.IsReady && !this.m_widget.IsChangingStates;
		}
	}

	// Token: 0x06005AD4 RID: 23252 RVA: 0x001DA104 File Offset: 0x001D8304
	private IEnumerator ShowWhenReady()
	{
		while (!this.IsReady)
		{
			yield return null;
		}
		if (this.m_doPositionForDebugShow)
		{
			this.PositionForDebugShow();
		}
		this.m_widget.Show();
		AnimationUtil.ShowWithPunch(base.gameObject, this.m_startScale, this.m_punchScale, this.m_afterPunchScale, "OnShown", true, null, null, null);
		this.m_fsm.SendEvent("Birth");
		yield break;
	}

	// Token: 0x06005AD5 RID: 23253 RVA: 0x001DA113 File Offset: 0x001D8313
	private void OnShown()
	{
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().m_hitbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClick));
		}
	}

	// Token: 0x06005AD6 RID: 23254 RVA: 0x001DA140 File Offset: 0x001D8340
	private void BindNextRewardItemDataModel()
	{
		if (this.m_rewardListDataModels.Count > 0)
		{
			RewardListDataModel dataModel = this.m_rewardListDataModels[0];
			this.m_rewardListDataModels.RemoveAt(0);
			this.m_widget.BindDataModel(dataModel, false);
		}
	}

	// Token: 0x06005AD7 RID: 23255 RVA: 0x001DA184 File Offset: 0x001D8384
	private void OnPlayMakerNextRewardItem()
	{
		this.m_isAnimating = false;
		this.m_numAnimationsRemaining--;
		if (this.m_numAnimationsRemaining > 0)
		{
			this.BindNextRewardItemDataModel();
			this.m_widget.TriggerEvent("RevealRewardItem", default(Widget.TriggerEventParameters));
			return;
		}
		this.SendPlayMakerDeath();
	}

	// Token: 0x06005AD8 RID: 23256 RVA: 0x001DA1D6 File Offset: 0x001D83D6
	private void OnPlayMakerFinished()
	{
		this.Hide();
	}

	// Token: 0x06005AD9 RID: 23257 RVA: 0x001DA1E0 File Offset: 0x001D83E0
	private void OnClick(UIEvent e)
	{
		if (this.m_numAnimationsRemaining > 0)
		{
			if (!this.m_isAnimating)
			{
				this.m_widget.TriggerEvent("AnimateRewardItem", default(Widget.TriggerEventParameters));
				this.m_isAnimating = true;
				return;
			}
		}
		else
		{
			this.SendPlayMakerDeath();
		}
	}

	// Token: 0x06005ADA RID: 23258 RVA: 0x001DA226 File Offset: 0x001D8426
	private void SendPlayMakerDeath()
	{
		if (!this.m_isHidePending)
		{
			this.m_fsm.SendEvent("Death");
			this.m_isHidePending = true;
		}
	}

	// Token: 0x06005ADB RID: 23259 RVA: 0x001DA248 File Offset: 0x001D8448
	private void Hide()
	{
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().m_hitbox.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClick));
		}
		if (base.gameObject != null)
		{
			AnimationUtil.ScaleFade(base.gameObject, new Vector3(0.01f, 0.01f, 0.01f), "OnClosed");
		}
	}

	// Token: 0x06005ADC RID: 23260 RVA: 0x001DA2B1 File Offset: 0x001D84B1
	private void OnClosed()
	{
		Action closedCallback = this.m_closedCallback;
		if (closedCallback == null)
		{
			return;
		}
		closedCallback();
	}

	// Token: 0x06005ADD RID: 23261 RVA: 0x001DA2C4 File Offset: 0x001D84C4
	public static void DebugShowFake(int leagueId, int starLevel, FormatType formatType, List<List<RewardData>> rewardData)
	{
		TranslatedMedalInfo tmi = MedalInfoTranslator.CreateTranslatedMedalInfo(formatType, leagueId, starLevel, 1337);
		Widget widget = WidgetInstance.Create(RankMgr.RANKED_REWARD_DISPLAY_PREFAB, false);
		widget.RegisterReadyListener(delegate(object _)
		{
			RankedRewardDisplay componentInChildren = widget.GetComponentInChildren<RankedRewardDisplay>();
			componentInChildren.ActivateDebugEquivalentsOfEndGameScreen();
			componentInChildren.Initialize(tmi, rewardData, new Action(componentInChildren.OnDebugShowComplete));
			componentInChildren.Show();
		}, null, true);
	}

	// Token: 0x06005ADE RID: 23262 RVA: 0x001DA320 File Offset: 0x001D8520
	private void ActivateDebugEquivalentsOfEndGameScreen()
	{
		FullScreenFXMgr.Get().SetBlurDesaturation(0.5f);
		FullScreenFXMgr.Get().Blur(1f, 0.5f, iTween.EaseType.easeInCirc, null);
		this.m_debugClickCatcher.gameObject.SetActive(true);
		this.m_debugClickCatcher.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClick));
		this.m_doPositionForDebugShow = true;
	}

	// Token: 0x06005ADF RID: 23263 RVA: 0x001DA384 File Offset: 0x001D8584
	private void PositionForDebugShow()
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			base.transform.localPosition = new Vector3(0f, 156.5f, 1.4f);
			return;
		}
		base.transform.localPosition = new Vector3(0f, 292f, -9f);
	}

	// Token: 0x06005AE0 RID: 23264 RVA: 0x001D9977 File Offset: 0x001D7B77
	private void OnDebugShowComplete()
	{
		FullScreenFXMgr.Get().StopAllEffects(0f);
		UnityEngine.Object.Destroy(base.transform.parent.gameObject);
	}

	// Token: 0x04004D9D RID: 19869
	[CustomEditField(Sections = "Animate In")]
	public Vector3_MobileOverride m_startScale;

	// Token: 0x04004D9E RID: 19870
	[CustomEditField(Sections = "Animate In")]
	public Vector3_MobileOverride m_punchScale;

	// Token: 0x04004D9F RID: 19871
	[CustomEditField(Sections = "Animate In")]
	public Vector3_MobileOverride m_afterPunchScale;

	// Token: 0x04004DA0 RID: 19872
	public PlayMakerFSM m_fsm;

	// Token: 0x04004DA1 RID: 19873
	public PegUIElement m_debugClickCatcher;

	// Token: 0x04004DA2 RID: 19874
	private Widget m_widget;

	// Token: 0x04004DA3 RID: 19875
	private List<RewardListDataModel> m_rewardListDataModels = new List<RewardListDataModel>();

	// Token: 0x04004DA4 RID: 19876
	private Action m_closedCallback;

	// Token: 0x04004DA5 RID: 19877
	private bool m_isHidePending;

	// Token: 0x04004DA6 RID: 19878
	private bool m_isAnimating;

	// Token: 0x04004DA7 RID: 19879
	private int m_numAnimationsRemaining;

	// Token: 0x04004DA8 RID: 19880
	private bool m_doPositionForDebugShow;
}
