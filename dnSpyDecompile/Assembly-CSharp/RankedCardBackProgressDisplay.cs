using System;
using System.Collections;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

// Token: 0x02000647 RID: 1607
[CustomEditClass]
public class RankedCardBackProgressDisplay : MonoBehaviour
{
	// Token: 0x06005AA6 RID: 23206 RVA: 0x001D9544 File Offset: 0x001D7744
	private void Awake()
	{
		this.m_widget = base.GetComponent<WidgetTemplate>();
		this.Reset();
	}

	// Token: 0x06005AA7 RID: 23207 RVA: 0x001D9558 File Offset: 0x001D7758
	private void OnDestroy()
	{
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().m_hitbox.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClick));
		}
	}

	// Token: 0x1700054D RID: 1357
	// (get) Token: 0x06005AA8 RID: 23208 RVA: 0x001D9584 File Offset: 0x001D7784
	private bool IsReady
	{
		get
		{
			return this.m_widget != null && this.m_widget.IsReady && !this.m_widget.IsChangingStates;
		}
	}

	// Token: 0x06005AA9 RID: 23209 RVA: 0x001D95B4 File Offset: 0x001D77B4
	public void Initialize(MedalInfoTranslator medalInfo, Action callback)
	{
		if (medalInfo == null)
		{
			return;
		}
		this.m_medalInfo = medalInfo;
		this.m_closedCallback = callback;
		int currentSeasonId = this.m_medalInfo.GetCurrentSeasonId();
		this.m_winsNeeded = this.m_medalInfo.GetSeasonCardBackMinWins();
		this.m_prevWins = Mathf.Min(this.m_medalInfo.TotalRankedWinsPrevious, this.m_winsNeeded);
		this.m_currWins = Mathf.Min(this.m_medalInfo.TotalRankedWins, this.m_winsNeeded);
		FormatType @enum = Options.Get().GetEnum<FormatType>(Option.FORMAT_TYPE);
		bool isTooltipEnabled = false;
		bool hasEarnedCardBack = this.m_medalInfo.HasEarnedSeasonCardBack();
		RankedPlayDataModel dataModel = this.m_medalInfo.CreateDataModel(@enum, RankedMedal.DisplayMode.Default, isTooltipEnabled, hasEarnedCardBack, null);
		this.m_widget.BindDataModel(dataModel, false);
		CardBackDataModel dataModel2 = new CardBackDataModel
		{
			CardBackId = RankMgr.Get().GetRankedCardBackIdForSeasonId(currentSeasonId)
		};
		this.m_widget.BindDataModel(dataModel2, false);
		this.m_widget.Hide();
	}

	// Token: 0x06005AAA RID: 23210 RVA: 0x001D9697 File Offset: 0x001D7897
	[ContextMenu("Reset")]
	public void Reset()
	{
		this.m_debugClickCatcher.gameObject.SetActive(false);
		this.m_widget.Hide();
		this.m_fsm.SendEvent("Reset");
	}

	// Token: 0x06005AAB RID: 23211 RVA: 0x001D96C5 File Offset: 0x001D78C5
	public void Show()
	{
		base.StartCoroutine(this.ShowWhenReady());
	}

	// Token: 0x06005AAC RID: 23212 RVA: 0x001D96D4 File Offset: 0x001D78D4
	private IEnumerator ShowWhenReady()
	{
		while (!this.IsReady)
		{
			yield return null;
		}
		if (this.m_isDebugShow)
		{
			this.PositionForDebugShow();
		}
		float progressBar = (float)this.m_prevWins / (float)this.m_winsNeeded;
		this.m_progressBar = this.m_widget.GetComponentInChildren<ProgressBar>();
		if (this.m_progressBar != null)
		{
			this.m_progressBar.SetLabel(GameStrings.Format("GLOBAL_REWARD_PROGRESS", new object[]
			{
				this.m_prevWins,
				this.m_winsNeeded
			}));
			this.m_progressBar.SetProgressBar(progressBar);
		}
		this.m_footerText.Text = GameStrings.Format("GLOBAL_REMINDER_CARDBACK_SEASON_END_DIALOG", new object[]
		{
			this.m_medalInfo.GetSeasonCardBackWinsRemaining()
		});
		this.m_widget.Show();
		AnimationUtil.ShowWithPunch(base.gameObject, this.m_startScale, this.m_punchScale, this.m_afterPunchScale, "OnShown", true, null, null, null);
		this.m_fsm.SendEvent("Birth");
		yield break;
	}

	// Token: 0x06005AAD RID: 23213 RVA: 0x001D96E4 File Offset: 0x001D78E4
	private void OnShown()
	{
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().m_hitbox.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClick));
		}
		if (this.m_currWins > this.m_prevWins)
		{
			if (this.m_progressBar != null)
			{
				float currVal = (float)this.m_currWins / (float)this.m_winsNeeded;
				this.m_progressBar.m_increaseAnimTime = this.m_progressBarAnimTime;
				this.m_progressBar.AnimateProgress(this.m_progressBar.Progress, currVal, iTween.EaseType.easeInOutQuad);
			}
			float delay = this.m_progressBarAnimTime / (float)this.m_winsNeeded;
			base.StartCoroutine(this.WaitThenTriggerPlayMaker(delay));
		}
	}

	// Token: 0x06005AAE RID: 23214 RVA: 0x001D978D File Offset: 0x001D798D
	private IEnumerator WaitThenTriggerPlayMaker(float delay)
	{
		yield return new WaitForSeconds(delay);
		this.m_progressBar.SetLabel(GameStrings.Format("GLOBAL_REWARD_PROGRESS", new object[]
		{
			this.m_currWins,
			this.m_winsNeeded
		}));
		if (this.m_currWins > this.m_prevWins && this.m_currWins >= this.m_winsNeeded)
		{
			this.m_fsm.SendEvent("StartAnim");
		}
		yield break;
	}

	// Token: 0x06005AAF RID: 23215 RVA: 0x001D97A3 File Offset: 0x001D79A3
	private void OnPlayMakerFinished()
	{
		this.Hide();
	}

	// Token: 0x06005AB0 RID: 23216 RVA: 0x001D97AC File Offset: 0x001D79AC
	private void OnClick(UIEvent e)
	{
		this.m_fsm.SendEvent("Death");
		this.m_widget.TriggerEvent("HIDE_FOOTER_TEXT", default(Widget.TriggerEventParameters));
	}

	// Token: 0x06005AB1 RID: 23217 RVA: 0x001D97E4 File Offset: 0x001D79E4
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

	// Token: 0x06005AB2 RID: 23218 RVA: 0x001D984D File Offset: 0x001D7A4D
	private void OnClosed()
	{
		Action closedCallback = this.m_closedCallback;
		if (closedCallback == null)
		{
			return;
		}
		closedCallback();
	}

	// Token: 0x06005AB3 RID: 23219 RVA: 0x001D9860 File Offset: 0x001D7A60
	public static void DebugShowFake(MedalInfoTranslator medalInfo)
	{
		Widget widget = WidgetInstance.Create(RankMgr.RANKED_CARDBACK_PROGRESS_DISPLAY_PREFAB, false);
		widget.RegisterReadyListener(delegate(object _)
		{
			RankedCardBackProgressDisplay componentInChildren = widget.GetComponentInChildren<RankedCardBackProgressDisplay>();
			componentInChildren.ActivateDebugEquivalentsOfEndGameScreen();
			componentInChildren.Initialize(medalInfo, new Action(componentInChildren.OnDebugShowComplete));
			componentInChildren.Show();
		}, null, true);
	}

	// Token: 0x06005AB4 RID: 23220 RVA: 0x001D98AC File Offset: 0x001D7AAC
	private void ActivateDebugEquivalentsOfEndGameScreen()
	{
		FullScreenFXMgr.Get().SetBlurDesaturation(0.5f);
		FullScreenFXMgr.Get().Blur(1f, 0.5f, iTween.EaseType.easeInCirc, null);
		this.m_debugClickCatcher.gameObject.SetActive(true);
		this.m_debugClickCatcher.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClick));
		this.m_isDebugShow = true;
	}

	// Token: 0x06005AB5 RID: 23221 RVA: 0x001D9910 File Offset: 0x001D7B10
	private void PositionForDebugShow()
	{
		Camera component = Box.Get().GetBoxCamera().m_IgnoreFullscreenEffectsCamera.GetComponent<Camera>();
		base.transform.localPosition = component.transform.position + (component.nearClipPlane + 0.04f * (component.farClipPlane - component.nearClipPlane)) * component.transform.forward;
	}

	// Token: 0x06005AB6 RID: 23222 RVA: 0x001D9977 File Offset: 0x001D7B77
	private void OnDebugShowComplete()
	{
		FullScreenFXMgr.Get().StopAllEffects(0f);
		UnityEngine.Object.Destroy(base.transform.parent.gameObject);
	}

	// Token: 0x04004D85 RID: 19845
	[CustomEditField(Sections = "Animate In")]
	public Vector3_MobileOverride m_startScale;

	// Token: 0x04004D86 RID: 19846
	[CustomEditField(Sections = "Animate In")]
	public Vector3_MobileOverride m_punchScale;

	// Token: 0x04004D87 RID: 19847
	[CustomEditField(Sections = "Animate In")]
	public Vector3_MobileOverride m_afterPunchScale;

	// Token: 0x04004D88 RID: 19848
	[CustomEditField(Sections = "Progress Bar")]
	public float m_progressBarAnimTime = 2f;

	// Token: 0x04004D89 RID: 19849
	public PlayMakerFSM m_fsm;

	// Token: 0x04004D8A RID: 19850
	public PegUIElement m_debugClickCatcher;

	// Token: 0x04004D8B RID: 19851
	public UberText m_footerText;

	// Token: 0x04004D8C RID: 19852
	private Widget m_widget;

	// Token: 0x04004D8D RID: 19853
	private ProgressBar m_progressBar;

	// Token: 0x04004D8E RID: 19854
	private MedalInfoTranslator m_medalInfo;

	// Token: 0x04004D8F RID: 19855
	private int m_winsNeeded;

	// Token: 0x04004D90 RID: 19856
	private int m_prevWins;

	// Token: 0x04004D91 RID: 19857
	private int m_currWins;

	// Token: 0x04004D92 RID: 19858
	private Action m_closedCallback;

	// Token: 0x04004D93 RID: 19859
	private bool m_isDebugShow;
}
